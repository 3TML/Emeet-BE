using AutoMapper;
using Emeet.Domain.Entities;
using Emeet.Domain.Enums;
using Emeet.Domain.Exceptions;
using Emeet.Domain.Interfaces;
using Emeet.Domain.Specifications;
using Emeet.Service.DTOs.Requests.Expert;
using Emeet.Service.DTOs.Responses.Expert;
using Emeet.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emeet.Service.Services
{
    public class ExpertService : IExpertService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMediaService _mediaService;

        public ExpertService(IUnitOfWork unitOfWork, IMapper mapper, IMediaService mediaService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mediaService = mediaService;
        }

        public async Task<bool> DeleteCertificateById(DeleteCertificateRequest request)
        {
            if (request.CerIds == null || !request.CerIds.Any())
                return false;

            var cers = await _unitOfWork.GetRepository<StaticFile>()
                .GetListAsync(predicate: x => request.CerIds.Contains(x.Id) && x.Type.Equals(MediaType.Image));
            foreach(var cer in cers)
            {
                bool isDelete = await _mediaService.DeleteImageAsync(cer.Link, MediaPath.Certificate + $"/{cer.ExpertId}");
                if (!isDelete)
                {
                    throw new Exception($"Error: Cannot delete image with id {cer.Id}");
                }
            }
            _unitOfWork.GetRepository<StaticFile>().DeleteRangeAsync(cers);
            return await _unitOfWork.CommitAsync()>0;
        }

        public async Task<IPaginate<GetCertificatesResponse>> GetCertificatesByExpertId(Guid ExpertId, int page, int size)
        {
            var certificates = await _unitOfWork.GetRepository<StaticFile>().GetPagingListAsync(
                predicate: x => x.ExpertId == ExpertId && x.Type.Equals(MediaType.Image),
                page: page,
                size: size,
                include: x => x.Include(s => s.Expert)
            );
            return new Paginate<GetCertificatesResponse>()
            {
                Page = certificates.Page,
                Size = certificates.Size,
                TotalPages = certificates.TotalPages,
                Items = _mapper.Map<IList<GetCertificatesResponse>>(certificates.Items)
            };
        }

        public async Task<GetExpertByIdResponse> GetExpertById(Guid expertId)
        {
            var user = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync( 
                                                                  predicate: x=>x.Experts.SingleOrDefault()!.Id == expertId,
                                                                  include: x=>x.Include(s=>s.Experts)
                                                              );
            var result = _mapper.Map<GetExpertByIdResponse>(user);
            result = _mapper.Map<GetExpertByIdResponse>(user.Experts.SingleOrDefault());

            return result;
        }

        public async Task<IPaginate<GetExpertResponse>> GetExpertByNameCategory(string name, string category, int page, int size)
        {
            var user = await _unitOfWork.GetRepository<User>().GetPagingListAsync(
                                                                    predicate: x=>x.FullName.Contains(name) 
                                                                                && x.Experts.Any(s=>s.ExpertCategories.Any(s=>s.Category.Name.Contains(category))),
                                                                    page: page,
                                                                    size: size,
                                                                    include: q => q.Include(u => u.Experts)
                                                                                    .ThenInclude(e => e.ExpertCategories)
                                                                                        .ThenInclude(ec => ec.Category)
                                                                 );
            return new Paginate<GetExpertResponse>()
            {
                Page = user.Page,
                Size = user.Size,
                TotalPages = user.TotalPages,
                Items = _mapper.Map<IList<GetExpertResponse>>(user.Items)
            };
        }

        public async Task<List<GetSuggestionExpert>> GetSuggestionExperts()
        {
            var userExpert = await _unitOfWork.GetRepository<User>()
                   .GetListAsync(
                       predicate: x => x.Status.Equals(UserStatus.Active) && x.Role.Equals(RoleUser.Expert),
                       orderBy: q => q.OrderByDescending(s => s.Experts.SingleOrDefault()!.Rate)
                                     .ThenByDescending(s => s.Experts.SingleOrDefault()!.TotalReview),
                       take: 20,
                       include: x=>x.Include(s=>s.Experts)
                   );

            var expertSuggestionResponses = new List<GetSuggestionExpert>();
            foreach(var user in userExpert)
            {
                var expert = user.Experts.SingleOrDefault();
                var expertSuggestion = new GetSuggestionExpert() 
                {
                    Avatar = user.Avatar,
                    Bio = user.Bio,
                    FullName = user.FullName,
                    Gender = user.Gender,
                    Experience = expert!.Experience,
                    Rate = expert.Rate,
                    TotalPreview = expert.TotalReview,
                };
                var listCategory = await _unitOfWork.GetRepository<Category>()
                    .GetListAsync(predicate: x => x.ExpertCategories.SingleOrDefault()!.ExpertId == expert.Id);
                expertSuggestion.ListCategory = listCategory.Select(s => s.Name).ToList();
                expertSuggestionResponses.Add(expertSuggestion);
            }
            return expertSuggestionResponses;
        }

        public async Task<bool> UploadCertificate(UploadCertificateRequest request)
        {
            var expert = await _unitOfWork.GetRepository<Expert>()
                .SingleOrDefaultAsync(predicate: x => x.Id == request.ExpertId);
            if (expert == null)
            {
                throw new NotFoundException("Expert not found");
            }

            var staticFile = await _unitOfWork.GetRepository<StaticFile>()
                                              .GetListAsync(
                                                    predicate: x => x.ExpertId == request.ExpertId && x.Type == MediaType.Image
                                               );
            int count = (staticFile==null) ? 0 : staticFile.Count();

            for (int i=0; i<request.Certificates.Count(); i++)
            {
                var file = request.Certificates[i];
                var url = await _mediaService.UploadAnImage(file, MediaPath.Certificate+$"/{expert.Id.ToString()}", Guid.NewGuid().ToString("N"));
                var certificate = new StaticFile()
                {
                    Description = $"Certificate of expert {expert.Id}",
                    ExpertId = expert.Id,
                    Id = Guid.NewGuid(),
                    Type = MediaType.Image,
                    Link = url,
                };
                await _unitOfWork.GetRepository<StaticFile>().InsertAsync(certificate);
            }
            return await _unitOfWork.CommitAsync() > 0;
        }
    }
}

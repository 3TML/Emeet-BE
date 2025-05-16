using Emeet.Service.DTOs.Requests.Email;
using Emeet.Service.DTOs.Requests.OTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emeet.Service.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmailOTPAsync(SendOtpEmailRequest sendEmailRequest);
        Task<bool> CheckOtpEmail(CheckOtpRequest checkOtpRequest);
        public Task<bool> CheckExistEmail(CheckExistEmailResrequest checkExistEmailResrequest);
        Task<bool> SendEmailAsyncNotifyOfExpired(string emailIndividual, string fullname, int REMAINING_DAY, DateTime EXPIRATION_DATE, string LINK_PAYMENT);
        Task<bool> SendEmailWithBodyAsync(string emailRequest, string subjectEmail, string fullName, string bodyEmail);
        Task<bool> SendEmailRegisterAccountAsync(string emailRequest, string subjectEmail, string fullName, string userNameAccount, string passwordAccount);
        Task<bool> SendConfirmWithdraw(string emailRequest, string subjectEmail, string fullName, string paymentDate,
                                            string accountHolderName, string accountNumber, string bankName, string Balance, string bankImg);
    }
}

using Emeet.API.Configurations;
using Emeet.API.Constants;
using Emeet.Service.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Config swagger
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Description = "Input JWT token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "ApiKey" },
                Name = "Authorization",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});


// ******************* Add services to the container  **************************

// Dependency Injection
builder.Services.AddUnitOfWork();
builder.Services.AddDIServices();
builder.Services.AddDIRepositories();
builder.Services.AddDIAccessor();

// Add services to the container.
//builder.Services.AddHttpClient<GeminiAIService>(client =>
//{
//    client.BaseAddress = new Uri("https://generativelanguage.googleapis.com/v1beta/");
//});

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(name: CorsConstant.PolicyName,
//        policy =>
//        {
//            policy.WithOrigins()
//                  .AllowAnyHeader()
//                  .AllowAnyMethod()
//                  .AllowCredentials();  // Allow credentials for CORS
//        });
//});
builder.Services.AddCors(options =>
{
    options.AddPolicy(CorsConstant.PolicyName, policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Jwt configuration starts here
var jwtIssuer = builder.Configuration.GetSection("JwtSettings:Issuer").Get<string>();
var jwtAudience = builder.Configuration.GetSection("JwtSettings:Audience").Get<string>();
var jwtKey = builder.Configuration.GetSection("JwtSettings:Key").Get<string>();

// Config JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
 .AddJwtBearer(options =>
 {
     options.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         ValidIssuer = jwtIssuer,
         ValidAudience = jwtAudience,
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
     };
 });

builder.Services.AddAuthorization();



var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors(CorsConstant.PolicyName);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

using System.ComponentModel;
using System.Linq.Expressions;
using Domain.Entities;
using LingoLearn.Contracts.Security;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Admins;

public class LogInAdminCommand
{
    public class Request : IRequest<OperationResponse<Response>>
    {
        [DefaultValue("admin@gmail.com")] public string Email { get; set; }
        [DefaultValue("1234")] public string password { get; set; }
    }
    
    public class Response : TokenDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        
        public static Expression<Func<Admin, Response>> Selector(string accessToken, string refreshToken)
            => e => new()
            {
                Id = e.Id,
                Email = e.Email,
                RefreshToken = refreshToken,
                AccessToken = accessToken,
                FullName = e.FullName
            };
    }
}
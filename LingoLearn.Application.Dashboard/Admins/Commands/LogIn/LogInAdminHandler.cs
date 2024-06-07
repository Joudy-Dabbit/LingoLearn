using Domain.Entities;
using Domain.Enum;
using Domain.Errors;
using Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Admins;

public class LogInAdminHandler : IRequestHandler<LogInAdminCommand.Request, 
    OperationResponse<LogInAdminCommand.Response>>
{
    private readonly UserManager<User> _userManager;
    private readonly IUserRepository _userRepository;

    public LogInAdminHandler(UserManager<User> userManager, IUserRepository userRepository)
    {
        _userManager = userManager;
        _userRepository = userRepository;
    }
    
    public async Task<OperationResponse<LogInAdminCommand.Response>> HandleAsync(LogInAdminCommand.Request query, CancellationToken cancellationToken = new CancellationToken())
    {
        var admin = await _userRepository.Query<Admin>()
            .FirstOrDefaultAsync(d => d.NormalizedEmail == query.Email.ToUpper(), cancellationToken);

        if (admin == null)
            return DomainError.User.NotFound;
         
        if (!await _userManager.CheckPasswordAsync(admin, query.password))
            return DomainError.User.EmailOrPasswordWrong;
         
        if (admin.DateBlocked.HasValue)
            return DomainError.User.Blocked;
         
        var accessToken = _userRepository.GenerateAccessToken(admin, new List<string>(){LingoLearnRoles.Admin.ToString()});
        var refreshToken = await _userRepository.GenerateRefreshToken(admin.Id);
        
        if (!refreshToken.IsSucceded)
            return OperationResponse.WithBadRequest(refreshToken.ErrorMessage).ToResponse<LogInAdminCommand.Response>();
          
        return await _userRepository.GetAsync(admin.Id, LogInAdminCommand.Response.Selector(accessToken, refreshToken.Token));
    }
}
using Domain.Entities;
using Domain.Enum;
using Domain.Errors;
using Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Mobile.Customers;

public class LogInStudentHandler : IRequestHandler<LogInStudentCommand.Request, 
    OperationResponse<LogInStudentCommand.Response>>
{
    private readonly UserManager<User> _userManager;
    private readonly IUserRepository _userRepository;

    public LogInStudentHandler(UserManager<User> userManager, IUserRepository userRepository)
    {
        _userManager = userManager;
        _userRepository = userRepository;
    }
    
    public async Task<OperationResponse<LogInStudentCommand.Response>> HandleAsync(LogInStudentCommand.Request query, CancellationToken cancellationToken = new CancellationToken())
    {
        var customer = await _userRepository.Query<Student>()
            .FirstOrDefaultAsync(d => d.NormalizedEmail == query.Email.ToUpper(), cancellationToken);

        if (customer == null)
            return DomainError.User.NotFound;
         
        if (!await _userManager.CheckPasswordAsync(customer, query.password))
            return DomainError.User.EmailOrPasswordWrong;
         
        if (customer.DateBlocked.HasValue)
            return DomainError.User.Blocked;
         
        var accessToken = _userRepository.GenerateAccessToken(customer, 
            new List<string>(){LingoLearnRoles.Customer.ToString()});
        var refreshToken = await _userRepository.GenerateRefreshToken(customer.Id);
        
        if (!refreshToken.IsSucceded)
            return OperationResponse.WithBadRequest(refreshToken.ErrorMessage).ToResponse<LogInStudentCommand.Response>();
          
        return await _userRepository.GetAsync(customer.Id,
            LogInStudentCommand.Response.Selector(accessToken, refreshToken.Token));
    }
}
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;
using LingoLearn.Application.Dashboard.Core.Abstractions.Http;

namespace LingoLearn.Application.Mobile.Customers;

public class ModifyStudentHandler: IRequestHandler<ModifyStudentCommand.Request,
    OperationResponse<GetStudentProfileQuery.Response>>
{
    private readonly IUserRepository _userRepository;
    private readonly UserManager<User> _userManager;
    private readonly IHttpService _httpService;

    public ModifyStudentHandler(IUserRepository userRepository, 
        UserManager<User> userManager, IHttpService httpService)
    {
        _userRepository = userRepository;
        _userManager = userManager;
        _httpService = httpService;
    }

    public async Task<OperationResponse<GetStudentProfileQuery.Response>> HandleAsync(ModifyStudentCommand.Request request, CancellationToken cancellationToken = new CancellationToken())
    {
        var customer = await _userRepository.TrackingQuery<Student>()
            .FirstAsync(c => c.Id == _httpService.CurrentUserId, cancellationToken);
        
        if(await _userRepository.IsEmailExist<Student>(request.Email, _httpService.CurrentUserId))
            return DomainError.User.EmailAlreadyUsed(request.Email);
        
        customer.Modify(request.FullName, request.BirthDate,
            request.Email, request.PhoneNumber, request.Gender);

        await _userManager.UpdateAsync(customer);
        await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return await _userRepository.GetAsync(customer.Id, 
            GetStudentProfileQuery.Response.Selector());
    }
}
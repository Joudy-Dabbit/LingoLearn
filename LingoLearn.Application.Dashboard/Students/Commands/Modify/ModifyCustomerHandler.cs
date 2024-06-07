using Application.Dashboard.Core.Abstractions;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;
using LingoLearn.Application.Dashboard.Core.Abstractions.Http;

namespace LingoLearn.Application.Dashboard.Students;

public class ModifyCustomerHandler : IRequestHandler<ModifyCustomerCommand.Request,
    OperationResponse<GetByIdStudentQuery.Response>>
{
    private readonly IUserRepository _userRepository;
    private readonly UserManager<User> _userManager;
    private readonly IFileService _fileService;

    public ModifyCustomerHandler(IUserRepository userRepository,
        UserManager<User> userManager, IFileService fileService)
    {
        _userRepository = userRepository;
        _userManager = userManager;
        _fileService = fileService;
    }

    public async Task<OperationResponse<GetByIdStudentQuery.Response>> HandleAsync(ModifyCustomerCommand.Request request, CancellationToken cancellationToken = new CancellationToken())
    {
        var student = await _userRepository.TrackingQuery<Student>()
            .FirstAsync(c => c.Id == request.Id, cancellationToken);
        
        if(await _userRepository.IsEmailExist<Student>(request.Email, request.Id))
            return DomainError.User.EmailAlreadyUsed(request.Email);
        
        var imageUrl = await _fileService.Modify(student.ImagUrl, request.ImageFile);
        student.Modify(request.FullName, request.BirthDate, request.Email,
            request.PhoneNumber, request.Gender, imageUrl ?? "");
        
        if (request.Password != null)
        {
            await _userRepository.TryModifyPassword(student, request.Password);
            await _userManager.UpdateAsync(student);
        }
        
        await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return await _userRepository.GetAsync(student.Id, GetByIdStudentQuery.Response.Selector());
    }
}
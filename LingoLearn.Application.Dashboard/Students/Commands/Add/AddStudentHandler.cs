using Application.Dashboard.Core.Abstractions;
using Domain.Entities;
using Domain.Enum;
using Domain.Errors;
using Domain.Repositories;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Students;

public class AddStudentHandler : IRequestHandler<AddStudentCommand.Request,
    OperationResponse<GetAllStudentsQuery.Response>>
{
    private readonly IUserRepository _userRepository;
    private readonly IFileService _fileService;

    public AddStudentHandler(IUserRepository userRepository, IFileService fileService)
    {
        _userRepository = userRepository;
        _fileService = fileService;
    } 
    public async Task<OperationResponse<GetAllStudentsQuery.Response>> HandleAsync(AddStudentCommand.Request request, CancellationToken cancellationToken = new CancellationToken())
    {
        if(await _userRepository.IsEmailExist<Student>(request.Email))
            return DomainError.User.EmailAlreadyUsed(request.Email);
        
        var imageUrl = await _fileService.Upload(request.ImageFile);
        var student = new Student(request.FullName,
            request.PhoneNumber, request.Email, request.BirthDate,
            request.Gender, imageUrl ?? "");
        
        var identityResult = await _userRepository.AddWithRole(student, LingoLearnRoles.Student, request.Password);
        if(!identityResult.Succeeded)
            return identityResult.ToOperationResponse<GetAllStudentsQuery.Response>();

        return await _userRepository.GetAsync(student.Id, GetAllStudentsQuery.Response.Selector());    
    }
}
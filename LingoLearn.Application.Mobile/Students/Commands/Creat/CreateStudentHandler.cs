using System.Net;
using Domain.Entities;
using Domain.Enum;
using Domain.Repositories;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;
using Application.Dashboard.Core.Abstractions;

namespace LingoLearn.Application.Mobile.Customers;

public class CreateStudentHandler : IRequestHandler<CreateStudentCommand.Request, 
    OperationResponse<CreateStudentCommand.Response>>
{
    private readonly IUserRepository _userRepository;
    private readonly IFileService _fileService;

    public CreateStudentHandler(IUserRepository userRepository, IFileService fileService)
    {
        _userRepository = userRepository;
        _fileService = fileService;
    }


    public async Task<OperationResponse<CreateStudentCommand.Response>> HandleAsync(CreateStudentCommand.Request request, CancellationToken cancellationToken = new CancellationToken())
    {
        var imageUrl = await _fileService.Upload(request.ImageFile);
        var student = new Student(request.FullName,
            request.PhoneNumber, request.Email, request.BirthDate,
            request.Gender, imageUrl ?? "", request.DeviceToken);
        
        var identityResult = await _userRepository.AddWithRole(student, LingoLearnRoles.Student, request.Password);
        
        if(!identityResult.Succeeded)
            return identityResult.ToOperationResponse<CreateStudentCommand.Response>();
        
        var accessToken = _userRepository.GenerateAccessToken(student, 
            new List<string>(){LingoLearnRoles.Student.ToString()});
        var refreshToken = await _userRepository.GenerateRefreshToken(student.Id);
        
        if (!refreshToken.IsSucceded)
            return OperationResponse.WithBadRequest(refreshToken.ErrorMessage).ToResponse<CreateStudentCommand.Response>();
        
        return await _userRepository.GetAsync(student.Id, 
            CreateStudentCommand.Response.Selector(accessToken, refreshToken.Token));
    }
}
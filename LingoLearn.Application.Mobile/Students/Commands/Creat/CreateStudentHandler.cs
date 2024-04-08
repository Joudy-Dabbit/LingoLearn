// using System.Net;
// using Domain.Entities;
// using Domain.Enum;
// using Domain.Repositories;
// using Neptunee.BaseCleanArchitecture.OResponse;
// using Neptunee.BaseCleanArchitecture.Requests;
// using Application.Dashboard.Core.Abstractions;
//
// namespace LingoLearn.Application.Mobile.Customers;
//
// public class CreateStudentHandler : IRequestHandler<CreateStudentCommand.Request, 
//     OperationResponse<CreateStudentCommand.Response>>
// {
//     private readonly IUserRepository _userRepository;
//     private readonly IFileService _fileService;
//
//     public CreateStudentHandler(IUserRepository userRepository)
//     {
//         _userRepository = userRepository;
//     }
//
//
//     public async Task<OperationResponse<CreateStudentCommand.Response>> HandleAsync(CreateStudentCommand.Request request, CancellationToken cancellationToken = new CancellationToken())
//     {
//         var customer = new Student(request.FullName,
//             request.PhoneNumber, request.Email,
//             request.BirthDate, request.DeviceToken, request.Gender);
//         
//         var identityResult = await _userRepository.AddWithRole(customer, LingoLearnRoles.Customer, request.Password);
//         
//         if(!identityResult.Succeeded)
//             return identityResult.ToOperationResponse<CreateStudentCommand.Response>();
//         
//         var accessToken = _userRepository.GenerateAccessToken(customer, 
//             new List<string>(){LingoLearnRoles.Customer.ToString()});
//         var refreshToken = await _userRepository.GenerateRefreshToken(customer.Id);
//         
//         if (!refreshToken.IsSucceded)
//             return OperationResponse.WithBadRequest(refreshToken.ErrorMessage).ToResponse<CreateStudentCommand.Response>();
//         
//         return await _userRepository.GetAsync(customer.Id, 
//             CreateStudentCommand.Response.Selector(accessToken, refreshToken.Token));
//     }
// }
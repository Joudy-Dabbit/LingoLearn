// using Application.Dashboard.Core.Abstractions;
// using Domain.Entities;
// using Domain.Enum;
// using Domain.Errors;
// using Domain.Repositories;
// using Neptunee.BaseCleanArchitecture.OResponse;
// using Neptunee.BaseCleanArchitecture.Requests;
//
// namespace LingoLearn.Application.Dashboard.Students;
//
// public class AddStudentHandler : IRequestHandler<AddStudentCommand.Request,
//     OperationResponse<GetAllStudentsQuery.Response>>
// {
//     private readonly IUserRepository _userRepository;
//     private readonly IFileService _fileService;
//
//     public AddStudentHandler(IUserRepository userRepository, IFileService fileService)
//     {
//         _userRepository = userRepository;
//         _fileService = fileService;
//     } 
//     public async Task<OperationResponse<GetAllStudentsQuery.Response>> HandleAsync(AddStudentCommand.Request request, CancellationToken cancellationToken = new CancellationToken())
//     {
//         if(await _userRepository.IsEmailExist<User>(request.Email))
//             return DomainError.User.EmailAlreadyUsed(request.Email);
//
//         var customer = new Student(request.FullName,
//             request.PhoneNumber, request.Email,   
//             request.BirthDate, request.Gender);
//         
//         // customer.AddAddress(request.Address.Name, request.Address.AreaId, 
//         //     request.Address.HouseNumber, request.Address.Street,
//         //     request.Address.Additional, request.Address.Floor);
//         
//         var identityResult = await _userRepository.AddWithRole(customer, LingoLearnRoles.Customer, request.Password);
//         
//         if(!identityResult.Succeeded)
//             return identityResult.ToOperationResponse<GetAllStudentsQuery.Response>();
//
//         return await _userRepository.GetAsync(customer.Id, GetAllStudentsQuery.Response.Selector());    
//     }
// }
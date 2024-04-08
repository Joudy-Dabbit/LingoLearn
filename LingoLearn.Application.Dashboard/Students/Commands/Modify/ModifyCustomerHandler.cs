// using Application.Dashboard.Core.Abstractions;
// using Domain.Entities;
// using Domain.Errors;
// using Domain.Repositories;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.EntityFrameworkCore;
// using Neptunee.BaseCleanArchitecture.OResponse;
// using Neptunee.BaseCleanArchitecture.Requests;
// using LingoLearn.Application.Dashboard.Core.Abstractions.Http;
//
// namespace LingoLearn.Application.Dashboard.Students;
//
// public class ModifyCustomerHandler : IRequestHandler<ModifyCustomerCommand.Request,
//     OperationResponse<GetByIdStudentQuery.Response>>
// {
//     private readonly IUserRepository _userRepository;
//     private readonly UserManager<User> _userManager;
//
//     public ModifyCustomerHandler(IUserRepository userRepository, UserManager<User> userManager)
//     {
//         _userRepository = userRepository;
//         _userManager = userManager;
//     }
//
//     public async Task<OperationResponse<GetByIdStudentQuery.Response>> HandleAsync(ModifyCustomerCommand.Request request, CancellationToken cancellationToken = new CancellationToken())
//     {
//         var customer = await _userRepository.TrackingQuery<Student>()
//             .FirstAsync(c => c.Id == request.Id, cancellationToken);
//         
//         if(await _userRepository.IsEmailExist<Student>(request.Email, request.Id))
//             return DomainError.User.EmailAlreadyUsed(request.Email);
//         
//
//         customer.Modify(request.FullName, request.BirthDate,
//             request.Email, request.CityId,
//             request.PhoneNumber, request.Gender);
//         
//         if (request.Password != null)
//         {
//             await _userRepository.TryModifyPassword(customer, request.Password);
//             await _userManager.UpdateAsync(customer);
//         }
//         
//         await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
//         return await _userRepository.GetAsync(customer.Id, 
//             GetByIdStudentQuery.Response.Selector());
//     }
// }
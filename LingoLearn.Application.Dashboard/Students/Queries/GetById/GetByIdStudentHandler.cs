
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Students;

public class GetByIdStudentHandler
    : IRequestHandler<GetByIdStudentQuery.Request, OperationResponse<GetByIdStudentQuery.Response>>
{   
    private readonly IUserRepository _userRepository;

    public GetByIdStudentHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<OperationResponse<GetByIdStudentQuery.Response>> HandleAsync(GetByIdStudentQuery.Request request,
        CancellationToken cancellationToken = new())
    {
        var student = await _userRepository.Query<Student>()
            .Where(s => s.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
      
      if(student is not { UtcDateDeleted: null })
          return OperationResponse.WithBadRequest("student Not found").ToResponse<GetByIdStudentQuery.Response>();
         
      return await _userRepository.GetAsync(request.Id, GetByIdStudentQuery.Response.Selector());
    } 
}
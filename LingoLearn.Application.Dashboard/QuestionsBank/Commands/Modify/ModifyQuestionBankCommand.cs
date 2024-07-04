using Domain.Enum;
using Microsoft.AspNetCore.Http;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Levels;

public class ModifyQuestionBankCommand
{
    public class Request : IRequest<OperationResponse<GetByIdQuestionBankQuery.Response>>
    {  
        public Guid Id { get; set; }
        public string Text { get; set; }
        public int Order { get; set; }
        public List<AddQuestionBankCommand.Request.AddAnswerReq> Answers { get;  set; }
    }
}
using Domain.Enum;
using Microsoft.AspNetCore.Http;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.ChallengeQuestions;

public class ModifyChallengeQuestionsCommand
{
    public class Request : IRequest<OperationResponse<GetByIdChallengeQuestionsQuery.Response>>
    {  
        public Guid Id { get; set; }
        public string Text { get; set; }
        public int Order { get; set; }
        public bool IsMultiChoices { get; set; }
        public List<AddChallengeQuestionsCommand.Request.AddAnswerReq> Answers { get;  set; }
    }
}
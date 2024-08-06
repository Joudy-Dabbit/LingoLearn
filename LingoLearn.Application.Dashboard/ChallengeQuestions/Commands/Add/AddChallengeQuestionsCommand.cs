using Domain.Enum;
using Microsoft.AspNetCore.Http;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.ChallengeQuestions;

public class AddChallengeQuestionsCommand
{
    public class Request : IRequest<OperationResponse<GetAllChallengeQuestionsQuery.Response>>
    {  
        public string Text { get; set; }
        public int Order { get; set; }
        public bool IsMultiChoices { get; set; }

        public Guid ChallengeId { get;  set; }
        public List<AddAnswerReq> Answers { get;  set; }
        
        public class AddAnswerReq
        {
            public string Text { get; set; }
            public bool IsCorrect { get; set; }
            public int Order { get; set; }
        }
    }
}
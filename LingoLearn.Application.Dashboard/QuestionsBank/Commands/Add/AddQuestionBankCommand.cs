using Domain.Enum;
using Microsoft.AspNetCore.Http;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Levels;

public class AddQuestionBankCommand
{
    public class Request : IRequest<OperationResponse<GetAllQuestionsBankQuery.Response>>
    {  
        public string Text { get; set; }
        public int Order { get; set; }

        public Guid LevelId { get;  set; }
        public List<AddAnswerReq> Answers { get;  set; }
        
        public class AddAnswerReq
        {
            public string Text { get; set; }
            public bool IsCorrect { get; set; }
            public int Order { get; set; }
        }
    }
}
using System.Linq.Expressions;
using Domain.Entities;
using Domain.Enum;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.ChallengeQuestions;

public class GetByIdChallengeQuestionsQuery
{
    public class Request : IRequest<OperationResponse<Response>>
    {
        public Guid Id { get; set; }
    }

    public class Response
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public int Order { get; set; }
        public bool IsMultiChoices { get; set; }

        public Guid ChallengeId { get;  set; }
        public List<AnswerRes> Answers { get;  set; }
        
        public class AnswerRes
        {
            public Guid Id { get; set; }
            public string Text { get; set; }
            public bool IsCorrect { get; set; }
            public int Order { get; set; }
        }

        public static Expression<Func<ChallengeQuestion, Response>> Selector => l
            => new()
            {
                Id = l.Id,
                Text = l.Text,
                Order = l.Order,
                ChallengeId = l.ChallengeId,
                IsMultiChoices = l.IsMultiChoices,
                Answers = l.Answers.Where(a => !a.UtcDateDeleted.HasValue).Select(v => new AnswerRes()
                {
                    Id = v.Id,
                    Order = v.Order,
                    Text = v.Text,
                    IsCorrect = v.IsCorrect
                }).OrderBy(a => a.Order).ToList()
            };
    }
}
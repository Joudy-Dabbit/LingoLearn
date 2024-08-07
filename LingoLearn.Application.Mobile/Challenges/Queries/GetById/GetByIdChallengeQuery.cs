using System.Linq.Expressions;
using Domain.Entities;
using Domain.Entities.General;
using Domain.Enum;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Mobile.Challenges;

public class GetByIdChallengeQuery
{
    public class Request : IRequest<OperationResponse<Response>>
    {
        public Guid Id { get; set; }
    }

    public class Response
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Points { get; set; }
        public Guid LanguageId { get; set; }
        public string ImageUrl { get; set; }
        public string CoverImageUrl { get; set; }
        public List<QuestionsRes> Questions { get; set; }
        public List<ParticipantsRes> Participants { get; set; }

        public class ParticipantsRes
        {
            public Guid Id { get; set; }
            public string FullName { get; set; }
            public string ImageUrl { get; set; }
            public int Score { get; set; }
        }
        

        public class QuestionsRes
        {
            public Guid Id { get; set; }
            public string Text { get; set; }
            public int Order { get; set; }
            public bool IsMultiChoices { get; set; }

            public Guid ChallengeId { get; set; }
            public List<AnswerRes> Answers { get; set; }
        }

        public class AnswerRes
        {
            public Guid Id { get; set; }
            public string Text { get; set; }
            public bool IsCorrect { get; set; }
            public int Order { get; set; }
        }
        
        public static Expression<Func<Challenge, Response>> Selector => l
            => new Response
            {
                Id = l.Id,
                Name = l.Name,
                Description = l.Description,
                StartDate = l.StartDate,
                EndDate = l.EndDate,
                Points = l.Points,
                LanguageId = l.LanguageId,
                ImageUrl = l.ImageUrl,
                CoverImageUrl = l.CoverImageUrl,
                Participants = l.Participants.Select(p => new ParticipantsRes()
                {
                    Id = p.Student.Id,
                    FullName = p.Student.FullName,
                    ImageUrl = p.Student.ImagUrl
                }).ToList(),
                Questions = l.Questions.OrderBy(a => a.Order).Select(q => new QuestionsRes()
                {
                    Id = q.Id,
                    Text = q.Text,
                    Order = q.Order,
                    ChallengeId = q.ChallengeId,
                    IsMultiChoices = q.IsMultiChoices,
                    Answers = q.Answers.Where(a => !a.UtcDateDeleted.HasValue).Select(v => new AnswerRes()
                    {
                        Id = v.Id,
                        Order = v.Order,
                        Text = v.Text,
                        IsCorrect = v.IsCorrect
                    }).OrderBy(a => a.Order).ToList()
                }).ToList()
            };
    }
}
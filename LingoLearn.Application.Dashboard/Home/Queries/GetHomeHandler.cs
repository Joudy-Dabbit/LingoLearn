using Domain.Entities;
using Domain.Entities.General;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Repository;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard;

public class GetHomeHandler : IRequestHandler<GetHomeQuery.Request, OperationResponse<GetHomeQuery.Response>>
{
    private readonly ILingoLearnRepository _repository;

    public GetHomeHandler(ILingoLearnRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResponse<GetHomeQuery.Response>> HandleAsync(GetHomeQuery.Request request,
        CancellationToken cancellationToken = default)
        => new GetHomeQuery.Response()
        {
            StudentCount = await _repository.Query<Student>().CountAsync(cancellationToken: cancellationToken),
            LanguageCount = await _repository.Query<Language>().CountAsync(cancellationToken: cancellationToken),
            LessonCount = await _repository.Query<Lesson>().CountAsync(cancellationToken: cancellationToken),
            AdvertisementCount = await _repository.Query<Advertisement>().CountAsync(cancellationToken: cancellationToken),
            LevelCount = await _repository.Query<Level>().CountAsync(cancellationToken: cancellationToken),
            AnswerCount = await _repository.Query<Answer>().CountAsync(cancellationToken: cancellationToken),
            QuestionCount = await _repository.Query<Question>().CountAsync(cancellationToken: cancellationToken),
            ChallengeCount = await _repository.Query<Challenge>().CountAsync(cancellationToken: cancellationToken),
            StudentCountMonthly = Enumerable.Range(1, 12)
                .GroupJoin(_repository.Query<Student>()
                        .Where(s => s.UtcDateCreated.Year == request.Year)
                         .ToList(),
                    m => m, q => q.UtcDateCreated.Month,
                    (m, q) => q.Count()).ToList(),
            AdvertisementCountMonthly = Enumerable.Range(1, 12)
                .GroupJoin(_repository.Query<Advertisement>()
                        .Where(s => s.UtcDateCreated.Year == request.Year)
                        .ToList(),
                    m => m, q => q.UtcDateCreated.Month,
                    (m, q) => q.Count()).ToList(),
            LanguageCountMonthly = Enumerable.Range(1, 12)
                .GroupJoin(_repository.Query<Language>()
                        .Where(s => s.UtcDateCreated.Year == request.Year)
                        .ToList(),
                    m => m, q => q.UtcDateCreated.Month,
                    (m, q) => q.Count()).ToList(),        
            LessonCountMonthly = Enumerable.Range(1, 12)
                .GroupJoin(_repository.Query<Lesson>()
                        .Where(s => s.UtcDateCreated.Year == request.Year)
                        .ToList(),
                    m => m, q => q.UtcDateCreated.Month,
                    (m, q) => q.Count()).ToList(),
            BestLanguages = await _repository.Query<Language>()
                .OrderBy(d => d.Participants.Count())
                .Select(d => new GetHomeQuery.Response.HomeInfoRes()
                {
                    Id = d.Id,
                    Name = d.Name.ToString(),
                    ImageUrl = d.ImageUrl
                })
                .Take(5).ToListAsync(cancellationToken),
            BestStudents = await _repository.Query<Student>()
                .OrderBy(d => d.Score)
                .Select(d => new GetHomeQuery.Response.HomeInfoRes()
                {
                    Id = d.Id,
                    Name = d.UserName,
                    ImageUrl = d.ImagUrl
                })
                .Take(5).ToListAsync(cancellationToken),
        };
}


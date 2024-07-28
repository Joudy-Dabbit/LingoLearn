using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enum;
using Domain.Repositories;
using LingoLearn.Application.Dashboard.Core.Abstractions.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Mobile.Home;

public class GetHomeLineHandler: IRequestHandler<GetHomeLineQuery.Request,
    OperationResponse<GetHomeLineQuery.Response>>
{
    private readonly ILingoLearnRepository _repository;
    private readonly IHttpService _httpService;

    public GetHomeLineHandler(ILingoLearnRepository repository, IHttpService httpService)
    {
        _repository = repository;
        _httpService = httpService;
    }

    public async Task<OperationResponse<GetHomeLineQuery.Response>> HandleAsync(GetHomeLineQuery.Request request,
        CancellationToken cancellationToken = new())
    {
        var lang = await _repository.Query<Language>()
            .Where(l => l.Name == _httpService.CurrentProgrammingLang)
            .FirstOrDefaultAsync(cancellationToken);
        
        if(lang is null)
            return OperationResponse.WithBadRequest("Language in header not found!").ToResponse<GetHomeLineQuery.Response>();

        var studentLanguage = await _repository.Query<StudentLanguage>()
            .Include(sl => sl.Language)
            .ThenInclude(l => l.Levels)
            .ThenInclude(l => l.Lessons)
            .Where(l => l.StudentId == _httpService.CurrentUserId
                     && l.LanguageId == lang.Id)
            .FirstOrDefaultAsync(cancellationToken);
        
        if(studentLanguage is null)
            return OperationResponse.WithBadRequest("student not selected any language!").ToResponse<GetHomeLineQuery.Response>();

        var res = await _repository.GetAsync(studentLanguage.Id ,GetHomeLineQuery.Response.Selector);
        
        res.Levels.ForEach(l =>
        {
            l.Lessons.ForEach(le =>
            {
                le.IsDone = _repository.Query<StudentLesson>()
                    .Any(sl => sl.StudentId == _httpService.CurrentUserId && sl.LessonId == le.Id);
            });
        });
        
        return res;
    }
}
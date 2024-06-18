using Domain.Entities;
using Domain.Enum;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Languages;

public class GetAllAvailableLanguagesHandler : IRequestHandler<GetAllAvailableLanguagesQuery.Request,
    OperationResponse<List<GetAllAvailableLanguagesQuery.Response>>>
{
    private readonly ILingoLearnRepository _repository;

    public GetAllAvailableLanguagesHandler(ILingoLearnRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResponse<List<GetAllAvailableLanguagesQuery.Response>>> HandleAsync(
        GetAllAvailableLanguagesQuery.Request request,
        CancellationToken cancellationToken = new())
    {
        var allLanguages = Enum.GetValues(typeof(ProgrammingLang)).Cast<ProgrammingLang>();

        var existingLanguages = await _repository.Query<Language>()
            .Where(l => !l.UtcDateDeleted.HasValue)
            .Select(l => l.Name)
            .ToListAsync(cancellationToken);
        
        var res = allLanguages
            .Where(lang => !existingLanguages.Contains(lang))
            .Select(lang => new GetAllAvailableLanguagesQuery.Response
            {
                Key = (int)lang,
                Value = lang.ToString()
            })
            .ToList();
        
        return res;
    }
}

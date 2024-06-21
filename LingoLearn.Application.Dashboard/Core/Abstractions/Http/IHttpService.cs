using Domain.Enum;

namespace LingoLearn.Application.Dashboard.Core.Abstractions.Http;

public interface IHttpService
{
    Guid? CurrentUserId { get; }
    ProgrammingLang? CurrentProgrammingLang { get; }
}
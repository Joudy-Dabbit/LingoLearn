using Neptunee.BaseCleanArchitecture.Repository;

namespace Domain.Repositories;

public interface IDeleteRepository : IRepository<Guid>
{
    Task DeleteLanguage(List<Guid> ids);
    Task DeleteLevels(List<Guid> ids);
    Task DeleteQuestions(List<Guid> ids);
}
using Domain.Repositories;
using LingoLearn.Persistence.Context;
using Neptunee.BaseCleanArchitecture.Repository;

namespace LingoLearn.Persistence.Repositories;

public class LingoLearnRepository : Repository<Guid, LingoLearnDbContext>, ILingoLearnRepository
{
    public LingoLearnRepository(LingoLearnDbContext context) : base(context)
    {
    }
}
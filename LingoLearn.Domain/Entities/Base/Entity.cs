using Neptunee.BaseCleanArchitecture.BaseEntity;

namespace Domain.Entities;

public class Entity : BaseEntity<Guid>
{
    public Entity()
    {
        Id = Guid.NewGuid();
    }
}
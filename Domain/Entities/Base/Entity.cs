using System.ComponentModel.DataAnnotations.Schema;
using Neptunee.Entities;

namespace Domain.Entities.Base;

public abstract class Entity: INeptuneeAuditableEntity<Guid>
{
    public Entity()
    {
        Id = Guid.NewGuid();
    }
    
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get;  set; }
    public DateTimeOffset? UtcDateDeleted { get; set; }
    public DateTimeOffset UtcDateCreated { get; set; }
    public DateTimeOffset? UtcDateUpdated { get; set; }
}
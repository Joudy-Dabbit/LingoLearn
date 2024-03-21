using Microsoft.AspNetCore.Identity;
using Neptunee.Entities;

namespace Domain.Entities.Base;

public class BaseIdentity : IdentityUser<Guid>, INeptuneeAuditableEntity<Guid>
{
    public DateTimeOffset? UtcDateDeleted { get; set; }
    public DateTimeOffset UtcDateCreated { get; set; }
    public DateTimeOffset? UtcDateUpdated { get; set; }
}
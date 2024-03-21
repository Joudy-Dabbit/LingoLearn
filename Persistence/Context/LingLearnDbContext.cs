using System.Linq.Expressions;
using System.Reflection;
using Domain.Entities.Achievements;
using Domain.Entities.Base;
using Domain.Entities.Favorite;
using Domain.Entities.Languages;
using Domain.Entities.Notifications;
using Domain.Entities.Security;
using Domain.Entities.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query;

namespace Persistence.Context;

public class LingLearnDbContext:  IdentityDbContext<User, IdentityRole<Guid>, Guid>, ILingLearnDbContext
{
    public LingLearnDbContext(DbContextOptions<LingLearnDbContext> options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        PrimaryKeyValueGenerated(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        var entities = builder.Model
            .GetEntityTypes()
            .Where(e => e.ClrType.BaseType?.Name == typeof(AggregateRoot).Name)
            .Select(e => e.ClrType);

        foreach (var entity in entities)
        {
            Expression<Func<AggregateRoot, bool>> expression = b => !b.UtcDateDeleted.HasValue;
            var newParam = Expression.Parameter(entity);
            var newbody = ReplacingExpressionVisitor.Replace(expression.Parameters.Single(), newParam, expression.Body);
            builder.Entity(entity).HasQueryFilter(Expression.Lambda(newbody, newParam));
        }
        base.OnModelCreating(builder);
    }
    
    protected void PrimaryKeyValueGenerated(ModelBuilder builder, ValueGenerated valueGenerated = ValueGenerated.Never)
    {
        foreach (IMutableEntityType entityType in builder.Model.GetEntityTypes())
        {
            foreach (IMutableProperty mutableProperty in entityType.GetProperties().Where<IMutableProperty>((Func<IMutableProperty, bool>) (p => p.IsPrimaryKey())))
                mutableProperty.ValueGenerated = valueGenerated;
        }
    } 
    
    
     #region -Security-
     public DbSet<User> Users => Set<User>();
     #endregion
     
     #region -Settings-
     public DbSet<Advertisement> Advertisements => Set<Advertisement>();
     #endregion
        
     #region -Languages-
     public DbSet<Language> Languages => Set<Language>();
     public DbSet<Challenge> Challenges => Set<Challenge>();
     public DbSet<Lesson> Lessons => Set<Lesson>();
     public DbSet<Level> Levels => Set<Level>();
     #endregion
     
     #region -Achievements-
     public DbSet<Certificate> Certificates => Set<Certificate>();
     public DbSet<ChallengeParticipant> ChallengeParticipants => Set<ChallengeParticipant>();
     public DbSet<Comment> Comments => Set<Comment>();
     public DbSet<Reply> Replies => Set<Reply>();
     public DbSet<StudentLanguage> StudentLanguages => Set<StudentLanguage>();
     #endregion

     #region -Notifications-
     public DbSet<Notification> Notifications => Set<Notification>();
     public DbSet<UserNotification> UserNotifications => Set<UserNotification>();
     #endregion

     #region -Favorites-
     public DbSet<FavoriteLanguage> FavoriteLanguages => Set<FavoriteLanguage>();
     public DbSet<FavoriteLesson> FavoriteLessons => Set<FavoriteLesson>();
     #endregion
}
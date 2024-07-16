using System.Linq.Expressions;
using System.Reflection;
using Domain.Entities;
using Domain.Entities.General;
using EasyRefreshToken.Models;
using LingoLearn.Application.Dashboard.Core.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query;
using Neptunee.BaseCleanArchitecture.BaseDbContexts;
using Neptunee.BaseCleanArchitecture.Clock;
using Neptunee.BaseCleanArchitecture.Dispatchers.DomainEventDispatcher;

namespace LingoLearn.Persistence.Context;

public class LingoLearnDbContext : BaseIdentityDbContext<Guid,User>, ILingoLearnDbContext
{
    public LingoLearnDbContext(DbContextOptions<LingoLearnDbContext> options, IClock clock,
        IDomainEventDispatcher domainEventDispatcher) : 
        base(options, clock, domainEventDispatcher) { }
    
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
    
    // public static void ConfigureDateDeletedQueryFilter<TKey>(this ModelBuilder builder) where TKey : struct, IEquatable<TKey>
    // {
    //     foreach (var entityType in builder.Model.GetEntityTypes().Where(x =>
    //                  !x.ClrType.IsDefined(typeof(IgnoreAddQueryFilterAttribute))
    //                  && (x.ClrType.BaseType.GetHierarchy().Any(a => a == typeof(BaseEntity<TKey>)) ||
    //                      x.ClrType.GetInterfaces().Any(i => i == typeof(IElIdentity)))))
    //     {
    //         AddQueryFilter<TKey>(builder, f => !f.DateDeleted.HasValue, entityType);
    //     }
    // }
    
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
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Admin> Admins => Set<Admin>();
    public DbSet<RefreshToken<User, Guid>> RefreshTokens { get; set; }
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
    public DbSet<StudentLesson> StudentLessons => Set<StudentLesson>();
    #endregion

    #region -Notifications-
    public DbSet<Notification> Notifications => Set<Notification>();
    public DbSet<UserNotification> UserNotifications => Set<UserNotification>();
    #endregion

    #region -Favorites-
    public DbSet<FavoriteLanguage> FavoriteLanguages => Set<FavoriteLanguage>();
    public DbSet<FavoriteLesson> FavoriteLessons => Set<FavoriteLesson>();
    #endregion

    #region - General -
    public DbSet<ContactUs> ContactsUs => Set<ContactUs>();
    public DbSet<Advertisement> Advertisements => Set<Advertisement>();
    #endregion
}

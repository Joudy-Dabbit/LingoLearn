using Domain.Entities.Achievements;
using Domain.Entities.Favorite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class AchievementsConfiguration : IEntityTypeConfiguration<Reply>
{
    public void Configure(EntityTypeBuilder<Reply> builder)
    {
        builder.HasOne(x => x.Comment)
            .WithMany(x => x.Replies)
            .HasForeignKey(x => x.CommentId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
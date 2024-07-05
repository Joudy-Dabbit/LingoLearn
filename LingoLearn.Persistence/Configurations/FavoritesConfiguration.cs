using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class FavoritesConfiguration : IEntityTypeConfiguration<FavoriteLesson>
{
    public void Configure(EntityTypeBuilder<FavoriteLesson> builder)
    {
        builder.HasOne(x => x.Student)
               .WithMany()
               .HasForeignKey(x => x.StudentId)
               .OnDelete(DeleteBehavior.Cascade); 
        
        builder.HasOne(x => x.Lesson)
               .WithMany()
               .HasForeignKey(x => x.LessonId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
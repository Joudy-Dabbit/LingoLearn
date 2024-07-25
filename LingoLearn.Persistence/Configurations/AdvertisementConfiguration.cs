using Domain.Entities;
using Domain.Entities.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace Persistence.Configurations;

public class AdvertisementConfiguration: IEntityTypeConfiguration<Advertisement>
{
    public void Configure(EntityTypeBuilder<Advertisement> builder)
    {
        builder.Property(e => e.ImagesUrl)
            .HasConversion(typeof(ListToStringConverter))
            .HasColumnName("ImagesUrl");
    }
}
public class ListToStringConverter : ValueConverter<List<string>, string>
{
    public ListToStringConverter() 
        : base(
            v => JsonConvert.SerializeObject(v), 
            v => JsonConvert.DeserializeObject<List<string>>(v))
    {
    }
}
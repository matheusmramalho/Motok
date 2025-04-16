using MatheusR.Motok.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MatheusR.Motok.Infra.Mappings;
public class MotorcycleMapping : IEntityTypeConfiguration<Motorcycle>
{
    public void Configure(EntityTypeBuilder<Motorcycle> builder)
    {
        builder.ToTable("motorcycles");
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Identifier).IsRequired().HasColumnName("identifier").HasMaxLength(100);
        builder.Property(m => m.Year).IsRequired().HasColumnName("year");
        builder.Property(m => m.Model).IsRequired().HasColumnName("model");
        builder.Property(m => m.LicencePlate).IsRequired().HasColumnName("licence_plate");
    }
}

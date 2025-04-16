using MatheusR.Motok.Domain.Entities;
using MatheusR.Motok.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MatheusR.Motok.Infra.Mappings;
public class DeliveryMapping : IEntityTypeConfiguration<Delivery>
{
    public void Configure(EntityTypeBuilder<Delivery> builder)
    {
        builder.ToTable("deliveries");
        builder.HasKey(m => m.Id);

        builder.HasIndex(a => a.Cnpj).IsUnique();
        builder.HasIndex(a => a.LicenceNumber).IsUnique();

        builder.Property(m => m.Name).IsRequired().HasColumnName("name").HasMaxLength(100);
        builder.Property(m => m.Cnpj).IsRequired().HasColumnName("cnpj").HasMaxLength(14);
        builder.Property(m => m.BirthDate).IsRequired().HasColumnName("birth_date");
        builder.Property(m => m.LicenceNumber).IsRequired().HasColumnName("licence_number").HasMaxLength(50);
        builder.Property(m => m.LicenceType).IsRequired().HasColumnName("licence_type").HasConversion(new EnumToStringConverter<LicenteType>());
        builder.Property(m => m.LicenteImagePath).IsRequired(false).HasColumnName("licence_image_path");
    }
}

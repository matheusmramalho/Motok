using MatheusR.Motok.Domain.Entities;
using MatheusR.Motok.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MatheusR.Motok.Infra.Mappings;
public class RentMapping : IEntityTypeConfiguration<Rent>
{
    public void Configure(EntityTypeBuilder<Rent> builder)
    {
        builder.ToTable("rents");
        builder.HasKey(m => m.Id);

        builder.Property(m => m.InitialDate).IsRequired().HasColumnName("initial_date");
        builder.Property(m => m.FinalDate).IsRequired(false).HasColumnName("final_date");
        builder.Property(m => m.ExpectedPrice).IsRequired().HasColumnName("expected_price");
        builder.Property(m => m.FinalPrice).IsRequired(false).HasColumnName("final_price");
        builder.Property(m => m.HasFine).IsRequired(false).HasColumnName("has_fine");
        builder.Property(m => m.RentPlan).HasColumnName("rent_plan").HasConversion(new EnumToStringConverter<RentPlanType>());
    }
}

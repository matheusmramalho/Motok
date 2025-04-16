using MatheusR.Motok.Domain.Entities;
using MatheusR.Motok.Domain.OtherTables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MatheusR.Motok.Infra.Mappings;
public class Motorcycle2024Mapping : IEntityTypeConfiguration<Motorcycle2024>
{
    public void Configure(EntityTypeBuilder<Motorcycle2024> builder)
    {
        builder.ToTable("motorcycles_2024");
        builder.HasKey(m => m.Id);
    }
}

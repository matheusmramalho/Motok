using MatheusR.Motok.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MatheusR.Motok.Infra.Mappings;
public class RoleMapping : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("roles");
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Name).IsRequired().HasColumnName("name").HasMaxLength(50);
    }
}

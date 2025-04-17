using MatheusR.Motok.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MatheusR.Motok.Infra.Mappings;
public class UserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Name).IsRequired().HasColumnName("name").HasMaxLength(100);
        builder.Property(m => m.UserName).IsRequired().HasColumnName("username");
        builder.Property(m => m.Password).IsRequired().HasColumnName("password");
        builder.Property(m => m.RoleId).IsRequired().HasColumnName("role_id");
    }
}

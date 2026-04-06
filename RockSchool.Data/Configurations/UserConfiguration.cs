using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RockSchool.Domain.Entities;

namespace RockSchool.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(u => u.Id);

        builder.Ignore(u => u.UserId);
        builder.Ignore(u => u.Login);

        builder.Property(u => u.Id).HasColumnName("UserId");
        builder.Property(u => u.UserName).HasColumnName("Login").IsRequired().HasMaxLength(100);
        builder.Property(u => u.NormalizedUserName).HasColumnName("NormalizedLogin").HasMaxLength(100);
        builder.Property(u => u.PasswordHash).HasColumnName("PasswordHash").IsRequired().HasMaxLength(500);
        builder.Property(u => u.SecurityStamp).HasMaxLength(100);
        builder.Property(u => u.ConcurrencyStamp).HasMaxLength(100);
        builder.Property(u => u.PhoneNumber).HasMaxLength(20);
        builder.Property(u => u.RoleId).IsRequired();
        builder.Property(u => u.IsActive).IsRequired();

        builder.HasIndex(u => u.NormalizedUserName)
            .IsUnique()
            .HasDatabaseName("IX_Users_NormalizedLogin");

        builder.HasOne(u => u.Role).WithMany().HasForeignKey(u => u.RoleId);
    }
}

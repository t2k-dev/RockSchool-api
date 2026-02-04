using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RockSchool.Domain.Entities;

namespace RockSchool.Data.Configurations;

public class BandConfiguration : IEntityTypeConfiguration<Band>
{
    public void Configure(EntityTypeBuilder<Band> builder)
    {
        builder.ToTable("Bands");
        builder.HasKey(b => b.BandId);
        
        builder.Property(b => b.Name).IsRequired().HasMaxLength(200);
        builder.Property(b => b.TeacherId).IsRequired();
        builder.Property(b => b.Status).IsRequired();
        
        builder.HasOne(b => b.Teacher).WithMany().HasForeignKey("TeacherId");
        
        builder.HasMany(b => b.BandStudents).WithOne(bs => bs.Band).HasForeignKey("BandId");
        builder.HasMany(b => b.Schedules).WithOne(s => s.Band).HasForeignKey("BandId");
        
        builder.Metadata.FindNavigation(nameof(Band.BandStudents))!.SetPropertyAccessMode(PropertyAccessMode.Field);
        builder.Metadata.FindNavigation(nameof(Band.Schedules))!.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
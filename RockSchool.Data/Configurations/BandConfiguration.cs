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
        builder.Property(b => b.IsActive).IsRequired().HasDefaultValue(true);
        
        //builder.HasOne(b => b.Teacher).WithMany().HasForeignKey("TeacherId");
        builder.HasOne(s => s.Branch).WithMany().HasForeignKey("BranchId");
        builder.HasMany(b => b.BandMembers).WithOne(bm => bm.Band).HasForeignKey("BandId");
        //builder.HasOne(b => b.Schedule).WithOne().HasForeignKey("ScheduleId");
        
        builder.Metadata.FindNavigation(nameof(Band.BandMembers))!.SetPropertyAccessMode(PropertyAccessMode.Field);
        //builder.Metadata.FindNavigation(nameof(Band.Schedule))!.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
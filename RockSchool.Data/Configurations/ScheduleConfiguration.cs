using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RockSchool.Domain.Entities;

namespace RockSchool.Data.Configurations;

public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
{
    public void Configure(EntityTypeBuilder<Schedule> builder)
    {
        builder.ToTable("Schedules");
        builder.HasKey(s => s.ScheduleId);
        builder.Property(s => s.Name).IsRequired().HasMaxLength(255);
        builder.Property(s => s.IsActive).IsRequired();
        
        builder.HasMany(s => s.ScheduleSlots).WithOne().HasForeignKey("ScheduleId");
        
        builder.Metadata.FindNavigation(nameof(Schedule.ScheduleSlots))!.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
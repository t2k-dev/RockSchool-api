using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RockSchool.Domain.Entities;

namespace RockSchool.Data.Configurations;

public class ScheduleSlotConfiguration : IEntityTypeConfiguration<ScheduleSlot>
{
    public void Configure(EntityTypeBuilder<ScheduleSlot> builder)
    {
        builder.ToTable("ScheduleSlots");
        builder.HasKey(s => s.ScheduleSlotId);
        
        builder.Property(s => s.ScheduleId).IsRequired();
        builder.Property(s => s.RoomId).IsRequired();
        builder.Property(s => s.WeekDay).IsRequired();
        builder.Property(s => s.StartTime).IsRequired();
        builder.Property(s => s.EndTime).IsRequired();
        
        builder.HasOne(s => s.Room).WithMany().HasForeignKey("RoomId");
        builder.HasIndex(s => s.ScheduleId);
    }
}

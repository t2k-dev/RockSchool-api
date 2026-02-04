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
        
        builder.Property(s => s.RoomId).IsRequired();
        builder.Property(s => s.WeekDay).IsRequired();
        builder.Property(s => s.StartTime).IsRequired();
        builder.Property(s => s.EndTime).IsRequired();
        builder.Property(s => s.SubscriptionId).IsRequired(false);
        builder.Property(s => s.BandId).IsRequired(false);
        
        builder.HasOne(s => s.Room).WithMany().HasForeignKey("RoomId");
        builder.HasOne(s => s.Subscription).WithMany(sub => sub.Schedules).HasForeignKey("SubscriptionId").IsRequired(false);
        builder.HasOne(s => s.Band).WithMany(b => b.Schedules).HasForeignKey("BandId").IsRequired(false);
    }
}
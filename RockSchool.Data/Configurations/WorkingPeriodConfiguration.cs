using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RockSchool.Domain.Entities;

namespace RockSchool.Data.Configurations;

public class WorkingPeriodConfiguration : IEntityTypeConfiguration<WorkingPeriod>
{
    public void Configure(EntityTypeBuilder<WorkingPeriod> builder)
    {
        builder.ToTable("WorkingPeriods");
        builder.HasKey(wp => wp.WorkingPeriodId);
        
        builder.Property(wp => wp.TeacherId).IsRequired();
        builder.Property(wp => wp.WeekDay).IsRequired();
        builder.Property(wp => wp.StartTime).IsRequired();
        builder.Property(wp => wp.EndTime).IsRequired();
        builder.Property(wp => wp.RoomId).IsRequired();
        
        //builder.HasOne(wp => wp.Teacher).WithMany().HasForeignKey("TeacherId");
        builder.HasOne(wp => wp.Room).WithMany().HasForeignKey("RoomId");
    }
}

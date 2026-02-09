using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RockSchool.Domain.Entities;

namespace RockSchool.Data.Configurations;

public class ScheduledWorkingPeriodConfiguration : IEntityTypeConfiguration<ScheduledWorkingPeriod>
{
    public void Configure(EntityTypeBuilder<ScheduledWorkingPeriod> builder)
    {
        builder.ToTable("ScheduledWorkingPeriods");
        builder.HasKey(swp => swp.ScheduledWorkingPeriodId);
        
        builder.Property(swp => swp.TeacherId).IsRequired();
        builder.Property(swp => swp.StartDate).IsRequired();
        builder.Property(swp => swp.EndDate).IsRequired();
        builder.Property(swp => swp.RoomId).IsRequired();
    }
}

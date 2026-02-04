using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RockSchool.Domain.Entities;

namespace RockSchool.Data.Configurations;

public class WaitingScheduleConfiguration : IEntityTypeConfiguration<WaitingSchedule>
{
    public void Configure(EntityTypeBuilder<WaitingSchedule> builder)
    {
        builder.ToTable("WaitingSchedules");
        builder.HasKey(ws => ws.ScheduleId);
        
        builder.Property(ws => ws.StudentId).IsRequired();
        builder.Property(ws => ws.DisciplineId).IsRequired();
        builder.Property(ws => ws.TeacherId).IsRequired();
        builder.Property(ws => ws.CreatedOn).IsRequired();
        builder.Property(ws => ws.WeekDay).IsRequired();
        builder.Property(ws => ws.StartTime).IsRequired();
        builder.Property(ws => ws.EndTime).IsRequired();
        
        builder.HasOne(ws => ws.Student).WithMany().HasForeignKey("StudentId");
        builder.HasOne(ws => ws.Discipline).WithMany().HasForeignKey("DisciplineId");
        builder.HasOne(ws => ws.Teacher).WithMany().HasForeignKey("TeacherId");
    }
}

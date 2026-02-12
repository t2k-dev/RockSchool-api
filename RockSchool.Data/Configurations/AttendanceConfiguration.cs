using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RockSchool.Domain.Entities;

namespace RockSchool.Data.Configurations;

public class AttendanceConfiguration : IEntityTypeConfiguration<Attendance>
{
    public void Configure(EntityTypeBuilder<Attendance> builder)
    {
        builder.ToTable("Attendances");
        builder.HasKey(a => a.AttendanceId);
        
        builder.Property(a => a.StartDate).IsRequired();
        builder.Property(a => a.EndDate).IsRequired();
        builder.Property(a => a.RoomId).IsRequired();
        builder.Property(a => a.BranchId).IsRequired();
        builder.Property(a => a.AttendanceType).IsRequired().HasConversion<int>();
        builder.Property(a => a.Status).IsRequired().HasConversion<int>();
        builder.Property(a => a.IsCompleted).IsRequired();
        builder.Property(a => a.StatusReason).HasMaxLength(500);
        builder.Property(a => a.Comment).HasMaxLength(1000);
        builder.Property(a => a.DisciplineId).IsRequired(false);
        builder.Property(a => a.TeacherId).IsRequired(false);
        builder.Property(a => a.GroupId).IsRequired(false);
        
        //builder.HasOne(a => a.Discipline).WithMany().HasForeignKey("DisciplineId").IsRequired(false);
        //builder.HasOne(a => a.Teacher).WithMany().HasForeignKey("TeacherId").IsRequired(false);
        //builder.HasOne(a => a.Branch).WithMany().HasForeignKey("BranchId");
        //builder.HasOne(a => a.Room).WithMany().HasForeignKey("RoomId");
        
        builder.HasMany(a => a.Attendees).WithOne().HasForeignKey(a => a.AttendanceId);
        
        //builder.Metadata.FindNavigation(nameof(Attendance.Attendees))!.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
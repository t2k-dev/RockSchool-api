using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RockSchool.Domain.Entities;

namespace RockSchool.Data.Configurations;

public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
{
    public void Configure(EntityTypeBuilder<Teacher> builder)
    {
        builder.ToTable("Teachers");
        builder.HasKey(t => t.TeacherId);
        
        builder.Property(t => t.FirstName).IsRequired().HasMaxLength(200);
        builder.Property(t => t.LastName).IsRequired().HasMaxLength(200);
        builder.Property(t => t.BirthDate).IsRequired();
        builder.Property(t => t.Sex).IsRequired();
        builder.Property(t => t.Phone).IsRequired();
        builder.Property(t => t.BranchId).IsRequired();
        builder.Property(t => t.AgeLimit).IsRequired();
        builder.Property(t => t.AllowGroupLessons).IsRequired();
        builder.Property(t => t.AllowBands).IsRequired();
        builder.Property(t => t.IsActive).IsRequired();
        
        builder.HasOne(t => t.User).WithMany().HasForeignKey("UserId").IsRequired(false);
        builder.HasOne(t => t.Branch).WithMany().HasForeignKey("BranchId");
        
        builder.Metadata.FindNavigation(nameof(Teacher.Disciplines))!.SetPropertyAccessMode(PropertyAccessMode.Field);
        builder.Metadata.FindNavigation(nameof(Teacher.WorkingPeriods))!.SetPropertyAccessMode(PropertyAccessMode.Field);
        builder.Metadata.FindNavigation(nameof(Teacher.ScheduledWorkingPeriods))!.SetPropertyAccessMode(PropertyAccessMode.Field);
        builder.Metadata.FindNavigation(nameof(Teacher.Bands))!.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
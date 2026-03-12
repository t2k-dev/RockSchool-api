using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RockSchool.Domain.Entities;
using RockSchool.Domain.Teachers;

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
        builder.HasOne(t => t.Branch).WithMany().HasForeignKey(t => t.BranchId);

        builder.HasMany(t => t.WorkingPeriods).WithOne().HasForeignKey(t => t.TeacherId);
        builder.HasMany(t => t.ScheduledWorkingPeriods).WithOne().HasForeignKey(t => t.TeacherId);

        // Configure explicit many-to-many relationship through TeacherDiscipline join entity
        builder.HasMany(t => t.Disciplines)
            .WithMany()
            .UsingEntity<TeacherDiscipline>(
                j => j.HasOne(td => td.Discipline).WithMany().HasForeignKey(td => td.DisciplineId),
                j => j.HasOne(td => td.Teacher).WithMany().HasForeignKey(td => td.TeacherId),
                j =>
                {
                    j.ToTable("TeacherDisciplines");
                    j.HasKey(td => new { td.TeacherId, td.DisciplineId });
                });

        // Safe navigation access with null check
        var disciplinesNav = builder.Metadata.FindNavigation(nameof(Teacher.Disciplines));
        if (disciplinesNav != null)
            disciplinesNav.SetPropertyAccessMode(PropertyAccessMode.Field);

        var bandsNav = builder.Metadata.FindNavigation(nameof(Teacher.Bands));
        if (bandsNav != null)
            bandsNav.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
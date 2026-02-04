using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RockSchool.Domain.Entities;

namespace RockSchool.Data.Configurations;

public class TeacherDisciplineConfiguration : IEntityTypeConfiguration<TeacherDiscipline>
{
    public void Configure(EntityTypeBuilder<TeacherDiscipline> builder)
    {
        builder.ToTable("TeacherDisciplines");
        builder.HasKey(td => new { td.TeacherId, td.DisciplineId });
        
        builder.Property(td => td.TeacherId).IsRequired();
        builder.Property(td => td.DisciplineId).IsRequired();
        
        builder.HasOne(td => td.Teacher).WithMany().HasForeignKey("TeacherId");
        builder.HasOne(td => td.Discipline).WithMany(d => d.TeacherDisciplines).HasForeignKey("DisciplineId");
    }
}
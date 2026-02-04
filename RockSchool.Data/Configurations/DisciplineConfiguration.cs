using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RockSchool.Domain.Entities;

namespace RockSchool.Data.Configurations;

public class DisciplineConfiguration : IEntityTypeConfiguration<Discipline>
{
    public void Configure(EntityTypeBuilder<Discipline> builder)
    {
        builder.ToTable("Disciplines");
        builder.HasKey(d => d.DisciplineId);
        
        builder.Property(d => d.Name).IsRequired().HasMaxLength(200);
        builder.Property(d => d.IsActive).IsRequired();
        
        builder.Metadata.FindNavigation(nameof(Discipline.RoomDisciplines))!.SetPropertyAccessMode(PropertyAccessMode.Field);
        builder.Metadata.FindNavigation(nameof(Discipline.TeacherDisciplines))!.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
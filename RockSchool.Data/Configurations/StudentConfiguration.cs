using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RockSchool.Domain.Entities;

namespace RockSchool.Data.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        // Table mapping
        builder.ToTable("Students");
        
        // Primary key
        builder.HasKey(s => s.StudentId);
        
        // Properties
        builder.Property(s => s.StudentId)
            .IsRequired();
        
        builder.Property(s => s.FirstName)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(s => s.LastName)
            .HasMaxLength(200);
        
        builder.Property(s => s.Sex)
            .IsRequired();
        
        builder.Property(s => s.BirthDate)
            .IsRequired();
        
        builder.Property(s => s.Phone)
            .IsRequired();
        
        builder.Property(s => s.Level)
            .IsRequired(false);
        
        builder.Property(s => s.IsWaiting)
            .IsRequired()
            .HasDefaultValue(false);
        
        // Relationships
        builder.HasOne(s => s.User)
            .WithMany()
            .HasForeignKey("UserId")
            .IsRequired(false);
        
        builder.HasOne(s => s.Branch)
            .WithMany()
            .HasForeignKey("BranchId")
            .IsRequired(false);
        
        // Collections - configure backing field
        builder.HasMany(s => s.BandStudents)
            .WithOne(bs => bs.Student)
            .HasForeignKey("StudentId");
        
        builder.Metadata
            .FindNavigation(nameof(Student.BandStudents))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
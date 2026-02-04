using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RockSchool.Domain.Entities;

namespace RockSchool.Data.Configurations;

public class BandStudentConfiguration : IEntityTypeConfiguration<BandStudent>
{
    public void Configure(EntityTypeBuilder<BandStudent> builder)
    {
        builder.ToTable("BandStudents");
        builder.HasKey(bs => bs.BandStudentId);
        
        builder.Property(bs => bs.BandId).IsRequired();
        builder.Property(bs => bs.StudentId).IsRequired();
        builder.Property(bs => bs.BandRoleId).IsRequired().HasConversion<int>();
        
        builder.HasOne(bs => bs.Band).WithMany(b => b.BandStudents).HasForeignKey("BandId");
        builder.HasOne(bs => bs.Student).WithMany(s => s.BandStudents).HasForeignKey("StudentId");
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RockSchool.Domain.Entities;

namespace RockSchool.Data.Configurations;

public class NoteConfiguration : IEntityTypeConfiguration<Note>
{
    public void Configure(EntityTypeBuilder<Note> builder)
    {
        builder.ToTable("Notes");
        builder.HasKey(n => n.NoteId);
        
        builder.Property(n => n.BranchId).IsRequired();
        builder.Property(n => n.Description).HasMaxLength(2000);
        builder.Property(n => n.Status).IsRequired().HasConversion<int>();
        builder.Property(n => n.CompleteDate).IsRequired(false);
        builder.Property(n => n.ActualCompleteDate).IsRequired(false);
        builder.Property(n => n.Comment).HasMaxLength(1000);
        
        builder.HasOne(n => n.Branch).WithMany().HasForeignKey("BranchId");
    }
}

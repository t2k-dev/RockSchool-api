using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RockSchool.Domain.Entities;

namespace RockSchool.Data.Configurations;

public class BandMemberConfiguration : IEntityTypeConfiguration<BandMember>
{
    public void Configure(EntityTypeBuilder<BandMember> builder)
    {
        builder.ToTable("BandMembers");
        builder.HasKey(bm => bm.BandMemberId);
        builder.Property(bm => bm.BandMemberId).HasColumnName("BandMemberId");
        
        builder.Property(bm => bm.BandId).IsRequired();
        builder.Property(bm => bm.StudentId).IsRequired();
        builder.Property(bm => bm.BandRoleId).HasConversion<int>();
        
        builder.HasOne(bm => bm.Band).WithMany(b => b.BandMembers).HasForeignKey("BandId");
        builder.HasOne(bm => bm.Student).WithMany(s => s.BandMembers).HasForeignKey("StudentId");
    }
}

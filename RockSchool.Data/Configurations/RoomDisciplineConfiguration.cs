using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RockSchool.Domain.Entities;

namespace RockSchool.Data.Configurations;

public class RoomDisciplineConfiguration : IEntityTypeConfiguration<RoomDiscipline>
{
    public void Configure(EntityTypeBuilder<RoomDiscipline> builder)
    {
        builder.ToTable("RoomDisciplines");
        builder.HasKey(rd => new { rd.RoomId, rd.DisciplineId });
        
        builder.Property(rd => rd.RoomId).IsRequired();
        builder.Property(rd => rd.DisciplineId).IsRequired();
        
        builder.HasOne(rd => rd.Room).WithMany(r => r.RoomDisciplines).HasForeignKey("RoomId");
        builder.HasOne(rd => rd.Discipline).WithMany(d => d.RoomDisciplines).HasForeignKey("DisciplineId");
    }
}
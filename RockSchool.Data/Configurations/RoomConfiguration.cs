using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RockSchool.Domain.Entities;

namespace RockSchool.Data.Configurations;

public class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.ToTable("Rooms");
        builder.HasKey(r => r.RoomId);
        
        builder.Property(r => r.Name).IsRequired().HasMaxLength(200);
        builder.Property(r => r.BranchId).IsRequired();
        builder.Property(r => r.SupportsRent).IsRequired();
        builder.Property(r => r.SupportsRehearsal).IsRequired();
        builder.Property(r => r.IsActive).IsRequired();
        //builder.Property(r => r.Status).IsRequired();
        
        builder.HasOne(r => r.Branch).WithMany().HasForeignKey("BranchId");
        
        builder.Metadata.FindNavigation(nameof(Room.RoomDisciplines))!.SetPropertyAccessMode(PropertyAccessMode.Field);
        builder.Metadata.FindNavigation(nameof(Room.Schedules))!.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
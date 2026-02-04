using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RockSchool.Domain.Entities;

namespace RockSchool.Data.Configurations;

public class TariffConfiguration : IEntityTypeConfiguration<Tariff>
{
    public void Configure(EntityTypeBuilder<Tariff> builder)
    {
        builder.ToTable("Tariffs");
        builder.HasKey(t => t.TariffId);
        
        builder.Property(t => t.Amount).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(t => t.StartDate).IsRequired();
        builder.Property(t => t.EndDate).IsRequired();
        builder.Property(t => t.AttendanceLength).IsRequired();
        builder.Property(t => t.AttendanceCount).IsRequired();
        builder.Property(t => t.SubscriptionType).IsRequired().HasConversion<int>();
        builder.Property(t => t.DisciplineId).IsRequired(false);
        
        builder.HasOne(t => t.Discipline).WithMany().HasForeignKey("DisciplineId").IsRequired(false);
    }
}
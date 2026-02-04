using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RockSchool.Domain.Entities;

namespace RockSchool.Data.Configurations;

public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.ToTable("Subscriptions");
        builder.HasKey(s => s.SubscriptionId);
        
        builder.Property(s => s.StudentId).IsRequired();
        builder.Property(s => s.BranchId).IsRequired();
        builder.Property(s => s.StartDate).IsRequired();
        builder.Property(s => s.AttendanceCount).IsRequired();
        builder.Property(s => s.AttendanceLength).IsRequired();
        builder.Property(s => s.AttendancesLeft).IsRequired();
        builder.Property(s => s.Status).IsRequired().HasConversion<int>();
        builder.Property(s => s.SubscriptionType).IsRequired().HasConversion<int>();
        builder.Property(s => s.Price).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(s => s.FinalPrice).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(s => s.AmountOutstanding).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(s => s.StatusReason).HasMaxLength(500);
        builder.Property(s => s.DisciplineId).IsRequired(false);
        builder.Property(s => s.TeacherId).IsRequired(false);
        builder.Property(s => s.GroupId).IsRequired(false);
        builder.Property(s => s.TariffId).IsRequired(false);
        builder.Property(s => s.TrialStatus).HasConversion<int?>().IsRequired(false);
        
        builder.HasOne(s => s.Student).WithMany().HasForeignKey("StudentId");
        builder.HasOne(s => s.Branch).WithMany().HasForeignKey("BranchId");
        builder.HasOne(s => s.Discipline).WithMany().HasForeignKey("DisciplineId").IsRequired(false);
        builder.HasOne(s => s.Teacher).WithMany().HasForeignKey("TeacherId").IsRequired(false);
        
        builder.HasMany(s => s.Schedules).WithOne(sc => sc.Subscription).HasForeignKey("SubscriptionId");
        builder.HasMany(s => s.Tenders).WithOne(t => t.Subscription).HasForeignKey("SubscriptionId");
        
        builder.Metadata.FindNavigation(nameof(Subscription.Schedules))!.SetPropertyAccessMode(PropertyAccessMode.Field);
        builder.Metadata.FindNavigation(nameof(Subscription.Tenders))!.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
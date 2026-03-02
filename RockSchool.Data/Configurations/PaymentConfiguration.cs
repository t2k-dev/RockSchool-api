using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RockSchool.Domain.Entities;

namespace RockSchool.Data.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payments");
        builder.HasKey(t => t.PaymentId);
        
        builder.Property(t => t.Amount).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(t => t.PaidOn).IsRequired();
        builder.Property(t => t.PaymentType).IsRequired().HasConversion<int>();
        builder.Property(t => t.SubscriptionId).IsRequired();
        
        builder.HasOne(t => t.Subscription).WithMany(s => s.Payments).HasForeignKey("SubscriptionId");
    }
}
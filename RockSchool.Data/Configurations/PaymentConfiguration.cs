using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RockSchool.Domain.Entities;

namespace RockSchool.Data.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payments");
        builder.HasKey(p => p.PaymentId);
        
        builder.Property(p => p.Amount).IsRequired();
        builder.Property(p => p.PaidOn).IsRequired();
        builder.Property(p => p.PaymentType).IsRequired().HasConversion<int>();
    }
}

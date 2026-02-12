using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RockSchool.Domain.Entities;

namespace RockSchool.Data.Configurations;

public class AttendeeConfiguration : IEntityTypeConfiguration<Attendee>
{
    public void Configure(EntityTypeBuilder<Attendee> builder)
    {
        builder.ToTable("Attendees");
        builder.HasKey(a => a.AttendeeId);
        
        builder.Property(a => a.SubscriptionId).IsRequired();
        builder.Property(a => a.StudentId).IsRequired();
        builder.Property(a => a.AttendanceId).IsRequired();
        builder.Property(a => a.Status).IsRequired().HasConversion<int>();
        
        //builder.HasOne<Subscription>.WithMany().HasForeignKey(a => a.);
        //builder.HasOne(a => a.Attendance).WithMany(att => att.Attendees).HasForeignKey("AttendanceId");
    }
}

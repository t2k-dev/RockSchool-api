using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RockSchool.Domain.Entities;
using RockSchool.Domain.Enums;
using RockSchool.Domain.Students;
using RockSchool.Domain.Teachers;

namespace RockSchool.Data.Data;

public class RockSchoolContext : IdentityUserContext<User, Guid, IdentityUserClaim<Guid>, IdentityUserLogin<Guid>, IdentityUserToken<Guid>>
{
    public RockSchoolContext(DbContextOptions<RockSchoolContext> options) : base(options)
    {
    }

    public DbSet<Discipline> Disciplines { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<ScheduleSlot> ScheduleSlots { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Attendance> Attendances { get; set; }
    public DbSet<Attendee> Attendees { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<TeacherDiscipline> TeacherDisciplines { get; set; }
    public DbSet<WorkingPeriod> WorkingPeriods { get; set; }
    public DbSet<ScheduledWorkingPeriod> ScheduledWorkingPeriods { get; set; }
    public DbSet<Note> Notes { get; set; }
    public DbSet<WaitingSchedule> WaitingSchedules { get; set; }
    public DbSet<Payment> Payments { get; set; } = null!;
    public DbSet<Tariff> Tariffs { get; set; }
    public DbSet<Band> Bands { get; set; }
    public DbSet<BandMember> BandMembers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RockSchoolContext).Assembly);
    }
}

using Microsoft.EntityFrameworkCore;
using RockSchool.Domain.Entities;
using RockSchool.Domain.Enums;
using RockSchool.Domain.Students;
using RockSchool.Domain.Teachers;

namespace RockSchool.Data.Data;

public class RockSchoolContext : DbContext
{
    public RockSchoolContext(DbContextOptions<RockSchoolContext> options) : base(options)
    {
    }

    public DbSet<Discipline> Disciplines { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Attendance> Attendances { get; set; }
    public DbSet<Attendee> Attendees { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<TeacherDiscipline> TeacherDisciplines { get; set; }
    public DbSet<WorkingPeriod> WorkingPeriods { get; set; }
    public DbSet<ScheduledWorkingPeriod> ScheduledWorkingPeriods { get; set; }
    public DbSet<Note> Notes { get; set; }
    public DbSet<WaitingSchedule> WaitingSchedules { get; set; }
    public DbSet<Tender> Tenders { get; set; } = null!;
    public DbSet<Tariff> Tariffs { get; set; }
    public DbSet<Band> Bands { get; set; }
    public DbSet<BandStudent> BandStudents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Apply all entity configurations
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RockSchoolContext).Assembly);
        
        // Seed data
        modelBuilder.Entity<Role>().HasData(
            new { RoleId = 1, RoleName = "Admin", IsActive = true },
            new { RoleId = 2, RoleName = "Teacher", IsActive = true },
            new { RoleId = 3, RoleName = "Student", IsActive = true });

        modelBuilder.Entity<Discipline>().HasData(
            new { Name = "Guitar", DisciplineId = 1, IsActive = true },
            new { Name = "Electric Guitar", DisciplineId = 2, IsActive = true },
            new { Name = "Bass Guitar", DisciplineId = 3, IsActive = true },
            new { Name = "Ukulele", DisciplineId = 4, IsActive = true },
            new { Name = "Vocal", DisciplineId = 5, IsActive = true },
            new { Name = "Drums", DisciplineId = 6, IsActive = true },
            new { Name = "Piano", DisciplineId = 7, IsActive = true },
            new { Name = "Violin", DisciplineId = 8, IsActive = true },
            new { Name = "Extreme Vocal", DisciplineId = 9, IsActive = true }
        );

        modelBuilder.Entity<Branch>().HasData(
            new { BranchId = 1, Name = "На Абая", Address = "Абая 137" },
            new { BranchId = 2, Name = "На Аль-Фараби", Address = "Аль-Фараби 15" }
        );

        modelBuilder.Entity<Room>().HasData(
            new { RoomId = 1, BranchId = 1, Name = "Гитарная", IsActive = true, SupportsRehearsal = true, SupportsRent = true },
            new { RoomId = 2, BranchId = 1, Name = "Вокальная", IsActive = true, SupportsRehearsal = true, SupportsRent = true },
            new { RoomId = 4, BranchId = 1, Name = "Барабанная", IsActive = true, SupportsRehearsal = true, SupportsRent = true },
            new { RoomId = 5, BranchId = 1, Name = "Желтая", IsActive = true, SupportsRehearsal = true, SupportsRent = true },
            new { RoomId = 6, BranchId = 1, Name = "Зелёная", IsActive = true, SupportsRehearsal = true, SupportsRent = true },
            new { RoomId = 10, BranchId = 2, Name = "Гитарная", IsActive = true, SupportsRehearsal = true, SupportsRent = true },
            new { RoomId = 11, BranchId = 2, Name = "Вокальная", IsActive = true, SupportsRehearsal = true, SupportsRent = true },
            new { RoomId = 12, BranchId = 2, Name = "Барабанная", IsActive = true, SupportsRehearsal = true, SupportsRent = true },
            new { RoomId = 13, BranchId = 2, Name = "Плакатная", IsActive = true, SupportsRehearsal = true, SupportsRent = true },
            new { RoomId = 14, BranchId = 2, Name = "Желтая", IsActive = true, SupportsRehearsal = true, SupportsRent = true }
        );

        modelBuilder.Entity<Tariff>().HasData(
            // TrialLesson
            new { TariffId = new Guid("107f43b0-46c0-4b5f-a6e0-58658a4d0aa8"), SubscriptionType = SubscriptionType.TrialLesson, AttendanceLength = 1, AttendanceCount = 1, DisciplineId = (int?)null, StartDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), EndDate = new DateTime(2026, 12, 31, 0, 0, 0, DateTimeKind.Utc), Amount = 2000m },
            
            // Lesson - general
            new { TariffId = new Guid("1605538b-4e77-4dda-a492-44040dee1ea0"), SubscriptionType = SubscriptionType.Lesson, AttendanceLength = 1, AttendanceCount = 1, DisciplineId = (int?)null, StartDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), EndDate = new DateTime(2026, 12, 31, 0, 0, 0, DateTimeKind.Utc), Amount = 9000m },
            new { TariffId = new Guid("1916fc99-d9d4-4b48-a30a-07f2b8271224"), SubscriptionType = SubscriptionType.Lesson, AttendanceLength = 1, AttendanceCount = 4, DisciplineId = (int?)null, StartDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), EndDate = new DateTime(2026, 12, 31, 0, 0, 0, DateTimeKind.Utc), Amount = 30000m },
            new { TariffId = new Guid("21a7e82a-b0fd-4e34-8d01-cce816ed34ba"), SubscriptionType = SubscriptionType.Lesson, AttendanceLength = 1, AttendanceCount = 8, DisciplineId = (int?)null, StartDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), EndDate = new DateTime(2026, 12, 31, 0, 0, 0, DateTimeKind.Utc), Amount = 54000m },
            new { TariffId = new Guid("24e75034-ebed-44e7-a44c-ea12e33a6767"), SubscriptionType = SubscriptionType.Lesson, AttendanceLength = 1, AttendanceCount = 12, DisciplineId = (int?)null, StartDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), EndDate = new DateTime(2026, 12, 31, 0, 0, 0, DateTimeKind.Utc), Amount = 74000m },
            
            // Lesson - with discipline ID 9 (Extreme Vocal)
            new { TariffId = new Guid("2c45b82e-16dc-4bf2-bd78-eb5922cbe3ec"), SubscriptionType = SubscriptionType.Lesson, AttendanceLength = 1, AttendanceCount = 1, DisciplineId = (int?)9, StartDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), EndDate = new DateTime(2026, 12, 31, 0, 0, 0, DateTimeKind.Utc), Amount = 11000m },
            new { TariffId = new Guid("32cfcefc-803e-4922-b358-6a9add488aab"), SubscriptionType = SubscriptionType.Lesson, AttendanceLength = 1, AttendanceCount = 4, DisciplineId = (int?)9, StartDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), EndDate = new DateTime(2026, 12, 31, 0, 0, 0, DateTimeKind.Utc), Amount = 42000m },
            new { TariffId = new Guid("3b5a0821-8663-4e1b-8fcc-b48c03d97185"), SubscriptionType = SubscriptionType.Lesson, AttendanceLength = 1, AttendanceCount = 8, DisciplineId = (int?)9, StartDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), EndDate = new DateTime(2026, 12, 31, 0, 0, 0, DateTimeKind.Utc), Amount = 69000m },
            new { TariffId = new Guid("6ee39316-06f7-4c1b-9167-f35c3dce5f80"), SubscriptionType = SubscriptionType.Lesson, AttendanceLength = 1, AttendanceCount = 12, DisciplineId = (int?)9, StartDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), EndDate = new DateTime(2026, 12, 31, 0, 0, 0, DateTimeKind.Utc), Amount = 96000m },
            
            // GroupLesson
            new { TariffId = new Guid("83a26ae5-6cda-4877-95ab-ddca0b064578"), SubscriptionType = SubscriptionType.GroupLesson, AttendanceLength = 1, AttendanceCount = 4, DisciplineId = (int?)null, StartDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), EndDate = new DateTime(2026, 12, 31, 0, 0, 0, DateTimeKind.Utc), Amount = 24000m },
            new { TariffId = new Guid("8d9e7ed0-9dcf-4686-bbf3-744f804d0826"), SubscriptionType = SubscriptionType.GroupLesson, AttendanceLength = 1, AttendanceCount = 8, DisciplineId = (int?)null, StartDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), EndDate = new DateTime(2026, 12, 31, 0, 0, 0, DateTimeKind.Utc), Amount = 36000m },
            new { TariffId = new Guid("a57918be-0f1c-41c6-bc2d-add554ac2969"), SubscriptionType = SubscriptionType.GroupLesson, AttendanceLength = 1, AttendanceCount = 4, DisciplineId = (int?)9, StartDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), EndDate = new DateTime(2026, 12, 31, 0, 0, 0, DateTimeKind.Utc), Amount = 35000m },
            new { TariffId = new Guid("adcf2ba3-588c-46d8-bab6-04b3a54c2a1d"), SubscriptionType = SubscriptionType.GroupLesson, AttendanceLength = 1, AttendanceCount = 8, DisciplineId = (int?)9, StartDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), EndDate = new DateTime(2026, 12, 31, 0, 0, 0, DateTimeKind.Utc), Amount = 59000m },
            
            // Rehearsal
            new { TariffId = new Guid("af7b2695-618a-4cf3-b57b-14e6aadc0187"), SubscriptionType = SubscriptionType.Rehearsal, AttendanceLength = 3, AttendanceCount = 4, DisciplineId = (int?)null, StartDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), EndDate = new DateTime(2026, 12, 31, 0, 0, 0, DateTimeKind.Utc), Amount = 24000m },
            
            // Rent
            new { TariffId = new Guid("becf83b9-bb78-4130-a654-c17c2f7a869a"), SubscriptionType = SubscriptionType.Rent, AttendanceLength = 1, AttendanceCount = 1, DisciplineId = (int?)null, StartDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), EndDate = new DateTime(2026, 12, 31, 0, 0, 0, DateTimeKind.Utc), Amount = 3000m },
            new { TariffId = new Guid("ddf4bb6c-686a-490e-9440-61c5e2e68513"), SubscriptionType = SubscriptionType.Rent, AttendanceLength = 1, AttendanceCount = 4, DisciplineId = (int?)null, StartDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), EndDate = new DateTime(2026, 12, 31, 0, 0, 0, DateTimeKind.Utc), Amount = 11000m },
            new { TariffId = new Guid("eaa18e35-d9a5-41b0-946b-7557fdc199a8"), SubscriptionType = SubscriptionType.Rent, AttendanceLength = 1, AttendanceCount = 8, DisciplineId = (int?)null, StartDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), EndDate = new DateTime(2026, 12, 31, 0, 0, 0, DateTimeKind.Utc), Amount = 20000m },
            new { TariffId = new Guid("f79be13e-77ae-4728-b1ce-6e922c9b066d"), SubscriptionType = SubscriptionType.Rent, AttendanceLength = 1, AttendanceCount = 12, DisciplineId = (int?)null, StartDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), EndDate = new DateTime(2026, 12, 31, 0, 0, 0, DateTimeKind.Utc), Amount = 30000m }
        );
    }
}
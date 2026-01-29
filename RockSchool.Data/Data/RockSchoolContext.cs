using Microsoft.EntityFrameworkCore;
using RockSchool.Data.Entities;
using RockSchool.Data.Enums;

namespace RockSchool.Data.Data;

public class RockSchoolContext : DbContext
{
    public RockSchoolContext(DbContextOptions<RockSchoolContext> options) : base(options)
    {
    }

    public DbSet<DisciplineEntity> Disciplines { get; set; }
    public DbSet<TeacherEntity> Teachers { get; set; }
    public DbSet<ScheduleEntity> Schedules { get; set; }
    public DbSet<StudentEntity> Students { get; set; }
    public DbSet<RoomEntity> Rooms { get; set; }
    public DbSet<AttendanceEntity> Attendances { get; set; }
    public DbSet<RoleEntity> Roles { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<BranchEntity> Branches { get; set; }
    public DbSet<SubscriptionEntity> Subscriptions { get; set; }
    public DbSet<TeacherDisciplineEntity> TeacherDisciplines { get; set; }
    public DbSet<WorkingPeriodEntity> WorkingPeriods { get; set; }
    public DbSet<ScheduledWorkingPeriodEntity> ScheduledWorkingPeriods { get; set; }
    public DbSet<NoteEntity> Notes { get; set; }
    public DbSet<WaitingScheduleEntity> WaitingSchedules { get; set; }
    public DbSet<TenderEntity> Tenders { get; set; } = null!;
    public DbSet<TariffEntity> Tariffs { get; set; }
    public DbSet<BandEntity> Bands { get; set; }
    public DbSet<BandStudentEntity> BandStudents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RoleEntity>().HasData(
            new RoleEntity { RoleId = 1, RoleName = "Admin" },
            new RoleEntity { RoleId = 2, RoleName = "Teacher" },
            new RoleEntity { RoleId = 3, RoleName = "Student" });

        modelBuilder.Entity<DisciplineEntity>().HasData(
            new DisciplineEntity { Name = "Guitar", IsActive = true, DisciplineId = 1 },
            new DisciplineEntity { Name = "Electric Guitar", IsActive = true, DisciplineId = 2 },
            new DisciplineEntity { Name = "Bass Guitar", IsActive = true, DisciplineId = 3 },
            new DisciplineEntity { Name = "Ukulele", IsActive = true, DisciplineId = 4 },
            new DisciplineEntity { Name = "Vocal", IsActive = true, DisciplineId = 5 },
            new DisciplineEntity { Name = "Drums", IsActive = true, DisciplineId = 6 },
            new DisciplineEntity { Name = "Piano", IsActive = true, DisciplineId = 7 },
            new DisciplineEntity { Name = "Violin", IsActive = true, DisciplineId = 8 },
            new DisciplineEntity { Name = "Extreme Vocal", IsActive = true, DisciplineId = 9 }
        );

        modelBuilder.Entity<BranchEntity>().HasData(
            new BranchEntity { BranchId = 1, Name = "На Абая", Phone = "77471237896", Address = "Абая 137" },
            new BranchEntity { BranchId = 2, Name = "На Аль-Фараби", Phone = "77471237896", Address = "Аль-Фараби 15" }
        );

        modelBuilder.Entity<RoomEntity>().HasData(
            new RoomEntity { RoomId = 1, BranchId = 1, Name = "Гитарная", Status = 1, IsActive = true },
            new RoomEntity { RoomId = 2, BranchId = 1, Name = "Вокальная", Status = 1, IsActive = true },
            new RoomEntity { RoomId = 4, BranchId = 1, Name = "Барабанная", Status = 1, IsActive = true },
            new RoomEntity { RoomId = 5, BranchId = 1, Name = "Желтая", Status = 1, IsActive = true },
            new RoomEntity { RoomId = 6, BranchId = 1, Name = "Зелёная", Status = 1, IsActive = true }
        );

        modelBuilder.Entity<RoomEntity>().HasData(
            new RoomEntity { RoomId = 10, BranchId = 2, Name = "Гитарная", Status = 1, IsActive = true },
            new RoomEntity { RoomId = 11, BranchId = 2, Name = "Вокальная", Status = 1, IsActive = true },
            new RoomEntity { RoomId = 12, BranchId = 2, Name = "Барабанная", Status = 1, IsActive = true },
            new RoomEntity { RoomId = 13, BranchId = 2, Name = "Плакатная", Status = 1, IsActive = true },
            new RoomEntity { RoomId = 14, BranchId = 2, Name = "Желтая", Status = 1, IsActive = true }
        );

        modelBuilder.Entity<TariffEntity>().HasData(
            // TrialLesson
            new TariffEntity { TariffId = new Guid("107f43b0-46c0-4b5f-a6e0-58658a4d0aa8"), SubscriptionType = SubscriptionType.TrialLesson, AttendanceLength = 1, AttendanceCount = 1, DisciplineId = null, StartDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), EndDate = new DateTime(2026, 12, 31, 0, 0, 0, DateTimeKind.Utc), Amount = 2000 },
            
            // Lesson - general
            new TariffEntity { TariffId = new Guid("1605538b-4e77-4dda-a492-44040dee1ea0"), SubscriptionType = SubscriptionType.Lesson, AttendanceLength = 1, AttendanceCount = 1, DisciplineId = null, StartDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), EndDate = new DateTime(2026, 12, 31, 0, 0, 0, DateTimeKind.Utc), Amount = 9000 },
            new TariffEntity { TariffId = new Guid("1916fc99-d9d4-4b48-a30a-07f2b8271224"), SubscriptionType = SubscriptionType.Lesson, AttendanceLength = 1, AttendanceCount = 4, DisciplineId = null, StartDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), EndDate = new DateTime(2026, 12, 31, 0, 0, 0, DateTimeKind.Utc), Amount = 30000 },
            new TariffEntity { TariffId = new Guid("21a7e82a-b0fd-4e34-8d01-cce816ed34ba"), SubscriptionType = SubscriptionType.Lesson, AttendanceLength = 1, AttendanceCount = 8, DisciplineId = null, StartDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), EndDate = new DateTime(2026, 12, 31, 0, 0, 0, DateTimeKind.Utc), Amount = 54000 },
            new TariffEntity { TariffId = new Guid("24e75034-ebed-44e7-a44c-ea12e33a6767"), SubscriptionType = SubscriptionType.Lesson, AttendanceLength = 1, AttendanceCount = 12, DisciplineId = null, StartDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), EndDate = new DateTime(2026, 12, 31, 0, 0, 0, DateTimeKind.Utc), Amount = 74000 },
            
            // Lesson - with discipline ID 9 (Extreme Vocal)
            new TariffEntity { TariffId = new Guid("2c45b82e-16dc-4bf2-bd78-eb5922cbe3ec"), SubscriptionType = SubscriptionType.Lesson, AttendanceLength = 1, AttendanceCount = 1, DisciplineId = 9, StartDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), EndDate = new DateTime(2026, 12, 31, 0, 0, 0, DateTimeKind.Utc), Amount = 11000 },
            new TariffEntity { TariffId = new Guid("32cfcefc-803e-4922-b358-6a9add488aab"), SubscriptionType = SubscriptionType.Lesson, AttendanceLength = 1, AttendanceCount = 4, DisciplineId = 9, StartDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), EndDate = new DateTime(2026, 12, 31, 0, 0, 0, DateTimeKind.Utc), Amount = 42000 },
            new TariffEntity { TariffId = new Guid("3b5a0821-8663-4e1b-8fcc-b48c03d97185"), SubscriptionType = SubscriptionType.Lesson, AttendanceLength = 1, AttendanceCount = 8, DisciplineId = 9, StartDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), EndDate = new DateTime(2026, 12, 31, 0, 0, 0, DateTimeKind.Utc), Amount = 69000 },
            new TariffEntity { TariffId = new Guid("6ee39316-06f7-4c1b-9167-f35c3dce5f80"), SubscriptionType = SubscriptionType.Lesson, AttendanceLength = 1, AttendanceCount = 12, DisciplineId = 9, StartDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), EndDate = new DateTime(2026, 12, 31, 0, 0, 0, DateTimeKind.Utc), Amount = 96000 },
            
            // GroupLesson
            new TariffEntity { TariffId = new Guid("83a26ae5-6cda-4877-95ab-ddca0b064578"), SubscriptionType = SubscriptionType.GroupLesson, AttendanceLength = 1, AttendanceCount = 4, DisciplineId = null, StartDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), EndDate = new DateTime(2026, 12, 31, 0, 0, 0, DateTimeKind.Utc), Amount = 24000 },
            new TariffEntity { TariffId = new Guid("8d9e7ed0-9dcf-4686-bbf3-744f804d0826"), SubscriptionType = SubscriptionType.GroupLesson, AttendanceLength = 1, AttendanceCount = 8, DisciplineId = null, StartDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), EndDate = new DateTime(2026, 12, 31, 0, 0, 0, DateTimeKind.Utc), Amount = 36000 },
            new TariffEntity { TariffId = new Guid("a57918be-0f1c-41c6-bc2d-add554ac2969"), SubscriptionType = SubscriptionType.GroupLesson, AttendanceLength = 1, AttendanceCount = 4, DisciplineId = 9, StartDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), EndDate = new DateTime(2026, 12, 31, 0, 0, 0, DateTimeKind.Utc), Amount = 35000 },
            new TariffEntity { TariffId = new Guid("adcf2ba3-588c-46d8-bab6-04b3a54c2a1d"), SubscriptionType = SubscriptionType.GroupLesson, AttendanceLength = 1, AttendanceCount = 8, DisciplineId = 9, StartDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), EndDate = new DateTime(2026, 12, 31, 0, 0, 0, DateTimeKind.Utc), Amount = 59000 },
            
            // Rehearsal
            new TariffEntity { TariffId = new Guid("af7b2695-618a-4cf3-b57b-14e6aadc0187"), SubscriptionType = SubscriptionType.Rehearsal, AttendanceLength = 3, AttendanceCount = 4, DisciplineId = null, StartDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), EndDate = new DateTime(2026, 12, 31, 0, 0, 0, DateTimeKind.Utc), Amount = 24000 },
            
            // Rent
            new TariffEntity { TariffId = new Guid("becf83b9-bb78-4130-a654-c17c2f7a869a"), SubscriptionType = SubscriptionType.Rent, AttendanceLength = 1, AttendanceCount = 1, DisciplineId = null, StartDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), EndDate = new DateTime(2026, 12, 31, 0, 0, 0, DateTimeKind.Utc), Amount = 3000 },
            new TariffEntity { TariffId = new Guid("ddf4bb6c-686a-490e-9440-61c5e2e68513"), SubscriptionType = SubscriptionType.Rent, AttendanceLength = 1, AttendanceCount = 4, DisciplineId = null, StartDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), EndDate = new DateTime(2026, 12, 31, 0, 0, 0, DateTimeKind.Utc), Amount = 11000 },
            new TariffEntity { TariffId = new Guid("eaa18e35-d9a5-41b0-946b-7557fdc199a8"), SubscriptionType = SubscriptionType.Rent, AttendanceLength = 1, AttendanceCount = 8, DisciplineId = null, StartDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), EndDate = new DateTime(2026, 12, 31, 0, 0, 0, DateTimeKind.Utc), Amount = 20000 },
            new TariffEntity { TariffId = new Guid("f79be13e-77ae-4728-b1ce-6e922c9b066d"), SubscriptionType = SubscriptionType.Rent, AttendanceLength = 1, AttendanceCount = 12, DisciplineId = null, StartDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), EndDate = new DateTime(2026, 12, 31, 0, 0, 0, DateTimeKind.Utc), Amount = 30000 }
        );

        modelBuilder.Entity<ScheduleEntity>().Property(s => s.StartTime).HasColumnType("time");
        modelBuilder.Entity<ScheduleEntity>().Property(s => s.EndTime).HasColumnType("time");
    }
}
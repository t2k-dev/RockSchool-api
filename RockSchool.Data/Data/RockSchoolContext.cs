using Microsoft.EntityFrameworkCore;
using RockSchool.Data.Entities;

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
            new BranchEntity { BranchId = 1, Name = "На Абая", Phone = "77471237896", Address = "Абая 137" }
        );

        modelBuilder.Entity<RoomEntity>().HasData(
            new RoomEntity { RoomId = 1, BranchId = 1, Name = "Гитарная", Status = 1, IsActive = true },
            new RoomEntity { RoomId = 2, BranchId = 1, Name = "Вокальная", Status = 1, IsActive = true },
            new RoomEntity { RoomId = 4, BranchId = 1, Name = "Барабанная", Status = 1, IsActive = true },
            new RoomEntity { RoomId = 5, BranchId = 1, Name = "Желтая", Status = 1, IsActive = true },
            new RoomEntity { RoomId = 6, BranchId = 1, Name = "Зелёная", Status = 1, IsActive = true }
        );
    }
}
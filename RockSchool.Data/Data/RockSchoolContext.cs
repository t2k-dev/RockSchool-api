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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RoleEntity>().HasData(
            new RoleEntity { RoleId = 1, RoleName = "Admin" },
            new RoleEntity { RoleId = 2, RoleName = "Teacher" },
            new RoleEntity { RoleId = 3, RoleName = "Student" });

        modelBuilder.Entity<DisciplineEntity>().HasData(
            new DisciplineEntity() { Name = "Guitar", IsActive = true, DisciplineId = 1},
            new DisciplineEntity() { Name = "Electric Guitar", IsActive = true, DisciplineId = 2},
            new DisciplineEntity() { Name = "Vocal", IsActive = true, DisciplineId = 5},
            new DisciplineEntity() { Name = "Drums", IsActive = true, DisciplineId = 6}
        );
    }
}
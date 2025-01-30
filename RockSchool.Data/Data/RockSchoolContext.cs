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
    public DbSet<UserEntity?> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RoleEntity>().HasData(
            new RoleEntity { RoleId = 1, RoleName = "Admin" },
            new RoleEntity { RoleId = 2, RoleName = "TeacherEntity" },
            new RoleEntity { RoleId = 3, RoleName = "StudentService" });
    }
}
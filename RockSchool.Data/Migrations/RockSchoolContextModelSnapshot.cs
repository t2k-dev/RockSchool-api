﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RockSchool.Data.Data;

#nullable disable

namespace RockSchool.Data.Migrations
{
    [DbContext(typeof(RockSchoolContext))]
    partial class RockSchoolContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DisciplineEntityTeacherEntity", b =>
                {
                    b.Property<int>("DisciplinesDisciplineId")
                        .HasColumnType("integer");

                    b.Property<Guid>("TeachersTeacherId")
                        .HasColumnType("uuid");

                    b.HasKey("DisciplinesDisciplineId", "TeachersTeacherId");

                    b.HasIndex("TeachersTeacherId");

                    b.ToTable("DisciplineEntityTeacherEntity");
                });

            modelBuilder.Entity("RockSchool.Data.Entities.AttendanceEntity", b =>
                {
                    b.Property<Guid>("AttendanceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("DisciplineId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsGroup")
                        .HasColumnType("boolean");

                    b.Property<int>("RoomId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("StatusReason")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SubscriptionId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TeacherId")
                        .HasColumnType("uuid");

                    b.HasKey("AttendanceId");

                    b.HasIndex("DisciplineId");

                    b.HasIndex("RoomId");

                    b.HasIndex("StudentId");

                    b.HasIndex("SubscriptionId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Attendances");
                });

            modelBuilder.Entity("RockSchool.Data.Entities.BranchEntity", b =>
                {
                    b.Property<int>("BranchId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("BranchId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("BranchId");

                    b.ToTable("Branches");

                    b.HasData(
                        new
                        {
                            BranchId = 1,
                            Address = "Абая 137",
                            Name = "На Абая",
                            Phone = "77471237896"
                        });
                });

            modelBuilder.Entity("RockSchool.Data.Entities.DisciplineEntity", b =>
                {
                    b.Property<int>("DisciplineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("DisciplineId"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("DisciplineId");

                    b.ToTable("Disciplines");

                    b.HasData(
                        new
                        {
                            DisciplineId = 1,
                            IsActive = true,
                            Name = "Guitar"
                        },
                        new
                        {
                            DisciplineId = 2,
                            IsActive = true,
                            Name = "Electric Guitar"
                        },
                        new
                        {
                            DisciplineId = 3,
                            IsActive = true,
                            Name = "Bass Guitar"
                        },
                        new
                        {
                            DisciplineId = 4,
                            IsActive = true,
                            Name = "Ukulele"
                        },
                        new
                        {
                            DisciplineId = 5,
                            IsActive = true,
                            Name = "Vocal"
                        },
                        new
                        {
                            DisciplineId = 6,
                            IsActive = true,
                            Name = "Drums"
                        },
                        new
                        {
                            DisciplineId = 7,
                            IsActive = true,
                            Name = "Piano"
                        },
                        new
                        {
                            DisciplineId = 8,
                            IsActive = true,
                            Name = "Violin"
                        },
                        new
                        {
                            DisciplineId = 9,
                            IsActive = true,
                            Name = "Extreme Vocal"
                        });
                });

            modelBuilder.Entity("RockSchool.Data.Entities.NoteEntity", b =>
                {
                    b.Property<Guid>("NoteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("BranchId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("NoteId");

                    b.HasIndex("BranchId");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("RockSchool.Data.Entities.RoleEntity", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("RoleId"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            RoleId = 1,
                            IsActive = false,
                            RoleName = "Admin"
                        },
                        new
                        {
                            RoleId = 2,
                            IsActive = false,
                            RoleName = "Teacher"
                        },
                        new
                        {
                            RoleId = 3,
                            IsActive = false,
                            RoleName = "Student"
                        });
                });

            modelBuilder.Entity("RockSchool.Data.Entities.RoomEntity", b =>
                {
                    b.Property<int>("RoomId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("RoomId"));

                    b.Property<int>("BranchId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("RoomId");

                    b.HasIndex("BranchId");

                    b.ToTable("Rooms");

                    b.HasData(
                        new
                        {
                            RoomId = 1,
                            BranchId = 1,
                            IsActive = true,
                            Name = "Гитарная",
                            Status = 1
                        },
                        new
                        {
                            RoomId = 2,
                            BranchId = 1,
                            IsActive = true,
                            Name = "Вокальная",
                            Status = 1
                        },
                        new
                        {
                            RoomId = 4,
                            BranchId = 1,
                            IsActive = true,
                            Name = "Барабанная",
                            Status = 1
                        },
                        new
                        {
                            RoomId = 5,
                            BranchId = 1,
                            IsActive = true,
                            Name = "Желтая",
                            Status = 1
                        },
                        new
                        {
                            RoomId = 6,
                            BranchId = 1,
                            IsActive = true,
                            Name = "Зелёная",
                            Status = 1
                        });
                });

            modelBuilder.Entity("RockSchool.Data.Entities.ScheduleEntity", b =>
                {
                    b.Property<Guid>("ScheduleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("SubscriptionId")
                        .HasColumnType("uuid");

                    b.Property<int>("WeekDay")
                        .HasColumnType("integer");

                    b.HasKey("ScheduleId");

                    b.HasIndex("SubscriptionId");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("RockSchool.Data.Entities.StudentEntity", b =>
                {
                    b.Property<Guid>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("BranchId")
                        .HasColumnType("integer");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<int?>("Level")
                        .HasColumnType("integer");

                    b.Property<long>("Phone")
                        .HasColumnType("bigint");

                    b.Property<short>("Sex")
                        .HasColumnType("smallint");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("StudentId");

                    b.HasIndex("BranchId");

                    b.HasIndex("UserId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("RockSchool.Data.Entities.SubscriptionEntity", b =>
                {
                    b.Property<Guid>("SubscriptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int?>("AttendanceCount")
                        .HasColumnType("integer");

                    b.Property<int?>("AttendanceLength")
                        .HasColumnType("integer");

                    b.Property<int>("BranchId")
                        .HasColumnType("integer");

                    b.Property<int>("DisciplineId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsGroup")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TeacherId")
                        .HasColumnType("uuid");

                    b.Property<int?>("TransactionId")
                        .HasColumnType("integer");

                    b.Property<int?>("TrialStatus")
                        .HasColumnType("integer");

                    b.HasKey("SubscriptionId");

                    b.HasIndex("BranchId");

                    b.HasIndex("DisciplineId");

                    b.HasIndex("StudentId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("RockSchool.Data.Entities.TeacherDisciplineEntity", b =>
                {
                    b.Property<Guid>("TeacherDisciplineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("DisciplineId")
                        .HasColumnType("integer");

                    b.Property<Guid>("TeacherId")
                        .HasColumnType("uuid");

                    b.HasKey("TeacherDisciplineId");

                    b.HasIndex("DisciplineId");

                    b.HasIndex("TeacherId");

                    b.ToTable("TeacherDisciplines");
                });

            modelBuilder.Entity("RockSchool.Data.Entities.TeacherEntity", b =>
                {
                    b.Property<Guid>("TeacherId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AgeLimit")
                        .HasColumnType("integer");

                    b.Property<bool>("AllowGroupLessons")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("BranchId")
                        .HasColumnType("integer");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<long>("Phone")
                        .HasColumnType("bigint");

                    b.Property<int>("Sex")
                        .HasColumnType("integer");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("TeacherId");

                    b.HasIndex("BranchId");

                    b.HasIndex("UserId");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("RockSchool.Data.Entities.UserEntity", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RockSchool.Data.Entities.WorkingPeriodEntity", b =>
                {
                    b.Property<Guid>("WorkingPeriodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("interval");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("interval");

                    b.Property<Guid>("TeacherId")
                        .HasColumnType("uuid");

                    b.Property<int>("WeekDay")
                        .HasColumnType("integer");

                    b.HasKey("WorkingPeriodId");

                    b.HasIndex("TeacherId");

                    b.ToTable("WorkingPeriods");
                });

            modelBuilder.Entity("DisciplineEntityTeacherEntity", b =>
                {
                    b.HasOne("RockSchool.Data.Entities.DisciplineEntity", null)
                        .WithMany()
                        .HasForeignKey("DisciplinesDisciplineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RockSchool.Data.Entities.TeacherEntity", null)
                        .WithMany()
                        .HasForeignKey("TeachersTeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RockSchool.Data.Entities.AttendanceEntity", b =>
                {
                    b.HasOne("RockSchool.Data.Entities.DisciplineEntity", "Discipline")
                        .WithMany()
                        .HasForeignKey("DisciplineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RockSchool.Data.Entities.RoomEntity", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RockSchool.Data.Entities.StudentEntity", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RockSchool.Data.Entities.SubscriptionEntity", "Subscription")
                        .WithMany()
                        .HasForeignKey("SubscriptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RockSchool.Data.Entities.TeacherEntity", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Discipline");

                    b.Navigation("Room");

                    b.Navigation("Student");

                    b.Navigation("Subscription");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("RockSchool.Data.Entities.NoteEntity", b =>
                {
                    b.HasOne("RockSchool.Data.Entities.BranchEntity", "Branch")
                        .WithMany()
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");
                });

            modelBuilder.Entity("RockSchool.Data.Entities.RoomEntity", b =>
                {
                    b.HasOne("RockSchool.Data.Entities.BranchEntity", "Branch")
                        .WithMany("Rooms")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");
                });

            modelBuilder.Entity("RockSchool.Data.Entities.ScheduleEntity", b =>
                {
                    b.HasOne("RockSchool.Data.Entities.SubscriptionEntity", "Subscription")
                        .WithMany()
                        .HasForeignKey("SubscriptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subscription");
                });

            modelBuilder.Entity("RockSchool.Data.Entities.StudentEntity", b =>
                {
                    b.HasOne("RockSchool.Data.Entities.BranchEntity", "Branch")
                        .WithMany()
                        .HasForeignKey("BranchId");

                    b.HasOne("RockSchool.Data.Entities.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Branch");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RockSchool.Data.Entities.SubscriptionEntity", b =>
                {
                    b.HasOne("RockSchool.Data.Entities.BranchEntity", "Branch")
                        .WithMany()
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RockSchool.Data.Entities.DisciplineEntity", "Discipline")
                        .WithMany()
                        .HasForeignKey("DisciplineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RockSchool.Data.Entities.StudentEntity", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RockSchool.Data.Entities.TeacherEntity", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");

                    b.Navigation("Discipline");

                    b.Navigation("Student");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("RockSchool.Data.Entities.TeacherDisciplineEntity", b =>
                {
                    b.HasOne("RockSchool.Data.Entities.DisciplineEntity", "Discipline")
                        .WithMany()
                        .HasForeignKey("DisciplineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RockSchool.Data.Entities.TeacherEntity", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Discipline");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("RockSchool.Data.Entities.TeacherEntity", b =>
                {
                    b.HasOne("RockSchool.Data.Entities.BranchEntity", "Branch")
                        .WithMany()
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RockSchool.Data.Entities.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Branch");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RockSchool.Data.Entities.UserEntity", b =>
                {
                    b.HasOne("RockSchool.Data.Entities.RoleEntity", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("RockSchool.Data.Entities.WorkingPeriodEntity", b =>
                {
                    b.HasOne("RockSchool.Data.Entities.TeacherEntity", "Teacher")
                        .WithMany("WorkingPeriods")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("RockSchool.Data.Entities.BranchEntity", b =>
                {
                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("RockSchool.Data.Entities.TeacherEntity", b =>
                {
                    b.Navigation("WorkingPeriods");
                });
#pragma warning restore 612, 618
        }
    }
}

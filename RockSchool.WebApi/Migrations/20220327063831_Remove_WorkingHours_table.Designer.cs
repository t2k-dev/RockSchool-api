﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RockSchool.WebApi.Entities;

namespace RockSchool.WebApi.Migrations
{
    [DbContext(typeof(RockSchoolContext))]
    [Migration("20220327063831_Remove_WorkingHours_table")]
    partial class Remove_WorkingHours_table
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("DisciplineTeacher", b =>
                {
                    b.Property<int>("DisciplinesId")
                        .HasColumnType("integer");

                    b.Property<int>("TeachersTeacherId")
                        .HasColumnType("integer");

                    b.HasKey("DisciplinesId", "TeachersTeacherId");

                    b.HasIndex("TeachersTeacherId");

                    b.ToTable("DisciplineTeacher");
                });

            modelBuilder.Entity("RockSchool.WebApi.Entities.Attendance", b =>
                {
                    b.Property<int>("AttendanceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("BeginDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<int>("Duration")
                        .HasColumnType("integer");

                    b.Property<int?>("RoomId")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<int>("StudentId")
                        .HasColumnType("integer");

                    b.Property<int>("TeacherId")
                        .HasColumnType("integer");

                    b.HasKey("AttendanceId");

                    b.HasIndex("RoomId");

                    b.HasIndex("StudentId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Attendances");
                });

            modelBuilder.Entity("RockSchool.WebApi.Entities.Discipline", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("DisciplineName")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("Disciplines");
                });

            modelBuilder.Entity("RockSchool.WebApi.Entities.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("RoleName")
                        .HasColumnType("text");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            RoleId = 1,
                            RoleName = "Admin"
                        },
                        new
                        {
                            RoleId = 2,
                            RoleName = "Teacher"
                        },
                        new
                        {
                            RoleId = 3,
                            RoleName = "Student"
                        });
                });

            modelBuilder.Entity("RockSchool.WebApi.Entities.Room", b =>
                {
                    b.Property<int>("RoomId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("RoomName")
                        .HasColumnType("text");

                    b.Property<int>("SchoolId")
                        .HasColumnType("integer");

                    b.HasKey("RoomId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("RockSchool.WebApi.Entities.Schedule", b =>
                {
                    b.Property<int>("ScheduleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("DisciplineId")
                        .HasColumnType("integer");

                    b.Property<int>("Duration")
                        .HasColumnType("integer");

                    b.Property<string>("StartTime")
                        .HasColumnType("text");

                    b.Property<int>("StudentId")
                        .HasColumnType("integer");

                    b.Property<int>("WeekDay")
                        .HasColumnType("integer");

                    b.HasKey("ScheduleId");

                    b.HasIndex("DisciplineId");

                    b.HasIndex("StudentId");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("RockSchool.WebApi.Entities.Student", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<long>("Phone")
                        .HasColumnType("bigint");

                    b.Property<short>("Sex")
                        .HasColumnType("smallint");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("StudentId");

                    b.HasIndex("UserId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("RockSchool.WebApi.Entities.Teacher", b =>
                {
                    b.Property<int>("TeacherId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("MiddleName")
                        .HasColumnType("text");

                    b.Property<long>("Phone")
                        .HasColumnType("bigint");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.Property<WorkingHours>("WorkingHours")
                        .HasColumnType("jsonb");

                    b.HasKey("TeacherId");

                    b.HasIndex("UserId");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("RockSchool.WebApi.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Login")
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DisciplineTeacher", b =>
                {
                    b.HasOne("RockSchool.WebApi.Entities.Discipline", null)
                        .WithMany()
                        .HasForeignKey("DisciplinesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RockSchool.WebApi.Entities.Teacher", null)
                        .WithMany()
                        .HasForeignKey("TeachersTeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RockSchool.WebApi.Entities.Attendance", b =>
                {
                    b.HasOne("RockSchool.WebApi.Entities.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId");

                    b.HasOne("RockSchool.WebApi.Entities.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RockSchool.WebApi.Entities.Teacher", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");

                    b.Navigation("Student");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("RockSchool.WebApi.Entities.Schedule", b =>
                {
                    b.HasOne("RockSchool.WebApi.Entities.Discipline", "Discipline")
                        .WithMany()
                        .HasForeignKey("DisciplineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RockSchool.WebApi.Entities.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Discipline");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("RockSchool.WebApi.Entities.Student", b =>
                {
                    b.HasOne("RockSchool.WebApi.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RockSchool.WebApi.Entities.Teacher", b =>
                {
                    b.HasOne("RockSchool.WebApi.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RockSchool.WebApi.Entities.User", b =>
                {
                    b.HasOne("RockSchool.WebApi.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });
#pragma warning restore 612, 618
        }
    }
}
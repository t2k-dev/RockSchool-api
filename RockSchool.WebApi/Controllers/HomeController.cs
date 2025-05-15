using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RockSchool.BL.Services.AttendanceService;
using RockSchool.BL.Services.BranchService;
using RockSchool.BL.Services.NoteService;
using RockSchool.WebApi.Models;

namespace RockSchool.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly INoteService _noteService;
        private readonly IAttendanceService _attendanceService;
        private readonly IBranchService _branchService;

        public HomeController(INoteService noteService, IAttendanceService attendanceService, IBranchService branchService)
        {
            _noteService = noteService;
            _attendanceService = attendanceService;
            _branchService = branchService;
        }

        [EnableCors("MyPolicy")]
        [HttpGet("getHomeScreenDetails/{branchId}")]
        public async Task<ActionResult> Get(int branchId)
        {
            var branch = await _branchService.GetBranchByIdAsync(branchId);

            var notes = await _noteService.GetNotesAsync(branchId);

            var attendances = await _attendanceService.GetByBranchIdAsync(branchId);
            var attendanceInfos = new List<AttendanceInfo>();
            foreach (var attendance in attendances)
            {
                attendanceInfos.Add(new AttendanceInfo
                {
                    AttendanceId = attendance.AttendanceId,
                    SubscriptionId = attendance.SubscriptionId,
                    Status = (int)attendance.Status,
                    StatusReason = attendance.StatusReason,
                    DisciplineId = attendance.DisciplineId,
                    RoomId = attendance.RoomId,
                    StartDate = attendance.StartDate,
                    EndDate = attendance.EndDate,
                    Student = attendance.Student,
                    Teacher = attendance.Teacher,
                    IsTrial = attendance.IsTrial,
                });
            }

            var fakeAttendanceInfos = new[]
            {
                new AttendanceInfo
                {
                    AttendanceId = Guid.NewGuid(),
                    StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 10, 0, 0),
                    EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 11, 0, 0),
                    DisciplineId = 5,
                    Status = 2,
                    RoomId = 4,
                    Student = new
                    {
                        FirstName = "Алексей",
                        LastName = "Кутузов",
                    },
                    Teacher = new
                    {
                        TeacherId = "01958931-da30-780a-aadc-e99ae26bd87f",
                        FirstName = "Варвара",
                    },
                },
                new AttendanceInfo
                {
                    AttendanceId = Guid.NewGuid(),
                    StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 10, 0, 0),
                    EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 11, 0, 0),
                    DisciplineId = 5,
                    Status = 3,
                    RoomId = 1,
                    Student = new
                    {
                        FirstName = "Витали",
                        LastName = "Шабанов",
                    },
                    Teacher = new
                    {
                        TeacherId = "01958931-da30-780a-aadc-e99ae26bd87f",
                        FirstName = "Варвара",
                    },
                },
                new AttendanceInfo
                {
                    AttendanceId = Guid.NewGuid(),
                    StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 10, 0, 0),
                    EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 11, 0, 0),
                    DisciplineId = 5,
                    Status = 2,
                    RoomId = 2,
                    IsTrial = true,
                    Student = new
                    {
                        StudentId = "01960baf-2c2f-7fdb-b621-84eb4a05cc6c",
                        FirstName = "Сеит",
                        LastName = "Тасбулатов",
                    },
                    Teacher = new
                    {
                        TeacherId = "01960b95-a2e0-7d75-8e55-ec81cea904ec",
                        FirstName = "Влад",
                        LastName = "Лобанов",
                    },
                },
                new AttendanceInfo
                {
                    AttendanceId = Guid.NewGuid(),
                    StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 11, 0, 0),
                    EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 0, 0),
                    DisciplineId = 5,
                    Status = 5,
                    RoomId = 3,
                    Student = new
                    {
                        FirstName = "Давид",
                        LastName = "Голенков",
                    },
                    Teacher = new
                    {
                        TeacherId = "01958931-da30-780a-aadc-e99ae26bd87f",
                        FirstName = "Варвара",
                    },
                },
                new AttendanceInfo
                {
                    AttendanceId = Guid.NewGuid(),
                    StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 15, 0, 0),
                    EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 16, 0, 0),
                    DisciplineId = 1,
                    Status = 1,
                    RoomId = 3,
                    Student = new
                    {
                        FirstName = "Назар",
                        LastName = "Рахимжанов",
                    },
                    Teacher = new
                    {
                        TeacherId = "0195d5c2-1cda-7136-987a-c4e591b59a78",
                        FirstName = "Оспан",
                    },
                },
                new AttendanceInfo
                {
                    AttendanceId = Guid.NewGuid(),
                    StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 16, 0, 0),
                    EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 0, 0),
                    DisciplineId = 1,
                    Status = 4,
                    RoomId = 3,
                    Student = new
                    {
                        FirstName = "Джамиля",
                        LastName = "Нургалиева",
                    },
                    Teacher = new
                    {
                        TeacherId = "0195d5c2-1cda-7136-987a-c4e591b59a78",
                        FirstName = "Оспан",
                    },
                },
            };

            //tmp:
            //attendanceInfos.AddRange(fakeAttendanceInfos);

            var result = new HomeScreenDetails
            {
                Branch = branch,
                Attendances = attendanceInfos.ToArray(),
                Notes = notes,
            };


            return Ok(result);
        }
    }
}

using System;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RockSchool.BL.Dtos;
using RockSchool.WebApi.Models.Subscriptions;
using System.Threading.Tasks;
using RockSchool.BL.Services.AttendanceService;
using RockSchool.BL.Services.ScheduleService;
using RockSchool.BL.Services.StudentService;
using RockSchool.BL.Services.SubscriptionService;
using RockSchool.Data.Enums;

namespace RockSchool.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly ISubscriptionService _subscriptionService;
        private readonly IAttendanceService _attendanceService;
        private readonly IScheduleService _scheduleService;

        public SubscriptionController(IStudentService studentService, ISubscriptionService subscriptionService, IAttendanceService attendanceService,
            IScheduleService scheduleService)
        {
            _studentService = studentService;
            _subscriptionService = subscriptionService;
            _attendanceService = attendanceService;
            _scheduleService = scheduleService;
        }

        [EnableCors("MyPolicy")]
        [HttpPost("addTrial")]
        public async Task<ActionResult> AddTrial(AddTrialRequest request)
        {
            var studentDto = new StudentDto
            {
                FirstName = request.Student.FirstName,
                LastName = request.Student.LastName,
                BirthDate = DateTime.Now.AddDays(-20).ToUniversalTime(),
                Phone = request.Student.Phone.Value,
                BranchId = request.BranchId,
                Level = request.Student.Level,
            };

            var newStudentId = await _studentService.AddStudentAsync(studentDto);

            var subscriptionDto = new SubscriptionDto()
            {
                DisciplineId = request.DisciplineId,
                StudentId = newStudentId,
                AttendanceCount = 1,
                AttendanceLength = 1,
                BranchId = request.BranchId,
                IsGroup = false,
                StartDate = request.TrialDate,
                TrialStatus = (int)TrialStatus.Created,
                TransactionId = null,
                Status = (int)SubscriptionStatus.Active,
                TeacherId = request.TeacherId
            };

            var subscriptionId = await _subscriptionService.AddSubscriptionAsync(subscriptionDto);

            var trialAttendance = new AttendanceDto()
            {
                StartDate = request.TrialDate,
                EndDate = request.TrialDate.AddHours(1),
                RoomId = 1,
                Comment = string.Empty,
                DisciplineId = request.DisciplineId,
                IsGroup = false,
                NumberOfAttendances = 1,
                Status = AttendanceStatus.New,
                StatusReason = string.Empty,
                StudentId = newStudentId,
                TeacherId = request.TeacherId,
                SubscriptionId = subscriptionId
            };

            var attendanceId = await _attendanceService.AddTrialAttendanceAsync(trialAttendance);

            return Ok(newStudentId);
        }

        [EnableCors("MyPolicy")]
        [HttpPost]
        public async Task<ActionResult> AddSubscription(AddSubscriptionRequestDto request)
        {
            var subscriptionDto = new SubscriptionDto()
            {
                TeacherId = request.TeacherId,
                DisciplineId = request.DisciplineId,
                StartDate = request.StartDate,
                StudentId = request.StudentId,
                AttendanceCount = 1,
                AttendanceLength = 1,
                BranchId = request.BranchId,
                IsGroup = false,
                TransactionId = null,
                TrialStatus = null,
                Status = (int)SubscriptionStatus.Active
            };

            var newSubscriptionId = await _subscriptionService.AddSubscriptionAsync(subscriptionDto);

            await _scheduleService.AddScheduleAsync(request.Schedule);

            return Ok(newSubscriptionId);
        }
    }
}
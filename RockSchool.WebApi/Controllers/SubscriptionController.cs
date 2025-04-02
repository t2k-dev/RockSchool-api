using System;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RockSchool.BL.Dtos;
using RockSchool.WebApi.Models.Subscriptions;
using System.Threading.Tasks;
using RockSchool.BL.Services.AttendanceService;
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

        public SubscriptionController(IStudentService studentService, ISubscriptionService subscriptionService, IAttendanceService attendanceService)
        {
            _studentService = studentService;
            _subscriptionService = subscriptionService;
            _attendanceService = attendanceService;
        }

        [EnableCors("MyPolicy")]
        [HttpPost("addTrial")]
        public async Task<ActionResult> AddTrial(AddTrialRequest request)
        {
            // Add student
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

            // Add subscription
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

            var subscriptionId = await _subscriptionService.AddAsync(subscriptionDto);

            // Add attendance
            var trialAttendance = new AttendanceDto()
            {
                StartDate = request.TrialDate,
                EndDate = request.TrialDate.AddHours(1),
                RoomId = 1,
                Comment = "Hello",
                DisciplineId = request.DisciplineId,
                IsGroup = false,
                NumberOfAttendances = 1,
                Status = AttendanceStatus.New,
                StatusReason = "no reason",
                StudentId = newStudentId,
                TeacherId = request.TeacherId,
                SubscriptionId = subscriptionId
            };

            var attendanceId = await _attendanceService.AddTrialAttendanceAsync(trialAttendance);

            return Ok(newStudentId);
        }

    }
}

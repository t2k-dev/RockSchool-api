using System;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RockSchool.BL.Dtos;
using RockSchool.WebApi.Models;
using RockSchool.WebApi.Models.Subscriptions;
using System.Threading.Tasks;
using RockSchool.BL.Services.AttendanceService;
using RockSchool.BL.Services.StudentService;
using RockSchool.BL.Services.SubscriptionService;

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
                DisciplineId = request.Discipline,
            };

            var subscription = _subscriptionService.AddAsync(subscriptionDto);

            // Add attendance
            var trialAttendance = new AttendanceDto()
            {
                StartDate = request.TrialDate,
                EndDate = request.TrialDate.AddHours(1),
                RoomId = 1,
            };

            _attendanceService.AddTrialAttendance(trialAttendance);


            return Ok(newStudentId);
        }

    }
}

using System;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RockSchool.BL.Dtos;
using RockSchool.WebApi.Models.Subscriptions;
using System.Threading.Tasks;
using RockSchool.BL.Services.AttendanceService;
using RockSchool.BL.Services.NoteService;
using RockSchool.BL.Services.ScheduleService;
using RockSchool.BL.Services.StudentService;
using RockSchool.BL.Services.SubscriptionService;
using RockSchool.Data.Enums;
using RockSchool.WebApi.Models;

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
        private readonly INoteService _noteService;
        private readonly IReschedulingService _reschedulingService;
        

        public SubscriptionController(IStudentService studentService, ISubscriptionService subscriptionService, IAttendanceService attendanceService,
            IScheduleService scheduleService, INoteService noteService, IReschedulingService reschedulingService)
        {
            _studentService = studentService;
            _subscriptionService = subscriptionService;
            _attendanceService = attendanceService;
            _scheduleService = scheduleService;
            _noteService = noteService;
            _reschedulingService = reschedulingService;
        }

        [EnableCors("MyPolicy")]
        [HttpGet("{id}/getNextAvailableSlot")]
        public async Task<ActionResult> GetNextAvailableSlot(Guid id)
        {
            var nextAttendanceDate = await _subscriptionService.GetNextAvailableSlotAsync(id);
            return Ok(nextAttendanceDate);
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
            var student = await _studentService.GetByIdAsync(newStudentId);
            var trialRequest = new TrialRequestDto
            {
                RoomId = request.RoomId,
                BranchId = request.BranchId,
                DisciplineId = request.DisciplineId,
                TeacherId = request.TeacherId,
                TrialDate = request.TrialDate,
                Student = student,
            };

            await _subscriptionService.AddTrialSubscriptionAsync(trialRequest);

            return Ok(newStudentId);
        }

        [EnableCors("MyPolicy")]
        [HttpPost("addTrial2")]
        public async Task<ActionResult> AddTrial2(AddTrialRequest request)
        {
            var student = await _studentService.GetByIdAsync(request.Student.StudentId);
            var trialRequest = new TrialRequestDto
            {
                RoomId = request.RoomId,
                BranchId = request.BranchId,
                DisciplineId = request.DisciplineId,
                TeacherId = request.TeacherId,
                TrialDate = request.TrialDate,
                Student = student,
            };

            await _subscriptionService.AddTrialSubscriptionAsync(trialRequest);

            return Ok(request.Student.StudentId);
        }

        [EnableCors("MyPolicy")]
        [HttpPost]
        public async Task<ActionResult> AddSubscription(AddSubscriptionRequestDto request)
        {
            var subscriptionDto = new SubscriptionDto
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

        [EnableCors("MyPolicy")]
        [HttpPost("rescheduleAttendance")]
        public async Task<ActionResult> RescheduleAttendance(RescheduleAttendanceRequestDto request)
        {
            var attendance = await _reschedulingService.RescheduleAttendanceByStudent(request.AttendanceId, request.NewStartDate);

            return Ok(attendance);
        }

    }
}
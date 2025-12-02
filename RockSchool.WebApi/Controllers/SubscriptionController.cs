using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RockSchool.BL.Dtos;
using RockSchool.BL.Services.AttendanceService;
using RockSchool.BL.Services.NoteService;
using RockSchool.BL.Services.ScheduleService;
using RockSchool.BL.Services.StudentService;
using RockSchool.BL.Services.SubscriptionService;
using RockSchool.BL.Services.TeacherService;
using RockSchool.Data.Enums;
using RockSchool.WebApi.Models;
using RockSchool.WebApi.Models.Students;
using RockSchool.WebApi.Models.Subscriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RockSchool.WebApi.Controllers
{
    [EnableCors("MyPolicy")]
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionController : Controller
    {
        private readonly ITeacherService _teacherService;
        private readonly IStudentService _studentService;
        private readonly ISubscriptionService _subscriptionService;
        private readonly IAttendanceService _attendanceService;
        private readonly IScheduleService _scheduleService;
        private readonly INoteService _noteService;
        private readonly IReschedulingService _reschedulingService;
        

        public SubscriptionController(IStudentService studentService, ISubscriptionService subscriptionService, IAttendanceService attendanceService,
            IScheduleService scheduleService, INoteService noteService, IReschedulingService reschedulingService, ITeacherService teacherService)
        {
            _studentService = studentService;
            _subscriptionService = subscriptionService;
            _attendanceService = attendanceService;
            _scheduleService = scheduleService;
            _noteService = noteService;
            _reschedulingService = reschedulingService;
            _teacherService = teacherService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            var subscription = await _subscriptionService.GetAsync(id);
            if (subscription == null)
            {
                return NotFound();
            }

            var schedules = await _scheduleService.GetAllBySubscriptionIdAsync(id);
            var scheduleInfos = schedules?.Select(schedule => new ScheduleInfo
                {
                    ScheduleId = schedule.ScheduleId,
                    SubscriptionId = schedule.SubscriptionId,
                    RoomId = schedule.RoomId,
                    WeekDay = schedule.WeekDay,
                    StartTime = schedule.StartTime.ToString(@"hh\:mm"),
                    EndTime = schedule.EndTime.ToString(@"hh\:mm"),
                })
                .ToArray();

            var response = new SubscriptionInfo
            {
                SubscriptionId = subscription.SubscriptionId,
                AttendanceCount = subscription.AttendanceCount,
                AttendanceLength = subscription.AttendanceLength,
                DisciplineId = subscription.DisciplineId,
                Status = subscription.Status,
                StartDate = subscription.StartDate,
                //IsTrial = 
                StudentId = subscription.StudentId,
                TeacherId = subscription.TeacherId,
                Schedules = scheduleInfos,
            };

            return Ok(response);
        }

        [HttpGet("{id}/form-data")]
        public async Task<ActionResult> GetSubscriptionFormData(Guid id)
        {
            var subscription = await _subscriptionService.GetAsync(id);
            if (subscription == null)
            {
                return NotFound();
            }

            var groupId = subscription.GroupId;

            var students = new List<StudentDto>();
            if (groupId != null)
            {
                var subscriptions = await _subscriptionService.GetSubscriptionByGroupIdAsync(groupId.Value);
                foreach (var groupSubscription in subscriptions)
                {
                    var student = await _studentService.GetByIdAsync(groupSubscription.StudentId);
                    students.Add(student);
                }
            }
            else
            {
                var student = await _studentService.GetByIdAsync(subscription.StudentId);
                students.Add(student);
            }

            var schedules = await _scheduleService.GetAllBySubscriptionIdAsync(id);
            var scheduleInfos = schedules?.Select(schedule => new ScheduleInfo
                {
                    ScheduleId = schedule.ScheduleId,
                    SubscriptionId = schedule.SubscriptionId,
                    RoomId = schedule.RoomId,
                    WeekDay = schedule.WeekDay,
                    StartTime = schedule.StartTime.ToString(@"hh\:mm"),
                    EndTime = schedule.EndTime.ToString(@"hh\:mm"),
                })
                .ToArray();

            var teacher = await _teacherService.GetTeacherByIdAsync(subscription.TeacherId);
            var studentInfos = students.Select(s => new StudentInfo
            {
                StudentId = s.StudentId, 
                FirstName = s.FirstName, 
                LastName = s.LastName
            }).ToArray();

            var response = new 
            {
                Subscription = new SubscriptionInfo{
                    SubscriptionId = subscription.SubscriptionId,
                    AttendanceCount = subscription.AttendanceCount,
                    AttendanceLength = subscription.AttendanceLength,
                    DisciplineId = subscription.DisciplineId,
                    Status = subscription.Status,
                    StartDate = subscription.StartDate,
                    Schedules = scheduleInfos,
                    GroupId = subscription.GroupId,
                },
                Teacher = new
                {
                    teacher.TeacherId,
                    teacher.FirstName,
                    teacher.LastName,
                },
                Students = studentInfos
            };

            return Ok(response);
        }

        [HttpGet("{id}/getNextAvailableSlot")]
        public async Task<ActionResult> GetNextAvailableSlot(Guid id)
        {
            var availableSlot = await _subscriptionService.GetNextAvailableSlotAsync(id);
            return Ok(availableSlot);
        }

        [HttpPost("addTrial")]
        public async Task<ActionResult> AddTrial(AddTrialRequest request)
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

        [HttpPost]
        public async Task<ActionResult> AddSubscription(AddSubscriptionRequest request)
        {
            await _subscriptionService.AddSubscriptionAsync(request.Subscription, request.StudentIds, request.Schedules);
            return Ok();
        }

        [HttpPost("rescheduleAttendance")]
        public async Task<ActionResult> RescheduleAttendance(RescheduleAttendanceRequestDto request)
        {
            var attendance = await _reschedulingService.RescheduleAttendanceByStudent(request.AttendanceId, request.NewStartDate);

            return Ok(attendance);
        }
    }
}
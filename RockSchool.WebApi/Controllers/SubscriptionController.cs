using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RockSchool.BL.Models;
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
using RockSchool.WebApi.Models.Teachers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RockSchool.WebApi.Helpers;

namespace RockSchool.WebApi.Controllers
{
    [EnableCors("MyPolicy")]
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionController(
        IStudentService studentService,
        ISubscriptionService subscriptionService,
        IScheduleService scheduleService,
        INoteService noteService,
        IReschedulingService reschedulingService,
        ITeacherService teacherService,
        ITrialSubscriptionService taxSubscriptionService,
        IPaymentService paymentService)
        : Controller
    {
        private readonly INoteService _noteService = noteService;


        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            var subscription = await subscriptionService.GetAsync(id);
            if (subscription == null)
            {
                return NotFound();
            }

            var schedules = await scheduleService.GetAllBySubscriptionIdAsync(id);
            var scheduleInfos = schedules?.ToInfos().ToArray();

            var response = new SubscriptionInfo
            {
                SubscriptionId = subscription.SubscriptionId,
                AttendanceCount = subscription.AttendanceCount,
                AttendancesLeft = subscription.AttendancesLeft,
                AttendanceLength = subscription.AttendanceLength,
                DisciplineId = subscription.DisciplineId,
                Status = subscription.Status,
                StartDate = subscription.StartDate,
                TrialStatus = subscription.TrialStatus,
                StudentId = subscription.StudentId,
                TeacherId = subscription.TeacherId,
                Schedules = scheduleInfos,
            };

            return Ok(response);
        }

        [HttpGet("{id}/form-data")]
        public async Task<ActionResult> GetSubscriptionFormData(Guid id)
        {
            var subscription = await subscriptionService.GetAsync(id);
            if (subscription == null)
            {
                return NotFound();
            }

            var groupId = subscription.GroupId;

            var students = new List<Student>();
            if (groupId != null)
            {
                var subscriptions = await subscriptionService.GetSubscriptionByGroupIdAsync(groupId.Value);
                foreach (var groupSubscription in subscriptions)
                {
                    var student = await studentService.GetByIdAsync(groupSubscription.StudentId);
                    students.Add(student);
                }
            }
            else
            {
                var student = await studentService.GetByIdAsync(subscription.StudentId);
                students.Add(student);
            }

            var schedules = await scheduleService.GetAllBySubscriptionIdAsync(id);
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

            var teacher = await teacherService.GetTeacherByIdAsync(subscription.TeacherId);
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
                    AttendancesLeft = subscription.AttendancesLeft,
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
            var availableSlot = await subscriptionService.GetNextAvailableSlotAsync(id);
            return Ok(availableSlot);
        }

        [HttpPost("addTrial")]
        public async Task<ActionResult> AddTrial(AddTrialRequest request)
        {
            var student = await studentService.GetByIdAsync(request.Student.StudentId);
            var trialRequest = new TrialRequestDto
            {
                RoomId = request.RoomId,
                BranchId = request.BranchId,
                DisciplineId = request.DisciplineId,
                TeacherId = request.TeacherId,
                TrialDate = request.TrialDate,
                Student = student,
            };

            await taxSubscriptionService.AddTrialSubscription(trialRequest);

            return Ok(request.Student.StudentId);
        }

        [HttpPost]
        public async Task<ActionResult> AddSubscription(AddSubscriptionRequest request)
        {
            await subscriptionService.AddSubscriptionAsync(request.Subscription, request.StudentIds, request.Schedules);
            return Ok();
        }

        [HttpPost("rescheduleAttendance")]
        public async Task<ActionResult> RescheduleAttendance(RescheduleAttendanceRequest request)
        {
            var attendance = await reschedulingService.RescheduleAttendanceByStudent(request.AttendanceId, request.NewStartDate);

            return Ok(attendance);
        }

        [HttpPut("{id}/schedules")]
        public async Task<ActionResult> UpdateSchedules(Guid id, [FromBody] UpdateSchedulesRequest request)
        {
            var newSchedules = request.Schedules.ToModel(id).ToArray();
            await reschedulingService.UpdateSchedules(id, DateTime.Now, newSchedules);

            return Ok();
        }

        [HttpPost("{id}/pay")]
        public async Task<ActionResult> Pay(Guid id, PaymentRequest request)
        {
            var payment = new Payment
            {
                Amount = request.Amount,
                PaidOn = request.PaidOn.ToUniversalTime(),
                PaymentType = (PaymentType)request.PaymentType,
            };

            await paymentService.Pay(id, payment);

            return Ok();
        }
    }
}
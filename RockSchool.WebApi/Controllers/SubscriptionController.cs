using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RockSchool.BL.Services.ScheduleService;
using RockSchool.BL.Services.SubscriptionService;
using RockSchool.BL.Services.SubscriptionDetailsService;
using RockSchool.WebApi.Models;
using RockSchool.WebApi.Models.Students;
using RockSchool.WebApi.Models.Subscriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RockSchool.BL.Subscriptions.Trial;
using RockSchool.WebApi.Helpers;
using RockSchool.Domain.Students;
using RockSchool.Domain.Teachers;
using RockSchool.BL.Students;
using RockSchool.BL.Teachers;
using RockSchool.BL.Subscriptions.Payments;

namespace RockSchool.WebApi.Controllers
{
    [EnableCors("MyPolicy")]
    [ApiController]
    [Route("api/[controller]s")]
    public class SubscriptionController(
        IStudentService studentService,
        ISubscriptionService subscriptionService,
        IScheduleService scheduleService,
        IReschedulingService reschedulingService,
        ITeacherService teacherService,
        IPaymentService paymentService,
        ICancelSubscriptionService cancelSubscriptionService,
        ITrialSubscriptionService trialSubscriptionService,
        ISubscriptionScreenDetailsService subscriptionScreenDetailsService,
        ISubscriptionFormDataService subscriptionFormDataService,
        ISubscriptionGetService subscriptionGetService
        ) : Controller
    {

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            var result = await subscriptionGetService.Query(id);

            var response = new SubscriptionReachInfo
            {
                SubscriptionId = result.Subscription.SubscriptionId,
                AttendanceCount = result.Subscription.AttendanceCount,
                AttendancesLeft = result.Subscription.AttendancesLeft,
                AttendanceLength = result.Subscription.AttendanceLength,
                DisciplineId = result.Subscription.DisciplineId,
                Status = (int)result.Subscription.Status,
                StartDate = result.Subscription.StartDate,
                TrialDecision = result.Subscription.TrialDecision,
                StudentId = result.Subscription.StudentId,
                TeacherId = result.Subscription.TeacherId,
                AmountOutstanding = result.Subscription.AmountOutstanding,
                Price = result.Subscription.Price,
                FinalPrice = result.Subscription.FinalPrice,
                ScheduleSlots = result.ScheduleSlots.ToInfos(),
                Payments = result.Payments.ToInfos(),
            };

            return Ok(response);
        }

        [HttpGet("{id}/form-data")]
        public async Task<ActionResult> GetSubscriptionFormData(Guid id)
        {
            var result = await subscriptionFormDataService.Query(id);

            var scheduleInfos = result.ScheduleSlots.Select(slot => new ScheduleSlotInfo
            {
                ScheduleId = slot.ScheduleId,
                RoomId = slot.RoomId,
                WeekDay = slot.WeekDay,
                StartTime = slot.StartTime.ToString(@"hh\:mm"),
                EndTime = slot.EndTime.ToString(@"hh\:mm"),
            }).ToArray();

            var response = new
            {
                Subscription = new SubscriptionReachInfo
                {
                    SubscriptionId = result.Subscription.SubscriptionId,
                    AttendanceCount = result.Subscription.AttendanceCount,
                    AttendancesLeft = result.Subscription.AttendancesLeft,
                    AttendanceLength = result.Subscription.AttendanceLength,
                    DisciplineId = result.Subscription.DisciplineId,
                    Status = (int)result.Subscription.Status,
                    StartDate = result.Subscription.StartDate,
                    ScheduleSlots = scheduleInfos,
                    GroupId = result.Subscription.GroupId,
                    SubscriptionType = (int)result.Subscription.SubscriptionType,
                    AmountOutstanding = result.Subscription.AmountOutstanding,
                    Price = result.Subscription.Price,
                    FinalPrice = result.Subscription.FinalPrice,
                    Payments = result.Payments?.ToInfos(),
                },
                Teacher = result.Teacher == null
                    ? null
                    : new
                    {
                        result.Teacher.TeacherId,
                        result.Teacher.FirstName,
                        result.Teacher.LastName,
                    },
                Students = result.Students.Select(s => new StudentInfo
                {
                    StudentId = s.StudentId,
                    FirstName = s.FirstName,
                    LastName = s.LastName
                }).ToArray()
            };

            return Ok(response);
        }

        [HttpGet("{id}/getNextAvailableSlot")]
        public async Task<ActionResult> GetNextAvailableSlot(Guid id)
        {
            var availableSlot = await subscriptionService.GetNextAvailableSlotAsync(id);
            return Ok(availableSlot);
        }

        [HttpGet("{id}/screen-data")]
        public async Task<ActionResult> GetSubscriptionScreenData(Guid id)
        {
            var result = await subscriptionScreenDetailsService.Query(id);

            var response = new
            {
                Subscription = new
                {
                    SubscriptionId = result.Subscription.SubscriptionId,
                    AttendanceCount = result.Subscription.AttendanceCount,
                    AttendancesLeft = result.Subscription.AttendancesLeft,
                    AttendanceLength = result.Subscription.AttendanceLength,
                    DisciplineId = result.Subscription.DisciplineId,
                    Status = (int)result.Subscription.Status,
                    StartDate = result.Subscription.StartDate,
                    TrialDecision = result.Subscription.TrialDecision,
                    StudentId = result.Subscription.StudentId,
                    TeacherId = result.Subscription.TeacherId,
                    AmountOutstanding = result.Subscription.AmountOutstanding,
                    Price = result.Subscription.Price,
                    FinalPrice = result.Subscription.FinalPrice,
                    Schedules = result.ScheduleSlots.ToInfos(),
                    GroupId = result.Subscription.GroupId,
                    SubscriptionType = (int)result.Subscription.SubscriptionType,
                    Attendances = result.Attendances.ToInfos(),
                    Student = new StudentInfo
                    {
                        StudentId = result.Student.StudentId,
                        FirstName = result.Student.FirstName,
                        LastName = result.Student.LastName
                    },
                    Teacher = result.Teacher == null
                        ? null
                        : new
                        {
                            result.Teacher.TeacherId,
                            result.Teacher.FirstName,
                            result.Teacher.LastName,
                        },
                    Payments = result.Payments.ToInfos()
                }
            };

            return Ok(response);
        }

        [HttpPost("addTrial")]
        public async Task<ActionResult> AddTrial(AddTrialRequest request)
        {
            var addTrialDto = new AddTrialDto
            {
                RoomId = request.RoomId,
                BranchId = request.BranchId,
                DisciplineId = request.DisciplineId,
                TeacherId = request.TeacherId,
                TrialDate = request.TrialDate,
                TariffId = request.TariffId,
                StudentId = request.Student.StudentId,
            };

            await trialSubscriptionService.AddTrial(addTrialDto);

            return Ok(request.Student.StudentId);
        }

        [HttpPost]
        public async Task<ActionResult> AddSubscription(AddSubscriptionRequest request)
        {
            await subscriptionService.AddSubscriptionAsync(request.Subscription, request.StudentIds, request.Schedules.ToDto());
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
            await paymentService.Pay(id, request.Amount, request.PaymentType, request.PaidOn);

            return Ok();
        }

        [HttpPut("{subscriptionId}/cancel")]
        public async Task<ActionResult> Cancel(Guid subscriptionId, CancelRequest request)
        {
            await cancelSubscriptionService.Cancel(subscriptionId, request.CancelDate, request.CancelReason);

            return Ok();
        }
    }
}
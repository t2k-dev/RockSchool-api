﻿using System;
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
    [EnableCors("MyPolicy")]
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
            var subscription = new SubscriptionDto
            {
                TeacherId = request.TeacherId,
                DisciplineId = request.DisciplineId,
                StartDate = request.StartDate.ToUniversalTime(),
                StudentId = request.StudentId,
                AttendanceCount = request.AttendanceCount,
                AttendanceLength = request.AttendanceLength,
                BranchId = request.BranchId,
                IsGroup = false,
                TransactionId = null,
                TrialStatus = null,
                Status = (int)SubscriptionStatus.Active,
                StatusReason = null,
            };

            var newSubscriptionId = await _subscriptionService.AddSubscriptionAsync(subscription);
            subscription.SubscriptionId = newSubscriptionId;

            foreach (var schedule in request.Schedules)
            {
                schedule.SubscriptionId = newSubscriptionId;
                await _scheduleService.AddScheduleAsync(schedule);
            }

            await _attendanceService.AddAttendancesToStudentAsync(subscription);

            return Ok(newSubscriptionId);
        }

        [HttpPost("rescheduleAttendance")]
        public async Task<ActionResult> RescheduleAttendance(RescheduleAttendanceRequestDto request)
        {
            var attendance = await _reschedulingService.RescheduleAttendanceByStudent(request.AttendanceId, request.NewStartDate);

            return Ok(attendance);
        }
    }
}
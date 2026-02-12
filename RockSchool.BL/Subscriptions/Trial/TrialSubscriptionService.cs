using Azure.Core;
using RockSchool.BL.Models;
using RockSchool.BL.Services.StudentService;
using RockSchool.BL.Services.TariffService;
using RockSchool.Data.Repositories;
using RockSchool.Domain.Entities;
using RockSchool.Domain.Enums;
using RockSchool.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockSchool.BL.Subscriptions.Trial
{
    public class TrialSubscriptionService(
        IStudentRepository studentRepository, 
        ISubscriptionRepository subscriptionRepository,
        IAttendanceRepository attendanceRepository,
        IAttendeeRepository attendeeRepository,
        ITariffService tariffService, 
        IUnitOfWork unitOfWork
        ) : ITrialSubscriptionService
    {
        public async Task AddTrial(AddTrialDto addTrialDto)
        {
            var tariff = await tariffService.GetTariffAsync(addTrialDto.TariffId);
            if (tariff == null)
            {
                throw new InvalidOperationException("Tariff is not found");
            }

            // Add Subscription
            var subscription = Subscription.CreateTrial(
                addTrialDto.StudentId,
                addTrialDto.BranchId,
                DateOnly.FromDateTime(addTrialDto.TrialDate),
                tariff.Amount,
                tariff.Amount,
                addTrialDto.DisciplineId,
                addTrialDto.TeacherId
            );

            await subscriptionRepository.AddSubscriptionAsync(subscription);

            // Add Attendance
            var trialAttendance = Attendance.CreateTrial(
                addTrialDto.TrialDate,
                addTrialDto.TrialDate.AddMinutes(subscription.AttendanceLength),
                addTrialDto.RoomId,
                addTrialDto.BranchId,
                addTrialDto.DisciplineId,
                addTrialDto.TeacherId
            );

            await attendanceRepository.AddAsync(trialAttendance);

            // Add Attendee
            var attendee = Attendee.Create(
                subscription.SubscriptionId,
                trialAttendance.AttendanceId
            );

            attendeeRepository.AddAsync(attendee);

            // Add note
            //await noteService.AddNoteAsync(request.BranchId, $"Пробное занятие {request.Student.FirstName}.", request.TrialDate.ToUniversalTime());
            
            await unitOfWork.SaveChangesAsync();
        }

        public async Task CompleteTrial(Guid subscriptionId, TrialStatus trialStatus, string statusReason)
        {
            var subscriptionEntity = await subscriptionRepository.GetAsync(subscriptionId);
            throw new NotImplementedException();
            /*
            subscriptionEntity.AttendancesLeft -= 1;
            subscriptionEntity.Status = SubscriptionStatus.Completed;
            subscriptionEntity.TrialStatus = trialStatus;
            subscriptionEntity.StatusReason = statusReason;

            await subscriptionRepository.UpdateSubscriptionAsync(subscriptionEntity);*/
        }
    }
}

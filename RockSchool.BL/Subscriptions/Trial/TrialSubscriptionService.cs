using Azure.Core;
using RockSchool.BL.Services.TariffService;
using RockSchool.Data.Repositories;
using RockSchool.Domain.Entities;
using RockSchool.Domain.Enums;
using RockSchool.Domain.Repositories;

namespace RockSchool.BL.Subscriptions.Trial
{
    public class TrialSubscriptionService(
        ISubscriptionRepository subscriptionRepository,
        IAttendanceRepository attendanceRepository,
        IAttendeeRepository attendeeRepository,
        ITariffService tariffService,
        INoteRepository noteRepository,
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

            await subscriptionRepository.AddAsync(subscription);

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
                trialAttendance.AttendanceId,
                addTrialDto.StudentId
            );

            await attendeeRepository.AddAsync(attendee);

            // Add note
            /*var note = Note.Create($"Пробное занятие {addTrialDto.Student.FirstName}.", addTrialDto.TrialDate, addTrialDto.BranchId);
            await noteRepository.AddNoteAsync(note);*/
            
            await unitOfWork.SaveChangesAsync();
        }

        public async Task AcceptTrial(Guid attendanceId, Guid subscriptionId, string statusReason)
        {
            // Update attendance
            var attendance = await attendanceRepository.GetAsync(attendanceId);
            if (attendance == null)
                throw new InvalidOperationException($"Attendance with id {attendanceId} not found");
            attendance.MarkAsAttended(statusReason);

            // Update subscription
            var subscription = await subscriptionRepository.GetAsync(subscriptionId);
            if (subscription == null)
                throw new InvalidOperationException($"Subscription with id {subscriptionId} not found");

            subscription.CompleteTrial(TrialDecision.Positive);

            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeclineTrial(Guid attendanceId, Guid subscriptionId, string? statusReason)
        {
            // Update subscription
            var subscription = await subscriptionRepository.GetAsync(subscriptionId);
            if (subscription == null)
                throw new InvalidOperationException($"Subscription with id {subscriptionId} not found");

            subscription.CompleteTrial(TrialDecision.Negative, statusReason);

            await unitOfWork.SaveChangesAsync();
        }
    }
}

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

        public async Task AcceptTrial(Guid attendanceId, Guid subscriptionId, string statusReason, string comment)
        {
            // Update attendance
            var attendance = await attendanceRepository.GetAsync(attendanceId);
            attendance.MarkAsAttended(statusReason);

            attendanceRepository.Update(attendance);

            // Update subscription
            var subscription = await subscriptionRepository.GetAsync(subscriptionId);

            subscription.CompleteTrial(subscriptionId, TrialDecision.Positive, null);

            subscriptionRepository.Update(subscription);

            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeclineTrial(Guid attendanceId, Guid subscriptionId, string statusReason, string comment)
        {
            // Update attendance
            var attendance = await attendanceRepository.GetAsync(attendanceId);
            attendance.MarkAsAttended(statusReason);

            attendanceRepository.Update(attendance);

            // Update subscription
            var subscription = await subscriptionRepository.GetAsync(subscriptionId);

            subscription.CompleteTrial(subscriptionId, TrialDecision.Negative, null);

            subscriptionRepository.Update(subscription);


            await unitOfWork.SaveChangesAsync();
        }

        public async Task MissedTrial(Guid attendanceId, Guid subscriptionId, string statusReason, string comment)
        {
            // Update attendance
            var attendance = await attendanceRepository.GetAsync(attendanceId);
            attendance.MarkAsMissed(statusReason);

            attendanceRepository.Update(attendance);

            // Update subscription
            var subscription = await subscriptionRepository.GetAsync(subscriptionId);

            subscription.CompleteTrial(subscriptionId, TrialDecision.Missed, null);

            subscriptionRepository.Update(subscription);

            await unitOfWork.SaveChangesAsync();
        }
    }
}

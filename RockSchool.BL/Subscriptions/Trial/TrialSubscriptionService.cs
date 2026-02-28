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
            //await noteService.AddNoteAsync(request.BranchId, $"Пробное занятие {request.Student.FirstName}.", request.TrialDate.ToUniversalTime());
            
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

            subscription.CompleteTrial(subscriptionId, TrialStatus.Positive, null);

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

            subscription.CompleteTrial(subscriptionId, TrialStatus.Negative, null);

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

            subscription.CompleteTrial(subscriptionId, TrialStatus.Missed, null);

            subscriptionRepository.Update(subscription);

            await unitOfWork.SaveChangesAsync();
        }
    }
}

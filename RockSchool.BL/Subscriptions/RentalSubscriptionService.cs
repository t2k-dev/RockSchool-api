using RockSchool.BL.Helpers;
using RockSchool.BL.Models;
using RockSchool.Domain.Entities;
using RockSchool.Domain.Enums;
using RockSchool.Domain.Repositories;

namespace RockSchool.BL.Subscriptions
{
    public class RentalSubscriptionService(
        IAttendanceRepository attendanceRepository,
        IAttendeeRepository attendeeRepository,
        ISubscriptionRepository subscriptionRepository,
        IScheduleRepository scheduleRepository,
        IUnitOfWork unitOfWork
        ) : IRentalSubscriptionService
    {
        public async Task<Guid> AddRentalSubscription(SubscriptionDetails subscriptionDetails, ScheduleDto[] scheduleDtos)
        {
            // Step 1: Subscription
            var subscription = Subscription.Create(subscriptionDetails.StudentId,
                subscriptionDetails.BranchId,
                subscriptionDetails.StartDate,
                subscriptionDetails.AttendanceCount,
                subscriptionDetails.AttendanceLength,
                SubscriptionType.Rent,
                subscriptionDetails.Price,
                subscriptionDetails.FinalPrice
            );

            await subscriptionRepository.AddAsync(subscription);

            // Step 2: Create Schedules
            foreach (var scheduleDto in scheduleDtos)
            {
                var schedule = Schedule.Create(
                    scheduleDto.RoomId,
                    scheduleDto.WeekDay,
                    scheduleDto.StartTime,
                    scheduleDto.EndTime,
                    subscription.SubscriptionId
                );

                await scheduleRepository.AddAsync(schedule);
            }

            // Step 3: Attendances
            var attendances = ScheduleHelper.GenerateAttendances(subscriptionDetails, scheduleDtos, false, subscription.SubscriptionId);
            foreach (var attendance in attendances)
            {
                await attendanceRepository.AddAsync(attendance);
            }

            // Step 4: Attendee
            foreach (var attendance in attendances)
            {
                var attendee = Attendee.Create(subscription.SubscriptionId, attendance.AttendanceId, subscriptionDetails.StudentId);

                await attendeeRepository.AddAsync(attendee);
            }

            await unitOfWork.SaveChangesAsync();

            return subscription.SubscriptionId;
        }
    }
}

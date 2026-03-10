using RockSchool.BL.Attendances;
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
        public async Task<Guid> AddRentalSubscription(SubscriptionDetails subscriptionDetails, ScheduleSlotDto[] scheduleDtos)
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

            // Step 2: Create a Schedule for this rental subscription with all the time slots
            if (scheduleDtos.Length > 0)
            {
                var schedule = Schedule.Create($"{subscription.StartDate:yyyy-MM-dd} - Rental Schedule");
                
                foreach (var scheduleDto in scheduleDtos)
                {
                    var slot = ScheduleSlot.Create(
                        schedule.ScheduleId,
                        scheduleDto.RoomId,
                        scheduleDto.WeekDay,
                        scheduleDto.StartTime,
                        scheduleDto.EndTime
                    );
                    schedule.AddScheduleSlot(slot);
                }
                
                await scheduleRepository.AddAsync(schedule);
                
                // Update subscription to reference the schedule
                subscription.AssignSchedule(schedule.ScheduleId);
                subscriptionRepository.Update(subscription);
            }

            // Step 3: Attendances
            var attendances = AttendanceScheduleHelper.Generate(
                subscriptionDetails.AttendanceCount,
                subscriptionDetails.AttendanceLength,
                subscriptionDetails.StartDate,
                subscriptionDetails.BranchId,
                subscriptionDetails.DisciplineId,
                subscriptionDetails.TeacherId,
                scheduleDtos, 
                AttendanceType.Rent
                );

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

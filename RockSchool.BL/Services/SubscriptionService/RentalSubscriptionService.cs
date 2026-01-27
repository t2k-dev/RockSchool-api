using RockSchool.BL.Helpers;
using RockSchool.BL.Models;
using RockSchool.BL.Services.AttendanceService;
using RockSchool.BL.Services.ScheduleService;
using RockSchool.Data.Enums;

namespace RockSchool.BL.Services.SubscriptionService
{
    public class RentalSubscriptionService(
        ISubscriptionService subscriptionService, 
        IScheduleService scheduleService,
        IAttendanceService attendanceService
        ) : IRentalSubscriptionService
    {
        public async Task<Guid> AddRentalSubscription(SubscriptionDetails details, Schedule[] schedules)
        {
            // Subscription
            var subscription = new Subscription
            {
                SubscriptionType = SubscriptionType.Rent,
                Status = SubscriptionStatus.Draft,
                StudentId = details.StudentId,
                AttendanceCount = details.AttendanceCount,
                AttendanceLength = details.AttendanceLength,
                AttendancesLeft = details.AttendanceCount,
                BranchId = details.BranchId,
                GroupId = null,
                StartDate = details.StartDate,
                
                TariffId = details.TariffId,
                Price = details.Price,
                FinalPrice = details.FinalPrice,
                AmountOutstanding = details.FinalPrice,
            };

            var newSubscriptionId = await subscriptionService.AddSubscriptionAsync(subscription);

            // Schedules
            foreach (var schedule in schedules)
            {
                schedule.SubscriptionId = newSubscriptionId;
                await scheduleService.AddScheduleAsync(schedule);
            }

            // Attendances
            var attendances = ScheduleHelper.GenerateAttendances(details, schedules, false, newSubscriptionId);
            await attendanceService.AddAttendancesAsync(attendances.ToArray());

            return newSubscriptionId;
        }
    }
}

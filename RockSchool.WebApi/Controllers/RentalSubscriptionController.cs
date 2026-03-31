using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RockSchool.BL.Models;
using RockSchool.BL.Subscriptions;
using RockSchool.WebApi.Models.Subscriptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RockSchool.WebApi.Controllers
{
    // [EnableCors("MyPolicy")]
    [ApiController]
    [Route("api/[controller]")]
    public class RentalSubscriptionController(IRentalSubscriptionService rentalSubscriptionService) : Controller
    {
        [HttpPost]
        public async Task<ActionResult> Add(AddRentalSubscriptionRequest request)
        {
            var scheduleDtos = new List<ScheduleSlotDto>();
            foreach (var requestSchedule in request.Schedules)
            {
                var scheduleDto = new ScheduleSlotDto
                {
                    RoomId = requestSchedule.RoomId,
                    WeekDay = requestSchedule.WeekDay,
                    StartTime = TimeSpan.Parse(requestSchedule.StartTime),
                    EndTime = TimeSpan.Parse(requestSchedule.EndTime),
                };

                scheduleDtos.Add(scheduleDto);
            }

            await rentalSubscriptionService.AddRentalSubscription(request.SubscriptionDetails, scheduleDtos.ToArray());

            return Ok();
        }
    }
}

using System;

namespace RockSchool.WebApi.Models.Subscriptions
{
    public class RescheduleAttendanceRequestDto
    {
        public Guid AttendanceId { get; set; }
        public DateTime NewStartDate { get; set; }

    }
}

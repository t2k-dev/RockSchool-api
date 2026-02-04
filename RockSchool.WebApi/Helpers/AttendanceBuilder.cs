using RockSchool.WebApi.Models.Attendances;
using System.Collections.Generic;
using System.Linq;
using RockSchool.BL.Models;
using RockSchool.Domain.Entities;

namespace RockSchool.WebApi.Helpers
{
    public class AttendanceBuilder
    {
        public static List<ParentAttendanceInfo> BuildGroupAttendanceInfos(IEnumerable<Attendance> attendances)
        {
            var parentAttendances = new List<ParentAttendanceInfo>();

            var groupIds = attendances.Select(a => a.GroupId).Distinct();

            foreach (var groupId in groupIds)
            {
                var currentGroupAttendances = attendances.Where(a => a.GroupId == groupId);
                var parentAttendance = currentGroupAttendances.First().ToParentAttendanceInfo();
                parentAttendance.ChildAttendances = currentGroupAttendances.ToInfos();

                parentAttendances.Add(parentAttendance);
            }

            return parentAttendances;
        }
    }
}

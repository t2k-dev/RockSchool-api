using RockSchool.BL.Dtos;
using RockSchool.WebApi.Models.Attendances;
using System.Collections.Generic;
using System.Linq;

namespace RockSchool.WebApi.Helpers
{
    public class AttendanceBuilder
    {
        public static List<ParentAttendanceInfo> BuildGroupAttendanceInfos(IEnumerable<AttendanceDto> attendances)
        {
            var parentAttendances = new List<ParentAttendanceInfo>();

            var groupIds = attendances.Select(a => a.GroupId).Distinct();

            foreach (var groupId in groupIds)
            {
                var currentGroupAttendances = attendances.Where(a => a.GroupId == groupId);
                var parentAttendance = currentGroupAttendances.First().ToParentAttendanceInfo();
                parentAttendance.ChildAttendances = currentGroupAttendances.ToAttendanceInfos().ToArray();

                parentAttendances.Add(parentAttendance);
            }

            return parentAttendances;
        }
    }
}

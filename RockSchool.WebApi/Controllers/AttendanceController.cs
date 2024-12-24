using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RockSchool.WebApi.Entities;
using RockSchool.WebApi.Helpers;
using RockSchool.WebApi.Models;

namespace RockSchool.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : MyBaseController
    {
        public AttendanceController(RockSchoolContext rockSchoolContext, IMapper mapper)
            : base(rockSchoolContext, mapper)
        {
        }

        [HttpGet]
        public ActionResult Get()
        {
            var attendances = _context.Attendances.ToList();

            if (attendances == null)
            {
                return NotFound();
            }

            return Ok(attendances);
        }

        [HttpPost("addLessons")]
        public ActionResult AddForStudent(AddAttendancesDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var schedules = _context.Schedules.Where(s => s.StudentId == model.StudentId).ToList();

            var startDate = model.StartingDate;
            var attendancesToAdd = model.NumberOfAttendances;

            var newAttendances = new List<Attendance>();

            while (attendancesToAdd > 0)
            {
                foreach (var item in schedules)
                {
                    var beginDate = ScheduleHelper.GetNextWeekday(startDate, item.WeekDay);

                    var attendance = new Attendance()
                    {
                        StudentId = model.StudentId,
                        TeacherId = model.TeacherId,
                        Status = 1,
                        Duration = item.Duration,
                        BeginDate = beginDate
                    };

                    newAttendances.Add(attendance);

                    attendancesToAdd--;
                }

                startDate = startDate.AddDays(7);
            }
            _context.Attendances.AddRange(newAttendances.ToArray());
            _context.SaveChanges();

            return Ok();
        }
    }
}

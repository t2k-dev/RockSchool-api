using RockSchool.Data.Entities;

namespace RockSchool.BL.Dtos.Service.Responses;

public class AttendanceDto
{
    public int AttendanceId { get; set; }
    public int StudentId { get; set; }
    public StudentEntity StudentEntity { get; set; }
    public int TeacherId { get; set; }
    public TeacherEntity TeacherEntity { get; set; }
    public DateTime BeginDate { get; set; }
    public int Status { get; set; }
    public int? RoomId { get; set; }
    public RoomEntity RoomEntity { get; set; }
    public int Duration { get; set; }
    public string Comment { get; set; }
}
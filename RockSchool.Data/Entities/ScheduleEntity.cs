namespace RockSchool.Data.Entities
{
    public class ScheduleEntity
    {
        public int ScheduleId { get; set; }
        public int StudentId { get; set; }
        public StudentEntity StudentEntity { get; set; }
        public int WeekDay { get; set; }
        public string StartTime { get; set; }
        public int Duration { get; set; }
        public int DisciplineId { get; set; }
        public DisciplineEntity DisciplineEntity { get; set; }
    }
}

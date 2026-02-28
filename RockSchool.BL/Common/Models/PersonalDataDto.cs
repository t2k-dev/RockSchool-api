namespace RockSchool.BL.Common.Models
{
    public class PersonalDataDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public short Sex { get; set; }
        public long Phone { get; set; }
        public int? Level { get; set; }
    }
}

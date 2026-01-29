using RockSchool.Data.Enums;

namespace RockSchool.BL.Models.Dtos
{
    public class BandMember
    {
        public Guid StudentId { get; set; }
        public BandRoleId BandRoleId { get; set; }
    }
}

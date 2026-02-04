using RockSchool.Domain.Enums;

namespace RockSchool.BL.Models.Dtos
{
    public class BandMember
    {
        public Guid StudentId { get; set; }
        public BandRoleId BandRoleId { get; set; }
    }
}

using RockSchool.Domain.Enums;

namespace RockSchool.BL.Models.Dtos
{
    public class BandMemberDto
    {
        public Guid StudentId { get; set; }
        public BandRoleId BandRoleId { get; set; }
    }
}

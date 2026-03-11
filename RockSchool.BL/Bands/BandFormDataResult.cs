using RockSchool.Domain.Entities;

namespace RockSchool.BL.Bands;

public class BandFormDataResult
{
    public Band Band { get; set; } = null!;
    public ScheduleSlot[] ScheduleSlots { get; set; } = [];
}

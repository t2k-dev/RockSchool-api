namespace RockSchool.BL.Subscriptions.Rehearsal;

public class AddRehearsalDto
{
    public Guid StudentId { get; set; }
    public Guid BandId { get; set; }
    public Guid TariffId { get; set; }
}

namespace RockSchool.BL.Bands;

public interface IBandScreenDetailsService
{
    Task<BandScreenDetailsResult> Query(Guid bandId);
}

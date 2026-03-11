namespace RockSchool.BL.Bands;

public interface IBandFormDataService
{
    Task<BandFormDataResult> Query(Guid bandId);
}

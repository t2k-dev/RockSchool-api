using RockSchool.Data.Data;
using RockSchool.Data.Entities;

namespace RockSchool.Data.Repositories
{
    public class TenderRepository(RockSchoolContext rockSchoolContext) : BaseRepository(rockSchoolContext)
    {
        public async Task<Guid> AddAsync(TenderEntity tenderEntity)
        {
            await RockSchoolContext.Tenders.AddAsync(tenderEntity);
            await RockSchoolContext.SaveChangesAsync();
            return tenderEntity.TenderId;
        }
    }
}

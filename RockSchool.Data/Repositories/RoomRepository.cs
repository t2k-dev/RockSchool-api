using Microsoft.EntityFrameworkCore;
using RockSchool.Data.Data;
using RockSchool.Data.Entities;

namespace RockSchool.Data.Repositories
{
    public class RoomRepository(RockSchoolContext rockSchoolContext) : BaseRepository(rockSchoolContext)
    {
        public async Task<RoomEntity[]> GetRentableRooms(int branchId)
        {
            return await rockSchoolContext.Rooms
                .Where(r => r.BranchId == branchId && r.CanBeRented)
                .ToArrayAsync();
        }
    }
}

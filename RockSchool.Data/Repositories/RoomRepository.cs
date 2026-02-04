using Microsoft.EntityFrameworkCore;
using RockSchool.Data.Data;
using RockSchool.Domain.Entities;

namespace RockSchool.Data.Repositories
{
    public class RoomRepository(RockSchoolContext rockSchoolContext) : BaseRepository(rockSchoolContext)
    {
        public async Task<Room[]> GetRentableRooms(int branchId)
        {
            return await rockSchoolContext.Rooms
                .Where(r => r.BranchId == branchId && r.SupportsRent)
                .ToArrayAsync();
        }

        public async Task<Room[]> GetRehearsableRooms(int branchId)
        {
            return await rockSchoolContext.Rooms
                .Where(r => r.BranchId == branchId && r.SupportsRehearsal)
                .ToArrayAsync();
        }
    }
}

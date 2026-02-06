using RockSchool.Data.Repositories;
using RockSchool.Domain.Entities;
using RockSchool.Domain.Rooms;

namespace RockSchool.BL.Services.RoomService
{
    public class RoomService(IRoomRepository roomRepository) : IRoomService
    {
        public async Task<Room[]> GetRentableRooms(int branchId)
        {
            return await roomRepository.GetRentableRooms(branchId);
        }

        public async Task<Room[]> GetRehearsableRooms(int branchId)
        {
            return await roomRepository.GetRehearsableRooms(branchId);
        }
    }
}

using RockSchool.BL.Helpers;
using RockSchool.BL.Models;
using RockSchool.Data.Repositories;

namespace RockSchool.BL.Services.RoomService
{
    public class RoomService(RoomRepository roomRepository) : IRoomService
    {
        public async Task<Room[]> GetRentableRooms(int branchId)
        {
            var rooms = await roomRepository.GetRentableRooms(branchId);
            return rooms.ToModel();
        }

        public async Task<Room[]> GetRehearsableRooms(int branchId)
        {
            var rooms = await roomRepository.GetRehearsableRooms(branchId);
            return rooms.ToModel();
        }
    }
}

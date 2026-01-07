using RockSchool.BL.Models;

namespace RockSchool.BL.Services.RoomService
{
    public interface IRoomService
    {
        Task<Room[]> GetRentableRooms(int branchId);
    }
}

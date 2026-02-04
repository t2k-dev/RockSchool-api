using RockSchool.Domain.Entities;

namespace RockSchool.BL.Services.RoomService
{
    public interface IRoomService
    {
        Task<Room[]> GetRentableRooms(int branchId);
        Task<Room[]> GetRehearsableRooms(int branchId);
    }
}

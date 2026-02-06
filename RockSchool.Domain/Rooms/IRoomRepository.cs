using RockSchool.Domain.Entities;

namespace RockSchool.Domain.Rooms
{
    public interface IRoomRepository
    {
        Task<Room[]> GetRentableRooms(int branchId);
        Task<Room[]> GetRehearsableRooms(int branchId);
    }
}

using RockSchool.BL.Models;
using RockSchool.BL.Services.RoomService;
using RockSchool.Domain.Enums;
using RockSchool.Domain.Repositories;

namespace RockSchool.BL.Services.BusySlotsService;

public class BusySlotsService(
    IRoomService roomService,
    IAttendanceRepository attendanceRepository) : IBusySlotsService
{
    public async Task<BusySlotsResultDto[]> GetBusySlotsByBranchAsync(int branchId)
    {
        var result = new List<BusySlotsResultDto>();

        var rentableRooms = await roomService.GetRentableRooms(branchId);
        foreach (var rentableRoom in rentableRooms)
        {
            var attendances = await attendanceRepository.GetByRoomIdAsync(rentableRoom.RoomId);
            
            // Filter out canceled attendances
            attendances = attendances
                .Where(a => a.Status != AttendanceStatus.CanceledByAdmin
                            && a.Status != AttendanceStatus.CanceledByStudent
                            && a.Status != AttendanceStatus.CanceledByTeacher)
                .ToArray();

            result.Add(new BusySlotsResultDto
            {
                RoomId = rentableRoom.RoomId,
                RoomName = rentableRoom.Name,
                Attendances = attendances
            });
        }

        return result.ToArray();
    }
}

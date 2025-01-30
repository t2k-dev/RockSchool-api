using RockSchool.BL.Dtos.Service.Requests.StudentService;
using RockSchool.BL.Dtos.Service.Requests.UserService;
using RockSchool.BL.Dtos.Service.Responses;

namespace RockSchool.BL.Services.UserService;

public interface IUserService
{
    Task<int> AddUserAsync(AddUserServiceRequestDto addUserServiceRequestDto);
    Task<UserDto?> GetUserByIdAsync(int userId);
    Task DeleteUserAsync(int userId);
}
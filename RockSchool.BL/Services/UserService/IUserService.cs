using RockSchool.BL.Dtos.Service.Requests.UserService;
using RockSchool.BL.Dtos.Service.Responses;

namespace RockSchool.BL.Services.UserService;

public interface IUserService
{
    Task<Guid> AddUserAsync(AddUserServiceRequestDto addUserServiceRequestDto);
    Task<UserDto?> GetUserByIdAsync(Guid userId);
    Task DeleteUserAsync(Guid userId);
}
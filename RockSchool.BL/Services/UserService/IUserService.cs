using RockSchool.BL.Dtos;

namespace RockSchool.BL.Services.UserService;

public interface IUserService
{
    Task<Guid> AddUserAsync(UserDto userDto);
    Task<UserDto?> GetUserByIdAsync(Guid userId);
    Task DeleteUserAsync(Guid userId);
}
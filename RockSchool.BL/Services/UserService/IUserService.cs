using RockSchool.BL.Models;

namespace RockSchool.BL.Services.UserService;

public interface IUserService
{
    Task<Guid> AddUserAsync(UserDto userDto, string? email = null);
    Task<RegisterUserResponseDto> RegisterUserAsync(string email, int roleId);
    Task<UserDto?> GetUserByIdAsync(Guid userId);
    Task DeleteUserAsync(Guid userId);
}
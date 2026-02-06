using Microsoft.AspNetCore.Identity;
using RockSchool.BL.Models;
using RockSchool.Data.Repositories;
using RockSchool.Domain.Entities;
using RockSchool.Domain.Users;

namespace RockSchool.BL.Services.UserService;

public class UserService : IUserService
{
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<Guid> AddUserAsync(UserDto userDto)
    {
        throw new NotImplementedException();
        /*
        var passwordValidationResult = ValidateAndGetFinalPassword(string.Empty, string.Empty);

        if (!passwordValidationResult.IsSuccess)
            throw new InvalidOperationException(">" + passwordValidationResult.ErrorMessage + "<");

        var newUser = CreateUserEntity(userDto);

        newUser.PasswordHash = _passwordHasher.HashPassword(newUser, passwordValidationResult.FinalPassword);

        await _userRepository.AddAsync(newUser);

        var savedUser = await _userRepository.GetByIdAsync(newUser.UserId);
        if (savedUser == null)
            throw new InvalidOperationException("Failed to add UserService.");

        return savedUser.UserId;*/
    }

    public async Task<UserDto?> GetUserByIdAsync(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user == null)
            return null;

        var userDto = new UserDto
        {
            UserId = user.UserId,
            Login = user.Login,
            PasswordHash = user.PasswordHash,
            RoleId = user.RoleId,
            Role = user.Role
        };

        return userDto;
    }

    public async Task DeleteUserAsync(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            throw new InvalidOperationException("UserService not found.");

        await _userRepository.DeleteAsync(user);
    }

    private (bool IsSuccess, string FinalPassword, string ErrorMessage) ValidateAndGetFinalPassword(
        string? password,
        string? confirmPassword)
    {
        if (string.IsNullOrWhiteSpace(password))
            return (true, "123456", string.Empty);

        if (string.IsNullOrWhiteSpace(confirmPassword))
            return (false, string.Empty, "ConfirmPassword is required if Password is provided.");

        if (!password.Equals(confirmPassword))
            return (false, string.Empty, "Password and ConfirmPassword do not match.");

        return (true, password, string.Empty);
    }

    private User CreateUserEntity(UserDto userDto)
    {
        throw new NotImplementedException();
        /*
        return new User
        {
            Login = userDto.Login,
            RoleId = userDto.RoleId
        };
        */
    }
}
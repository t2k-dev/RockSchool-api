using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RockSchool.BL.Models;
using RockSchool.BL.Services.EmailService;
using RockSchool.Data.Data;
using RockSchool.Data.Entities;
using RockSchool.Data.Repositories;

namespace RockSchool.BL.Services.UserService;

public class UserService : IUserService
{
    private readonly IPasswordHasher<UserEntity> _passwordHasher;
    private readonly UserRepository _userRepository;
    private readonly IEmailService _emailService;
    private readonly UserManager<UserEntity> _userManager;
    private readonly RockSchoolContext _context;
    private readonly ILogger<UserService> _logger;

    public UserService(
        UserRepository userRepository,
        IPasswordHasher<UserEntity> passwordHasher,
        IEmailService emailService,
        UserManager<UserEntity> userManager,
        RockSchoolContext context,
        ILogger<UserService> logger)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _emailService = emailService;
        _userManager = userManager;
        _context = context;
        _logger = logger;
    }

    public async Task<Guid> AddUserAsync(UserDto userDto, string? email = null)
    {
        var passwordValidationResult = ValidateAndGetFinalPassword(string.Empty, string.Empty);

        if (!passwordValidationResult.IsSuccess)
            throw new InvalidOperationException(">" + passwordValidationResult.ErrorMessage + "<");

        var newUser = CreateUserEntity(userDto);

        newUser.PasswordHash = _passwordHasher.HashPassword(newUser, passwordValidationResult.FinalPassword);

        await _userRepository.AddAsync(newUser);

        var savedUser = await _userRepository.GetByIdAsync(newUser.UserId);
        if (savedUser == null)
            throw new InvalidOperationException("Failed to add UserService.");

        if (!string.IsNullOrWhiteSpace(email))
        {
            try
            {
                await _emailService.SendPasswordEmailAsync(email, userDto.Login, passwordValidationResult.FinalPassword);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send welcome email.");
            }
        }

        return savedUser.UserId;
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

    public async Task<RegisterUserResponseDto> RegisterUserAsync(string email, int roleId)
    {
        var existingUser = await _userManager.FindByEmailAsync(email);
        if (existingUser != null)
        {
            return new RegisterUserResponseDto
            {
                Success = false,
                Message = "User with this email already exists",
                UserId = null
            };
        }

        var password = GenerateSecurePassword(12);

        var user = new UserEntity
        {
            UserId = Guid.NewGuid(),
            UserName = email,
            Email = email,
            Login = email,
            RoleId = roleId,
            IsActive = true
        };

        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return new RegisterUserResponseDto
            {
                Success = false,
                Message = $"Failed to create user: {errors}",
                UserId = null
            };
        }

        var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleId == roleId);
        if (role != null)
        {
            await _userManager.AddToRoleAsync(user, role.Name);
        }

        try
        {
            await _emailService.SendPasswordEmailAsync(email, email, password);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to send welcome email: {ex.Message}");
        }

        return new RegisterUserResponseDto
        {
            Success = true,
            Message = "User registered successfully",
            UserId = user.UserId
        };
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

    private UserEntity CreateUserEntity(UserDto userDto)
    {
        return new UserEntity
        {
            Login = userDto.Login,
            RoleId = userDto.RoleId
        };
    }
    
    private string GenerateSecurePassword(int length = 12)
    {
        if (length < 6)
            throw new ArgumentException("Password length must be at least 8 characters", nameof(length));

        const string uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string lowercase = "abcdefghijklmnopqrstuvwxyz";
        const string digits = "0123456789";
        const string specialChars = "!@#$%^&*()_+-=[]{}|;:,.<>?";
        const string allChars = uppercase + lowercase + digits + specialChars;

        var password = new StringBuilder(length);

        // Ensure at least one character from each category for password strength
        password.Append(GetRandomChar(uppercase));
        password.Append(GetRandomChar(lowercase));
        password.Append(GetRandomChar(digits));
        password.Append(GetRandomChar(specialChars));

        // Fill the rest with random characters from all categories
        for (int i = 4; i < length; i++)
        {
            password.Append(GetRandomChar(allChars));
        }

        // Shuffle the password to avoid predictable patterns
        return ShuffleString(password.ToString());
    }

    private char GetRandomChar(string chars)
    {
        var randomIndex = RandomNumberGenerator.GetInt32(0, chars.Length);
        return chars[randomIndex];
    }

    private string ShuffleString(string input)
    {
        var array = input.ToCharArray();
        int n = array.Length;

        for (int i = n - 1; i > 0; i--)
        {
            int j = RandomNumberGenerator.GetInt32(0, i + 1);
            (array[i], array[j]) = (array[j], array[i]);
        }

        return new string(array);
    }
}
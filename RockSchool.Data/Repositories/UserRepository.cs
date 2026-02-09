using Microsoft.EntityFrameworkCore;
using RockSchool.Data.Data;
using RockSchool.Domain.Entities;
using RockSchool.Domain.Repositories;

namespace RockSchool.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly RockSchoolContext _context;

    public UserRepository(RockSchoolContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(Guid userId)
    {
        return await _context.Users.SingleOrDefaultAsync(u => u.UserId == userId);
    }

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(User? user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
}
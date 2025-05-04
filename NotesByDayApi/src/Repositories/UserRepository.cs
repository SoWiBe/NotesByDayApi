using Microsoft.EntityFrameworkCore;
using NotesByDayApi.Common.Dto;
using NotesByDayApi.Common.Models;
using NotesByDayApi.Data;
using NotesByDayApi.Exceptions;
using NotesByDayApi.Repositories.Core;

namespace NotesByDayApi.Repositories;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
    
    public async Task<User> CreateUser(UserDto user, CancellationToken ctn)
    {
        var users = await _context.Users.ToListAsync(ctn);
        if (users.Any(u => u.Username == user.Username))
            throw new NotesByDayException("Username already exists");

        if (users.Any(u => u.Email == user.Email))
            throw new NotesByDayException("Email already exists");

        var createdUser = new User
        {
            Username = user.Username,
            Email = user.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(user.Password),
            Age = user.Age,
            FirstName = user.FirstName,
            LastName = user.LastName
        };

        await _context.Users.AddAsync(createdUser, ctn);
        await _context.SaveChangesAsync(ctn);

        return createdUser;
    }

    public async Task<User> GetUserByUsername(string username, CancellationToken ctn)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username, ctn);
        if (user is null)
            throw new NotesByDayException("Username or password is incorrect");
        
        return user;
    }
}
using NotesByDayApi.Common.Dto;
using NotesByDayApi.Common.Models;
using NotesByDayApi.Repositories.Core;

namespace NotesByDayApi.Repositories;

public interface IUserRepository : IRepositoryBase<User>
{
    Task<User> CreateUser(UserDto user, CancellationToken ctn);
    Task<User> GetUserByUsername(string username, CancellationToken ctn);
}
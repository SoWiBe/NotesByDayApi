using NotesByDayApi.Common.Endpoints.Auth.Requests;

namespace NotesByDayApi.Services;

public interface IAuthService
{
    Task<string> Register(RegistrationUserRequest request, CancellationToken ctn);
    Task<string> Login(LoginRequest request, CancellationToken ctn);
}
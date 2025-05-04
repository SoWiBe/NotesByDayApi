using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NotesByDayApi.Common.Dto;
using NotesByDayApi.Common.Endpoints.Auth.Requests;
using NotesByDayApi.Common.Models;
using NotesByDayApi.Data;
using NotesByDayApi.Exceptions;
using NotesByDayApi.Repositories;

namespace NotesByDayApi.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper; 
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository, IConfiguration configuration, IMapper mapper)
    {
        _configuration = configuration;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<string> Register(RegistrationUserRequest request, CancellationToken ctn)
    {
        var user = await _userRepository.CreateUser(_mapper.Map<UserDto>(request), ctn);
        return user is null ? null : GenerateJwtToken(user);
    }

    public async Task<string> Login(LoginRequest request, CancellationToken ctn)
    {
        var user = await _userRepository.GetUserByUsername(request.Username, ctn);
        
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            throw new NotesByDayException("Username or password is incorrect");

        return GenerateJwtToken(user);
    }
    
    private string GenerateJwtToken(User user)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var key = Encoding.ASCII.GetBytes(jwtSettings["SecretKey"]!);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email)
            }),
            Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpiryMinutes"])),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
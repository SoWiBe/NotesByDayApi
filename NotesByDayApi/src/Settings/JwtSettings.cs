namespace NotesByDayApi.Settings;

public record JwtSettings
{
    public string SecretKey { get; set; } = null!;
    public int ExpiryMinutes { get; set; }
}
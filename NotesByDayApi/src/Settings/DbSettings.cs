namespace NotesByDayApi.Settings;

public record DbSettings
{
    public string PostgreSql { get; set; } = null!;
}
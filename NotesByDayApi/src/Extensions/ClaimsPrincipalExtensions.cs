using System.Security.Claims;

namespace NotesByDayApi.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static int GetUserId(this ClaimsPrincipal principal)
    {
        var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) throw new Exception("User ID not found in claims");
        return int.Parse(userId);
    }
}
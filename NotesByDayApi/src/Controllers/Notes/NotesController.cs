using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesByDayApi.Common.Endpoints.Notes.Requests;
using NotesByDayApi.Extensions;
using NotesByDayApi.Services.Notes;

namespace NotesByDayApi.Controllers.Notes;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class NotesController : ControllerBase
{
    private readonly INotesService _notesService;

    public NotesController(INotesService notesService)
    {
        _notesService = notesService;
    }

    [HttpPost("/v1/notes/")]
    public async Task<IActionResult> Create(CreateNoteRequest request, CancellationToken cancellationToken = default)
    {
        var userId = User.GetUserId();
        var token = await _notesService.Create(request, userId, cancellationToken);
        return Ok(token);
    }
    
    [HttpGet("/v1/notes/")]
    public async Task<IActionResult> GetNotes(CancellationToken cancellationToken = default)
    {
        var userId = User.GetUserId();
        var notes = await _notesService.GetNotes(userId, cancellationToken);
        return Ok(notes);
    }
    
    [HttpDelete("/v1/notes/{id}")]
    public async Task<IActionResult> RemoveNote(int id, CancellationToken cancellationToken = default)
    {
        var status = await _notesService.RemoveNote(id, cancellationToken);
        return Ok(status);
    }
}
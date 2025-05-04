using NotesByDayApi.Common.Endpoints.Notes.Requests;
using NotesByDayApi.Common.Models;

namespace NotesByDayApi.Services.Notes;

public interface INotesService
{
    Task<Note> Create(CreateNoteRequest request, int userId, CancellationToken ctn);
    Task<ICollection<Note>> GetNotes(int userId, CancellationToken ctn);

    Task<bool> RemoveNote(int noteId, CancellationToken ctn);
}
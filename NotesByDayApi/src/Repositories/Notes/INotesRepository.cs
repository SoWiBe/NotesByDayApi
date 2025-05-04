using NotesByDayApi.Common.Dto;
using NotesByDayApi.Common.Models;
using NotesByDayApi.Repositories.Core;

namespace NotesByDayApi.Repositories.Notes;

public interface INotesRepository : IRepositoryBase<Note>
{
    Task<Note> Create(NoteDto note, int userId, CancellationToken ctn);
    Task<Note> GetNoteById(int id, CancellationToken ctn);
    Task<bool> RemoveNote(int id, CancellationToken ctn);
    Task<ICollection<Note>> GetNotesByUserId(int userId, CancellationToken ctn);
}
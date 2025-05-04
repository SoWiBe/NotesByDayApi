using AutoMapper;
using NotesByDayApi.Common.Dto;
using NotesByDayApi.Common.Endpoints.Notes.Requests;
using NotesByDayApi.Common.Models;
using NotesByDayApi.Repositories.Notes;

namespace NotesByDayApi.Services.Notes;

public class NotesService : INotesService
{
    private readonly INotesRepository _notesRepository;
    private readonly IMapper _mapper;

    public NotesService(INotesRepository notesRepository, IMapper mapper)
    {
        _notesRepository = notesRepository;
        _mapper = mapper;
    }

    public async Task<Note> Create(CreateNoteRequest request, int userId, CancellationToken ctn)
    {
        var dto = _mapper.Map<NoteDto>(request);
        return await _notesRepository.Create(dto, userId, ctn);
    }

    public async Task<ICollection<Note>> GetNotes(int userId, CancellationToken ctn)
    {
        return await _notesRepository.GetNotesByUserId(userId, ctn);
    }

    public async Task<bool> RemoveNote(int noteId, CancellationToken ctn)
    {
        return await _notesRepository.RemoveNote(noteId, ctn);
    }
}
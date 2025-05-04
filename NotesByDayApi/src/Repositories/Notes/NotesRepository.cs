using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NotesByDayApi.Common.Dto;
using NotesByDayApi.Common.Models;
using NotesByDayApi.Data;
using NotesByDayApi.Exceptions;
using NotesByDayApi.Repositories.Core;

namespace NotesByDayApi.Repositories.Notes;

public class NotesRepository : RepositoryBase<Note>, INotesRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public NotesRepository(AppDbContext context, IMapper mapper) : base(context)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Note> Create(NoteDto note, int userId, CancellationToken ctn)
    {
        var createdNote = _mapper.Map<Note>(note);

        createdNote.UserId = userId;

        await _context.Notes.AddAsync(createdNote, ctn);
        await _context.SaveChangesAsync(ctn);

        return createdNote;
    }

    public async Task<Note> GetNoteById(int id, CancellationToken ctn)
    {
        var note = await _context.Notes.FirstOrDefaultAsync(x => x.Id == id, ctn);
        return note ?? throw new NotesByDayException("Заметка не найдена");

    }

    public async Task<bool> RemoveNote(int id, CancellationToken ctn)
    {
        var note = await _context.Notes.FirstOrDefaultAsync(x => x.Id == id, ctn);
        if (note is null)
            throw new NotesByDayException("Заметка не найдена");

        _context.Notes.Remove(note);
        await _context.SaveChangesAsync(ctn);

        return true;
    }

    public async Task<ICollection<Note>> GetNotesByUserId(int userId, CancellationToken ctn)
    {
        if (!_context.Users.Any(x => x.Id == userId))
            throw new NotesByDayException("Пользователь не найден");
        
        return await _context.Notes.Where(x => x.UserId == userId).ToListAsync(ctn);
    }
}
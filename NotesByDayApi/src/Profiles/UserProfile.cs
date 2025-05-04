using AutoMapper;
using NotesByDayApi.Common.Dto;
using NotesByDayApi.Common.Endpoints.Auth.Requests;
using NotesByDayApi.Common.Endpoints.Notes.Requests;
using NotesByDayApi.Common.Models;

namespace NotesByDayApi.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegistrationUserRequest, UserDto>().ReverseMap();
        CreateMap<CreateNoteRequest, NoteDto>().ReverseMap();
        CreateMap<NoteDto, Note>().ReverseMap();
    }
}
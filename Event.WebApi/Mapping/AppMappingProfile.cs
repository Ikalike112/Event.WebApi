using AutoMapper;
using Event.Application.Events.Commands.CreateEvent;
using Event.Application.Events.Commands.UpdateEvent;
using Event.Application.Events.Queries.GetEventById;
using Event.Application.Events.Queries.GetEvents;
using Event.WebApi.Models.Events;

namespace Event.WebApi.Mapping
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<Domain.Event, EventLookupDto>()
                .ForMember(dest => dest.SpeakerName,
                opt => opt.MapFrom(src => src.Speaker.FirstName + " " + src.Speaker.LastName))
                .ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.Id));
            CreateMap<Domain.Event, EventDetailsDto>()
                 .ForMember(dest => dest.SpeakerName,
                 opt => opt.MapFrom(src => src.Speaker.FirstName + " " + src.Speaker.LastName))
                 .ForMember(dest => dest.Name,
                 opt => opt.MapFrom(src => src.Name))
                 .ForMember(dest => dest.Id,
                 opt => opt.MapFrom(src => src.Id))
                 .ForMember(dest => dest.Venue,
                 opt => opt.MapFrom(src => src.Venue));
            CreateMap<CreateEventDto, CreateEventCommand>();
            CreateMap<UpdateEventDto, UpdateEventCommand>();
        }
    }
}

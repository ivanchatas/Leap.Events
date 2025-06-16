using Application.Dtos.Response;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping
{
    internal class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<EventResponseDto, Event>().ReverseMap();
        }
    }
}

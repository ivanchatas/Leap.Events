using Application.Dtos.Response;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping
{
    internal class SalesTicketProfile : Profile
    {
        public SalesTicketProfile()
        {
            CreateMap<TickectSaleResponseDto, TicketSale>().ReverseMap();            
        }
    }
}

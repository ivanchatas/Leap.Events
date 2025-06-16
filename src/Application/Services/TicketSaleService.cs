using Application.Dtos.Response;
using Application.Helpers;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using AutoMapper;
using NHibernate.Linq;

namespace Application.Services
{
    public class TicketSaleService : ITicketSaleService
    {
        private readonly ITicketSaleRepository _ticketSaleRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public TicketSaleService(ITicketSaleRepository ticketSaleRepository, IEventRepository eventRepository, IMapper mapper)
        {
            _ticketSaleRepository = ticketSaleRepository ?? throw new ArgumentNullException(nameof(ticketSaleRepository));
            _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));    
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<TickectSaleResponseDto>> GetTicketsByEventId(string id)
        {
            var result = await _ticketSaleRepository.GetAllAsync(predicate: x => x.EventId == Guid.Parse(id));
            return _mapper.Map<List<TickectSaleResponseDto>>(result.ToList());
        }

        public async Task<List<HighSalesResponseDto>> GetHighestSellingByPrice()
        {
            var query = await _ticketSaleRepository.GetAllAsync();
            var top = query
                .GroupBy(ts => new { ts.Event.Id, ts.Event.Name })
                .Select(group => new HighSalesResponseDto
                {
                    EventId = group.Key.Id,
                    EventName = group.Key.Name,
                    Value = group.Sum(ts => ts.PriceInCents) / 100
                })
                .OrderByDescending(dto => dto.Value)
                .Take(5)
                .ToList();

            return top;
        }

        public async Task<List<HighSalesResponseDto>> GetHighestSellingByNumber()
        {
            var query = await _ticketSaleRepository.GetAllAsync();
            var top = query
                .GroupBy(ts => new { ts.Event.Id, ts.Event.Name })
                .Select(group => new HighSalesResponseDto
                {
                    EventId = group.Key.Id,
                    EventName = group.Key.Name,
                    Value = group.Count()
                })
                .OrderByDescending(dto => dto.Value)
                .Take(5)
                .ToList();

            return top;
        }
    }
}

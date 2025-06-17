using Application.Dtos.Response;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Wrapper;
using AutoMapper;
using Domain.Entities;

namespace Application.Services
{
    public class TicketSaleService : ITicketSaleService
    {
        private readonly ITicketSaleRepository _ticketSaleRepository;
        private readonly IMapper _mapper;

        public TicketSaleService(ITicketSaleRepository ticketSaleRepository, IEventRepository eventRepository, IMapper mapper)
        {
            _ticketSaleRepository = ticketSaleRepository ?? throw new ArgumentNullException(nameof(ticketSaleRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<List<TickectSaleResponseDto>>> GetTicketsByEventId(string id)
        {
            //var result = await _ticketSaleRepository.GetAllAsync(predicate: x => x.EventId == Guid.Parse(id));
            var sql = $"SELECT Id, UserId, PurchaseDate, PriceInCents FROM TicketSales WHERE EventId = '{id}'";
            var query = await _ticketSaleRepository.GetBySqlQuery(typeof(TicketSale), sql);
            var result = _mapper.Map<List<TickectSaleResponseDto>>(query.ToList());
            return new Result<List<TickectSaleResponseDto>> { Data = result };
        }

        public async Task<Result<List<HighSalesResponseDto>>> GetHighestSellingByPrice()
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

            return new Result<List<HighSalesResponseDto>> { Data = top };
        }

        public async Task<Result<List<HighSalesResponseDto>>> GetHighestSellingByNumber()
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

            return new Result<List<HighSalesResponseDto>> { Data = top };
        }
    }
}

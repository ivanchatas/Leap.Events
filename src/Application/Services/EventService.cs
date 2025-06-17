using Application.Dtos.Response;
using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Wrapper;
using AutoMapper;

namespace Application.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public EventService(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<EventResponseDto>> GetById(string id)
        {
            var query = await _eventRepository.FirstOrDefaultAsync(predicate: x => x.Id == Guid.Parse(id));
            var result = _mapper.Map<EventResponseDto>(query);
            return new Result<EventResponseDto> { Data = result };
        }

        public async Task<Result<IList<EventResponseDto>>> GetUpcoming(int days) 
        {
            if (days <= 0)
            {
                throw new ApiException("Days must be greater than zero.");
            }

            var today = DateTime.UtcNow;
            var maxDate = today.AddDays(days);
            var events = await _eventRepository.GetAllAsync(predicate: x => (x.StartsOn >= today && x.EndsOn >= today) && 
                                                                            (x.StartsOn <= maxDate && x.EndsOn <= maxDate),
                                                            orderBy: x => x.OrderBy(y => y.StartsOn).ThenBy(y => y.Name));
            var result = _mapper.Map<List<EventResponseDto>>(events.ToList());
            return new Result<IList<EventResponseDto>> { Data = result };
        }

        public async Task<Result<IList<EventResponseDto>>> GetUpcomingNext30Days()
            => await GetUpcoming(30);

        public async Task<Result<IList<EventResponseDto>>> GetUpcomingNext60Days()
            => await GetUpcoming(60);

        public async Task<Result<IList<EventResponseDto>>> GetUpcomingNext180Days()
            => await GetUpcoming(180);

    }
}

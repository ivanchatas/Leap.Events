using Application.Dtos.Response;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
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

        public async Task<EventResponseDto> GetById(string id)
        {
            var result = await _eventRepository.FirstOrDefaultAsync(predicate: x => x.Id == Guid.Parse(id));
            return _mapper.Map<EventResponseDto>(result);
        }

        public async Task<IList<EventResponseDto>> GetUpcoming(int days) 
        {
            var today = DateTime.UtcNow;
            var maxDate = today.AddDays(days);
            var events = await _eventRepository.GetAllAsync(predicate: x => (x.StartsOn >= today && x.EndsOn >= today) && (x.StartsOn >= maxDate && x.EndsOn >= maxDate));
            return _mapper.Map<List<EventResponseDto>>(events.ToList());
        }

        public async Task<IList<EventResponseDto>> GetUpcomingNext30Days()
            => await GetUpcoming(30);

        public async Task<IList<EventResponseDto>> GetUpcomingNext60Days()
            => await GetUpcoming(60);

        public async Task<IList<EventResponseDto>> GetUpcomingNext180Days()
            => await GetUpcoming(180);

    }
}

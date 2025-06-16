using Application.Dtos.Response;

namespace Application.Interfaces.Services
{
    public interface IEventService
    {
        Task<IList<EventResponseDto>> GetUpcoming(int days);
        Task<IList<EventResponseDto>> GetUpcomingNext30Days();
        Task<IList<EventResponseDto>> GetUpcomingNext60Days();
        Task<IList<EventResponseDto>> GetUpcomingNext180Days();
    }
}

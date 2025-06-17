using Application.Dtos.Response;
using Application.Wrapper;

namespace Application.Interfaces.Services
{
    public interface IEventService
    {
        Task<Result<EventResponseDto>> GetById(string id);
        Task<Result<IList<EventResponseDto>>> GetUpcoming(int days);
        Task<Result<IList<EventResponseDto>>> GetUpcomingNext30Days();
        Task<Result<IList<EventResponseDto>>> GetUpcomingNext60Days();
        Task<Result<IList<EventResponseDto>>> GetUpcomingNext180Days();
    }
}

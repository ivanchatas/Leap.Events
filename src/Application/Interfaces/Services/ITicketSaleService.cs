using Application.Dtos.Response;
using Application.Wrapper;

namespace Application.Interfaces.Services
{
    public interface ITicketSaleService
    {
        Task<Result<List<TickectSaleResponseDto>>> GetTicketsByEventId(string id);
        Task<Result<List<HighSalesResponseDto>>> GetHighestSellingByPrice();
        Task<Result<List<HighSalesResponseDto>>> GetHighestSellingByNumber();
    }
}

using Application.Dtos.Response;

namespace Application.Interfaces.Services
{
    public interface ITicketSaleService
    {
        Task<List<TickectSaleResponseDto>> GetTicketsByEventId(string id);
        Task<List<HighSalesResponseDto>> GetHighestSellingByPrice();
        Task<List<HighSalesResponseDto>> GetHighestSellingByNumber();
    }
}

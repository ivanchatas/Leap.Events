using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleTicketController : ControllerBase
    {
        private readonly ITicketSaleService _ticketSaleService;

        public SaleTicketController(ITicketSaleService ticketSaleService)
        {
            _ticketSaleService = ticketSaleService ?? throw new ArgumentNullException(nameof(ticketSaleService));
        }

        [HttpGet("event/{id}")]
        public async Task<IActionResult> GetTicketsByEventId(string id) 
        {
            return Ok(await _ticketSaleService.GetTicketsByEventId(id));
        }

        [HttpGet("highestSellingByPrice")]
        public async Task<IActionResult> GetHighestSellingByPrice()
            => Ok(await _ticketSaleService.GetHighestSellingByPrice());

        [HttpGet("highestSellingByAmount")]
        public async Task<IActionResult>GetHighestSellingByAmount()
            => Ok(await _ticketSaleService.GetHighestSellingByNumber());
    }
}

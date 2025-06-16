using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService ?? throw new ArgumentNullException(nameof(eventService));
        }

        [HttpGet("upcoming")]
        public async Task<IActionResult> GetUpcomingEvents(int days)
        {
            if (days <= 0)
            {
                return BadRequest("Days must be greater than zero.");
            }
            var events = await _eventService.GetUpcoming(days);
            return Ok(events);
        }

    }
}

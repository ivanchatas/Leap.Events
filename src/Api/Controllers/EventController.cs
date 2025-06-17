using Application.Dtos.Response;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id) 
            => Ok(await _eventService.GetById(id));

        [HttpGet("upcoming")]
        public async Task<IActionResult> GetUpcomingEvents(int days)
        {
           
            var events = await _eventService.GetUpcoming(days);
            return Ok(events);
        }

    }
}

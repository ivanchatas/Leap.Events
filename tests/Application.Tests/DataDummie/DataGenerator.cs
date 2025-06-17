using Application.Dtos.Response;

namespace Application.Tests.DataDummie
{
    internal static class DataGenerator
    {
        public static List<Domain.Entities.Event> GenerateEvents(int count)
        {
            var events = new List<Domain.Entities.Event>();
            for (int i = 0; i < count; i++)
            {
                events.Add(new Domain.Entities.Event
                {
                    Id = Guid.NewGuid(),
                    Name = $"Event {i + 1}",
                    StartsOn = DateTime.UtcNow.AddDays(i),
                    EndsOn = DateTime.UtcNow.AddDays(i + 1)
                });
            }
            return events;
        }
        public static List<EventResponseDto> GenerateEventDtos(List<Domain.Entities.Event> events)
        {
            return events.Select(e => new EventResponseDto
            {
                Id = e.Id.ToString(),
                Name = e.Name
            }).ToList();
        }
    }
}

using Domain.Entities;

namespace Application.Dtos.Response
{
    public class EventResponseDto
    {
        public  string Id { get; set; }
        public  string Name { get; set; }
        public  DateTime StartsOn { get; set; }
        public  DateTime EndsOn { get; set; }
        public  string Location { get; set; }
        public  IList<TicketSale> TicketSale { get; set; }

    }
}

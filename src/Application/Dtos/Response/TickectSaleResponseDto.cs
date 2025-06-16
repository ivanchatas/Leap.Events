using Domain.Entities;

namespace Application.Dtos.Response
{
    public class TickectSaleResponseDto
    {
        public string Id { get; set; }
        public string EventId { get; set; }
        public string UserId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime PriceInCents { get; set; }

        public Event Event { get; set; }
    }
}

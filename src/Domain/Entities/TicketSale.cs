﻿namespace Domain.Entities
{
    public class TicketSale
    {
        public virtual Guid Id { get; set; }
        public virtual Guid EventId { get; set; }
        public virtual Guid UserId { get; set; }
        public virtual DateTime PurchaseDate { get; set; }
        public virtual int PriceInCents { get; set; }

        public virtual Event Event { get; set; }
    }
}

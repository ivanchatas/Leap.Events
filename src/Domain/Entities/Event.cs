namespace Domain.Entities
{
    public class Event
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime StartsOn { get; set; }
        public virtual DateTime EndsOn { get; set; }
        public virtual string Location { get; set; }

        public virtual IList<TicketSale> TicketSale { get; set; }

        public Event()
        {
            TicketSale = new List<TicketSale>();
        }
    }
}

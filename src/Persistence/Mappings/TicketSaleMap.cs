using Domain.Entities;
using FluentNHibernate.Mapping;

namespace Persistence.Mappings
{
    internal class TicketSaleMap : ClassMap<TicketSale>
    {
        public TicketSaleMap()
        {
            Table("TicketSales");
            Id(x => x.Id);
            //Map(x => x.EventId).Column("EventId").Not.Nullable();
            Map(x => x.UserId).Not.Nullable();
            Map(x => x.PurchaseDate).Not.Nullable();
            Map(x => x.PriceInCents).Not.Nullable();

            References(x => x.Event)
                .Column("EventId")
                .Not.Nullable()
                .Cascade.None();
        }
    }
}
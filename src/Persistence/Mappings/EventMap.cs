using Domain.Entities;
using FluentNHibernate.Mapping;

namespace Persistence.Mappings
{
    internal class EventMap : ClassMap<Event>
    {
        public EventMap()
        {
            Table("Events");
            Id(x => x.Id);
            Map(x => x.Name).Not.Nullable();
            Map(x => x.StartsOn).Not.Nullable();
            Map(x => x.EndsOn).Not.Nullable();
            Map(x => x.Location).Not.Nullable();
        }
    }
}

using Application.Interfaces.Repositories;
using Domain.Entities;
using NHibernate;

namespace Persistence.Repositories
{
    public class EventRepository : BaseRepository<Event, Guid>, IEventRepository
    {
        public EventRepository(ISession session) : base(session)
        {
        }
    }
}

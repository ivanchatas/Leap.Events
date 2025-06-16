using Application.Interfaces.Repositories;
using Domain.Entities;
using NHibernate;

namespace Persistence.Repositories
{
    public class TicketSaleRepository : BaseRepository<TicketSale, Guid>, ITicketSaleRepository
    {
        public TicketSaleRepository(ISession session) : base(session)
        {
        }
    }
}

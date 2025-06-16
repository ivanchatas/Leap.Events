using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using Persistence.Mappings;

namespace Persistence.Helpers
{
    public static class NHibernateSessionFactory
    {
        public static ISessionFactory Create(string connectionString)
        {
            return Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.ConnectionString(connectionString).ShowSql())
                .Mappings(m =>
                {
                    m.FluentMappings.AddFromAssemblyOf<EventMap>();
                    m.FluentMappings.AddFromAssemblyOf<TicketSaleMap>();
                })
                .BuildSessionFactory();
        }
    }
}

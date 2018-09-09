using NHibernate;
using NHibernate.Cfg;
using NHibernate.Connection;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using ORMHowTo.Infrastructure;
using System.Data.SQLite;
using System.Linq;

namespace ORMHowTo.NHibernate
{
    public abstract class NHibernateRepository : Repository
    {
        protected Configuration Configuration { get; private set; }

        private ISessionFactory _sessionFactory;
        protected ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory != null)
                {
                    return _sessionFactory;
                }

                var configuration = new Configuration();
                configuration.DataBaseIntegration(x =>
                {
                    x.Driver<SQLite20Driver>();
                    x.Dialect<SQLiteDialect>();
                    x.ConnectionProvider<DriverConnectionProvider>();
                    x.ConnectionString = ConnectionString;
                    x.ConnectionReleaseMode = ConnectionReleaseMode.OnClose;
                    x.LogSqlInConsole = true;
                    x.LogFormattedSql = true;
                });

                var mapper = new ModelMapper();
                var mappings = typeof(ArtistMap).Assembly.GetTypes().Where(t => t.Name.EndsWith("Map"));
                mapper.AddMappings(mappings);

                configuration.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());
                Configuration = configuration;

                return (_sessionFactory = configuration.BuildSessionFactory());
            }
        }
        protected ISession Session
        {
            get
            {
                var openConnection = SessionFactory.OpenSession();
#if DEBUG
                var sqliteConnection = openConnection.Connection as SQLiteConnection;
                sqliteConnection.Trace += (object sender, TraceEventArgs e) =>
                {
                    System.Diagnostics.Debug.WriteLine($"[SQLITE] {e.Statement}");
                };
#endif

                return openConnection;
            }
        }

        public NHibernateRepository(string connectionString) : base(connectionString) { }
    }
}

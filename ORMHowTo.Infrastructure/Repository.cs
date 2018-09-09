namespace ORMHowTo.Infrastructure
{
    public abstract class Repository
    {
        public string ConnectionString { get; }
        public Repository(string connectionString) => this.ConnectionString = connectionString;
    }
}

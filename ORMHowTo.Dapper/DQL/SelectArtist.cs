using Dapper;
using ORMHowTo.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq.Expressions;

namespace ORMHowTo.Dapper.DQL
{
    public class SelectArtist : Repository, ISelectRepository<Artist, int>
    {
        public SelectArtist(string connectionString) : base(connectionString) { }

        public Artist GetById(int id)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.QueryFirstOrDefault<Artist>(
                    "SELECT ArtistId, Name from Artist WHERE ArtistId = @id",
                    new { id }
                );
            }
        }

        public Artist Get(Artist model)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.QueryFirstOrDefault<Artist>(
                    "SELECT ArtistId, Name from Artist WHERE ArtistId = @id",
                    new { id = model.ArtistId }
                );
            }
        }

        public IEnumerable<Artist> List(Artist model)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<Artist>(
                    "SELECT ArtistId, Name from Artist WHERE Name LIKE @name",
                    new { name = $"%{model.Name}%" }
                );
            }
        }

        public Artist FindOne(Expression<Func<Artist, bool>> predicate)
        {
            throw new NotSupportedException();
        }

        public IEnumerable<Artist> Find(Expression<Func<Artist, bool>> predicate)
        {
            throw new NotSupportedException();
        }
    }
}

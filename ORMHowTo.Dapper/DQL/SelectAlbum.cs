using Dapper;
using ORMHowTo.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Linq.Expressions;

namespace ORMHowTo.Dapper.DQL
{
    public class SelectAlbum : Repository, ISelectRepository<Album, int>
    {
        public SelectAlbum(string connectionString) : base(connectionString) { }

        public Album GetById(int id)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<Album, Artist, Album>(
                    @"
                        SELECT al.AlbumId, al.Title, ar.ArtistId, ar.Name 
                        FROM Album al 
                        INNER JOIN Artist ar ON ar.ArtistId = al.ArtistId 
                        WHERE al.AlbumId = @id",
                    (album, artist) =>
                    {
                        album.ArtistId = artist.ArtistId;
                        album.Artist = artist;
                        return album;
                    },
                    new { id },
                    splitOn: "ArtistId"
                ).FirstOrDefault();
            }
        }

        public Album Get(Album model)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<Album, Artist, Album>(
                    @"
                        SELECT al.AlbumId, al.Title, ar.ArtistId, ar.Name 
                        FROM Album al 
                        INNER JOIN Artist ar ON ar.ArtistId = al.ArtistId 
                        WHERE al.AlbumId = @id",
                    (album, artist) =>
                    {
                        album.ArtistId = artist.ArtistId;
                        album.Artist = artist;
                        return album;
                    },
                    new { id = model.AlbumId },
                    splitOn: "ArtistId"
                ).FirstOrDefault();
            }
        }

        public IEnumerable<Album> List(Album model)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<Album, Artist, Album>(
                    @"
                        SELECT al.AlbumId, al.Title, ar.ArtistId, ar.Name 
                        FROM Album al 
                        INNER JOIN Artist ar ON ar.ArtistId = al.ArtistId 
                        WHERE al.Title LIKE @title",
                    (album, artist) =>
                    {
                        album.ArtistId = artist.ArtistId;
                        album.Artist = artist;
                        return album;
                    },
                    new { title = $"%{model.Title}%" },
                    splitOn: "ArtistId"
                );
            }
        }

        public Album FindOne(Expression<Func<Album, bool>> predicate)
        {
            throw new NotSupportedException();
        }

        public IEnumerable<Album> Find(Expression<Func<Album, bool>> predicate)
        {
            throw new NotSupportedException();
        }
    }
}

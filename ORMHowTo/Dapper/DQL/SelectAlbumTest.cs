using ORMHowTo.App;
using ORMHowTo.Infrastructure;
using System;
using System.Linq;
using Xunit;

namespace ORMHowTo.Dapper.DQL
{
    public class SelectAlbumTest
    {
        protected SelectAlbum selector;
        public SelectAlbumTest() => this.selector = new SelectAlbum(Config.ConnectionString);

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void GetById(int id)
        {
            var album = this.selector.GetById(id);

            Assert.NotNull(album);
            Assert.NotNull(album.Artist);
            Assert.Equal(album.Artist.ArtistId, album.ArtistId);
            Assert.Equal(album.AlbumId, id);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Get(int id)
        {
            var album = this.selector.Get(new Album { AlbumId = id });

            Assert.NotNull(album);
            Assert.NotNull(album.Artist);
            Assert.Equal(album.Artist.ArtistId, album.ArtistId);
            Assert.Equal(album.AlbumId, id);
        }

        [Theory]
        [InlineData("Black", 5)]
        [InlineData("greatest", 8)]
        [InlineData("hits", 9)]
        public void List(string titleLike, int total)
        {
            var albums = this.selector.List(new Album { Title = titleLike });

            Assert.NotNull(albums);
            Assert.Equal(total, albums.Count());
            Assert.True(albums.All(a => a.Artist != null));
            Assert.True(albums.All(a => a.ArtistId == a.Artist.ArtistId));
            Assert.Empty(albums.Where(a => !a.Title.ToLower().Contains(titleLike.ToLower())));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void FindOne(int id)
        {
            Assert.Throws<NotSupportedException>(() =>
                this.selector.FindOne((Album a) => a.AlbumId == id)
            );
        }

        [Theory]
        [InlineData("Black", 5)]
        [InlineData("greatest", 8)]
        [InlineData("hits", 9)]
#pragma warning disable xUnit1026 // Theory methods should use all of their parameters
        public void Find(string titleLike, int total)
#pragma warning restore xUnit1026 // Theory methods should use all of their parameters
        {
            Assert.Throws<NotSupportedException>(() =>
                this.selector.Find((Album a) => a.Title == titleLike)
            );
        }
    }
}

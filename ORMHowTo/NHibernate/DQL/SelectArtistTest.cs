using ORMHowTo.App;
using ORMHowTo.Infrastructure;
using System.Linq;
using Xunit;

namespace ORMHowTo.NHibernate.DQL
{
    public class SelectArtistTest
    {
        protected SelectArtist selector;
        public SelectArtistTest() => this.selector = new SelectArtist(Config.ConnectionString);

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void GetById(int id)
        {
            var artist = this.selector.GetById(id);

            Assert.NotNull(artist);
            Assert.Equal(artist.ArtistId, id);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Get(int id)
        {
            var artist = this.selector.Get(new Artist { ArtistId = id });

            Assert.NotNull(artist);
            Assert.Equal(artist.ArtistId, id);
        }

        [Theory]
        [InlineData("Black", 5)]
        [InlineData("Chico", 2)]
        [InlineData("Jack", 3)]
        public void List(string nameLike, int total)
        {
            var artists = this.selector.List(new Artist { Name = nameLike });

            Assert.NotNull(artists);
            Assert.Equal(total, artists.Count());
            Assert.Empty(artists.Where(a => !a.Name.ToLower().Contains(nameLike.ToLower())));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void FindOne(int id)
        {
            var artist = this.selector.FindOne((Artist a) => a.ArtistId == id);

            Assert.NotNull(artist);
            Assert.Equal(artist.ArtistId, id);
        }

        [Theory]
        [InlineData("Black", 5)]
        [InlineData("Chico", 2)]
        [InlineData("Jack", 3)]
        public void Find(string nameLike, int total)
        {
            var artists = this.selector.Find((Artist a) => a.Name.ToLower().Contains(nameLike.ToLower()));

            Assert.NotNull(artists);
            Assert.Equal(total, artists.Count());
            Assert.Empty(artists.Where(a => !a.Name.ToLower().Contains(nameLike.ToLower())));
        }
    }
}

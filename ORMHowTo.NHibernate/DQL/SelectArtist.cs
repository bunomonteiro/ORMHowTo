using ORMHowTo.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using NExpression = NHibernate.Criterion.Expression;

namespace ORMHowTo.NHibernate.DQL
{
    public class SelectArtist : NHibernateRepository, ISelectRepository<Artist, int>
    {
        public SelectArtist(string connectionString) : base(connectionString) { }

        public Artist GetById(int id)
        {
            using (var session = Session)
            {
                var artist = session.Get<Artist>(id);

                return artist;
            }
        }

        public Artist Get(Artist model)
        {
            using (var session = Session)
            {
                var artist = session.Get<Artist>(model.ArtistId);

                return artist;
            }
        }

        public IEnumerable<Artist> List(Artist model)
        {
            using (var session = Session)
            {
                var artists = session.CreateCriteria<Artist>()
                    .Add(NExpression.Like("Name", $"%{model.Name}%"))
                    .List<Artist>();

                return artists;
            }
        }

        public Artist FindOne(Expression<Func<Artist, bool>> predicate)
        {
            using (var session = Session)
            {
                var artist = session.Query<Artist>().FirstOrDefault(predicate);

                return artist;
            }
        }

        public IEnumerable<Artist> Find(Expression<Func<Artist, bool>> predicate)
        {
            using (var session = Session)
            {
                var artists = session.Query<Artist>().Where(predicate).ToList();

                return artists;
            }
        }
    }
}

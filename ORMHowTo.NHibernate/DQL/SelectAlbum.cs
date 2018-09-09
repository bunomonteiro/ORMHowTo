using NHibernate;
using ORMHowTo.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using NExpression = NHibernate.Criterion.Expression;

namespace ORMHowTo.NHibernate.DQL
{
    public class SelectAlbum : NHibernateRepository, ISelectRepository<Album, int>
    {
        public SelectAlbum(string connectionString) : base(connectionString) { }

        public Album GetById(int id)
        {
            using (var session = Session)
            {
                var album = session.Get<Album>(id);
                return album;
            }
        }

        public Album Get(Album model)
        {
            using (var session = Session)
            {
                var album = session.Get<Album>(model.AlbumId);

                return album;
            }
        }

        public IEnumerable<Album> List(Album model)
        {
            using (var session = Session)
            {
                var albums = session.CreateCriteria<Album>()
                    .SetFetchMode("Artist", FetchMode.Join) // force to load
                    .Add(NExpression.Like("Title", $"%{model.Title}%"))
                    .List<Album>();

                return albums;
            }
        }

        public Album FindOne(Expression<Func<Album, bool>> predicate)
        {
            using (var session = Session)
            {
                var album = session.Query<Album>().FirstOrDefault(predicate);

                return album;
            }
        }

        public IEnumerable<Album> Find(Expression<Func<Album, bool>> predicate)
        {
            using (var session = Session)
            {
                var albums = session.Query<Album>().Where(predicate).ToList();

                return albums;
            }
        }
    }
}

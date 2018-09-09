using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ORMHowTo.Infrastructure
{
    public interface ISelectRepository<TModel, TId> where TModel : Model
    {
        TModel GetById(TId id);
        TModel Get(TModel model);
        IEnumerable<TModel> List(TModel model);
        TModel FindOne(Expression<Func<TModel, bool>> predicate);
        IEnumerable<TModel> Find(Expression<Func<TModel, bool>> predicate);
    }
}

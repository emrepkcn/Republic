using Republic.Model.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Republic.BLL.Interfaces
{
    public interface IBaseGenelBLL<TEntity> : IBaseBLL
         where TEntity : BaseEntity
    {
        IQueryable<TResult> List<TResult>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TResult>> selector);
        bool Insert(TEntity entity);
        bool Update(TEntity oldEntity, TEntity currentEntity);
        bool Delete(TEntity entity);
        bool Insert(IList<TEntity> entities);
        bool Update(IList<TEntity> oldEntities, IList<TEntity> currentEntities);
        bool Delete(IList<TEntity> entities);
        TResult Single<TResult>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TResult>> selector);
        int Count(Expression<Func<TEntity, bool>> filter);
    }
}

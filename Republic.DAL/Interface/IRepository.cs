using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Republic.DAL.Interface
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        void Insert(TEntity entity);
        void InsertRange(IEnumerable<TEntity> entities);
      
        /// <summary>
        /// TEntity: tablosundaki alanlara göre update yapar 
        /// </summary> 
        /// <typeparamref name="TEntity">tablosundaki alanlara göre update yapar </typeparamref>
        /// <param name="entity">tablosundaki</param>
        /// <param name="fields">alanlar</param>
        void Update(TEntity entity, IEnumerable<string> fields);
        /// <summary>
        /// TEntity  tablosundaki alanlara göre update yapar 
        /// </summary>
        /// <param name="keyValuePairs"></param>
        void UpdateRange(IDictionary<TEntity,IEnumerable<string>> keyValuePairs);
        void Delete(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entities);
        TResult Find<TResult>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TResult>> selector);
        /// <summary>
        /// entitye göre filitreliyip tresult tipinde geri değer döner.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="filter">Filitreler</param>
        /// <param name="selector">Tresolt tipinde Geri Döner</param>
        /// <returns></returns>
        IQueryable<TResult> Select<TResult>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TResult>> selector);
        int Count(Expression<Func<TEntity, bool>> filter = null);

    }
}

using Republic.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Republic.DAL.Base
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext Db;
        private readonly DbSet<TEntity> DbSet;


        public Repository(DbContext db)
        {
            if (db == null) return;
            this.Db = db;
            DbSet = Db.Set<TEntity>();
        }
        public int Count(Expression<Func<TEntity, bool>> filter = null)
            => filter == null ? DbSet.Count() : DbSet.Count(filter);


        public void Delete(TEntity entity)
        {
            Db.Entry(entity).State = EntityState.Deleted;
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
            => entities.ToList().ForEach(entity => Delete(entity));
        public TResult Find<TResult>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TResult>> selector)
        {
            return filter == null ? DbSet.Select(selector).FirstOrDefault() : DbSet.Where(filter).Select(selector).FirstOrDefault();
        }

        public void Insert(TEntity entity)
        {
            Db.Entry(entity).State = EntityState.Added;
        }
        public void InsertRange(IEnumerable<TEntity> entities)
        {
            entities.ToList().ForEach(x => Insert(x));
        }
        public IQueryable<TResult> Select<TResult>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TResult>> selector)
        {
            return filter == null ? DbSet.Select(selector) : DbSet.Where(filter).Select(selector);
        }
        public void Update(TEntity entity, IEnumerable<string> fields)
        {
            //bu entityle işlem yapıcağımızı söyledik
            DbSet.Attach(entity);
            //biz entitynin fields'ları arasında dolaşım güncelleme yapıcağımızı söyledik
            var entry = Db.Entry(entity);

            fields.ToList().ForEach(field =>
            {
                entry.Property(field).IsModified = true;
            });
        }
        public void UpdateRange(IDictionary<TEntity, IEnumerable<string>> keyValuePairs)
        {
            keyValuePairs.ToList().ForEach(x => Update(x.Key, x.Value));
        }

        #region Dispose

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                    Db.Dispose();
                disposedValue = true;

            }
        }


        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }








        #endregion
    }
}

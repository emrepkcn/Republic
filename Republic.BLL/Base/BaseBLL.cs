using Republic.BLL.Functions;
using Republic.BLL.Interfaces;
using Republic.DAL.Interface;
using Republic.Data.Context;
using Republic.Model.Entities.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
//using static Republic.BLL.General.GeneralFunction;
namespace Republic.BLL.Base
{
    public class BaseBLL<TEntity, TContext> : IBaseBLL
         where TEntity : BaseEntity
         where TContext : DbContext
    {
        private IUnitOfWork<TEntity> uow;


        protected IQueryable<TResult> BaseList<TResult>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TResult>> selector)
        {
            GeneralFunction.CreateUnitOfWork<TEntity, TContext>(ref uow);
            return uow.Rep.Select(filter, selector);

        }
        protected TResult BaseSingle<TResult>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TResult>> selector)
        {
            GeneralFunction.CreateUnitOfWork<TEntity, TContext>(ref uow);
            return uow.Rep.Find(filter, selector);
        }

        protected int BaseCount(Expression<Func<TEntity, bool>> filter)
        {
            return uow.Rep.Count(filter);
        }

        protected bool BaseDelete(IList<TEntity> entities)
        {
            GeneralFunction.CreateUnitOfWork<TEntity, TContext>(ref uow);
            uow.Rep.DeleteRange(entities);
            return uow.Save();
        }
        protected bool BaseDelete(TEntity entity)
        {
            GeneralFunction.CreateUnitOfWork<TEntity, TContext>(ref uow);
            uow.Rep.Delete(entity);
            return uow.Save();
        }

        protected bool BaseInsert(TEntity entity)
        {
            GeneralFunction.CreateUnitOfWork<TEntity, TContext>(ref uow);
            uow.Rep.Insert(entity);
            return uow.Save();
        }

        protected bool BaseInsert(IList<TEntity> entities)
        {
            GeneralFunction.CreateUnitOfWork<TEntity, TContext>(ref uow);
            uow.Rep.InsertRange(entities);
            return uow.Save();
        }
        protected bool BaseUpdate(TEntity oldEntity, TEntity currentEntity)
        {
            if (oldEntity is null | currentEntity is null) return false;
            GeneralFunction.CreateUnitOfWork<TEntity, TContext>(ref uow);
            var değişenalanlar = oldEntity.GetChangedFields(currentEntity);
            if (değişenalanlar.Count.Equals(0)) return true;

            uow.Rep.Update(currentEntity, değişenalanlar);
            return uow.Save();
        }
        protected bool BaseUpdate(IList<TEntity> oldEntities, IList<TEntity> currentEntities)
        {
            var keyValuePairs = new Dictionary<TEntity, IEnumerable<string>>();
            foreach (var item in oldEntities)
            {
                var curent = currentEntities.FirstOrDefault(x => x.Id == item.Id);
                if (curent is null) return false;
                var değişenalanlar = item.GetChangedFields(curent);

                keyValuePairs.Add(curent.EntityConvert<TEntity>(), değişenalanlar);
            }

            uow.Rep.UpdateRange(keyValuePairs);
            return uow.Save();
        }





        #region Dispose

        public void Dispose()
        {

            uow?.Dispose();

        }





        #endregion
    }
}

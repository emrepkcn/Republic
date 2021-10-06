using Republic.BLL.Interfaces;
using Republic.Common.Enums;
using Republic.Data.Context;
using Republic.Model.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Republic.BLL.Base
{
    public class BaseContextBLL<TEntity> : BaseBLL<TEntity, RepublicContext>, IBaseBLL
        where TEntity : BaseEntity
    {
        public readonly KartTuru KartTuru;
        public BaseContextBLL() { }
        public BaseContextBLL(KartTuru kartTuru) { KartTuru = kartTuru; }
        public virtual IEnumerable<TEntity> List(Expression<Func<TEntity, bool>> filter)
        {
            return base.BaseList(filter, x => x).ToList();

        }
        public virtual TEntity Single(Expression<Func<TEntity, bool>> filter)
        {
            return base.BaseSingle(filter, x => x);

        }
        public int Count(Expression<Func<TEntity, bool>> filter)
        {
            return base.BaseCount(filter);
        }

        public bool Delete(TEntity entity)
        {
            return base.BaseDelete(entity);

        }

        public bool Delete(IList<TEntity> entities)
        {
            return base.BaseDelete(entities);

        }
        public bool Insert(TEntity entity)
        {
            return base.BaseInsert(entity);

        }

        public bool Insert(IList<TEntity> entities)
        {
            return base.BaseInsert(entities);

        }





        public bool Update(TEntity oldEntity, TEntity currentEntity)
        {
            return base.BaseUpdate(oldEntity, currentEntity);

        }

        public bool Update(IList<TEntity> oldEntities, IList<TEntity> currentEntities)
        {
            return base.BaseUpdate(oldEntities, currentEntities);

        }
    }
}

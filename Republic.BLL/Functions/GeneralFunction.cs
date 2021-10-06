using Republic.DAL.Base;
using Republic.DAL.Interface;
using Republic.Model.Entities.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Republic.BLL.Functions
{
    public static class GeneralFunction
    {
        private static TContext CreateContext<TContext>() where TContext : DbContext
        {
            return (TContext)Activator.CreateInstance(typeof(TContext));
        }
        public static void CreateUnitOfWork<TEntity, TContext>(ref IUnitOfWork<TEntity> uow)
        where TEntity : class, IBaseEntity
        where TContext : DbContext
        {
            uow?.Dispose();
            uow = new UnitOfWork<TEntity>(CreateContext<TContext>());
        }
 
        public static IList<string> GetChangedFields<T>(this T oldEntity, T currentEntity)
        {
            IList<string> alanlar = new List<string>();

            currentEntity.GetType().GetProperties()
                .Where(x => x.PropertyType.Namespace != "System.Collections.Generic")//collection tipindeki propertiler dışındakilerde işlem yapılıcak
                .ToList()
                .ForEach(prop =>
                {
                    //null olan değerler karşılaştırma yapılamadığından.null ise stringe çek. 
                    var oldvalue = prop.GetValue(oldEntity) ?? string.Empty;
                    var curvalue = prop.GetValue(currentEntity) ?? string.Empty;
                    //byte tipindeyse [Resim Olabili]
                    if (prop.PropertyType == typeof(byte[]))
                    {
                        if (string.IsNullOrEmpty(oldvalue.ToString()))
                            oldvalue = new byte[] { 0 };
                        if (string.IsNullOrEmpty(curvalue.ToString()))
                            curvalue = new byte[] { 0 };
                        if (((byte[])oldvalue).Length != ((byte[])oldvalue).Length)
                            alanlar.Add(prop.Name);
                    }
                    else if (!curvalue.Equals(oldvalue)) // normal ise
                        alanlar.Add(prop.Name);
                });


            return alanlar;
        }
    }
}

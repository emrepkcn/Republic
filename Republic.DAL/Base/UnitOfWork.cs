using Republic.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Republic.DAL.Base
{
    public class UnitOfWork<TEntity> : IUnitOfWork<TEntity> where TEntity : class
    {
        private readonly DbContext Db;


        public IRepository<TEntity> Rep => new Repository<TEntity>(Db);

        public UnitOfWork(DbContext db)
        {
            this.Db = db;

        }

        public int ExecuteCommand(string cmd)
        {
            return Db.Database.ExecuteSqlCommand(cmd);

        }

        public bool Save()
        {
            try
            {
                return Db.SaveChanges() > 0;
            }
            catch (DbUpdateException ex)
            {
                var sqlexp = (SqlException)ex.InnerException?.InnerException;
                if (sqlexp == null)
                {
                    //Msg.HataMesajı(ex.Message);
                    return false;
                }
                switch (sqlexp.Number)
                {
                    case 208://tablo dbde yok
                        //Msg.HataMesajı("İşlem yapmış olduğunuz tablo veritabanında bulunamadı.");
                        break;
                    case 547://$"Seçilen kaydın işlem görmüş haraketleri var. Kayıt Silinemez!.\n{sqlexp?.Message}"
                        //Msg.HataMesajı($"Seçilen kaydın işlem görmüş haraketleri var. Kayıt Silinemez!.\n{sqlexp?.Message}");
                        break;
                    case 2601:
                    case 2627:
                        //Msg.HataMesajı("Aynı Kayıttan zaten var");
                        break;
                    case 4060:
                        //Msg.HataMesajı("işlem yapmak istediğiniz veritabanı Sunucuda Bulunamadı.");
                        break;
                    case 18456:
                        //Msg.HataMesajı("Servere Bağlanılmak istenilen kullanıcı adı veya şifre Hatalıdır.");
                        break;
                    default:
                        //Msg.HataMesajı(ex.Message);
                        break;
                }
                return false;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors.SelectMany((x) => x.ValidationErrors).Select((o) => o.ErrorMessage);
                var fullErrorMessage = string.Join("\n", errorMessages);
                //var exceptionMessage = string.Concat(ex.Message, " The validation errors are:\n ", fullErrorMessage);
                var exceptionMessage = string.Concat("", "", fullErrorMessage);

                //Msg.HataMesajı(exceptionMessage);
                return false;
                //throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
            catch (Exception ex)
            {
                //Msg.HataMesajı(ex.Message);
                return false;
            }
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

using Republic.Data.Migration;
using Republic.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Republic.Data.Context
{
    public class RepublicContext : BaseDbContext<RepublicContext, Configuartion>
    {
        public RepublicContext() : base() { }
        public RepublicContext(string constr) : base(constr) { }

        public DbSet<Rol> Rol { get; set; }
        public DbSet<RolYetkileri> RolYetkileri { get; set; }

        public DbSet<Kullanici> Kullanici { get; set; }




        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //ilişkiler burda düzenle silme
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Rol>()
                .HasMany(x => x.RolYetkileri)//rolün birden çok rolyetkileri vardır
                .WithRequired()
                .WillCascadeOnDelete(true); //rolün rolyetkilerini kademeli olarak sil
        }
    }
}

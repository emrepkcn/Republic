using Republic.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Republic.Data.Migration
{
    public class Configuartion : DbMigrationsConfiguration<RepublicContext>
    {
        public Configuartion()
        {
            //migrationa izin ver
            AutomaticMigrationsEnabled = true;
            
            // migration işlemi yaparken veri kaybına izin ver
            //-örn long tipinde alanını int tipine dönüştürdüğünde longda iken db de veri varsa int dönüşümünde veri kaybı olur.
            AutomaticMigrationDataLossAllowed = true;
        }
        protected override void Seed(RepublicContext context)
        {

            ///doldur

        }
    }
}

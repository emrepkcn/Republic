using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Republic.Data.Context
{
    public class BaseDbContext<TContext, TConfiguartion> : DbContext
        where TContext : DbContext
        where TConfiguartion : DbMigrationsConfiguration<TContext>, new()
    {
        public static string nameOrConnectionString { get; set; } = typeof(TContext).Name;
        public BaseDbContext() : base(nameOrConnectionString)
        {
            Init();
        }
        public BaseDbContext(string constring) : base(constring)
        {
            Init();
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TContext, TConfiguartion>());
            nameOrConnectionString = constring;

        }
        void Init()
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.AutoDetectChangesEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }
    }
}

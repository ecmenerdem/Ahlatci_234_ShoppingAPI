using Microsoft.EntityFrameworkCore;
using Shopping.DAL.Concrete.EntityFramework.Mapping;

namespace Shopping.DAL.Concrete.EntityFramework.Context
{
    public class ShoppingContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")=="Development")
                //{
                //    optionsBuilder.UseSqlServer("data source=.\\ZRVSQL2008;initial catalog=ShoppingDB;integrated security=True; TrustServerCertificate=true");
                //}

                //if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
                //{
                //    optionsBuilder.UseSqlServer("data source=.\\ZRVSQL2008;initial catalog=ShoppingDB;integrated security=True; TrustServerCertificate=true");
                //}

                optionsBuilder.UseSqlServer("data source=.\\ZRVSQL2008;initial catalog=Shopping234DB;integrated security=True; TrustServerCertificate=true");

            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new OrderDetailMap());
            modelBuilder.ApplyConfiguration(new OrderMap());
            modelBuilder.ApplyConfiguration(new UserMap());


            base.OnModelCreating(modelBuilder);
        }
    }
}

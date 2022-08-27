using System.Reflection;
using AktifBank.CustomerOrder.Shared.Entities;
using AktifBank.CustomerOrder.Shared.Entities.Base.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Omsan.DataAccess;

namespace AktifBank.CustomerOrder.DataAccess.Contexts
{
    public class ProjectDbContext : DbContext
    { 
        private IConfiguration Configuration { get; }

        public ProjectDbContext(IConfiguration configuration)
        { 
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                base.OnConfiguring(optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.SetCutomerOrderForeigKey();
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        } 
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            SetDefaultVal();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void SetDefaultVal()
        {
            var changeTrack = this.ChangeTracker.Entries()
                .Where(c => c.State == EntityState.Added | c.State == EntityState.Modified | c.State == EntityState.Deleted)
                .ToList();

            foreach (var entry in changeTrack)
            {

                if (!(entry.Entity is IHasDefaultFields))
                {
                    continue;
                }

                var track = entry.Entity as IHasDefaultFields;

                if (entry.State == EntityState.Added)
                {
                    track.AddTime = DateTime.Now;  
                }
                if (entry.State == EntityState.Modified)
                {
                    track.UpdateTime = DateTime.Now; 
                }
            }
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Shared.Entities.CustomerOrder> CustomerOrders { get; set; }
        public DbSet<CustomerOrderDetail> CustomerOrderDetails { get; set; }
    }
}

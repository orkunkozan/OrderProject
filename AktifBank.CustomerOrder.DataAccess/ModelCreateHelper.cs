using Microsoft.EntityFrameworkCore; 
using System.Reflection;
using AktifBank.CustomerOrder.Shared.Entities;

namespace Omsan.DataAccess
{
    public static class ModelCreateHelper
    { 

        public static void SetCutomerOrderForeigKey(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasMany(x => x.CustomerOrders).WithOne(x => x.Customer)
                .HasForeignKey(x => x.CustomerId);

            modelBuilder.Entity<CustomerOrder>().HasMany(x => x.CustomerOrderDetails).WithOne(x => x.CustomerOrder)
                .HasForeignKey(x => x.CustomerOrderId);  
        } 

    }
}

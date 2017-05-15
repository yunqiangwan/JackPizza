using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using JackPizza.Models;
using JackPizza.ViewModels;

namespace JackPizza.DAL
{
    public class JackPizzaContext:DbContext

    {
        public JackPizzaContext()         {

        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<CustomerOrder> CustomerOrders { get; set; }

        public DbSet<OrderedProduct> Orderedproducts { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<PizzaStore> PizzaStores { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Location> Locations { get; set; }

        public System.Data.Entity.DbSet<JackPizza.Models.SuburbStore> SuburbStores { get; set; }

        public System.Data.Entity.DbSet<JackPizza.Models.Message> Messages { get; set; }
    }
}
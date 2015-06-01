using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace StoreManager.Models
{
   
    public class SaleDBContext : DbContext
    {
        public DbSet<Product> vanzare { get; set; }
        public decimal PretTotal { get; set; }
    }
}
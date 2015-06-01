using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace StoreManager.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Nume { get; set; }
        public DateTime DataFabricatie { get; set; }
        public int Stoc { get; set; }
        public decimal Pret { get; set; }
    }
    public class ProductDBContext : DbContext
    {
        public DbSet<Product> produse { get; set; }
    }
}
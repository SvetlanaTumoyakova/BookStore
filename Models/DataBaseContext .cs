using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Models
{
    public class DataBaseContext : DbContext
    {
        private readonly string _connectionString;
        
        public DbSet<Product> Products { get; set; }
        public DbSet<PublisherHouse> PublisherHouses { get; set; }

        public DataBaseContext(string connectionString)
        {
            _connectionString = connectionString;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString).UseSnakeCaseNamingConvention();
        }
    }
}

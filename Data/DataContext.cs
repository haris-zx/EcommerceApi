using DummyProject.Models;
using Microsoft.EntityFrameworkCore;

namespace DummyProject.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Product> Product { get; set; }

        public DbSet<Category> Category { get; set; }

        public DbSet<User> main { get; set; }

        public DbSet<Cart> Cart { get; set; }
    }

}

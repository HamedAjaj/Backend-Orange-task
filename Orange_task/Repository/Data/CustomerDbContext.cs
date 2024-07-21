using Microsoft.EntityFrameworkCore;
using Orange_task.Domain.Entities;
using System.Reflection;

namespace Orange_task.Repository.Data
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options):base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Customer>  Customers { get; set; }
    }
}

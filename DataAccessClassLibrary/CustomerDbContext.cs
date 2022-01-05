using Microsoft.EntityFrameworkCore;


namespace DataAccessClassLibrary
{
    public class CustomerDbContext: DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public CustomerDbContext() 
        {

        }
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options)
            :base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("Customer");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=Test;Trusted_Connection=True");
        }

  
    }
}
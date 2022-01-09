using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Objects.DataClasses;

namespace WebApplication1.DataAccessLibrary
{
    public class CustomerDbContext : DbContext
    {
        public DbSet<CustomerModel> Customers { get; set; }

        public CustomerDbContext()
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerModel>().ToTable("Customer");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=Test;Trusted_Connection=True;");
        }


    }

    public class CustomerModel
    {
        [Key]
        public int CustomerId { get; set; }
        [Required]
        [StringLength(10)]
        [DisplayName("Customer Code")]
        public string? CustomerCode { get; set; }

        [Required]
        [StringLength(15)]
        [DisplayName("Customer Name")]
        public string? CustomerName { get; set; }
    }
}

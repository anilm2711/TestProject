using FnFManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace FnFManagement.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options ):base(options)
        {

        }

        public DbSet<Person> People { get; set; }
    }
}

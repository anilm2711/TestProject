using eBazar.Models;
using Microsoft.EntityFrameworkCore;

namespace eBazar.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options ):base(options)
        {
                
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor_Movie>().HasKey(p => new { p.ActorId, p.MovieId });
            modelBuilder.Entity<Actor_Movie>().HasOne(p=>p.Movie).WithMany(x=>x.Actor_Movies).HasForeignKey(z=>z.MovieId);
            modelBuilder.Entity<Actor_Movie>().HasOne(p => p.Actor).WithMany(x => x.Actor_Movies).HasForeignKey(z => z.ActorId);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Actor> Actors { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Actor_Movie> Actor_Movies { get; set; }


    }
}

using Microsoft.EntityFrameworkCore;
using MovieReviewApp.Models;

namespace MovieReviewApp.Data
{
    public class MovieReviewDbContext : DbContext
    {
        public MovieReviewDbContext(DbContextOptions<MovieReviewDbContext> options) : base(options) { }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<ShowTime> ShowTimes { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<PaymentGateway> PaymentGateways { get; set; }
        // public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PaymentGateway>().HasKey(pg => pg.GatewayId); // Define primary key explicitly
        }
    }
}

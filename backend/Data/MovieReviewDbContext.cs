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
        public DbSet<User> Users { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Set MovieId as Foreign Key on ShowTime Table
            modelBuilder.Entity<ShowTime>()
                .HasOne(t => t.Movie)
                .WithMany()
                .HasForeignKey(t => t.MovieId)
                .OnDelete(DeleteBehavior.Cascade); 

            //Set UserId as Foreign Key on ShowTime Table (not required, can be null)
            modelBuilder.Entity<Cart>()
                .HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.UserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict); 

            //Set ShowTimeId as Foreign Key on Ticket Table
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.ShowTime)
                .WithMany()
                .HasForeignKey(t => t.ShowTimeId)
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<PaymentGateway>().HasKey(pg => pg.GatewayId); // Define primary key explicitly

            //Set CartId as Foreign Key on Ticket Table (not required, can be null)
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Cart)
                .WithMany()
                .HasForeignKey(t => t.CartId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            //Set MovieId as Foreign Key on Review Table (not required, can be null)
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Movie)
                .WithMany()
                .HasForeignKey(r => r.MovieId)
                .IsRequired(false)  
                .OnDelete(DeleteBehavior.Cascade);       

            //Set UserId as Foreign Key on Review Table (not required, can be null)
            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .IsRequired(false)  
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Movie>().HasData(
                //Movie sample data provided by ChatGPT
                new Movie { MovieId = 1, Title = "John Wick: Chapter 4", Genre = "Action", Description = "John Wick uncovers a path to defeating The High Table, but before he can earn his freedom, he must face a new enemy with powerful alliances across the globe.", Rating = MPAARating.R },
                new Movie { MovieId = 2, Title = "Barbie", Genre = "Comedy", Description = "After being expelled from Barbieland for being a less-than-perfect doll, Barbie embarks on a journey of self-discovery in the real world.", Rating = MPAARating.PG },
                new Movie { MovieId = 3, Title = "Oppenheimer", Genre = "History", Description = "A dramatic portrayal of the life of J. Robert Oppenheimer and his role in the development of the atomic bomb.", Rating = MPAARating.R },
                new Movie { MovieId = 4, Title = "Guardians of the Galaxy Vol. 3", Genre = "Comedy", Description = "The Guardians must protect Rocket, who is in grave danger, while dealing with their pasts and new threats to the galaxy.", Rating = MPAARating.PG13 },
                new Movie { MovieId = 5, Title = "Mission: Impossible - Dead Reckoning Part One", Genre = "Action", Description = "Ethan Hunt and his IMF team must track down a dangerous new weapon before it falls into the wrong hands.", Rating = MPAARating.PG13 },
                new Movie { MovieId = 6, Title = "Spider-Man: Across the Spider-Verse", Genre = "Action", Description = "Miles Morales returns for a new adventure across the multiverse, meeting other Spider-People along the way.", Rating = MPAARating.PG },
                new Movie { MovieId = 7, Title = "The Marvels", Genre = "Action", Description = "Captain Marvel teams up with Ms. Marvel and Monica Rambeau to face a new cosmic threat.", Rating = MPAARating.PG },
                new Movie { MovieId = 8, Title = "The Super Mario Bros. Movie", Genre = "Adventure", Description = "Mario and Luigi embark on a journey through the Mushroom Kingdom to rescue Princess Peach from Bowser.", Rating = MPAARating.PG },
                new Movie { MovieId = 9, Title = "Evil Dead Rise", Genre = "Horror", Description = "Two estranged sisters must battle a group of flesh-possessing demons to save the family they never knew they had.", Rating = MPAARating.R },
                new Movie { MovieId = 10, Title = "Dune: Part Two", Genre = "Sci-Fi", Description = "Paul Atreides unites with Chani and the Fremen while seeking revenge against those who destroyed his family.", Rating = MPAARating.PG13 }
               );
            modelBuilder.Entity<ShowTime>().HasData(
                //Showtime sample data provided by ChatGPT
                new ShowTime { ShowTimeId = 1, MovieId = 1, ViewingTime = new DateTime(2024, 10, 29, 14, 0, 0), Status = MovieStatus.Available },
                new ShowTime { ShowTimeId = 2, MovieId = 1, ViewingTime = new DateTime(2024, 11, 1, 18, 30, 0), Status = MovieStatus.Available },
                new ShowTime { ShowTimeId = 3, MovieId = 2, ViewingTime = new DateTime(2024, 11, 2, 15, 0, 0), Status = MovieStatus.Available },
                new ShowTime { ShowTimeId = 4, MovieId = 3, ViewingTime = new DateTime(2024, 10, 30, 16, 15, 0), Status = MovieStatus.Available },
                new ShowTime { ShowTimeId = 5, MovieId = 3, ViewingTime = new DateTime(2024, 11, 4, 19, 0, 0), Status = MovieStatus.Available },
                new ShowTime { ShowTimeId = 6, MovieId = 4, ViewingTime = new DateTime(2024, 11, 6, 11, 30, 0), Status = MovieStatus.Available },
                new ShowTime { ShowTimeId = 7, MovieId = 5, ViewingTime = new DateTime(2024, 11, 12, 17, 0, 0), Status = MovieStatus.Available },
                new ShowTime { ShowTimeId = 8, MovieId = 5, ViewingTime = new DateTime(2024, 11, 14, 12, 30, 0), Status = MovieStatus.Available },
                new ShowTime { ShowTimeId = 9, MovieId = 6, ViewingTime = new DateTime(2024, 11, 1, 10, 0, 0), Status = MovieStatus.Available },
                new ShowTime { ShowTimeId = 10, MovieId = 7, ViewingTime = new DateTime(2024, 11, 5, 16, 0, 0), Status = MovieStatus.Available },
                new ShowTime { ShowTimeId = 11, MovieId = 8, ViewingTime = new DateTime(2024, 10, 31, 15, 0, 0), Status = MovieStatus.Available },
                new ShowTime { ShowTimeId = 12, MovieId = 8, ViewingTime = new DateTime(2024, 11, 3, 19, 0, 0), Status = MovieStatus.Available },
                new ShowTime { ShowTimeId = 13, MovieId = 9, ViewingTime = new DateTime(2024, 11, 8, 11, 0, 0), Status = MovieStatus.Available },
                new ShowTime { ShowTimeId = 14, MovieId = 10, ViewingTime = new DateTime(2024, 11, 10, 16, 30, 0), Status = MovieStatus.Available },
                new ShowTime { ShowTimeId = 15, MovieId = 10, ViewingTime = new DateTime(2024, 11, 19, 18, 0, 0), Status = MovieStatus.Available }
            );
            modelBuilder.Entity<Ticket>().HasData(
                //Ticket sample data provided by ChatGPT
                new Ticket { TicketId = 1, ShowTimeId = 1, Price = 12.5, Availability = true, CartId = 1 },
                new Ticket { TicketId = 2, ShowTimeId = 1, Price = 12.5, Availability = false, CartId = 1 },
                new Ticket { TicketId = 3, ShowTimeId = 2, Price = 14.0, Availability = true, CartId = 1 },
                new Ticket { TicketId = 4, ShowTimeId = 2, Price = 14.0, Availability = false, CartId = 1 },
                new Ticket { TicketId = 5, ShowTimeId = 2, Price = 14.0, Availability = false, CartId = 1 },
                new Ticket { TicketId = 6, ShowTimeId = 3, Price = 10.0, Availability = true, CartId = 2 },
                new Ticket { TicketId = 7, ShowTimeId = 4, Price = 15.0, Availability = true, CartId = 2 },
                new Ticket { TicketId = 8, ShowTimeId = 4, Price = 15.0, Availability = false, CartId = 2 },
                new Ticket { TicketId = 9, ShowTimeId = 4, Price = 15.0, Availability = false, CartId = 2 },
                new Ticket { TicketId = 10, ShowTimeId = 5, Price = 11.0, Availability = true, CartId = 2 },
                new Ticket { TicketId = 11, ShowTimeId = 5, Price = 11.0, Availability = false, CartId = 3 },
                new Ticket { TicketId = 12, ShowTimeId = 6, Price = 13.5, Availability = true, CartId = 3 },
                new Ticket { TicketId = 13, ShowTimeId = 6, Price = 13.5, Availability = false, CartId = 4 },
                new Ticket { TicketId = 14, ShowTimeId = 6, Price = 13.5, Availability = false, CartId = 4 },
                new Ticket { TicketId = 15, ShowTimeId = 7, Price = 12.0, Availability = true, CartId = 4 },
                new Ticket { TicketId = 16, ShowTimeId = 8, Price = 15.5, Availability = true, CartId = 4 },
                new Ticket { TicketId = 17, ShowTimeId = 8, Price = 15.5, Availability = false, CartId = 4 },
                new Ticket { TicketId = 18, ShowTimeId = 9, Price = 13.0, Availability = true, CartId = 4 },
                new Ticket { TicketId = 19, ShowTimeId = 9, Price = 13.0, Availability = false, CartId = 5 },
                new Ticket { TicketId = 20, ShowTimeId = 10, Price = 16.0, Availability = true, CartId = 1 },
                new Ticket { TicketId = 21, ShowTimeId = 10, Price = 16.0, Availability = false, CartId = 1 },
                new Ticket { TicketId = 22, ShowTimeId = 11, Price = 14.5, Availability = true, CartId = 2 },
                new Ticket { TicketId = 23, ShowTimeId = 11, Price = 14.5, Availability = false, CartId = 2 },
                new Ticket { TicketId = 24, ShowTimeId = 12, Price = 10.5, Availability = true, CartId = 1 },
                new Ticket { TicketId = 25, ShowTimeId = 12, Price = 10.5, Availability = false, CartId = 5 },
                new Ticket { TicketId = 26, ShowTimeId = 13, Price = 11.0, Availability = true, CartId = 5 },
                new Ticket { TicketId = 27, ShowTimeId = 14, Price = 12.0, Availability = true, CartId = 5 },
                new Ticket { TicketId = 28, ShowTimeId = 14, Price = 12.0, Availability = false, CartId = 5 },
                new Ticket { TicketId = 29, ShowTimeId = 14, Price = 12.0, Availability = false, CartId = 5 },
                new Ticket { TicketId = 30, ShowTimeId = 15, Price = 13.0, Availability = true, CartId = 5 },
                new Ticket { TicketId = 31, ShowTimeId = 15, Price = 13.0, Availability = false, CartId = 5 }
            );
            modelBuilder.Entity<Cart>().HasData(
                new Cart { CartId = 1, UserId = 1, Total = 50.0 },
                new Cart { CartId = 2, UserId = 2, Total = 75.5 },
                new Cart { CartId = 3, UserId = 3, Total = 100.0 },
                new Cart { CartId = 4, UserId = 4, Total = 0.0 },
                new Cart { CartId = 5, UserId = 5, Total = 20.0 }
            );

            // //Ticket sample review provided by ChatGPT
            modelBuilder.Entity<Review>().HasData(
                new Review { ReviewId = 1, MovieId = 1, UserId = 1, Content = "An incredible journey through dreams.", ReviewerName = "Anonymous", CreatedAt = DateTime.Now.AddDays(-10), Rating = 5 },
                new Review { ReviewId = 2, MovieId = 4, UserId = 1, Content = "A masterpiece in modern cinema.", ReviewerName = "johndoe", CreatedAt = DateTime.Now.AddDays(-9), Rating = 5 },
                new Review { ReviewId = 3, MovieId = 2, UserId = 2, Content = "Great for kids and adults alike.", ReviewerName = "Charlie", CreatedAt = DateTime.Now.AddDays(-5), Rating = 2 },
                new Review { ReviewId = 4, MovieId = 3, UserId = 3, Content = "A compelling story about loyalty and power.", ReviewerName = "Dave", CreatedAt = DateTime.Now.AddDays(-2), Rating = 3 },
                new Review { ReviewId = 5, MovieId = 3, UserId = 4, Content = "One of the best films ever made.", ReviewerName = "Eve", CreatedAt = DateTime.Now.AddDays(-1), Rating = 5 }
            );
            modelBuilder.Entity<User>().HasData(
            new User{
                UserId = 1,
                Email = "john.doe@example.com",
                Username = "johndoe",
                FirstName = "John",
                LastName = "Doe",
                Password = "d1f23b1a4e5f6a7b8c9d0e",
                NotiPreference = UserPreference.SMS
            },
            new User{
                UserId = 2,
                Email = "jane.smith@example.com",
                Username = "janesmith",
                FirstName = "Jane",
                LastName = "Smith",
                Password = "e2f34c1b5g6h7i8j9k0l1m",
                NotiPreference = UserPreference.Email
            },
            new User{
                UserId = 3,
                Email = "michael.jones@example.com",
                Username = "mikejones",
                FirstName = "Michael",
                LastName = "Jones",
                Password = "f3g45d2e6h7i8j9k0l1m2n",
                NotiPreference = UserPreference.Email
            },
            new User{
                UserId = 4,
                Email = "sarah.connor@example.com",
                Username = "sconnor",
                FirstName = "Sarah",
                LastName = "Connor",
                Password = "g4h56e3f7i8j9k0l1m2n3o",
                NotiPreference = UserPreference.Email
            },
            new User{
                UserId = 5,
                Email = "david.lee@example.com",
                Username = "dlee",
                FirstName = "David",
                LastName = "Lee",
                Password = "h5i67f4g8j9k0l1m2n3o4p",
                NotiPreference = UserPreference.Email
            }
            );
        }
    }
}

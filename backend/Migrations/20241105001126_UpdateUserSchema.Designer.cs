﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MovieReviewApp.Data;

#nullable disable

namespace MovieReviewApp.Migrations
{
    [DbContext(typeof(MovieReviewDbContext))]
    [Migration("20241105001126_UpdateUserSchema")]
    partial class UpdateUserSchema
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MovieReviewApp.Models.Cart", b =>
                {
                    b.Property<int>("CartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CartId"));

                    b.Property<double>("Total")
                        .HasColumnType("float");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("CartId");

                    b.HasIndex("UserId");

                    b.ToTable("Carts");

                    b.HasData(
                        new
                        {
                            CartId = 1,
                            Total = 50.0,
                            UserId = 1
                        },
                        new
                        {
                            CartId = 2,
                            Total = 75.5,
                            UserId = 2
                        },
                        new
                        {
                            CartId = 3,
                            Total = 100.0,
                            UserId = 3
                        },
                        new
                        {
                            CartId = 4,
                            Total = 0.0,
                            UserId = 4
                        },
                        new
                        {
                            CartId = 5,
                            Total = 20.0,
                            UserId = 5
                        });
                });

            modelBuilder.Entity("MovieReviewApp.Models.Movie", b =>
                {
                    b.Property<int>("MovieId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MovieId"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Genre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MovieId");

                    b.ToTable("Movies");

                    b.HasData(
                        new
                        {
                            MovieId = 1,
                            Description = "John Wick uncovers a path to defeating The High Table, but before he can earn his freedom, he must face a new enemy with powerful alliances across the globe.",
                            Genre = "Action",
                            Rating = 3,
                            Title = "John Wick: Chapter 4"
                        },
                        new
                        {
                            MovieId = 2,
                            Description = "After being expelled from Barbieland for being a less-than-perfect doll, Barbie embarks on a journey of self-discovery in the real world.",
                            Genre = "Comedy",
                            Rating = 1,
                            Title = "Barbie"
                        },
                        new
                        {
                            MovieId = 3,
                            Description = "A dramatic portrayal of the life of J. Robert Oppenheimer and his role in the development of the atomic bomb.",
                            Genre = "History",
                            Rating = 3,
                            Title = "Oppenheimer"
                        },
                        new
                        {
                            MovieId = 4,
                            Description = "The Guardians must protect Rocket, who is in grave danger, while dealing with their pasts and new threats to the galaxy.",
                            Genre = "Comedy",
                            Rating = 2,
                            Title = "Guardians of the Galaxy Vol. 3"
                        },
                        new
                        {
                            MovieId = 5,
                            Description = "Ethan Hunt and his IMF team must track down a dangerous new weapon before it falls into the wrong hands.",
                            Genre = "Action",
                            Rating = 2,
                            Title = "Mission: Impossible - Dead Reckoning Part One"
                        },
                        new
                        {
                            MovieId = 6,
                            Description = "Miles Morales returns for a new adventure across the multiverse, meeting other Spider-People along the way.",
                            Genre = "Action",
                            Rating = 1,
                            Title = "Spider-Man: Across the Spider-Verse"
                        },
                        new
                        {
                            MovieId = 7,
                            Description = "Captain Marvel teams up with Ms. Marvel and Monica Rambeau to face a new cosmic threat.",
                            Genre = "Action",
                            Rating = 1,
                            Title = "The Marvels"
                        },
                        new
                        {
                            MovieId = 8,
                            Description = "Mario and Luigi embark on a journey through the Mushroom Kingdom to rescue Princess Peach from Bowser.",
                            Genre = "Adventure",
                            Rating = 1,
                            Title = "The Super Mario Bros. Movie"
                        },
                        new
                        {
                            MovieId = 9,
                            Description = "Two estranged sisters must battle a group of flesh-possessing demons to save the family they never knew they had.",
                            Genre = "Horror",
                            Rating = 3,
                            Title = "Evil Dead Rise"
                        },
                        new
                        {
                            MovieId = 10,
                            Description = "Paul Atreides unites with Chani and the Fremen while seeking revenge against those who destroyed his family.",
                            Genre = "Sci-Fi",
                            Rating = 2,
                            Title = "Dune: Part Two"
                        });
                });

            modelBuilder.Entity("MovieReviewApp.Models.PaymentGateway", b =>
                {
                    b.Property<int>("GatewayId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GatewayId"));

                    b.Property<int>("CVC")
                        .HasColumnType("int");

                    b.Property<string>("CardHolderName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CardNumber")
                        .HasColumnType("int");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.HasKey("GatewayId");

                    b.ToTable("PaymentGateways");
                });

            modelBuilder.Entity("MovieReviewApp.Models.ShowTime", b =>
                {
                    b.Property<int>("ShowTimeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ShowTimeId"));

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("ViewingTime")
                        .HasColumnType("datetime2");

                    b.HasKey("ShowTimeId");

                    b.HasIndex("MovieId");

                    b.ToTable("ShowTimes");

                    b.HasData(
                        new
                        {
                            ShowTimeId = 1,
                            MovieId = 1,
                            Status = 0,
                            ViewingTime = new DateTime(2024, 10, 29, 14, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            ShowTimeId = 2,
                            MovieId = 1,
                            Status = 0,
                            ViewingTime = new DateTime(2024, 11, 1, 18, 30, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            ShowTimeId = 3,
                            MovieId = 2,
                            Status = 0,
                            ViewingTime = new DateTime(2024, 11, 2, 15, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            ShowTimeId = 4,
                            MovieId = 3,
                            Status = 0,
                            ViewingTime = new DateTime(2024, 10, 30, 16, 15, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            ShowTimeId = 5,
                            MovieId = 3,
                            Status = 0,
                            ViewingTime = new DateTime(2024, 11, 4, 19, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            ShowTimeId = 6,
                            MovieId = 4,
                            Status = 0,
                            ViewingTime = new DateTime(2024, 11, 6, 11, 30, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            ShowTimeId = 7,
                            MovieId = 5,
                            Status = 0,
                            ViewingTime = new DateTime(2024, 11, 12, 17, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            ShowTimeId = 8,
                            MovieId = 5,
                            Status = 0,
                            ViewingTime = new DateTime(2024, 11, 14, 12, 30, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            ShowTimeId = 9,
                            MovieId = 6,
                            Status = 0,
                            ViewingTime = new DateTime(2024, 11, 1, 10, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            ShowTimeId = 10,
                            MovieId = 7,
                            Status = 0,
                            ViewingTime = new DateTime(2024, 11, 5, 16, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            ShowTimeId = 11,
                            MovieId = 8,
                            Status = 0,
                            ViewingTime = new DateTime(2024, 10, 31, 15, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            ShowTimeId = 12,
                            MovieId = 8,
                            Status = 0,
                            ViewingTime = new DateTime(2024, 11, 3, 19, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            ShowTimeId = 13,
                            MovieId = 9,
                            Status = 0,
                            ViewingTime = new DateTime(2024, 11, 8, 11, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            ShowTimeId = 14,
                            MovieId = 10,
                            Status = 0,
                            ViewingTime = new DateTime(2024, 11, 10, 16, 30, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            ShowTimeId = 15,
                            MovieId = 10,
                            Status = 0,
                            ViewingTime = new DateTime(2024, 11, 19, 18, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("MovieReviewApp.Models.Ticket", b =>
                {
                    b.Property<int>("TicketId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TicketId"));

                    b.Property<bool>("Availability")
                        .HasColumnType("bit");

                    b.Property<int?>("CartId")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("ShowTimeId")
                        .HasColumnType("int");

                    b.HasKey("TicketId");

                    b.HasIndex("CartId");

                    b.HasIndex("ShowTimeId");

                    b.ToTable("Tickets");

                    b.HasData(
                        new
                        {
                            TicketId = 1,
                            Availability = true,
                            CartId = 1,
                            Price = 12.5,
                            Quantity = 1,
                            ShowTimeId = 1
                        },
                        new
                        {
                            TicketId = 2,
                            Availability = false,
                            CartId = 1,
                            Price = 12.5,
                            Quantity = 1,
                            ShowTimeId = 1
                        },
                        new
                        {
                            TicketId = 3,
                            Availability = true,
                            CartId = 1,
                            Price = 14.0,
                            Quantity = 1,
                            ShowTimeId = 2
                        },
                        new
                        {
                            TicketId = 4,
                            Availability = false,
                            CartId = 1,
                            Price = 14.0,
                            Quantity = 1,
                            ShowTimeId = 2
                        },
                        new
                        {
                            TicketId = 5,
                            Availability = false,
                            CartId = 1,
                            Price = 14.0,
                            Quantity = 1,
                            ShowTimeId = 2
                        },
                        new
                        {
                            TicketId = 6,
                            Availability = true,
                            CartId = 2,
                            Price = 10.0,
                            Quantity = 1,
                            ShowTimeId = 3
                        },
                        new
                        {
                            TicketId = 7,
                            Availability = true,
                            CartId = 2,
                            Price = 15.0,
                            Quantity = 1,
                            ShowTimeId = 4
                        },
                        new
                        {
                            TicketId = 8,
                            Availability = false,
                            CartId = 2,
                            Price = 15.0,
                            Quantity = 1,
                            ShowTimeId = 4
                        },
                        new
                        {
                            TicketId = 9,
                            Availability = false,
                            CartId = 2,
                            Price = 15.0,
                            Quantity = 1,
                            ShowTimeId = 4
                        },
                        new
                        {
                            TicketId = 10,
                            Availability = true,
                            CartId = 2,
                            Price = 11.0,
                            Quantity = 1,
                            ShowTimeId = 5
                        },
                        new
                        {
                            TicketId = 11,
                            Availability = false,
                            CartId = 3,
                            Price = 11.0,
                            Quantity = 1,
                            ShowTimeId = 5
                        },
                        new
                        {
                            TicketId = 12,
                            Availability = true,
                            CartId = 3,
                            Price = 13.5,
                            Quantity = 1,
                            ShowTimeId = 6
                        },
                        new
                        {
                            TicketId = 13,
                            Availability = false,
                            CartId = 4,
                            Price = 13.5,
                            Quantity = 1,
                            ShowTimeId = 6
                        },
                        new
                        {
                            TicketId = 14,
                            Availability = false,
                            CartId = 4,
                            Price = 13.5,
                            Quantity = 1,
                            ShowTimeId = 6
                        },
                        new
                        {
                            TicketId = 15,
                            Availability = true,
                            CartId = 4,
                            Price = 12.0,
                            Quantity = 1,
                            ShowTimeId = 7
                        },
                        new
                        {
                            TicketId = 16,
                            Availability = true,
                            CartId = 4,
                            Price = 15.5,
                            Quantity = 1,
                            ShowTimeId = 8
                        },
                        new
                        {
                            TicketId = 17,
                            Availability = false,
                            CartId = 4,
                            Price = 15.5,
                            Quantity = 1,
                            ShowTimeId = 8
                        },
                        new
                        {
                            TicketId = 18,
                            Availability = true,
                            CartId = 4,
                            Price = 13.0,
                            Quantity = 1,
                            ShowTimeId = 9
                        },
                        new
                        {
                            TicketId = 19,
                            Availability = false,
                            CartId = 5,
                            Price = 13.0,
                            Quantity = 1,
                            ShowTimeId = 9
                        },
                        new
                        {
                            TicketId = 20,
                            Availability = true,
                            CartId = 1,
                            Price = 16.0,
                            Quantity = 1,
                            ShowTimeId = 10
                        },
                        new
                        {
                            TicketId = 21,
                            Availability = false,
                            CartId = 1,
                            Price = 16.0,
                            Quantity = 1,
                            ShowTimeId = 10
                        },
                        new
                        {
                            TicketId = 22,
                            Availability = true,
                            CartId = 2,
                            Price = 14.5,
                            Quantity = 1,
                            ShowTimeId = 11
                        },
                        new
                        {
                            TicketId = 23,
                            Availability = false,
                            CartId = 2,
                            Price = 14.5,
                            Quantity = 1,
                            ShowTimeId = 11
                        },
                        new
                        {
                            TicketId = 24,
                            Availability = true,
                            CartId = 1,
                            Price = 10.5,
                            Quantity = 1,
                            ShowTimeId = 12
                        },
                        new
                        {
                            TicketId = 25,
                            Availability = false,
                            CartId = 5,
                            Price = 10.5,
                            Quantity = 1,
                            ShowTimeId = 12
                        },
                        new
                        {
                            TicketId = 26,
                            Availability = true,
                            CartId = 5,
                            Price = 11.0,
                            Quantity = 1,
                            ShowTimeId = 13
                        },
                        new
                        {
                            TicketId = 27,
                            Availability = true,
                            CartId = 5,
                            Price = 12.0,
                            Quantity = 1,
                            ShowTimeId = 14
                        },
                        new
                        {
                            TicketId = 28,
                            Availability = false,
                            CartId = 5,
                            Price = 12.0,
                            Quantity = 1,
                            ShowTimeId = 14
                        },
                        new
                        {
                            TicketId = 29,
                            Availability = false,
                            CartId = 5,
                            Price = 12.0,
                            Quantity = 1,
                            ShowTimeId = 14
                        },
                        new
                        {
                            TicketId = 30,
                            Availability = true,
                            CartId = 5,
                            Price = 13.0,
                            Quantity = 1,
                            ShowTimeId = 15
                        },
                        new
                        {
                            TicketId = 31,
                            Availability = false,
                            CartId = 5,
                            Price = 13.0,
                            Quantity = 1,
                            ShowTimeId = 15
                        });
                });

            modelBuilder.Entity("MovieReviewApp.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NotiPreference")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            Email = "john.doe@example.com",
                            FirstName = "John",
                            LastName = "Doe",
                            NotiPreference = 0,
                            Password = "d1f23b1a4e5f6a7b8c9d0e",
                            Username = "johndoe"
                        },
                        new
                        {
                            UserId = 2,
                            Email = "jane.smith@example.com",
                            FirstName = "Jane",
                            LastName = "Smith",
                            NotiPreference = 1,
                            Password = "e2f34c1b5g6h7i8j9k0l1m",
                            Username = "janesmith"
                        },
                        new
                        {
                            UserId = 3,
                            Email = "michael.jones@example.com",
                            FirstName = "Michael",
                            LastName = "Jones",
                            NotiPreference = 1,
                            Password = "f3g45d2e6h7i8j9k0l1m2n",
                            Username = "mikejones"
                        },
                        new
                        {
                            UserId = 4,
                            Email = "sarah.connor@example.com",
                            FirstName = "Sarah",
                            LastName = "Connor",
                            NotiPreference = 1,
                            Password = "g4h56e3f7i8j9k0l1m2n3o",
                            Username = "sconnor"
                        },
                        new
                        {
                            UserId = 5,
                            Email = "david.lee@example.com",
                            FirstName = "David",
                            LastName = "Lee",
                            NotiPreference = 1,
                            Password = "h5i67f4g8j9k0l1m2n3o4p",
                            Username = "dlee"
                        });
                });

            modelBuilder.Entity("MovieReviewApp.Models.Cart", b =>
                {
                    b.HasOne("MovieReviewApp.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("User");
                });

            modelBuilder.Entity("MovieReviewApp.Models.ShowTime", b =>
                {
                    b.HasOne("MovieReviewApp.Models.Movie", "Movie")
                        .WithMany()
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("MovieReviewApp.Models.Ticket", b =>
                {
                    b.HasOne("MovieReviewApp.Models.Cart", "Cart")
                        .WithMany()
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MovieReviewApp.Models.ShowTime", "ShowTime")
                        .WithMany()
                        .HasForeignKey("ShowTimeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cart");

                    b.Navigation("ShowTime");
                });
#pragma warning restore 612, 618
        }
    }
}
using CozyHaven.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace CozyHaven.Context
{
    public class RequestTrackerContext : DbContext
    {
        public RequestTrackerContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Admin>? Admins { get; set; }
        public DbSet<Amenity>? Amenities { get; set; }
        public DbSet<Hotel>? Hotels { get; set; }
        public DbSet<HotelOwner> HotelOwners { get; set; }
        public DbSet<Payment>? Payments { get; set; }
        public DbSet<Reservation>? Reservations { get; set; }
        public DbSet<Review>? Reviews { get; set; }
        public DbSet<Room>? Rooms { get; set; }
        public DbSet<User>? ApplicationUsers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships

            // User - Admin (One-to-One)
            modelBuilder.Entity<Admin>()
                .HasOne(u => u.User)
                .WithMany(a => a.Admin)
                .HasForeignKey("UserID");

            modelBuilder.Entity<Hotel>()
                .HasOne(h => h.HotelOwner)
                .WithMany(o => o.Hotels)
                .HasForeignKey("HotelOwnerID"); // Prevent cascade delete

            modelBuilder.Entity<HotelAmenity>()
                .HasMany<Hotel>(ha => ha.Hotels)
                .WithMany(h => h.HotelAmenities);
            //.HasForeignKey("HotelID");

            modelBuilder.Entity<HotelAmenity>()
                .HasMany(h => h.Hotels)
                .WithMany(h => h.HotelAmenities);
            //.HasForeignKey("AmenityID");

            modelBuilder.Entity<HotelOwner>()
                .HasOne(u => u.User)
                .WithMany(o => o.HotelOwners);
            //.HasForeignKey<HotelOwner>(o => o.UserID);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Reservation)
                .WithOne(r => r.Payment)
                .HasForeignKey<Payment>("ReservationID");

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey("UserID");

            // Reservation - Room (Many-to-One)
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Room)
                .WithMany(r => r.Reservations)
                .HasForeignKey("RoomID");

            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey("UserID");

            // Review - Hotel (Many-to-One)
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Hotel)
                .WithMany(h => h.Reviews)
                .HasForeignKey("HotelID");

            // Hotel - Room (One-to-Many)
            modelBuilder.Entity<Room>()
                .HasOne(h => h.Hotel)
                .WithMany(r => r.Rooms)
                .HasForeignKey("HotelID");
        
        }
    }
}

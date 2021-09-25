using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingApplication.Models;

namespace BookingApplication.Data
{
    public class BookingContext : DbContext
    {
        public BookingContext(DbContextOptions<BookingContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<BookingApplication.Models.Hotel> Hotel { get; set; }

        public DbSet<BookingApplication.Models.HotelPhoto> HotelPhoto { get; set; }

        public DbSet<BookingApplication.Models.Apartment> Apartment { get; set; }

        public DbSet<BookingApplication.Models.ApartmentPhoto> ApartmentPhoto { get; set; }

        public DbSet<BookingApplication.Models.Reservation> Reservation { get; set; }

    }
}

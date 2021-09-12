using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApplication.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public string Location { get; set; }
        [Range(1, 10)]
        public double Rate { get; set; }
        [Range(1,5)]
        public int Stars { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string Region { get; set; }
        [Required]
        public string City { get; set; }

        public List<Apartment> Apartments { get; set; }
        public List<HotelPhoto> HotelPhotos { get; set; }
    }
}

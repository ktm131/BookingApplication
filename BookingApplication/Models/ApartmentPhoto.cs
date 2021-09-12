using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApplication.Models
{
    public class ApartmentPhoto
    {
        public int Id { get; set; }
        public int ApartmentId { get; set; }
        [Required]
        public string Photo { get; set; }
        [Required]
        public string Title { get; set; }

        public Apartment Apartment { get; set; }
    }
}

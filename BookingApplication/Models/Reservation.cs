using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApplication.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int ApartmentId { get; set; }
        public string UserId { get; set; }
        [Required]
        public string Surname { get; set; }
        [Phone]
        public string Phone { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [Range(1,20)]
        public int People { get; set; }

        public Apartment Apartment { get; set; }
    }
}

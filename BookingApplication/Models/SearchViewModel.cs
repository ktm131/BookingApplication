using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApplication.Models
{
    public class SearchViewModel
    {
        //main
        [Required]
        public string Name { get; set; }
        [Range(1, 20)]
        public int People { get; set; } = 1;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ApartmentId { get; set; }

        //additional filters
        public int Beds { get; set; }
        public double MinRate { get; set; }
        public int MinStars { get; set; }        
        public bool Balcony { get; set; }
        public bool PrivateBathroom { get; set; }
        public bool Kitchen { get; set; }
        public bool Tv { get; set; }
        public decimal MaxNightPrice { get; set; }

        public List<Hotel> Hotels { get; set; }
    }
}

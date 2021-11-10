using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApplication.Models
{
    public class Apartment
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        [Required]
        public string Name { get; set; }
        [Range(1,10)]
        public int Rooms { get; set; }
        [Range(1, 10)]
        public int People { get; set; }
        [Range(1, 10)]
        public int Beds { get; set; }
        public bool Balcony { get; set; }      
        public bool PrivateBathroom { get; set; }
        public bool Kitchen { get; set; }
        public bool Tv { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal NightPrice { get; set; }

        public Hotel Hotel { get; set; }
        public List<ApartmentPhoto> ApartmentPhotos { get; set; }
        public List<Reservation> Reservations { get; set; }

        [NotMapped]
        public string PeopleTooltipText
        {
            get
            {
                if (this.People == 1)
                {
                    return $"For {this.People} person";
                }
                else
                {
                    return $"For {this.People} people";
                }
            }
        }

        [NotMapped]
        public string BedsTooltipText
        {
            get
            {
                return $"Number of beds: {this.Beds}";
            }
        }
    }
}

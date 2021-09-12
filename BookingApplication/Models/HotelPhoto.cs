using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApplication.Models
{
    public class HotelPhoto
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public string Photo { get; set; }
        public string Title { get; set; }

        public Hotel Hotel { get; set; }
    }
}

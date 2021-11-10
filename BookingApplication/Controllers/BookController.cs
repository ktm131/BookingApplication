using BookingApplication.Data;
using BookingApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApplication.Controllers
{
    public class BookController : Controller
    {
        private readonly BookingContext _context;

        public BookController(BookingContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search()
        {
            return View(new SearchViewModel() { StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(1) });
        }

        [HttpPost]
        public async Task<IActionResult> Search(SearchViewModel model)
        {
            if (ModelState.IsValid)
            {
                var hotels = await _context.Hotel
                    .Where(w =>
                    (w.Name.ToLower().Contains(model.Name.ToLower())
                    || w.City.ToLower().Contains(model.Name.ToLower())
                    || w.Country.ToLower().Contains(model.Name.ToLower()))
                    && w.Rate >= model.MinRate
                    && w.Stars >= model.MinStars)
                    .Include(i => i.HotelPhotos)
                    .Include(i => i.Apartments.Where(w => 
                    w.People >= model.People
                    && w.Beds >= model.Beds
                    && (!model.Balcony || w.Balcony)
                    && (!model.Kitchen || w.Kitchen)
                    && (!model.Tv || w.Tv)
                    && (model.MaxNightPrice == 0 || w.NightPrice <= model.MaxNightPrice))
                    .OrderBy(o=>o.NightPrice))
                    .ThenInclude(i => i.Reservations.Where(w => w.StartDate < model.EndDate && model.StartDate < w.EndDate))
                    .Where(w => w.Apartments.Where(w => w.Reservations.Count == 0).Any())
                    .ToListAsync();

                foreach(var hotel in hotels)
                {
                    foreach(var apartment in hotel.Apartments)
                    {
                        apartment.ApartmentPhotos = await _context.ApartmentPhoto.Where(w => w.ApartmentId == apartment.Id).ToListAsync();
                    }
                }

                model.Hotels = hotels;

                return View(model);
            }
            else
            {
                return View(model);
            }
        }

    }
}


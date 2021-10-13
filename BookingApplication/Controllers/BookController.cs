﻿using BookingApplication.Data;
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
                    .Include(i => i.Apartments.Where(w=>w.People>=model.People 
                    && w.Beds >= model.Beds 
                    && (!model.Balcony || w.Balcony) 
                    && (!model.Kitchen || w.Kitchen) 
                    && (!model.Tv || w.Tv)
                    && (model.MaxNightPrice == 0 || w.NightPrice <= model.MaxNightPrice)))               
                    .ThenInclude(i=>i.ApartmentPhotos)
                    .Where(w => w.Apartments.Any())
                    .ToListAsync();

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


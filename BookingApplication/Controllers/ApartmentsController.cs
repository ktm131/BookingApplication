using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookingApplication.Data;
using BookingApplication.Models;
using Microsoft.AspNetCore.Authorization;

namespace BookingApplication.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ApartmentsController : Controller
    {
        private readonly BookingContext _context;

        public ApartmentsController(BookingContext context)
        {
            _context = context;
        }

        // GET: Apartments
        public async Task<IActionResult> Index(int id)
        {
            var bookingContext = _context.Apartment.Include(a => a.Hotel).Include(i=>i.ApartmentPhotos).Where(w=>w.HotelId==id);
            ViewData["hid"] = id;
            return View(await bookingContext.ToListAsync());
        }

        // GET: Apartments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartment = await _context.Apartment
                .Include(a => a.Hotel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (apartment == null)
            {
                return NotFound();
            }

            return View(apartment);
        }

        // GET: Apartments/Create
        public IActionResult Create(int id)
        {
            Apartment apartment = new Apartment() { HotelId = id };
            ViewData["HotelId"] = new SelectList(_context.Hotel, "Id", "Name");
            return View(apartment);
        }

        // POST: Apartments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HotelId,Name,Rooms,People,Beds,Balcony,PrivateBathroom,Kitchen,Tv,NightPrice")] Apartment apartment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(apartment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id = apartment.HotelId });
            }
            ViewData["HotelId"] = new SelectList(_context.Hotel, "Id", "Name", apartment.HotelId);
            return View(apartment);
        }

        // GET: Apartments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartment = await _context.Apartment.FindAsync(id);
            if (apartment == null)
            {
                return NotFound();
            }
            ViewData["HotelId"] = new SelectList(_context.Hotel, "Id", "Name", apartment.HotelId);
            return View(apartment);
        }

        // POST: Apartments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HotelId,Name,Rooms,People,Beds,Balcony,PrivateBathroom,Kitchen,Tv,NightPrice")] Apartment apartment)
        {
            if (id != apartment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(apartment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApartmentExists(apartment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { id = apartment.HotelId });
            }
            ViewData["HotelId"] = new SelectList(_context.Hotel, "Id", "Name", apartment.HotelId);
            return View(apartment);
        }

        // GET: Apartments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartment = await _context.Apartment
                .Include(a => a.Hotel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (apartment == null)
            {
                return NotFound();
            }

            return View(apartment);
        }

        // POST: Apartments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var apartment = await _context.Apartment.FindAsync(id);
            int hotelId = apartment.HotelId;
            _context.Apartment.Remove(apartment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index),new { id = hotelId });
        }

        private bool ApartmentExists(int id)
        {
            return _context.Apartment.Any(e => e.Id == id);
        }
    }
}

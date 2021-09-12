using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookingApplication.Data;
using BookingApplication.Models;

namespace BookingApplication.Controllers
{
    public class HotelPhotoesController : Controller
    {
        private readonly BookingContext _context;

        public HotelPhotoesController(BookingContext context)
        {
            _context = context;
        }

        // GET: HotelPhotoes
        public async Task<IActionResult> Index(int id)
        {
            var bookingContext = _context.HotelPhoto.Include(h => h.Hotel).Where(w => w.HotelId == id);
            ViewData["hid"] = id;
            return View(await bookingContext.ToListAsync());
        }

        // GET: HotelPhotoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotelPhoto = await _context.HotelPhoto
                .Include(h => h.Hotel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hotelPhoto == null)
            {
                return NotFound();
            }

            return View(hotelPhoto);
        }

        // GET: HotelPhotoes/Create
        public IActionResult Create(int id)
        {
            HotelPhoto hotelPhoto = new HotelPhoto() { HotelId = id };
            ViewData["HotelId"] = new SelectList(_context.Hotel, "Id", "Name");
            return View(hotelPhoto);
        }

        // POST: HotelPhotoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HotelId,Photo,Title")] HotelPhoto hotelPhoto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hotelPhoto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id = hotelPhoto.HotelId });
            }
            ViewData["HotelId"] = new SelectList(_context.Hotel, "Id", "City", hotelPhoto.HotelId);
            return View(hotelPhoto);
        }

        // GET: HotelPhotoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotelPhoto = await _context.HotelPhoto
                .Include(h => h.Hotel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hotelPhoto == null)
            {
                return NotFound();
            }

            return View(hotelPhoto);
        }

        // POST: HotelPhotoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hotelPhoto = await _context.HotelPhoto.FindAsync(id);
            int hotelId = hotelPhoto.HotelId;
            _context.HotelPhoto.Remove(hotelPhoto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index),new { id = hotelId });
        }

        private bool HotelPhotoExists(int id)
        {
            return _context.HotelPhoto.Any(e => e.Id == id);
        }
    }
}

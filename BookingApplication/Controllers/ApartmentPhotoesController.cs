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
    public class ApartmentPhotoesController : Controller
    {
        private readonly BookingContext _context;

        public ApartmentPhotoesController(BookingContext context)
        {
            _context = context;
        }

        // GET: ApartmentPhotoes
        public async Task<IActionResult> Index(int id)
        {
            var bookingContext = _context.ApartmentPhoto.Include(a => a.Apartment).Where(w=>w.ApartmentId==id);
            ViewData["aid"] = id;
            return View(await bookingContext.ToListAsync());
        }

        // GET: ApartmentPhotoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartmentPhoto = await _context.ApartmentPhoto
                .Include(a => a.Apartment)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (apartmentPhoto == null)
            {
                return NotFound();
            }

            return View(apartmentPhoto);
        }

        // GET: ApartmentPhotoes/Create
        public IActionResult Create(int id)
        {
            ApartmentPhoto apartmentPhoto = new ApartmentPhoto() { ApartmentId = id };
            ViewData["ApartmentId"] = new SelectList(_context.Apartment, "Id", "Name");
            return View(apartmentPhoto);
        }

        // POST: ApartmentPhotoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApartmentId,Photo,Title")] ApartmentPhoto apartmentPhoto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(apartmentPhoto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index),new { id = apartmentPhoto.ApartmentId });
            }
            ViewData["ApartmentId"] = new SelectList(_context.Apartment, "Id", "Name", apartmentPhoto.ApartmentId);
            return View(apartmentPhoto);
        }

        // GET: ApartmentPhotoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartmentPhoto = await _context.ApartmentPhoto
                .Include(a => a.Apartment)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (apartmentPhoto == null)
            {
                return NotFound();
            }

            return View(apartmentPhoto);
        }

        // POST: ApartmentPhotoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var apartmentPhoto = await _context.ApartmentPhoto.FindAsync(id);
            int apartmentId = apartmentPhoto.ApartmentId;
            _context.ApartmentPhoto.Remove(apartmentPhoto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = apartmentId });
        }

        private bool ApartmentPhotoExists(int id)
        {
            return _context.ApartmentPhoto.Any(e => e.Id == id);
        }
    }
}

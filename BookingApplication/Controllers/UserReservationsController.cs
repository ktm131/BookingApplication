using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookingApplication.Data;
using BookingApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace BookingApplication.Controllers
{
    [Authorize]
    public class UserReservationsController : Controller
    {
        private readonly BookingContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public UserReservationsController(BookingContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: UserReservations
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            var bookingContext = _context.Reservation.Where(w=>w.UserId == user.Id).Include(r => r.Apartment).ThenInclude(i => i.Hotel);
            return View(await bookingContext.ToListAsync());
        }

        // GET: UserReservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);

            var reservation = await _context.Reservation
                .Where(w => w.UserId == user.Id)
                .Include(r => r.Apartment)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }
    }
}

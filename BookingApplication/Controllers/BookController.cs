using BookingApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApplication.Controllers
{
    public class BookController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search()
        {
            return View(new SearchViewModel() { StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(1) });
        }
    }
}


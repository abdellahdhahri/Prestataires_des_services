using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServicesPlomberie.Data;
using SQLitePCL;

namespace ServicesPlomberie.Controllers
{
    public class LayoutController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LayoutController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult NavbarData()
        {
            // Fetch data from the backend
            var data = _context.Service.ToList();

            // Pass the data to the view
            ViewBag.NavbarData = data;

            return PartialView("_Layout"); // A partial view to render the select tag
        }
    }
        }
    
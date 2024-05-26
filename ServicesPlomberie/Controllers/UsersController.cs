using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ServicesPlomberie.Data;
using ServicesPlomberie.Models;
using Microsoft.AspNetCore.Identity;

namespace ServicesPlomberie.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<Utilisateur> _userManager;
        private readonly RoleManager<Utilisateur> _roleManager;

        public UsersController(UserManager<Utilisateur> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        
        public async Task<IActionResult> users()
        {

            var users = await _userManager.GetUsersInRoleAsync("travailleur");
            return View(users);
        }
        // GET: Users
        public async Task<IActionResult> Index( string search,string search2)
        {
            IQueryable<Utilisateur> query = _context.Users;
            if ( !string.IsNullOrEmpty(search) && !string.IsNullOrEmpty(search2))
            {
                var allRoles = await _context.Roles.ToListAsync();

               
                    query = query.Where(u => u.region.StartsWith(search2) && u.service.StartsWith(search));
                
            }

            var users = await query.ToListAsync();

            return View(users); 

/*
            if (!string.IsNullOrEmpty(searchby) && !string.IsNullOrEmpty(search) && !string.IsNullOrEmpty(search2))
            {
                var allRoles = await _context.Roles.ToListAsync();

                if (searchby == "simple")
                {
            query = query.Where(u => u.firstName.StartsWith(search));
                }
                else if (searchby == "avance")
                {
                    query = query.Where(u => u.region.StartsWith(search2) && u.service.StartsWith(search));
                }
            }

            var users = await query.ToListAsync();

            return View(users);*/



            /*  if (searchby == "name")
              {

                  return _context.Users != null ?
                  View(await _context.Users.Where(x => x.lastName.StartsWith(search)).ToListAsync()) :
                                          Problem("Entity set 'ApplicationDbContext.Client'  is null.");



              }
              else
              {

                  return _context.Users != null ?
                  View(await _context.Users.Where(x => x.firstName.StartsWith(search)).ToListAsync()) :
                                          Problem("Entity set 'ApplicationDbContext.Client'  is null.");


              }*/
            ///var users = await _userManager.GetUsersInRoleAsync("user");

            ///return View(users);*/
        }

        // GET: UsersController1/Details/5
        public async Task<ActionResult> DetailsAsync(string id)
        {
            IQueryable<Utilisateur> query = _context.Users;
            if (id == null || query == null)
            {
                return NotFound();
            }
            else
            {
                query = query.Where(u=>u.Id == id);
            }

            var users = await query.ToListAsync();



            return View(users);
        }
        // GET: Users
        public async Task<IActionResult> Search(string searchby, string search)
        {
            
            if (searchby == "name")
            {
               
                return _context.Users != null ?
                View(await _context.Users.Where(x => x.lastName.StartsWith(search)).ToListAsync()) :
                                        Problem("Entity set 'ApplicationDbContext.Client'  is null.");



            }
            else
            {
               
                 return _context.Users != null ?
                 View(await _context.Users.Where(x => x.firstName.StartsWith(search)).ToListAsync()) :
                                         Problem("Entity set 'ApplicationDbContext.Client'  is null.");


            }
        }

        // GET: UsersController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsersController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsersController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UsersController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsersController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UsersController1/Delete/5
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

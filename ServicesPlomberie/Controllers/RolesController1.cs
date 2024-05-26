using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ServicesPlomberie.Controllers
{
    [Authorize(Roles="admin")]
    public class rolesController1 : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public rolesController1(RoleManager<IdentityRole> roleManager)
            
        {
            _roleManager = roleManager;
        }
        // GET: rolesController1
        public ActionResult Index()
        {
            var roles = _roleManager.Roles;

            return View(roles);
        }

        // GET: rolesController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: rolesController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: rolesController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IdentityRole model)
        {

            //if (!_roleManager.RoleExistsAsync(Model.Name).GetAwaiter().GetResult) { }
            _roleManager.CreateAsync(new IdentityRole(model.Name)).GetAwaiter().GetResult();
                return RedirectToAction("Index");
            
        }

        // GET: rolesController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: rolesController1/Edit/5
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

        // GET: rolesController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: rolesController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}

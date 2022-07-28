using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using testNavigation.Context;
using testNavigation.Models;
using testNavigation.Models.ViewModel;

namespace testNavigation.Controllers
{
    public class HomeController : Controller
    {
        private readonly testDBConnectionContext _context;

        public HomeController(testDBConnectionContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> List()
        {
            List<User> userList = await _context.Users.Include(c => c.IdActiveNavigation).ToListAsync();

            return View(userList);
        }

        #region Create
        [HttpGet]
        public async Task<IActionResult> Create(int id)
        {
            UserVM? oUserVM = new()
            {
                oUser = new(),
                oListaActivo = await _context.Actives.Select(status => new SelectListItem()
                {
                    Text = status.Status,
                    Value = status.Id.ToString()
                }).ToListAsync()
            };

            if (id != 0)
            {
                oUserVM.oUser = await _context.Users.FindAsync(id);
            }

            return View(oUserVM);
        }

        [HttpPost]
        public async Task<IActionResult> NewUser(UserVM user)
        {
            if (user.oUser.Id == 0)
            {
                _context.Users.Add(user.oUser);
            }
            else
            {
                _context.Users.Update(user.oUser);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("List", "Home");
        }
        #endregion

        #region Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            User? user = await _context.Users.Include(c => c.IdActiveNavigation).Where(e => e.Id == id).FirstOrDefaultAsync();

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return RedirectToAction("List", "Home");
        }
        #endregion

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
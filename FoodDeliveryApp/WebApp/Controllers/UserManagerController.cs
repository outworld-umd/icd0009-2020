using System;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Domain;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class UserManagerController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public UserManagerController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        // GET: RestaurantUser
        public async Task<IActionResult> Index()
        {
           var users = await _userManager.Users.ToListAsync();
           return View(users);
        }

        // GET: RestaurantUser/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.Users.FirstAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: RestaurantUser/Create
        public IActionResult Create()
        {
            var vm = new UserManagerCreateEditViewModel {
                Roles = new SelectList(_roleManager.Roles, nameof(AppRole.Id), nameof(AppRole.Name))
            };
            return View(vm);
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserManagerCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.User.Id = Guid.NewGuid();
                await _userManager.CreateAsync(vm.User);
                return RedirectToAction(nameof(Index));
            }

            vm.Roles = new SelectList(_roleManager.Roles, nameof(AppRole.Id), nameof(AppRole.Name));
            return View(vm);;
        }

        // GET: RestaurantUser/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var vm = new UserManagerCreateEditViewModel {
                User = await _userManager.Users.FirstAsync(u => u.Id == id)
            };
            if (vm.User == null)
            {
                return NotFound();
            }
            vm.Roles = new SelectList(_roleManager.Roles, nameof(AppRole.Id), nameof(AppRole.Name));
            return View(vm);
        }

        // POST: RestaurantUser/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UserManagerCreateEditViewModel vm)
        {
            if (id != vm.User.Id)
            {
                Console.WriteLine("LOLKEK");
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _userManager.UpdateAsync(vm.User);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(vm.User.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            vm.Roles = new SelectList(_roleManager.Roles, nameof(AppRole.Id), nameof(AppRole.Name));
            return View(vm);
        }

        // GET: RestaurantUser/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.Users.FirstAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: RestaurantUser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == id);
            await _userManager.DeleteAsync(user);
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(Guid id)
        {
            return _userManager.Users.Any(u => u.Id == id);
        }
    }
}
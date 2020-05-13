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
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<AppRole> roleManager;

        public UserManagerController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        // GET: RestaurantUser
        public async Task<IActionResult> Index()
        {
           var users = await userManager.Users.ToListAsync();
           return View(users);
        }

        // GET: RestaurantUser/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await userManager.Users.FirstAsync(u => u.Id == id);
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
                Roles = new SelectList(roleManager.Roles, nameof(AppRole.Id), nameof(AppRole.Name))
            };
            return View(vm);
        }

        // POST: RestaurantUser/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserManagerCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.User.Id = Guid.NewGuid();
                await userManager.CreateAsync(vm.User);
                return RedirectToAction(nameof(Index));
            }

            vm.Roles = new SelectList(roleManager.Roles, nameof(AppRole.Id), nameof(AppRole.Name));
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
                User = await userManager.Users.FirstAsync(u => u.Id == id)
            };
            if (vm.User == null)
            {
                return NotFound();
            }
            vm.Roles = new SelectList(roleManager.Roles, nameof(AppRole.Id), nameof(AppRole.Name));
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
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await userManager.UpdateAsync(vm.User);
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
            vm.Roles = new SelectList(roleManager.Roles, nameof(AppRole.Id), nameof(AppRole.Name));
            return View(vm);
        }

        // GET: RestaurantUser/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await userManager.Users.FirstAsync(u => u.Id == id);
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
            var user = await userManager.Users.FirstAsync(u => u.Id == id);
            await userManager.DeleteAsync(user);
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(Guid id)
        {
            return userManager.Users.Any(u => u.Id == id);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.App.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Areas.Admin.ViewModels;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserManagerController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public UserManagerController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: UserManager
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var vm = new UserManagerIndexViewModel
            {
                Users = users,
                UserRoles = GetUserRoles(users)
            };
            return View(vm);
        }

        private Dictionary<AppUser, AppRole> GetUserRoles(List<AppUser> users)
        {
            var userRoles = new Dictionary<AppUser, AppRole>();
            foreach (var user in users)
            {
                var roles =  _userManager.GetRolesAsync(user).Result;
                var role = _roleManager.FindByNameAsync(roles.First()).Result;
                userRoles.Add(user, role);
            }

            return userRoles;
        }

        // GET: UserManager/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _userManager.Users.FirstAsync(u => u.Id == id);
            var roles = await _userManager.GetRolesAsync(user);
            var role = _roleManager.FindByNameAsync(roles.First()).Result;
            var vm = new UserManagerDetailsDeleteViewModel
            {
                User = user,
                Role = role,
                CurrentRoles = string.Join(", ", roles.ToArray())
            };
            if (user == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        // GET: UserManager/Create
        public IActionResult Create()
        {
            var vm = new UserManagerCreateViewModel
            {
                Roles = new SelectList(_roleManager.Roles, nameof(AppRole.Name), nameof(AppRole.Name))
            };
            return View(vm);
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserManagerCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.User.Id = Guid.NewGuid();
                vm.User.UserName = vm.Email;
                vm.User.Email = vm.Email;

                await _userManager.CreateAsync(vm.User, vm.Password);
                await _userManager.AddToRoleAsync(vm.User, vm.Role!.Name);
                return RedirectToAction(nameof(Index));
            }

            vm.Roles = new SelectList(_roleManager.Roles, nameof(AppRole.Name), nameof(AppRole.Name));
            return View(vm);
            ;
        }

        // GET: UserManager/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var user = await _userManager.Users.FirstAsync(u => u.Id == id);
            var roles = await _userManager.GetRolesAsync(user);
            var role = _roleManager.FindByNameAsync(roles.First()).Result;
            var vm = new UserManagerEditViewModel
            {
                User = user,
                Role = role,
                Roles = new SelectList(_roleManager.Roles.Where(r => !roles.Contains(r.Name)), nameof(AppRole.Name), nameof(AppRole.Name)),
            };

            return View(vm);
        }

        // POST: UserManager/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UserManagerEditViewModel vm)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == id);
            if (id == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    var role = _roleManager.FindByNameAsync(roles.First()).Result;
                    user.UserName = vm.User.Email;
                    user.Email = vm.User.Email;
                    user.Phone = vm.User.Phone;
                    user.FirstName = vm.User.FirstName;
                    user.LastName = vm.User.LastName;
                    // Console.WriteLine(role.Name + "Current role");
                    // Console.WriteLine(vm.Role.Name + "New role");
                    if (role.Name != vm.Role!.Name)
                    {
                        await _userManager.RemoveFromRolesAsync(user, roles);
                        await _userManager.AddToRoleAsync(user, vm.Role.Name);
                    }

                    await _userManager.UpdateAsync(user);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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

            vm.Roles = new SelectList(_roleManager.Roles, nameof(AppRole.Name), nameof(AppRole.Name));
            return View(vm);
        }

        // GET: UserManager/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.Users.FirstAsync(u => u.Id == id);
            var roles = await _userManager.GetRolesAsync(user);
            var role = _roleManager.FindByNameAsync(roles.First()).Result;
            var vm = new UserManagerDetailsDeleteViewModel
            {
                User = user,
                Role = role,
                CurrentRoles = string.Join(", ", roles.ToArray())
            };
            if (user == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        // POST: UserManager/Delete/5
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class PersonFormController : Controller
    {
        private readonly AppDbContext _context;

        public PersonFormController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PersonForm
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.PersonForms
                .Include(p => p.Form)
                .Include(p => p.FormRole)
                .Include(p => p.Person);
            return View(await appDbContext.ToListAsync());
        }

        // GET: PersonForm/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personForm = await _context.PersonForms
                .Include(p => p.Form)
                .Include(p => p.FormRole)
                .Include(p => p.Person)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personForm == null)
            {
                return NotFound();
            }

            return View(personForm);
        }

        // GET: PersonForm/Create
        public IActionResult Create()
        {
            var vm = new PersonFormCreateEditViewModel {
                Forms = new SelectList(_context.Forms, nameof(Form.Id), nameof(Form.Name)),
                FormRoles = new SelectList(_context.FormRoles, nameof(FormRole.Id), nameof(FormRole.Name)),
                Persons = new SelectList(_context.Persons, nameof(Person.Id), nameof(Person.LastName))
            };
            return View(vm);
        }

        // POST: PersonForm/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PersonFormCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.PersonForm.Id = Guid.NewGuid();
                _context.Add(vm.PersonForm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.Forms = new SelectList(_context.Forms, nameof(Form.Id), nameof(Form.Name), vm.PersonForm.FormId);
            vm.FormRoles = new SelectList(_context.FormRoles, nameof(FormRole.Id), nameof(FormRole.Name), vm.PersonForm.FormRoleId);
            vm.Persons = new SelectList(_context.Persons, nameof(Person.Id), nameof(Person.LastName), vm.PersonForm.FormRoleId);
            return View(vm);
        }

        // GET: PersonForm/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var vm = new PersonFormCreateEditViewModel();
            vm.PersonForm = await _context.PersonForms.FindAsync(id);
            if (vm.PersonForm == null)
            {
                return NotFound();
            }
            vm.Forms = new SelectList(_context.Forms, nameof(Form.Id), nameof(Form.Name), vm.PersonForm.FormId);
            vm.FormRoles = new SelectList(_context.FormRoles, nameof(FormRole.Id), nameof(FormRole.Name), vm.PersonForm.FormRoleId);
            vm.Persons = new SelectList(_context.Persons, nameof(Person.Id), nameof(Person.LastName), vm.PersonForm.FormRoleId);
            return View(vm);
        }

        // POST: PersonForm/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, PersonFormCreateEditViewModel vm) {
            vm.PersonForm.Id = id;
            if (id != vm.PersonForm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vm.PersonForm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonFormExists(vm.PersonForm.Id))
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
            vm.Forms = new SelectList(_context.Forms, nameof(Form.Id), nameof(Form.Name), vm.PersonForm.FormId);
            vm.FormRoles = new SelectList(_context.FormRoles, nameof(FormRole.Id), nameof(FormRole.Name), vm.PersonForm.FormRoleId);
            vm.Persons = new SelectList(_context.Persons, nameof(Person.Id), nameof(Person.LastName), vm.PersonForm.FormRoleId);
            return View(vm);
        }

        // GET: PersonForm/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personForm = await _context.PersonForms
                .Include(p => p.Form)
                .Include(p => p.FormRole)
                .Include(p => p.Person)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personForm == null)
            {
                return NotFound();
            }

            return View(personForm);
        }

        // POST: PersonForm/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var personForm = await _context.PersonForms.FindAsync(id);
            _context.PersonForms.Remove(personForm);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonFormExists(Guid id)
        {
            return _context.PersonForms.Any(e => e.Id == id);
        }
    }
}

using System;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.DTO;
using Domain.App.Enums;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class ChoicesController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public ChoicesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Choices
        public async Task<IActionResult> Index()
        {
            return View(await _uow.Choices.GetAllAsync());
        }

        // GET: Choices/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var choice = await _uow.Choices.FirstOrDefaultAsync(id.Value);
            if (choice == null)
            {
                return NotFound();
            }

            return View(choice);
        }

        // GET: Choices/Create
        public IActionResult Create()
        {
            var vm = new ChoicesCreateEditViewModel()
            {
                Questions = new SelectList(_uow.Questions.GetAll(), nameof(Question.Id), nameof(Question.Title))
            };
            return View(vm);
        }

        // POST: Choices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ChoicesCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.Choice.Id = Guid.NewGuid();
                _uow.Choices.Add(vm.Choice);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            vm.Questions = new SelectList(await _uow.Questions.GetAllAsync(), nameof(Question.Id),
                nameof(Question.Title));
            return View(vm);
        }

        // GET: Choices/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vm = new ChoicesCreateEditViewModel()
            {
                Choice = await _uow.Choices.FirstOrDefaultAsync(id.Value)
            };
            if (vm.Choice == null)
            {
                return NotFound();
            }

            vm.Questions = new SelectList(await _uow.Questions.GetAllAsync(), nameof(Question.Id),
                nameof(Question.Title));
            return View(vm);
        }

        // POST: Choices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ChoicesCreateEditViewModel vm)
        {
            if (id != vm.Choice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _uow.Choices.UpdateAsync(vm.Choice);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChoiceExists(vm.Choice.Id))
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

            vm.Questions = new SelectList(await _uow.Questions.GetAllAsync(), nameof(Question.Id),
                nameof(Question.Title));
            return View(vm);
        }

        // GET: Choices/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var choice = await _uow.Choices
                .FirstOrDefaultAsync(id.Value);
            if (choice == null)
            {
                return NotFound();
            }

            return View(choice);
        }

        // POST: Choices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var choice = await _uow.Choices.FirstOrDefaultAsync(id);
            await _uow.Choices.RemoveAsync(choice);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChoiceExists(Guid id)
        {
            return _uow.Choices.Any(e => e.Id == id);
        }
    }
}
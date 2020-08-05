using System;
using System.Linq;
using System.Threading.Tasks;
using BLL.App;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BLL.App.DTO;
using Extensions;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class TranslationsController : Controller
    {
        private readonly IAppBLL _bll;

        public TranslationsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Translation
        public async Task<IActionResult> Index()
        {
            return  View(await _bll.Translations.GetAllAsync());
        }

        // GET: Translation/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var translation = await _bll.Translations
                .FirstOrDefaultAsync(id.Value);
            if (translation == null)
            {
                return NotFound();
            }

            return View(translation);
        }

        // GET: Translation/Create
        public IActionResult Create()
        {
            var vm = new TranslationCreateEditViewModel() {
                Translations = new SelectList(_bll.Categories.GetAll(), nameof(Category.Id), nameof(Category.Name))
            };
            return View(vm);
        }

        // POST: Translation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TranslationCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.Translation.Id = Guid.NewGuid();
                _bll.Translations.Add(vm.Translation);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            vm.Translations = new SelectList((await _bll.Categories.GetAllAsync()), nameof(Category.Id),
                nameof(Category.Name));
            return View(vm);
        }

        // GET: Translation/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vm = new TranslationCreateEditViewModel() {
                Translation = await _bll.Translations.FirstOrDefaultAsync(id.Value)
            };
            if (vm.Translation == null)
            {
                return NotFound();
            }

            vm.Translations = new SelectList( await _bll.Categories.GetAllAsync(), nameof(Category.Id),
                nameof(Category.Name));
            return View(vm);
        }

        // POST: Translation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, TranslationCreateEditViewModel vm)
        {
            if (id != vm.Translation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _bll.Translations.UpdateAsync(vm.Translation);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TranslationExists(vm.Translation.Id))
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
            vm.Translations = new SelectList(await _bll.Categories.GetAllAsync(), nameof(Category.Id),
                nameof(Category.Name));
            return View(vm);
        }

        // GET: Translation/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var translation = await _bll.Translations
                .FirstOrDefaultAsync(id.Value);
            if (translation == null)
            {
                return NotFound();
            }

            return View(translation);
        }

        // POST: Translation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.Translations.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        private bool TranslationExists(Guid id)
        {
            return _bll.Translations.Any(e => e.Id == id);
        }
    }
}
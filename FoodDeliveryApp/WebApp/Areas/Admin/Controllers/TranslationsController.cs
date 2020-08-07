// using System;
// using System.Threading.Tasks;
// using BLL.App.DTO;
// using Contracts.BLL.App;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.Rendering;
// using Microsoft.EntityFrameworkCore;
//
// namespace WebApp.Areas.Admin.Controllers
// {
//     [Authorize(Roles = "Admin")]
//     public class TranslationsController : Controller
//     {
//         private readonly IAppBLL _bll;
//
//         public TranslationsController(IAppBLL bll)
//         {
//             _bll = bll;
//         }
//
//         // GET: Translation
//         public async Task<IActionResult> Index()
//         {
//             return View(await _bll.Translations.GetAllAsync());
//         }
//
//         // GET: Translation/Details/5
//         public async Task<IActionResult> Details(Guid? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//
//             var translation = await _bll.Translations
//                 .FirstOrDefaultAsync(id.Value);
//             if (translation == null)
//             {
//                 return NotFound();
//             }
//
//             return View(translation);
//         }
//
//         // GET: Translation/Create
//         public IActionResult Create()
//         {
//             ViewData["LangStringId"] = new SelectList(_bll.LangStrings.GetAll(), "Id", "Id");
//             return View();
//         }
//
//         // POST: Translation/Create
//         // To protect from overposting attacks, enable the specific properties you want to bind to, for 
//         // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Create([Bind("Culture,Value,LangStringId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Translation translation)
//         {
//             if (ModelState.IsValid)
//             {
//                 translation.Id = Guid.NewGuid();
//                 _bll.Translations.Add(translation);
//                 await _bll.SaveChangesAsync();
//                 return RedirectToAction(nameof(Index));
//             }
//             ViewData["LangStringId"] = new SelectList(_bll.LangStrings.GetAll(), "Id", "Id", translation.LangStringId);
//             return View(translation);
//         }
//
//         // GET: Translation/Edit/5
//         public async Task<IActionResult> Edit(Guid? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//
//             var translation = await _bll.Translations.FirstOrDefaultAsync(id.Value);
//             if (translation == null)
//             {
//                 return NotFound();
//             }
//             ViewData["LangStringId"] = new SelectList(_bll.LangStrings.GetAll(), "Id", "Id", translation.LangStringId);
//             return View(translation);
//         }
//
//         // POST: Translation/Edit/5
//         // To protect from overposting attacks, enable the specific properties you want to bind to, for 
//         // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Edit(Guid id, [Bind("Culture,Value,LangStringId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Translation translation)
//         {
//             if (id != translation.Id)
//             {
//                 return NotFound();
//             }
//
//             if (ModelState.IsValid)
//             {
//                 try
//                 {
//                     await _bll.Translations.UpdateAsync(translation);
//                     await _bll.SaveChangesAsync();
//                 }
//                 catch (DbUpdateConcurrencyException)
//                 {
//                     if (!TranslationExists(translation.Id))
//                     {
//                         return NotFound();
//                     }
//                     else
//                     {
//                         throw;
//                     }
//                 }
//                 return RedirectToAction(nameof(Index));
//             }
//             ViewData["LangStringId"] = new SelectList(_bll.LangStrings.GetAll(), "Id", "Id", translation.LangStringId);
//             return View(translation);
//         }
//
//         // GET: Translation/Delete/5
//         public async Task<IActionResult> Delete(Guid? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//
//             var translation = await _bll.Translations
//                 .FirstOrDefaultAsync(id.Value);
//             if (translation == null)
//             {
//                 return NotFound();
//             }
//
//             return View(translation);
//         }
//
//         // POST: Translation/Delete/5
//         [HttpPost, ActionName("Delete")]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> DeleteConfirmed(Guid id)
//         {
//             await _bll.Translations.RemoveAsync(id);
//             await _bll.SaveChangesAsync();
//             return RedirectToAction(nameof(Index));
//         }
//
//         private bool TranslationExists(Guid id)
//         {
//             return _bll.Translations.Any(e => e.Id == id);
//         }
//     }
// }

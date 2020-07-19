using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NutritionInfosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public NutritionInfosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/NutritionInfos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NutritionInfo>>> GetNutritionInfos()
        {
            return await _context.NutritionInfos.ToListAsync();
        }

        // GET: api/NutritionInfos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NutritionInfo>> GetNutritionInfo(Guid id)
        {
            var nutritionInfo = await _context.NutritionInfos.FindAsync(id);

            if (nutritionInfo == null)
            {
                return NotFound();
            }

            return nutritionInfo;
        }

        // PUT: api/NutritionInfos/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNutritionInfo(Guid id, NutritionInfo nutritionInfo)
        {
            if (id != nutritionInfo.Id)
            {
                return BadRequest();
            }

            _context.Entry(nutritionInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NutritionInfoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/NutritionInfos
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<NutritionInfo>> PostNutritionInfo(NutritionInfo nutritionInfo)
        {
            _context.NutritionInfos.Add(nutritionInfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNutritionInfo", new { id = nutritionInfo.Id }, nutritionInfo);
        }

        // DELETE: api/NutritionInfos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<NutritionInfo>> DeleteNutritionInfo(Guid id)
        {
            var nutritionInfo = await _context.NutritionInfos.FindAsync(id);
            if (nutritionInfo == null)
            {
                return NotFound();
            }

            _context.NutritionInfos.Remove(nutritionInfo);
            await _context.SaveChangesAsync();

            return nutritionInfo;
        }

        private bool NutritionInfoExists(Guid id)
        {
            return _context.NutritionInfos.Any(e => e.Id == id);
        }
    }
}

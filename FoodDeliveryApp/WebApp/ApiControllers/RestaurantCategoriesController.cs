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
    public class RestaurantCategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RestaurantCategoriesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/RestaurantCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantCategory>>> GetRestaurantCategories()
        {
            return await _context.RestaurantCategories.ToListAsync();
        }

        // GET: api/RestaurantCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RestaurantCategory>> GetRestaurantCategory(Guid id)
        {
            var restaurantCategory = await _context.RestaurantCategories.FindAsync(id);

            if (restaurantCategory == null)
            {
                return NotFound();
            }

            return restaurantCategory;
        }

        // PUT: api/RestaurantCategories/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRestaurantCategory(Guid id, RestaurantCategory restaurantCategory)
        {
            if (id != restaurantCategory.Id)
            {
                return BadRequest();
            }

            _context.Entry(restaurantCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RestaurantCategoryExists(id))
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

        // POST: api/RestaurantCategories
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<RestaurantCategory>> PostRestaurantCategory(RestaurantCategory restaurantCategory)
        {
            _context.RestaurantCategories.Add(restaurantCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRestaurantCategory", new { id = restaurantCategory.Id }, restaurantCategory);
        }

        // DELETE: api/RestaurantCategories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RestaurantCategory>> DeleteRestaurantCategory(Guid id)
        {
            var restaurantCategory = await _context.RestaurantCategories.FindAsync(id);
            if (restaurantCategory == null)
            {
                return NotFound();
            }

            _context.RestaurantCategories.Remove(restaurantCategory);
            await _context.SaveChangesAsync();

            return restaurantCategory;
        }

        private bool RestaurantCategoryExists(Guid id)
        {
            return _context.RestaurantCategories.Any(e => e.Id == id);
        }
    }
}

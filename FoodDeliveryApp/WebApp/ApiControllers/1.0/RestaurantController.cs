using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RestaurantController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly RestaurantMapper _mapper = new RestaurantMapper();

        public RestaurantController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Restaurant
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetRestaurants()
        {
            return Ok((await _bll.Restaurants.GetAllAsync()).Select(e => _mapper.MapRestaurant(e)));
        }

        // GET: api/Restaurant/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Restaurant>> GetRestaurant(Guid id)
        {
            var restaurant = await _bll.Restaurants.FirstOrDefaultAsync(id);

            if (restaurant == null)
            {
                return NotFound(new MessageDTO($"Restaurant with id {id} not found"));
            }

            return Ok(_mapper.Map(restaurant));
        }

        // PUT: api/Restaurant/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRestaurant(Guid id, Restaurant restaurant)
        {
            if (id != restaurant.Id)
            {
                return BadRequest(new MessageDTO("Id and Restaurant.Id do not match"));
            }

            await _bll.Restaurants.UpdateAsync(_mapper.Map(restaurant));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Restaurant
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Restaurant>> PostRestaurant(Restaurant restaurant)
        {
            var bllEntity = _mapper.Map(restaurant);
            _bll.Restaurants.Add(bllEntity);
            await _bll.SaveChangesAsync();
            restaurant.Id = bllEntity.Id;

            return CreatedAtAction("GetRestaurant",
                new {id = restaurant.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                restaurant);
        }

        // DELETE: api/Restaurant/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Restaurant>> DeleteRestaurant(Guid id)
        {
            var restaurant = await _bll.Restaurants.FirstOrDefaultAsync(id);
            if (restaurant == null)
            {
                return NotFound(new MessageDTO("Restaurant not found"));
            }

            await _bll.Restaurants.RemoveAsync(restaurant);
            await _bll.SaveChangesAsync();

            return Ok(restaurant);
        }

        private bool RestaurantExists(Guid id)
        {
            return _bll.Restaurants.Exists(id);
        }
    }
}

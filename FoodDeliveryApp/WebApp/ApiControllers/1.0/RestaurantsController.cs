using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using V1DTO=PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers._1._0
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RestaurantsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly RestaurantMapper _mapper = new RestaurantMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public RestaurantsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Restaurant
        /// <summary>
        /// Get restaurants for single session 
        /// </summary>
        /// <returns>restaurants for session</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.RestaurantView>))]
        public async Task<ActionResult<IEnumerable<V1DTO.RestaurantView>>> GetRestaurants()
        {
            return Ok((await _bll.Restaurants.GetAllAsync(User.UserGuidId())).Select(e => _mapper.MapRestaurantView(e)));
        }

        // GET: api/Restaurant/5
        /// <summary>
        /// Get a single restaurant
        /// </summary>
        /// <param name="id">id for restaurant</param>
        /// <returns>restaurant</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Restaurant))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.Restaurant))]
        public async Task<ActionResult<V1DTO.Restaurant>> GetRestaurant(Guid id)
        {
            var restaurant = await _bll.Restaurants.FirstOrDefaultAsync(id, User.UserGuidId());

            if (restaurant == null)
            {
                return NotFound(new V1DTO.MessageDTO($"Restaurant with id {id} not found"));
            }

            return Ok(_mapper.MapRestaurant(restaurant));
        }

        // PUT: api/Restaurant/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Update restaurant
        /// </summary>
        /// <param name="id"></param>
        /// <param name="restaurant"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutRestaurant(Guid id, V1DTO.Restaurant restaurant)
        {
            if (id != restaurant.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("Id and Restaurant.Id do not match"));
            }

            await _bll.Restaurants.UpdateAsync(_mapper.Map(restaurant));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Restaurant
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Create/add a new restaurant
        /// </summary>
        /// <param name="restaurant">Restaurant info</param>
        /// <returns></returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.Restaurant))]
        [HttpPost]
        public async Task<ActionResult<V1DTO.Restaurant>> PostRestaurant(V1DTO.Restaurant restaurant)
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
        /// <summary>
        /// Deletes the restaurant
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Restaurant))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [HttpDelete("{id}")]
        public async Task<ActionResult<V1DTO.Restaurant>> DeleteRestaurant(Guid id)
        {
            var restaurant = await _bll.Restaurants.FirstOrDefaultAsync(id);
            if (restaurant == null)
            {
                return NotFound(new V1DTO.MessageDTO("Restaurant not found"));
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

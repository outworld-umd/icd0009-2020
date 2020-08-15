using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.App;
using Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using V1DTO=PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;
using PublicApi.DTO.v1.Mappers.Base;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Saved categories of restaurants
    /// </summary>
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RestaurantCategoriesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly APIMapper<V1DTO.RestaurantCategory, RestaurantCategory> _mapper = new APIMapper<V1DTO.RestaurantCategory, RestaurantCategory>();

        /// <summary>
        /// Constructor
        /// </summary>
        public RestaurantCategoriesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Restaurant/5
        /// <summary>
        /// Get a single restaurant category
        /// </summary>
        /// <param name="id">id for restaurant category</param>
        /// <returns>restaurant category</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ApiExplorerSettings(IgnoreApi = true)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.RestaurantCategory))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.RestaurantCategory))]
        public async Task<ActionResult<V1DTO.Restaurant>> GetRestaurantCategory(Guid id)
        {
            var restaurant = await _bll.RestaurantCategories.FirstOrDefaultAsync(id, User.UserGuidId());

            if (restaurant == null)
            {
                return NotFound(new V1DTO.MessageDTO($"RestaurantCategory with id {id} not found"));
            }

            return Ok(_mapper.Map(restaurant));
        }

        // POST: api/Restaurant
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Create/add a new restaurant category
        /// </summary>
        /// <param name="restaurantCategory">Restaurant category info</param>
        /// <returns></returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(Roles = "Restaurant, Admin")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.RestaurantCategory))]
        [HttpPost]
        public async Task<ActionResult<V1DTO.RestaurantCategory>> PostRestaurantCategory(V1DTO.RestaurantCategory restaurantCategory)
        {
            var bllEntity = _mapper.Map(restaurantCategory);
            _bll.RestaurantCategories.Add(bllEntity);
            await _bll.SaveChangesAsync();
            restaurantCategory.Id = bllEntity.Id;

            return CreatedAtAction("GetRestaurantCategory",
                new {id = restaurantCategory.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                restaurantCategory);
        }

        // DELETE: api/Restaurant/5
        /// <summary>
        /// Deletes the restaurant category
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [Authorize(Roles = "Restaurant, Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<V1DTO.Restaurant>> DeleteRestaurantCategory(Guid id)
        {
            var restaurantCategory = await _bll.RestaurantCategories.FirstOrDefaultAsync(id);
            if (restaurantCategory == null)
            {
                return NotFound(new V1DTO.MessageDTO("RestaurantCategory not found"));
            }

            await _bll.RestaurantCategories.RemoveAsync(restaurantCategory);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool RestaurantCategoryExists(Guid id)
        {
            return _bll.RestaurantCategories.Exists(id);
        }
    }
}

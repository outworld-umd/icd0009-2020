using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using V1DTO=PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers._1._0
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CategoriesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly CategoryMapper _mapper = new CategoryMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public CategoriesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Category
        /// <summary>
        /// Get categories for single session 
        /// </summary>
        /// <returns>categories for session</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.Category>))]
        public async Task<ActionResult<IEnumerable<V1DTO.Category>>> GetCategories()
        {
            var userIdTKey = User.IsInRole("Admin") ? null : (Guid?) User.UserGuidId();

            return Ok((await _bll.Categories.GetAllAsync(userIdTKey)).Select(e => _mapper.MapCategory(e)));
        }

        // GET: api/Category/5
        /// <summary>
        /// Get a single category
        /// </summary>
        /// <param name="id">id for category</param>
        /// <returns>category</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Category))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.Category))]
        public async Task<ActionResult<V1DTO.Category>> GetCategory(Guid id)
        {
            var userIdTKey = User.IsInRole("Admin") ? null : (Guid?) User.UserGuidId();
            var category = await _bll.Categories.FirstOrDefaultAsync(id, userIdTKey);

            if (category == null)
            {
                return NotFound(new V1DTO.MessageDTO($"Category with id {id} not found"));
            }
            

            return Ok(_mapper.MapCategory(category));
        }

        // PUT: api/Category/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Update category
        /// </summary>
        /// <param name="id"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutCategory(Guid id, V1DTO.Category category)
        {
            var userIdTKey = User.IsInRole("Admin") ? null : (Guid?) User.UserGuidId();
            if (id != category.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("Id and Category.Id do not match"));
            }
            if (!await _bll.Addresses.ExistsAsync(category.Id, userIdTKey))
            {
                return NotFound(new V1DTO.MessageDTO($"Current user does not have session with this id {id}"));
            }
            await _bll.Categories.UpdateAsync(_mapper.Map(category));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Category
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Create/add a new category
        /// </summary>
        /// <param name="category">Category info</param>
        /// <returns></returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.Category))]
        [HttpPost]
        public async Task<ActionResult<V1DTO.Category>> PostCategory(V1DTO.Category category)
        {
            var bllEntity = _mapper.Map(category);
            _bll.Categories.Add(bllEntity);
            await _bll.SaveChangesAsync();
            category.Id = bllEntity.Id;

            return CreatedAtAction("GetCategory",
                new {id = category.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                category);
        }

        // DELETE: api/Category/5
        /// <summary>
        /// Deletes the category
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Category))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [HttpDelete("{id}")]
        public async Task<ActionResult<V1DTO.Category>> DeleteCategory(Guid id)
        {
            var category = await _bll.Categories.FirstOrDefaultAsync(id);
            if (category == null)
            {
                return NotFound(new V1DTO.MessageDTO("Category not found"));
            }

            await _bll.Categories.RemoveAsync(category);
            await _bll.SaveChangesAsync();

            return Ok(category);
        }

        private bool CategoryExists(Guid id)
        {
            return _bll.Categories.Exists(id);
        }
    }
}

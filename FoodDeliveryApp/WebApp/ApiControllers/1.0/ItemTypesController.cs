using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using V1DTO=PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Types of items
    /// </summary>
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ItemTypesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ItemTypeMapper _mapper = new ItemTypeMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public ItemTypesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/ItemType
        /// <summary>
        /// Get all the item types
        /// </summary>
        /// <returns>Array consisting of types of items</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(Roles = "Restaurant, Admin")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.ItemType>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<IEnumerable<V1DTO.ItemType>>> GetItemTypes()
        {
            return Ok((await _bll.ItemTypes.GetAllAsync()).Select(e => _mapper.MapItemType(e)));
        }

        // GET: api/ItemType/5
        /// <summary>
        /// Get a single item type
        /// </summary>
        /// <param name="id">id for item type</param>
        /// <returns>Category</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(Roles = "Customer, Restaurant, Admin")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.ItemType))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]

        public async Task<ActionResult<V1DTO.ItemType>> GetItemType(Guid id)
        {
            var itemType = await _bll.ItemTypes.FirstOrDefaultAsync(id);

            if (itemType == null)
            {
                return NotFound(new V1DTO.MessageDTO($"ItemType with id {id} not found"));
            }

            return Ok(_mapper.MapItemType(itemType));
        }
        
        // GET: api/ItemType/Restaurant
        /// <summary>
        /// Get item types by restaurant
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Restaurant/{restaurantId}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(Roles = "Customer, Restaurant, Admin")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.ItemType))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<IEnumerable<V1DTO.ItemType>>> GetItemTypesByRestaurant(Guid id) {
            return Ok((await _bll.ItemTypes.GetAllByRestaurantAsync(id)).Select(e => _mapper.MapItemType(e)));
        }

        // PUT: api/ItemType/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        ///     Update item type
        /// </summary>
        /// <param name="id">id of item type to update</param>
        /// <param name="itemType">item type info</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(Roles = "Restaurant, Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutItemType(Guid id, V1DTO.ItemType itemType)
        {
            if (id != itemType.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("Id and ItemType.Id do not match"));
            }

            await _bll.ItemTypes.UpdateAsync(_mapper.Map(itemType));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/ItemType
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        ///     Create/add a new item type
        /// </summary>
        /// <param name="itemType">Item type info</param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(Roles = "Restaurant, Admin")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.ItemType))]
        public async Task<ActionResult<V1DTO.ItemType>> PostItemType(V1DTO.ItemType itemType)
        {
            var bllEntity = _mapper.Map(itemType);
            _bll.ItemTypes.Add(bllEntity);
            await _bll.SaveChangesAsync();
            itemType.Id = bllEntity.Id;

            return CreatedAtAction("GetItemType",
                new {id = itemType.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                itemType);
        }

        // DELETE: api/ItemType/5
        /// <summary>
        ///     Deletes the item type
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(Roles = "Restaurant, Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.ItemType>> DeleteItemType(Guid id)
        {
            var itemType = await _bll.ItemTypes.FirstOrDefaultAsync(id);
            if (itemType == null)
            {
                return NotFound(new V1DTO.MessageDTO("Item not found"));
            }

            await _bll.ItemTypes.RemoveAsync(itemType);
            await _bll.SaveChangesAsync();

            return Ok(itemType);
        }

        private bool ItemTypeExists(Guid id)
        {
            return _bll.ItemTypes.Exists(id);
        }
    }
}

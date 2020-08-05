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
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ItemTypesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ItemTypeMapper _mapper = new ItemTypeMapper();

        public ItemTypesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/ItemType
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(Roles = "Restaurant, Admin")]
        public async Task<ActionResult<IEnumerable<V1DTO.ItemType>>> GetItemTypes()
        {
            return Ok((await _bll.ItemTypes.GetAllAsync()).Select(e => _mapper.MapItemType(e)));
        }

        // GET: api/ItemType/5
        [HttpGet("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(Roles = "Customer, Restaurant, Admin")]
        public async Task<ActionResult<V1DTO.ItemType>> GetItemType(Guid id)
        {
            var itemType = await _bll.ItemTypes.FirstOrDefaultAsync(id);

            if (itemType == null)
            {
                return NotFound(new V1DTO.MessageDTO($"ItemType with id {id} not found"));
            }

            return Ok(_mapper.MapItemType(itemType));
        }
        
        [HttpGet]
        [Route("Restaurant/{restaurantId}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(Roles = "Customer, Restaurant, Admin")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.ItemType))]
        public async Task<ActionResult<IEnumerable<V1DTO.ItemType>>> GetItemTypesByRestaurant(Guid id) {
            return Ok((await _bll.ItemTypes.GetAllByRestaurantAsync(id)).Select(e => _mapper.MapItemType(e)));
        }

        // PUT: api/ItemType/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(Roles = "Restaurant, Admin")]
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
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(Roles = "Restaurant, Admin")]
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
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(Roles = "Restaurant, Admin")]
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublicApi.DTO.v1.Mappers;
using V1DTO = PublicApi.DTO.v1;

namespace WebApp.ApiControllers._1._0 {

    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ItemsController : ControllerBase {
        private readonly IAppBLL _bll;
        private readonly ItemMapper _mapper = new ItemMapper();

        /// <summary>
        ///     Constructor
        /// </summary>
        public ItemsController(IAppBLL bll) {
            _bll = bll;
        }

        // GET: api/Item
        /// <summary>
        ///     Get items for single session
        /// </summary>
        /// <returns>items for session</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.Item>))]
        public async Task<ActionResult<IEnumerable<V1DTO.Item>>> GetItems() {
            var userTKey = User.IsInRole("Customer") ? (Guid?) User.UserGuidId() : null;
            
            return Ok((await _bll.Items.GetAllAsync(userTKey)).Select(e => _mapper.MapItem(e)));
        }
        
        // GET: api/Item/restaurant/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("restaurant/{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Item))]
        public async Task<ActionResult<IEnumerable<V1DTO.Item>>> GetItemsByRestaurant(Guid id) {
            return Ok((await _bll.Items.GetAllByRestaurantAsync(id)).Select(e => _mapper.MapItem(e)));
        }

        // GET: api/Item/5
        /// <summary>
        ///     Get a single item
        /// </summary>
        /// <param name="id">id for item</param>
        /// <returns>item</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Item))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.Item))]
        public async Task<ActionResult<V1DTO.Item>> GetItem(Guid id) {
            var item = await _bll.Items.FirstOrDefaultAsync(id, User.UserGuidId());

            if (item == null) return NotFound(new V1DTO.MessageDTO($"Item with id {id} not found"));

            return Ok(_mapper.MapItem(item));
        }

        // PUT: api/Item/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        ///     Update item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(Roles="Restaurant")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutItem(Guid id, V1DTO.Item item) {
            if (id != item.Id) return BadRequest(new V1DTO.MessageDTO("Id and Item.Id do not match"));
            await _bll.Items.UpdateAsync(_mapper.Map(item));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Item
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        ///     Create/add a new item
        /// </summary>
        /// <param name="item">Item info</param>
        /// <returns></returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.Item))]
        [HttpPost]
        public async Task<ActionResult<V1DTO.Item>> PostItem(V1DTO.Item item) {
            if (!await _bll.RestaurantUsers.AnyAsync(ru =>
                ru.AppUserId.Equals(User.UserGuidId()) && ru.RestaurantId.Equals(item.RestaurantId))) {
                return Unauthorized(new V1DTO.MessageDTO("User not authorized for this restaurant"));
            }
            var bllEntity = _mapper.Map(item);
            _bll.Items.Add(bllEntity);
            await _bll.SaveChangesAsync();
            item.Id = bllEntity.Id;

            return CreatedAtAction(nameof(GetItem),
                new {id = item.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                item);
        }

        // DELETE: api/Item/5
        /// <summary>
        ///     Deletes the item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Item))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [HttpDelete("{id}")]
        public async Task<ActionResult<V1DTO.Item>> DeleteItem(Guid id) {
            var item = await _bll.Items.FirstOrDefaultAsync(id);
            if (item == null) return NotFound(new V1DTO.MessageDTO("Item not found"));
            if (!await _bll.RestaurantUsers.AnyAsync(ru =>
                ru.AppUserId.Equals(User.UserGuidId()) && ru.RestaurantId.Equals(item.RestaurantId))) {
                return Unauthorized(new V1DTO.MessageDTO("User not authorized for this restaurant"));
            }
            
            await _bll.Items.RemoveAsync(item);
            await _bll.SaveChangesAsync();

            return Ok(item);
        }

        private bool ItemExists(Guid id) {
            return _bll.Items.Exists(id);
        }
    }

}
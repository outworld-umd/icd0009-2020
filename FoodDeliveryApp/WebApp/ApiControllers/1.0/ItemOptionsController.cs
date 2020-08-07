using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using V1DTO=PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Saved options of items
    /// </summary>
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ItemOptionsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ItemOptionMapper _mapper = new ItemOptionMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public ItemOptionsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/ItemOption
        /// <summary>
        /// Get all the item options
        /// </summary>
        /// <returns>Array consisting of item options</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(Roles = "Restaurant, Admin")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.ItemOption>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<IEnumerable<V1DTO.ItemOption>>> GetItemOptions()
        {
            return Ok((await _bll.ItemOptions.GetAllAsync()).Select(e => _mapper.MapItemOption(e)));
        }

        // GET: api/ItemOption/5
        /// <summary>
        /// Get a single item option by given id
        /// </summary>
        /// <param name="id">Id for item option</param>
        /// <returns>ItemOption</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(Roles = "Restaurant, Admin")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.ItemOption))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.ItemOption))]
        public async Task<ActionResult<V1DTO.ItemOption>> GetItemOption(Guid id)
        {
            var itemOption = await _bll.ItemOptions.FirstOrDefaultAsync(id);

            if (itemOption == null)
            {
                return NotFound(new V1DTO.MessageDTO($"ItemOption with id {id} not found"));
            }

            return Ok(_mapper.Map(itemOption));
        }

        // PUT: api/ItemOption/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Update the item option
        /// </summary>
        /// <param name="id">Id for item option</param>
        /// <param name="itemOption">Edited item option</param>
        /// <returns>No content</returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(Roles = "Restaurant, Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)] 
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]

        public async Task<IActionResult> PutItemOption(Guid id, V1DTO.ItemOption itemOption)
        {
            if (id != itemOption.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("Id and ItemOption.Id do not match"));
            }

            await _bll.ItemOptions.UpdateAsync(_mapper.Map(itemOption));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/ItemOption
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Create a new item option
        /// </summary>
        /// <param name="itemOption">New item option info</param>
        /// <returns>Newly created item option</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(Roles = "Restaurant, Admin")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.ItemOption))]
        public async Task<ActionResult<V1DTO.ItemOption>> PostItemOption(V1DTO.ItemOption itemOption)
        {
            var bllEntity = _mapper.Map(itemOption);
            _bll.ItemOptions.Add(bllEntity);
            await _bll.SaveChangesAsync();
            itemOption.Id = bllEntity.Id;

            return CreatedAtAction("GetItemOption",
                new {id = itemOption.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                itemOption);
        }

        // DELETE: api/ItemOption/5
        /// <summary>
        /// Delete the item option
        /// </summary>
        /// <param name="id">Id for item option</param>
        /// <returns>No content</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(Roles = "Restaurant, Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.ItemOption>> DeleteItemOption(Guid id)
        {
            var itemOption = await _bll.ItemOptions.FirstOrDefaultAsync(id);
            if (itemOption == null)
            {
                return NotFound(new V1DTO.MessageDTO("ItemOption not found"));
            }

            await _bll.ItemOptions.RemoveAsync(itemOption);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemOptionExists(Guid id)
        {
            return _bll.ItemOptions.Exists(id);
        }
    }
}

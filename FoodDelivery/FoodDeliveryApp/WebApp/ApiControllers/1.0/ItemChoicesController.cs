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
    /// Choices for the options in the item menu
    /// </summary>
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ItemChoicesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ItemChoiceMapper _mapper = new ItemChoiceMapper();


        /// <summary>
        /// Constructor
        /// </summary>
        public ItemChoicesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/ItemChoice
        /// <summary>
        /// Get all the item choices
        /// </summary>
        /// <returns>Array consisting of item choices</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(Roles = "Restaurant, Admin")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.ItemChoice>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<IEnumerable<V1DTO.ItemChoice>>> GetItemChoice()
        {
            return Ok((await _bll.ItemChoices.GetAllAsync()).Select(e => _mapper.MapItemChoice(e)));
        }

        // GET: api/ItemChoice/5
        /// <summary>
        /// Get a single item choice
        /// </summary>
        /// <param name="id">Id for item choice</param>
        /// <returns>Item choice</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(Roles = "Restaurant, Admin")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.ItemChoice))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.ItemChoice>> GetItemChoice(Guid id)
        {
            var itemChoice = await _bll.ItemChoices.FirstOrDefaultAsync(id);

            if (itemChoice == null)
            {
                return NotFound(new V1DTO.MessageDTO($"ItemChoice with id {id} not found"));
            }

            return Ok(_mapper.MapItemChoice(itemChoice));
        }

        // PUT: api/ItemChoice/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Update the item choice
        /// </summary>
        /// <param name="id">Id for item choice</param>
        /// <param name="itemChoice">Edited item choice</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(Roles = "Restaurant, Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutItemChoice(Guid id, V1DTO.ItemChoice itemChoice)
        {
            if (id != itemChoice.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("Id and ItemChoice.Id do not match"));
            }
            await _bll.ItemChoices.UpdateAsync(_mapper.Map(itemChoice));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/ItemChoice
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Create a new item choice
        /// </summary>
        /// <param name="itemChoice">New item choice info</param>
        /// <returns>Newly created item choice</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(Roles = "Restaurant, Admin")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.ItemChoice))]
        [HttpPost]
        public async Task<ActionResult<V1DTO.ItemChoice>> PostItemChoice(V1DTO.ItemChoice itemChoice)
        {
            var bllEntity = _mapper.Map(itemChoice);
            _bll.ItemChoices.Add(bllEntity);
            await _bll.SaveChangesAsync();
            itemChoice.Id = bllEntity.Id;

            return CreatedAtAction(nameof(GetItemChoice),
                new {id = itemChoice.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                itemChoice);
        }

        // DELETE: api/ItemChoice/5
        /// <summary>
        /// Delete the item choice
        /// </summary>
        /// <param name="id">Id for choice</param>
        /// <returns>No content</returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpDelete("{id}")]
        [Authorize(Roles = "Restaurant, Admin")]
        public async Task<ActionResult<V1DTO.ItemChoice>> DeleteItemChoice(Guid id)
        {
            var itemChoice = await _bll.ItemChoices.FirstOrDefaultAsync(id);
            if (itemChoice == null)
            {
                return NotFound(new V1DTO.MessageDTO("Item choice not found"));
            }
            
            await _bll.ItemChoices.RemoveAsync(itemChoice);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemChoiceExists(Guid id)
        {
            return _bll.ItemChoices.Exists(id);
        }
    }
}

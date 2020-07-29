using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using V1DTO=PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Relation between item and its type
    /// </summary>
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ItemInTypesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ItemInTypeMapper _mapper = new ItemInTypeMapper();
        
        /// <summary>
        /// Constructor
        /// </summary>
        public ItemInTypesController(IAppBLL bll)
        {
            _bll = bll;
        }


        // GET: api/ItemInType/5
        [HttpGet("{id}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult<V1DTO.ItemInType>> GetItemInType(Guid id)
        {
            var itemInType = await _bll.ItemInTypes.FirstOrDefaultAsync(id);

            if (itemInType == null)
            {
                return NotFound(new V1DTO.MessageDTO($"ItemInType with id {id} not found"));
            }

            return Ok(_mapper.Map(itemInType));
        }

        // POST: api/ItemInType
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Create a new relation between item and type
        /// </summary>
        /// <param name="itemInType">New relation</param>
        /// <returns>Newly created relation</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(Roles = "Restaurant, Admin")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.ItemChoice))]
        public async Task<ActionResult<V1DTO.ItemInType>> PostItemInType(V1DTO.ItemInType itemInType)
        {
            var bllEntity = _mapper.Map(itemInType);
            _bll.ItemInTypes.Add(bllEntity);
            await _bll.SaveChangesAsync();
            itemInType.Id = bllEntity.Id;

            return CreatedAtAction(nameof(GetItemInType),
                new {id = itemInType.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                itemInType);
        }

        // DELETE: api/ItemInType/5
        /// <summary>
        /// Delete the relation between item and type
        /// </summary>
        /// <param name="id">Id for relation</param>
        /// <returns>No content</returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpDelete("{id}")]
        [Authorize(Roles = "Restaurant, Admin")]
        public async Task<ActionResult<V1DTO.ItemInType>> DeleteItemInType(Guid id)
        {
            var ItemInType = await _bll.ItemInTypes.FirstOrDefaultAsync(id);
            if (ItemInType == null)
            {
                return NotFound(new V1DTO.MessageDTO("ItemInType not found"));
            }

            await _bll.ItemInTypes.RemoveAsync(ItemInType);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemTypeExists(Guid id)
        {
            return _bll.ItemInTypes.Exists(id);
        }
    }
}

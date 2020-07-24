using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using V1DTO=PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers._1._0
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ItemInTypesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ItemInTypeMapper _mapper = new ItemInTypeMapper();
        
        public ItemInTypesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/ItemInType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<V1DTO.ItemInType>>> GetItemInType()
        {
            return Ok((await _bll.ItemInTypes.GetAllAsync()).Select(e => _mapper.MapItemInType(e)));
        }

        // GET: api/ItemInType/5
        [HttpGet("{id}")]
        public async Task<ActionResult<V1DTO.ItemInType>> GetItemInType(Guid id)
        {
            var itemInType = await _bll.ItemInTypes.FirstOrDefaultAsync(id);

            if (itemInType == null)
            {
                return NotFound(new V1DTO.MessageDTO($"ItemInType with id {id} not found"));
            }

            return Ok(_mapper.Map(itemInType));
        }

        // PUT: api/ItemInType/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemType(Guid id, V1DTO.ItemInType itemInType)
        {
            if (id != itemInType.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("Id and ItemInType.Id do not match"));
            }

            await _bll.ItemInTypes.UpdateAsync(_mapper.Map(itemInType));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/ItemInType
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<V1DTO.ItemInType>> PostItemInType(V1DTO.ItemInType itemInType)
        {
            var bllEntity = _mapper.Map(itemInType);
            _bll.ItemInTypes.Add(bllEntity);
            await _bll.SaveChangesAsync();
            itemInType.Id = bllEntity.Id;

            return CreatedAtAction("GetItemInType",
                new {id = itemInType.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                itemInType);
        }

        // DELETE: api/ItemInType/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<V1DTO.ItemInType>> DeleteItemInType(Guid id)
        {
            var ItemInType = await _bll.ItemInTypes.FirstOrDefaultAsync(id);
            if (ItemInType == null)
            {
                return NotFound(new V1DTO.MessageDTO("ItemInType not found"));
            }

            await _bll.ItemInTypes.RemoveAsync(ItemInType);
            await _bll.SaveChangesAsync();

            return Ok(ItemInType);
        }

        private bool ItemTypeExists(Guid id)
        {
            return _bll.ItemInTypes.Exists(id);
        }
    }
}

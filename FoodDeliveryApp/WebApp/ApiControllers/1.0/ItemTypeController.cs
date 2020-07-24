using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ItemTypeController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ItemTypeMapper _mapper = new ItemTypeMapper();

        public ItemTypeController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/ItemType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemType>>> GetItemTypes()
        {
            return Ok((await _bll.ItemTypes.GetAllAsync()).Select(e => _mapper.MapItemType(e)));
        }

        // GET: api/ItemType/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemType>> GetItemType(Guid id)
        {
            var itemType = await _bll.ItemTypes.FirstOrDefaultAsync(id);

            if (itemType == null)
            {
                return NotFound(new MessageDTO($"ItemType with id {id} not found"));
            }

            return Ok(_mapper.Map(itemType));
        }

        // PUT: api/ItemType/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemType(Guid id, ItemType itemType)
        {
            if (id != itemType.Id)
            {
                return BadRequest(new MessageDTO("Id and ItemType.Id do not match"));
            }

            await _bll.ItemTypes.UpdateAsync(_mapper.Map(itemType));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/ItemType
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ItemType>> PostItemType(ItemType itemType)
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
        public async Task<ActionResult<ItemType>> DeleteItemType(Guid id)
        {
            var itemType = await _bll.ItemTypes.FirstOrDefaultAsync(id);
            if (itemType == null)
            {
                return NotFound(new MessageDTO("Item not found"));
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

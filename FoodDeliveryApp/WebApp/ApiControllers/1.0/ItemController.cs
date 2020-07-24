using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class ItemController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ItemMapper _mapper = new ItemMapper();

        public ItemController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Item
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems()
        {
            return Ok((await _bll.Items.GetAllAsync()).Select(e => _mapper.MapItem(e)));
        }

        // GET: api/Item/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(Guid id)
        {
            var item = await _bll.Items.FirstOrDefaultAsync(id);

            if (item == null)
            {
                return NotFound(new MessageDTO($"Item with id {id} not found"));
            }

            return Ok(_mapper.Map(item));
        }

        // PUT: api/Item/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(Guid id, Item item)
        {
            if (id != item.Id)
            {
                return BadRequest(new MessageDTO("Id and Item.Id do not match"));
            }

            await _bll.Items.UpdateAsync(_mapper.Map(item));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Item
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Item>> PostItem(Item item)
        {
            var bllEntity = _mapper.Map(item);
            _bll.Items.Add(bllEntity);
            await _bll.SaveChangesAsync();
            item.Id = bllEntity.Id;

            return CreatedAtAction("GetItem",
                new {id = item.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                item);
        }

        // DELETE: api/Item/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Item>> DeleteItem(Guid id)
        {
            var item = await _bll.Items.FirstOrDefaultAsync(id);
            if (item == null)
            {
                return NotFound(new MessageDTO("Item not found"));
            }

            await _bll.Items.RemoveAsync(item);
            await _bll.SaveChangesAsync();

            return Ok(item);
        }

        private bool ItemExists(Guid id)
        {
            return _bll.Items.Exists(id);
        }
    }
}

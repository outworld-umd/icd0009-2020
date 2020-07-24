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

namespace WebApp.ApiControllers._1._0
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ItemOptionsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ItemOptionMapper _mapper = new ItemOptionMapper();

        public ItemOptionsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/ItemOption
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemOption>>> GetItemOptions()
        {
            return Ok((await _bll.ItemOptions.GetAllAsync()).Select(e => _mapper.MapItemOption(e)));
        }

        // GET: api/ItemOption/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemOption>> GetItemOption(Guid id)
        {
            var itemOption = await _bll.ItemOptions.FirstOrDefaultAsync(id);

            if (itemOption == null)
            {
                return NotFound(new MessageDTO($"ItemOption with id {id} not found"));
            }

            return Ok(_mapper.Map(itemOption));
        }

        // PUT: api/ItemOption/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemOption(Guid id, ItemOption itemOption)
        {
            if (id != itemOption.Id)
            {
                return BadRequest(new MessageDTO("Id and ItemOption.Id do not match"));
            }

            await _bll.ItemOptions.UpdateAsync(_mapper.Map(itemOption));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/ItemOption
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ItemOption>> PostItemOption(ItemOption itemOption)
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
        [HttpDelete("{id}")]
        public async Task<ActionResult<ItemOption>> DeleteItemOption(Guid id)
        {
            var itemOption = await _bll.ItemOptions.FirstOrDefaultAsync(id);
            if (itemOption == null)
            {
                return NotFound(new MessageDTO("ItemOption not found"));
            }

            await _bll.ItemOptions.RemoveAsync(itemOption);
            await _bll.SaveChangesAsync();

            return Ok(itemOption);
        }

        private bool ItemOptionExists(Guid id)
        {
            return _bll.ItemOptions.Exists(id);
        }
    }
}

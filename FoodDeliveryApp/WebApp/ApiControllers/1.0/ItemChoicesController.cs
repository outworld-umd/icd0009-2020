using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
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
    public class ItemChoicesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ItemChoiceMapper _mapper = new ItemChoiceMapper();


        public ItemChoicesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/ItemChoice
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemChoice>>> GetItemChoice()
        {
            return Ok((await _bll.ItemChoices.GetAllAsync()).Select(e => _mapper.MapItemChoice(e)));
        }

        // GET: api/ItemChoice/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemChoice>> GetItemChoice(Guid id)
        {
            var itemChoice = await _bll.ItemChoices.FirstOrDefaultAsync(id);

            if (itemChoice == null)
            {
                return NotFound(new MessageDTO($"ItemChoice with id {id} not found"));
            }

            return Ok(_mapper.MapItemChoice(itemChoice));
        }

        // PUT: api/ItemChoice/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemChoice(Guid id, ItemChoice itemChoice)
        {
            if (id != itemChoice.Id)
            {
                return BadRequest(new MessageDTO("Id and ItemChoice.Id do not match"));
            }

            await _bll.ItemChoices.UpdateAsync(_mapper.Map(itemChoice));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/ItemChoice
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ItemChoice>> PostItemChoice(ItemChoice itemChoice)
        {
            var bllEntity = _mapper.Map(itemChoice);
            _bll.ItemChoices.Add(bllEntity);
            await _bll.SaveChangesAsync();
            itemChoice.Id = bllEntity.Id;

            return CreatedAtAction("GetItemChoice",
                new {id = itemChoice.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                itemChoice);
        }

        // DELETE: api/ItemChoice/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ItemChoice>> DeleteItemChoice(Guid id)
        {
            var itemChoice = await _bll.ItemChoices.FirstOrDefaultAsync(id);
            if (itemChoice == null)
            {
                return NotFound(new MessageDTO("Address not found"));
            }

            await _bll.ItemChoices.RemoveAsync(itemChoice);
            await _bll.SaveChangesAsync();

            return Ok(itemChoice);
        }

        private bool ItemChoiceExists(Guid id)
        {
            return _bll.ItemChoices.Exists(id);
        }
    }
}

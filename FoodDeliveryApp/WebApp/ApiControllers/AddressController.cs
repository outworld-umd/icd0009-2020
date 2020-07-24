using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;
using PublicApi.DTO.v1.Mappers.Base;

namespace WebApp.ApiControllers
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AddressController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly AddressMapper _mapper = new AddressMapper();


        public AddressController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Address
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddresses()
        {
            return Ok((await _bll.Addresses.GetAllAsync()).Select(e => _mapper.MapAddress(e)));
        }

        // GET: api/Address/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> GetAddress(Guid id)
        {
            var address = await _bll.Addresses.FirstOrDefaultAsync(id);

            if (address == null)
            {
                return NotFound(new MessageDTO($"Address with id {id} not found"));
            }

            return Ok(_mapper.Map(address));
        }

        // PUT: api/Address/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddress(Guid id, Address address)
        {
            if (id != address.Id)
            {
                return BadRequest(new MessageDTO("Id and Address.Id do not match"));
            }

            await _bll.Addresses.UpdateAsync(_mapper.Map(address));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Address
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Address>> PostAddress(Address address)
        {
            var bllEntity = _mapper.Map(address);
            _bll.Addresses.Add(bllEntity);
            await _bll.SaveChangesAsync();
            address.Id = bllEntity.Id;

            return CreatedAtAction("GetAddress",
                new {id = address.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                address);
        }

        // DELETE: api/Address/5
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Address))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<Address>> DeleteAddress(Guid id)
        {
            var address = await _bll.Addresses.FirstOrDefaultAsync(id);
            if (address == null)
            {
                return NotFound(new MessageDTO("Address not found"));
            }

            await _bll.Addresses.RemoveAsync(address);
            await _bll.SaveChangesAsync();

            return Ok(address);
        }

        // private bool AddressExists(Guid id)
        // {
        //     return _bll.Addresses.Any(e => e.Id == id);
        // }
    }
}

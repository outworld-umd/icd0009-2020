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
using V1DTO=PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Saved addresses of customers
    /// </summary>
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AddressesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly AddressMapper _mapper = new AddressMapper();


        /// <summary>
        /// Constructor
        /// </summary>
        public AddressesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Address
        /// <summary>
        /// Get all the addresses. If user is a customer, then only user-specific addresses
        /// </summary>
        /// <returns>Array consisting of addresses</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(Roles = "Customer, Admin")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.Address>))]
        public async Task<ActionResult<IEnumerable<V1DTO.Address>>> GetAddresses()
        {
            var userIdTKey = User.IsInRole("Admin") ? null : (Guid?) User.UserGuidId();
            return Ok((await _bll.Addresses.GetAllAsync(userIdTKey)).Select(e => _mapper.MapAddress(e)));
        }
        
        // GET: api/Address/User/5
        /// <summary>
        /// Get addresses that belong to a certain user
        /// </summary>
        /// <returns>Addresses for session</returns>
        [HttpGet("User/{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.Address>))]
        public async Task<ActionResult<IEnumerable<V1DTO.Address>>> GetAddressesByUser(Guid id)
        {
            return Ok((await _bll.Addresses.GetAllAsync(id)).Select(e => _mapper.MapAddress(e)));
        }

        // GET: api/Address/5
        /// <summary>
        /// Get a single address by given id
        /// </summary>
        /// <param name="id">Id for address</param>
        /// <returns>Address</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(Roles = "Customer, Admin")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Address))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.Address))]
        public async Task<ActionResult<V1DTO.Address>> GetAddress(Guid id)
        {
            var userIdTKey = User.IsInRole("Admin") ? null : (Guid?) User.UserGuidId();
            var address = await _bll.Addresses.FirstOrDefaultAsync(id, userIdTKey);

            if (address == null)
            {
                return NotFound(new V1DTO.MessageDTO($"Address with id {id} not found"));
            }

            return Ok(_mapper.MapAddress(address));
        }

        // PUT: api/Address/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Update the address
        /// </summary>
        /// <param name="id">Id for address</param>
        /// <param name="address">Edited address</param>
        /// <returns>No content</returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(Roles = "Customer, Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutAddress(Guid id, V1DTO.Address address)
        {
            var userIdTKey = User.IsInRole("Admin") ? null : (Guid?) User.UserGuidId();
            if (id != address.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("Id and Address.Id do not match"));
            }
            if (!await _bll.Addresses.ExistsAsync(address.Id, userIdTKey))
            {
                return NotFound(new V1DTO.MessageDTO($"Current user does not have session with this id {id}"));
            }
            var bllEntity = _mapper.Map(address);
            bllEntity.AppUserId = User.UserGuidId();
            await _bll.Addresses.UpdateAsync(bllEntity);
            await _bll.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/Address
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Create a new address for a current user
        /// </summary>
        /// <param name="address">New address info</param>
        /// <returns>Newly created address</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(Roles = "Customer, Admin")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.Address))]
        [HttpPost]
        public async Task<ActionResult<V1DTO.Address>> PostAddress(V1DTO.Address address)
        {
            var bllEntity = _mapper.Map(address);
            bllEntity.AppUserId = User.UserGuidId();
            _bll.Addresses.Add(bllEntity);
            await _bll.SaveChangesAsync();
            address.Id = bllEntity.Id;
            return CreatedAtAction(nameof(GetAddress),
                new {id = address.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                address);
        }
        
        // POST: api/Address/User/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Create a new address for a specific user
        /// </summary>
        /// <param name="id">Target user id</param>
        /// <param name="address">New address info</param>
        /// <returns>Newly created address</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.Address))]
        [HttpPost("User/{id}")]
        public async Task<ActionResult<V1DTO.Address>> PostAddressByUser(Guid id, [FromBody] V1DTO.Address address)
        {
            var bllEntity = _mapper.Map(address);
            bllEntity.AppUserId = id;
            _bll.Addresses.Add(bllEntity);
            await _bll.SaveChangesAsync();
            address.Id = bllEntity.Id;
            return CreatedAtAction(nameof(GetAddress),
                new {id = address.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                address);
        }

        // DELETE: api/Address/5
        /// <summary>
        /// Delete the address
        /// </summary>
        /// <param name="id">Id for address</param>
        /// <returns>No content</returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [Authorize(Roles = "Customer, Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<V1DTO.Address>> DeleteAddress(Guid id)
        {
            var userIdTKey = User.IsInRole("Admin") ? null : (Guid?) User.UserGuidId();
            var address = await _bll.Addresses.FirstOrDefaultAsync(id, userIdTKey);
            if (address == null)
            {
                return NotFound(new V1DTO.MessageDTO("Address not found"));
            }
            await _bll.Addresses.RemoveAsync(address);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool AddressExists(Guid id)
        {
            return _bll.Addresses.Exists(id);
        }
    }
}

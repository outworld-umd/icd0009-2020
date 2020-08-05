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
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderRowsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly OrderRowMapper _mapper = new OrderRowMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public OrderRowsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/OrderRow
        /// <summary>
        /// Get order rows for single session 
        /// </summary>
        /// <returns>order rows for session</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.OrderRow>))]
        public async Task<ActionResult<IEnumerable<V1DTO.OrderRow>>> GetOrderRows()
        {
            return Ok((await _bll.OrderRows.GetAllAsync()).Select(e => _mapper.MapOrderRow(e)));
        }

        // GET: api/OrderRow/5
        /// <summary>
        /// Get a single order row
        /// </summary>
        /// <param name="id">id for order row</param>
        /// <returns>order row</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.OrderRow))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.OrderRow))]
        public async Task<ActionResult<V1DTO.OrderRow>> GetOrderRow(Guid id)
        {
            var orderRow = await _bll.OrderRows.FirstOrDefaultAsync(id);

            if (orderRow == null)
            {
                return NotFound(new V1DTO.MessageDTO($"OrderRow with id {id} not found"));
            }

            return Ok(_mapper.Map(orderRow));
        }

        // PUT: api/OrderRow/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Update order row
        /// </summary>
        /// <param name="id"></param>
        /// <param name="orderRow"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutOrderRow(Guid id, V1DTO.OrderRow orderRow)
        {
            if (id != orderRow.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("Id and OrderRow.Id do not match"));
            }

            await _bll.OrderRows.UpdateAsync(_mapper.Map(orderRow));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/OrderRow
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Create/add a new order row
        /// </summary>
        /// <param name="orderRow">Order row info</param>
        /// <returns></returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.OrderRow))]
        [HttpPost]
        public async Task<ActionResult<V1DTO.OrderRow>> PostOrderRow(V1DTO.OrderRow orderRow)
        {
            var bllEntity = _mapper.Map(orderRow);
            _bll.OrderRows.Add(bllEntity);
            await _bll.SaveChangesAsync();
            orderRow.Id = bllEntity.Id;

            return CreatedAtAction("GetOrderRow",
                new {id = orderRow.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                orderRow);
        }

        // DELETE: api/OrderRow/5
        /// <summary>
        /// Deletes the order row
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.OrderRow))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<V1DTO.OrderRow>> DeleteOrderRow(Guid id)
        {
            var orderRow = await _bll.OrderRows.FirstOrDefaultAsync(id);
            if (orderRow == null)
            {
                return NotFound(new V1DTO.MessageDTO("OrderRow not found"));
            }

            await _bll.OrderRows.RemoveAsync(orderRow);
            await _bll.SaveChangesAsync();

            return Ok(orderRow);
        }

        private bool OrderRowExists(Guid id)
        {
            return _bll.OrderRows.Exists(id);
        }
    }
}

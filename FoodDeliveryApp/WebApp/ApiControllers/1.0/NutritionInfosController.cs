using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using V1DTO=PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Saved nutrition infos of items
    /// </summary>
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class NutritionInfosController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly NutritionInfoMapper _mapper = new NutritionInfoMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public NutritionInfosController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/NutritionInfo
        /// <summary>
        /// Get a all the nutrition infos
        /// </summary>
        /// <returns>Array consisting of nutrition infos</returns>
        /// 
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(Roles = "Restaurant, Admin")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.NutritionInfo>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<IEnumerable<V1DTO.NutritionInfo>>> GetNutritionInfos()
        {
            return Ok((await _bll.NutritionInfos.GetAllAsync()).Select(e => _mapper.MapNutritionInfo(e)));
        }

        // GET: api/NutritionInfo/5
        /// <summary>
        /// Get a single nutrtition info
        /// </summary>
        /// <param name="id">Id for nutrtion info</param>
        /// <returns>Nutrtion info</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(Roles = "Customer, Restaurant, Admin")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.NutritionInfo))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.NutritionInfo>> GetNutritionInfo(Guid id)
        {
            var nutritionInfo = await _bll.NutritionInfos.FirstOrDefaultAsync(id);

            if (nutritionInfo == null)
            {
                return NotFound(new V1DTO.MessageDTO($"NutritionInfo with id {id} not found"));
            }

            return Ok(_mapper.Map(nutritionInfo));
        }
        
        /// <summary>
        /// Get nutrition infos by item
        /// </summary>
        /// <param name="itemId">id of item</param>
        /// <returns></returns>
        [HttpGet("Item/{itemId}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(Roles = "Customer, Restaurant, Admin")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.NutritionInfo))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<IEnumerable<V1DTO.NutritionInfo>>> GetNutritionInfosByItem(Guid itemId)
        {
            return Ok((await _bll.NutritionInfos.GetAllByItemAsync(itemId)).Select(e => _mapper.MapNutritionInfo(e)));
        }

        // PUT: api/NutritionInfo/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Update the nutrition info
        /// </summary>
        /// <param name="id">Id for nutrition info</param>
        /// <param name="nutritionInfo">Edited nutrition info</param>
        /// <returns>No content</returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(Roles = "Restaurant, Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutNutritionInfo(Guid id, V1DTO.NutritionInfo nutritionInfo)
        {
            if (id != nutritionInfo.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("Id and NutritionInfo.Id do not match"));
            }

            await _bll.NutritionInfos.UpdateAsync(_mapper.Map(nutritionInfo));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/NutritionInfo
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Create a new nutrition info
        /// </summary>
        /// <param name="nutritionInfo">New nutrition info info</param>
        /// <returns>Newly created nutrition info</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(Roles = "Restaurant, Admin")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.NutritionInfo))]
        public async Task<ActionResult<V1DTO.NutritionInfo>> PostNutritionInfo(V1DTO.NutritionInfo nutritionInfo)
        {
            var bllEntity = _mapper.Map(nutritionInfo);
            _bll.NutritionInfos.Add(bllEntity);
            await _bll.SaveChangesAsync();
            nutritionInfo.Id = bllEntity.Id;

            return CreatedAtAction("GetNutritionInfo",
                new {id = nutritionInfo.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                nutritionInfo);
        }

        // DELETE: api/NutritionInfo/5
        /// <summary>
        /// Delete the nutrition info
        /// </summary>
        /// <param name="id">Id for nutrition info</param>
        /// <returns>No content</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(Roles = "Restaurant, Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.NutritionInfo>> DeleteNutritionInfo(Guid id)
        {
            var nutritionInfo = await _bll.NutritionInfos.FirstOrDefaultAsync(id);
            if (nutritionInfo == null)
            {
                return NotFound(new V1DTO.MessageDTO("NutritionInfo not found"));
            }

            await _bll.NutritionInfos.RemoveAsync(nutritionInfo);
            await _bll.SaveChangesAsync();

            return Ok(nutritionInfo);
        }

        private bool NutritionInfoExists(Guid id)
        {
            return _bll.NutritionInfos.Exists(id);
        }
    }
}

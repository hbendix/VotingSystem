using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using VotingSystemEntities;
using VotingSystemServices.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VotingSystemApi.Controllers
{
    /// <summary>
    /// Area endpoint
    /// </summary>
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AreaController : Controller
    {
        IAreaServices _areaServices;

        public AreaController(IAreaServices areaServices)
        {
            _areaServices = areaServices;
        }

        /// <summary>
        /// Get all areas
        /// </summary>
        /// <returns>List of Areas</returns>
        [Route("Get")]
        [HttpGet]
        public IActionResult GetAllAreas()
        {
            try
            {
                return Ok(_areaServices.GetAllAreas());
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        /// <summary>
        /// Add an area
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        [Route("Admin/Add")]
        [HttpPost]
        public async Task<IActionResult> AddArea(AreaViewModel area)
        {
            try
            {
                await _areaServices.AddArea(area);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        /// <summary>
        /// Update an area
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        [Route("Admin/Update")]
        [HttpPost]
        public async Task<IActionResult> UpdateArea(AreaViewModel area)
        {
            try
            {
                await _areaServices.UpdateArea(area);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        /// <summary>
        /// Delete a area
        /// </summary>
        /// <param name="areaId"></param>
        /// <returns></returns>
        [Route("Admin/Delete")]
        [HttpPost]
        public async Task<IActionResult> DeleteArea(int areaId)
        {
            try
            {
                await _areaServices.DeleteArea(areaId);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}

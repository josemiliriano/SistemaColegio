using Application.SubPeriodo;
using Application.SubPeriodo.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SistemaColegio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrador")]
    public class SubPeriodController : ControllerBase
    {
        private readonly ISubPeriodAppService _subPeriodAppService;

        public SubPeriodController(ISubPeriodAppService subPeriodAppService)
        {
            _subPeriodAppService = subPeriodAppService;
        }


        // GET: api/SubPeriod
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _subPeriodAppService.GetAllSubPeriod();

            return Ok(result);
        }


        // GET: api/SubPeriod/not-deleted
        [HttpGet("not-deleted")]
        public async Task<IActionResult> GetNotDeleted()
        {
            var result = await _subPeriodAppService.GetSubPeriodNotDeleted();

            return Ok(result);
        }


        // GET: api/SubPeriod/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _subPeriodAppService.GetSubPeriodById(id);

            if (result == null)
            {
                return NotFound(new
                {
                    mensaje = "El subperiodo no existe."
                });
            }

            return Ok(result);
        }


        // POST: api/SubPeriod
        [HttpPost]
        public async Task<IActionResult> Add(SubPeriodDto subPeriod)
        {
            try
            {
                var result = await _subPeriodAppService.AddSubPeriod(subPeriod);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    mensaje = ex.Message
                });
            }
        }


        // PUT: api/SubPeriod
        [HttpPut]
        public async Task<IActionResult> Update(SubPeriodDto subPeriod)
        {
            try
            {
                var result = await _subPeriodAppService.UpdateSubPeriod(subPeriod);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    mensaje = ex.Message
                });
            }
        }


        // DELETE: api/SubPeriod
        [HttpDelete]
        public async Task<IActionResult> Delete(SubPeriodDto subPeriod)
        {
            try
            {
                var result = await _subPeriodAppService.DeleteSubPeriod(subPeriod);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    mensaje = ex.Message
                });
            }
        }
    }
}

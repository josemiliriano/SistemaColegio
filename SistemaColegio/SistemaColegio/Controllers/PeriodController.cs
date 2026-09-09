using Application.Periodo;
using Application.Periodo.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SistemaColegio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrador")]
    public class PeriodController : ControllerBase
    {
        private readonly IPeriodAppService _periodAppService;

        public PeriodController(IPeriodAppService periodAppService)
        {
            _periodAppService = periodAppService;
        }

        // GET: api/Period
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var periods =
                await _periodAppService.GetAllPeriod();

            return Ok(periods);
        }

        // GET: api/Period/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var period =
                await _periodAppService.GetPeriodById(id);

            if (period == null)
            {
                return NotFound(new
                {
                    mensaje = "El período no existe."
                });
            }

            return Ok(period);
        }

        // POST: api/Period
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] PeriodDto period)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var newPeriod =
                    await _periodAppService.AddPeriod(period);

                return Ok(newPeriod);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    mensaje = ex.Message
                });
            }
        }

        // PUT: api/Period/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] PeriodDto period)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                period.IdPeriodo = id;

                var updatedPeriod =
                    await _periodAppService.UpdatePeriod(period);

                if (updatedPeriod == null)
                {
                    return NotFound(new
                    {
                        mensaje = "El período no existe."
                    });
                }

                return Ok(updatedPeriod);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    mensaje = ex.Message
                });
            }
        }

        // DELETE: api/Period/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var period =
                    await _periodAppService.GetPeriodById(id);

                if (period == null)
                {
                    return NotFound(new
                    {
                        mensaje = "El período no existe."
                    });
                }

                var deletedPeriod =
                    await _periodAppService.DeletePeriod(period);

                return Ok(deletedPeriod);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    mensaje = ex.Message
                });
            }
        }

        // GET: api/Period/not-deleted
        [HttpGet("not-deleted")]
        public async Task<IActionResult> GetNotDeleted()
        {
            var periods =
                await _periodAppService.GetPeriodNotDeleted();

            return Ok(periods);
        }
    }
}
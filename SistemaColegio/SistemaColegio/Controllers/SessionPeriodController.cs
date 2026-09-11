using Application.PeriodoSesion;
using Application.PeriodoSesion.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SistemaColegio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrador")]
    public class SessionPeriodController : ControllerBase
    {
        private readonly ISessionPeriodAppService
            _sessionPeriodAppService;

        public SessionPeriodController(
            ISessionPeriodAppService sessionPeriodAppService)
        {
            _sessionPeriodAppService =
                sessionPeriodAppService;
        }

        // GET: api/SessionPeriod
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var sessionPeriods =
                await _sessionPeriodAppService
                    .GetAllSessionPeriod();

            return Ok(sessionPeriods);
        }

        // GET: api/SessionPeriod/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var sessionPeriod =
                await _sessionPeriodAppService
                    .GetSessionPeriodById(id);

            if (sessionPeriod == null)
            {
                return NotFound(new
                {
                    mensaje = "La relación sección-período no existe."
                });
            }

            return Ok(sessionPeriod);
        }

        // POST: api/SessionPeriod
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] SessionPeriodDto sessionPeriod)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var newSessionPeriod =
                    await _sessionPeriodAppService
                        .AddSessionPeriod(sessionPeriod);

                return Ok(newSessionPeriod);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    mensaje = ex.Message
                });
            }
        }

        // PUT: api/SessionPeriod/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] SessionPeriodDto sessionPeriod)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                sessionPeriod.IdSessionPeriod = id;

                var updatedSessionPeriod =
                    await _sessionPeriodAppService
                        .UpdateSessionPeriod(sessionPeriod);

                if (updatedSessionPeriod == null)
                {
                    return NotFound(new
                    {
                        mensaje = "La relación sección-período no existe."
                    });
                }

                return Ok(updatedSessionPeriod);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    mensaje = ex.Message
                });
            }
        }

        // DELETE: api/SessionPeriod/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var sessionPeriod =
                    await _sessionPeriodAppService
                        .GetSessionPeriodById(id);

                if (sessionPeriod == null)
                {
                    return NotFound(new
                    {
                        mensaje = "La relación sección-período no existe."
                    });
                }

                var deletedSessionPeriod =
                    await _sessionPeriodAppService
                        .DeleteSessionPeriod(sessionPeriod);

                return Ok(deletedSessionPeriod);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    mensaje = ex.Message
                });
            }
        }

        // GET: api/SessionPeriod/not-deleted
        [HttpGet("not-deleted")]
        public async Task<IActionResult> GetNotDeleted()
        {
            var sessionPeriods =
                await _sessionPeriodAppService
                    .GetSessionPeriodNotDeleted();

            return Ok(sessionPeriods);
        }
    }
}
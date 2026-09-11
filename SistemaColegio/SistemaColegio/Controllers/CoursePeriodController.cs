using Application.CursoPeriodo;
using Application.CursoPeriodo.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SistemaColegio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrador")]
    public class CoursePeriodController : ControllerBase
    {
        private readonly ICoursePeriodAppService _coursePeriodAppService;

        public CoursePeriodController(
            ICoursePeriodAppService coursePeriodAppService)
        {
            _coursePeriodAppService = coursePeriodAppService;
        }

        // GET: api/CoursePeriod
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var coursePeriods =
                await _coursePeriodAppService.GetAllCoursePeriod();

            return Ok(coursePeriods);
        }

        // GET: api/CoursePeriod/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var coursePeriod =
                await _coursePeriodAppService
                    .GetCoursePeriodById(id);

            if (coursePeriod == null)
            {
                return NotFound(new
                {
                    mensaje = "La relación curso-período no existe."
                });
            }

            return Ok(coursePeriod);
        }

        // POST: api/CoursePeriod
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CursoPeriodoDto coursePeriod)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var newCoursePeriod =
                    await _coursePeriodAppService
                        .AddCoursePeriod(coursePeriod);

                return Ok(newCoursePeriod);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    mensaje = ex.Message
                });
            }
        }

        // PUT: api/CoursePeriod/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] CursoPeriodoDto coursePeriod)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                coursePeriod.IdCursoPeriodo = id;

                var updatedCoursePeriod =
                    await _coursePeriodAppService
                        .UpdateCoursePeriod(coursePeriod);

                if (updatedCoursePeriod == null)
                {
                    return NotFound(new
                    {
                        mensaje = "La relación curso-período no existe."
                    });
                }

                return Ok(updatedCoursePeriod);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    mensaje = ex.Message
                });
            }
        }

        // DELETE: api/CoursePeriod/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var coursePeriod =
                    await _coursePeriodAppService
                        .GetCoursePeriodById(id);

                if (coursePeriod == null)
                {
                    return NotFound(new
                    {
                        mensaje = "La relación curso-período no existe."
                    });
                }

                var deletedCoursePeriod =
                    await _coursePeriodAppService
                        .DeleteCoursePeriod(coursePeriod);

                return Ok(deletedCoursePeriod);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    mensaje = ex.Message
                });
            }
        }

        // GET: api/CoursePeriod/not-deleted
        [HttpGet("not-deleted")]
        public async Task<IActionResult> GetNotDeleted()
        {
            var coursePeriods =
                await _coursePeriodAppService
                    .GetCoursePeriodNotDeleted();

            return Ok(coursePeriods);
        }
    }
}
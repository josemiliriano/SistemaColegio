using Application.SubPeriodos;
using Application.SubPeriodos.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SistemaColegio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrador")]
    public class AcademicSubPeriodController : ControllerBase
    {
        private readonly IAcademicSubPeriodAppService _academicSubPeriodAppService;
    public AcademicSubPeriodController(
        IAcademicSubPeriodAppService academicSubPeriodAppService)
        {
            _academicSubPeriodAppService = academicSubPeriodAppService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var academicSubPeriods =
                await _academicSubPeriodAppService.GetAllAcademicSubPeriod();

            return Ok(academicSubPeriods);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var academicSubPeriod =
                await _academicSubPeriodAppService
                    .GetAcademicSubPeriodById(id);

            if (academicSubPeriod == null)
            {
                return NotFound(new
                {
                    mensaje = "El subperíodo académico no existe."
                });
            }

            return Ok(academicSubPeriod);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] AcademicSubPeriodDto academicSubPeriod)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var newAcademicSubPeriod =
                    await _academicSubPeriodAppService
                        .AddAcademicSubPeriod(academicSubPeriod);

                return Ok(newAcademicSubPeriod);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    mensaje = ex.Message
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] AcademicSubPeriodDto academicSubPeriod)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                academicSubPeriod.IdPeriodoAcademico = id;

                var updatedAcademicSubPeriod =
                    await _academicSubPeriodAppService
                        .UpdateAcademicSubPeriod(academicSubPeriod);

                if (updatedAcademicSubPeriod == null)
                {
                    return NotFound(new
                    {
                        mensaje = "El subperíodo académico no existe."
                    });
                }

                return Ok(updatedAcademicSubPeriod);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    mensaje = ex.Message
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var academicSubPeriod =
                    await _academicSubPeriodAppService
                        .GetAcademicSubPeriodById(id);

                if (academicSubPeriod == null)
                {
                    return NotFound(new
                    {
                        mensaje = "El subperíodo académico no existe."
                    });
                }

                var deletedAcademicSubPeriod =
                    await _academicSubPeriodAppService
                        .DeleteAcademicSubPeriod(academicSubPeriod);

                return Ok(deletedAcademicSubPeriod);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    mensaje = ex.Message
                });
            }
        }

        [HttpGet("not-deleted")]
        public async Task<IActionResult> GetNotDeleted()
        {
            var academicSubPeriods =
                await _academicSubPeriodAppService
                    .GetAcademicSubPeriodNotDeleted();

            return Ok(academicSubPeriods);
        }
    }
}

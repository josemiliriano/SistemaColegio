using Application.MesPeriodo;
using Application.MesPeriodo.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SistemaColegio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrador")]
    public class AcademicMonthController : ControllerBase
    {
        private readonly IAcademicMonthAppService _academicMonthAppService;
    public AcademicMonthController(IAcademicMonthAppService academicMonthAppService)
        {
            _academicMonthAppService = academicMonthAppService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var academicMonths =
                await _academicMonthAppService.GetAllAcademicMonth();

            return Ok(academicMonths);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var academicMonth =
                await _academicMonthAppService
                    .GetAcademicMonthById(id);

            if (academicMonth == null)
            {
                return NotFound(new
                {
                    mensaje = "El mes académico no existe."
                });
            }

            return Ok(academicMonth);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] AcademicMonthDto academicMonth)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var newAcademicMonth =
                    await _academicMonthAppService
                        .AddAcademicMonth(academicMonth);

                return Ok(newAcademicMonth);
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
            [FromBody] AcademicMonthDto academicMonth)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                academicMonth.IdMesAcademico = id;

                var updatedAcademicMonth =
                    await _academicMonthAppService
                        .UpdateAcademicMonth(academicMonth);

                if (updatedAcademicMonth == null)
                {
                    return NotFound(new
                    {
                        mensaje = "El mes académico no existe."
                    });
                }

                return Ok(updatedAcademicMonth);
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
                var academicMonth =
                    await _academicMonthAppService
                        .GetAcademicMonthById(id);

                if (academicMonth == null)
                {
                    return NotFound(new
                    {
                        mensaje = "El mes académico no existe."
                    });
                }

                var deletedAcademicMonth =
                    await _academicMonthAppService
                        .DeleteAcademicMonth(academicMonth);

                return Ok(deletedAcademicMonth);
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
            var academicMonths =
                await _academicMonthAppService
                    .GetAcademicMonthNotDeleted();

            return Ok(academicMonths);
        }
    }
}

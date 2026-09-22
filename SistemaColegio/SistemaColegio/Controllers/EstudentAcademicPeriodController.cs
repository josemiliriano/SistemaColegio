using Application.PeriodoAcademicoEstudiante;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SistemaColegio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrador")]
    public class EstudentAcademicPeriodController : ControllerBase
    {
        private readonly IEstudentAcademicPeriodAppService _EstudentAcademicPeriodAppService;

        public EstudentAcademicPeriodController(IEstudentAcademicPeriodAppService EstudentAcademicPeriodAppService)
        {
            _EstudentAcademicPeriodAppService = EstudentAcademicPeriodAppService;
                
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result =
                await _EstudentAcademicPeriodAppService
                    .GetAllStudentAcademicPeriod();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result =
                await _EstudentAcademicPeriodAppService
                    .GetStudentAcademicPeriodById(id);

            if (result == null)
            {
                return NotFound(new
                {
                    mensaje = "El historial académico no existe."
                });
            }

            return Ok(result);
        }

        [HttpGet("Estudent/{idEstudiante}")]
        public async Task<IActionResult> GetByStudent(
            int idEstudiante)
        {
            var result = await _EstudentAcademicPeriodAppService.GetStudentAcademicPeriodByStudent(idEstudiante);
            return Ok(result);
        }
    }
}

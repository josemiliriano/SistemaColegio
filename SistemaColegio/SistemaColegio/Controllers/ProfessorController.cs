using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Application.Profesor;
using Application.Profesor.DTOs;

namespace SistemaColegio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrador")]
    public class ProfessorController : ControllerBase
    {
        private readonly IProfesorAppService _profesorAppService;

        public ProfessorController(IProfesorAppService profesorAppService)
        {
            _profesorAppService = profesorAppService;
        }

        // GET: api/Professor
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var professors = await _profesorAppService.GetAllProfessor();
            return Ok(professors);
        }

        // GET: api/Professor/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var professor = await _profesorAppService.GetProfessorById(id);
            if (professor == null)
            {
                return NotFound(new { mensaje = "El profesor no existe." });
            }
            return Ok(professor);
        }

        // POST: api/Professor
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProfesorDto professor)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var newProfessor = await _profesorAppService.AddProfessor(professor);
                return Ok(newProfessor);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // PUT: api/Professor/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProfesorDto professor)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var updatedProfessor = await _profesorAppService.UpdateProfessor(id, professor);
                if (updatedProfessor == null) return NotFound(new { mensaje = "El profesor no existe." });
                return Ok(updatedProfessor);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // DELETE: api/Professor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deletedProfessor = await _profesorAppService.DeleteProfessor(id);
                if (deletedProfessor == null) return NotFound(new { mensaje = "El profesor no existe." });
                return Ok(deletedProfessor);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // GET: api/Professor/not-deleted
        [HttpGet("not-deleted")]
        public async Task<IActionResult> GetNotDeleted()
        {
            var professors = await _profesorAppService.GetProfessorNotDeleted();
            return Ok(professors);
        }
    }
}

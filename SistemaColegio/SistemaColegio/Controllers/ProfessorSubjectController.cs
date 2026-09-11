using Application.ProfesorMateria;
using Application.ProfesorMateria.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SistemaColegio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrador")]
    public class ProfessorSubjectController : ControllerBase
    {
        private readonly IProfessorSubjectAppService
            _professorSubjectAppService;

        public ProfessorSubjectController(
            IProfessorSubjectAppService professorSubjectAppService)
        {
            _professorSubjectAppService = professorSubjectAppService;
        }

        // GET: api/ProfessorSubject
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var professorSubjects =
                await _professorSubjectAppService
                    .GetAllProfessorSubject();

            return Ok(professorSubjects);
        }

        // GET: api/ProfessorSubject/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var professorSubject =
                await _professorSubjectAppService
                    .GetProfessorSubjectById(id);

            if (professorSubject == null)
            {
                return NotFound(new
                {
                    mensaje = "La relación profesor-materia no existe."
                });
            }

            return Ok(professorSubject);
        }

        // POST: api/ProfessorSubject
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] ProfessorSubjectDto professorSubject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var newProfessorSubject =
                    await _professorSubjectAppService
                        .AddProfessorSubject(professorSubject);

                return Ok(newProfessorSubject);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    mensaje = ex.Message
                });
            }
        }

        // PUT: api/ProfessorSubject/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] ProfessorSubjectDto professorSubject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                professorSubject.IdProfesorMateria = id;

                var updatedProfessorSubject =
                    await _professorSubjectAppService
                        .UpdateProfessorSubject(professorSubject);

                if (updatedProfessorSubject == null)
                {
                    return NotFound(new
                    {
                        mensaje = "La relación profesor-materia no existe."
                    });
                }

                return Ok(updatedProfessorSubject);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    mensaje = ex.Message
                });
            }
        }

        // DELETE: api/ProfessorSubject/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var professorSubject =
                    await _professorSubjectAppService
                        .GetProfessorSubjectById(id);

                if (professorSubject == null)
                {
                    return NotFound(new
                    {
                        mensaje = "La relación profesor-materia no existe."
                    });
                }

                var deletedProfessorSubject =
                    await _professorSubjectAppService
                        .DeleteProfessorSubject(professorSubject);

                return Ok(deletedProfessorSubject);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    mensaje = ex.Message
                });
            }
        }

        // GET: api/ProfessorSubject/not-deleted
        [HttpGet("not-deleted")]
        public async Task<IActionResult> GetNotDeleted()
        {
            var professorSubjects =
                await _professorSubjectAppService
                    .GetProfessorSubjectNotDeleted();

            return Ok(professorSubjects);
        }
    }
}
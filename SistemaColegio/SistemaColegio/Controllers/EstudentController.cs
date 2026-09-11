using Application.Estudiante;
using Application.Estudiante.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SistemaColegio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrador")]
    public class EstudentController : ControllerBase
    {
        private readonly IEstudentAppService _estudentAppService;

    public EstudentController(IEstudentAppService estudentAppService)
        {
            _estudentAppService = estudentAppService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await _estudentAppService.GetAllEstudent();

            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var student = await _estudentAppService.GetEstudentById(id);

            if (student == null)
            {
                return NotFound(new
                {
                    mensaje = "El estudiante no existe."
                });
            }

            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] EstudentDto student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var newStudent =
                    await _estudentAppService.AddEstudent(student);

                return Ok(newStudent);
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
            [FromBody] EstudentDto student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                student.IdEstudiante = id;

                var updatedStudent =
                    await _estudentAppService.UpdateEstudent(student);

                if (updatedStudent == null)
                {
                    return NotFound(new
                    {
                        mensaje = "El estudiante no existe."
                    });
                }

                return Ok(updatedStudent);
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
                var student =
                    await _estudentAppService.GetEstudentById(id);

                if (student == null)
                {
                    return NotFound(new
                    {
                        mensaje = "El estudiante no existe."
                    });
                }

                var deletedStudent =
                    await _estudentAppService.DeleteEstudent(student);

                return Ok(deletedStudent);
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
            var students =
                await _estudentAppService.GetEstudentNotDeleted();

            return Ok(students);
        }
    }

}

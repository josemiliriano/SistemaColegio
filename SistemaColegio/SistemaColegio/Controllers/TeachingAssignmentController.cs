using Application.AsignacionDocente;
using Application.AsignacionDocente.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SistemaColegio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrador")]
    public class TeachingAssignmentController : ControllerBase
    {
        private readonly ITeachingAssignmentAppService
            _teachingAssignmentAppService;

        public TeachingAssignmentController(
            ITeachingAssignmentAppService teachingAssignmentAppService)
        {
            _teachingAssignmentAppService =
                teachingAssignmentAppService;
        }

        // GET: api/TeachingAssignment
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var teachingAssignments =
                await _teachingAssignmentAppService
                    .GetAllTeachingAssignment();

            return Ok(teachingAssignments);
        }

        // GET: api/TeachingAssignment/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var teachingAssignment =
                await _teachingAssignmentAppService
                    .GetTeachingAssignmentById(id);

            if (teachingAssignment == null)
            {
                return NotFound(new
                {
                    mensaje = "La asignación docente no existe."
                });
            }

            return Ok(teachingAssignment);
        }

        // POST: api/TeachingAssignment
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] TeachingAssignmentDto teachingAssignment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var newTeachingAssignment =
                    await _teachingAssignmentAppService
                        .AddTeachingAssignment(teachingAssignment);

                return Ok(newTeachingAssignment);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    mensaje = ex.Message
                });
            }
        }

        // PUT: api/TeachingAssignment/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] TeachingAssignmentDto teachingAssignment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                teachingAssignment.IdAsignacionDocente = id;

                var updatedTeachingAssignment =
                    await _teachingAssignmentAppService
                        .UpdateTeachingAssignment(teachingAssignment);

                if (updatedTeachingAssignment == null)
                {
                    return NotFound(new
                    {
                        mensaje = "La asignación docente no existe."
                    });
                }

                return Ok(updatedTeachingAssignment);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    mensaje = ex.Message
                });
            }
        }

        // DELETE: api/TeachingAssignment/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var teachingAssignment =
                    await _teachingAssignmentAppService
                        .GetTeachingAssignmentById(id);

                if (teachingAssignment == null)
                {
                    return NotFound(new
                    {
                        mensaje = "La asignación docente no existe."
                    });
                }

                var deletedTeachingAssignment =
                    await _teachingAssignmentAppService
                        .DeleteTeachingAssignment(
                            teachingAssignment);

                return Ok(deletedTeachingAssignment);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    mensaje = ex.Message
                });
            }
        }

        // GET: api/TeachingAssignment/not-deleted
        [HttpGet("not-deleted")]
        public async Task<IActionResult> GetNotDeleted()
        {
            var teachingAssignments =
                await _teachingAssignmentAppService
                    .GetTeachingAssignmentNotDeleted();

            return Ok(teachingAssignments);
        }
    }
}
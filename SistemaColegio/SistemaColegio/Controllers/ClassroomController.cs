using Application.Aula;
using Application.Aula.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SistemaColegio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrador")]
    public class ClassroomController : ControllerBase
    {
        private readonly IClassroomAppService _classroomAppService;

        public ClassroomController(IClassroomAppService classroomAppService)
        {
            _classroomAppService = classroomAppService;
        }

        // GET: api/Classroom
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var classrooms =
                await _classroomAppService.GetAllClassroom();

            return Ok(classrooms);
        }

        // GET: api/Classroom/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var classroom =
                await _classroomAppService.GetClassroomById(id);

            if (classroom == null)
            {
                return NotFound(new
                {
                    mensaje = "El aula no existe."
                });
            }

            return Ok(classroom);
        }

        // POST: api/Classroom
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] ClassroomDto classroom)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var newClassroom =
                    await _classroomAppService.AddClassroom(classroom);

                return Ok(newClassroom);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    mensaje = ex.Message
                });
            }
        }

        // PUT: api/Classroom/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] ClassroomDto classroom)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                classroom.IdAula = id;

                var updatedClassroom =
                    await _classroomAppService.UpdateClassroom(classroom);

                if (updatedClassroom == null)
                {
                    return NotFound(new
                    {
                        mensaje = "El aula no existe."
                    });
                }

                return Ok(updatedClassroom);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    mensaje = ex.Message
                });
            }
        }

        // DELETE: api/Classroom/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var classroom =
                    await _classroomAppService.GetClassroomById(id);

                if (classroom == null)
                {
                    return NotFound(new
                    {
                        mensaje = "El aula no existe."
                    });
                }

                var deletedClassroom =
                    await _classroomAppService.DeleteClassroom(classroom);

                return Ok(deletedClassroom);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    mensaje = ex.Message
                });
            }
        }

        // GET: api/Classroom/not-deleted
        [HttpGet("not-deleted")]
        public async Task<IActionResult> GetNotDeleted()
        {
            var classrooms =
                await _classroomAppService.GetClassroomNotDeleted();

            return Ok(classrooms);
        }
    }
}
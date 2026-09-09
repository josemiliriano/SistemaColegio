using Application.Curso;
using Application.Curso.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SistemaColegio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrador")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseAppService _courseAppService;

        public CourseController(ICourseAppService courseAppService)
        {
            _courseAppService = courseAppService;
        }

        // GET: api/Course
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var courses =
                await _courseAppService.GetAllCourse();

            return Ok(courses);
        }

        // GET: api/Course/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var course =
                await _courseAppService.GetCourseById(id);

            if (course == null)
            {
                return NotFound(new
                {
                    mensaje = "El curso no existe."
                });
            }

            return Ok(course);
        }

        // POST: api/Course
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CourseDto course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var newCourse =
                    await _courseAppService.AddCourse(course);

                return Ok(newCourse);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    mensaje = ex.Message
                });
            }
        }

        // PUT: api/Course/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] CourseDto course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                course.IdCurso = id;

                var updatedCourse =
                    await _courseAppService.UpdateCourse(course);

                if (updatedCourse == null)
                {
                    return NotFound(new
                    {
                        mensaje = "El curso no existe."
                    });
                }

                return Ok(updatedCourse);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    mensaje = ex.Message
                });
            }
        }

        // DELETE: api/Course/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var course =
                    await _courseAppService.GetCourseById(id);

                if (course == null)
                {
                    return NotFound(new
                    {
                        mensaje = "El curso no existe."
                    });
                }

                var deletedCourse =
                    await _courseAppService.DeleteCourse(course);

                return Ok(deletedCourse);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    mensaje = ex.Message
                });
            }
        }

        // GET: api/Course/not-deleted
        [HttpGet("not-deleted")]
        public async Task<IActionResult> GetNotDeleted()
        {
            var courses =
                await _courseAppService.GetCourseNotDeleted();

            return Ok(courses);
        }
    }
}

using Application.CursoMateria;
using Application.CursoMateria.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SistemaColegio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrador")]
    public class CourseSubjectController : ControllerBase
    {
        private readonly ICourseSubjectAppService _courseSubjectAppService;

        public CourseSubjectController(
            ICourseSubjectAppService courseSubjectAppService)
        {
            _courseSubjectAppService = courseSubjectAppService;
        }

        // GET: api/CourseSubject
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var courseSubjects =
                await _courseSubjectAppService
                    .GetAllCourseSubject();

            return Ok(courseSubjects);
        }

        // GET: api/CourseSubject/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var courseSubject =
                await _courseSubjectAppService
                    .GetCourseSubjectById(id);

            if (courseSubject == null)
            {
                return NotFound(new
                {
                    mensaje = "La relación curso-materia no existe."
                });
            }

            return Ok(courseSubject);
        }

        // POST: api/CourseSubject
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CourseSubjectDto courseSubject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var newCourseSubject =
                    await _courseSubjectAppService
                        .AddCourseSubject(courseSubject);

                return Ok(newCourseSubject);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    mensaje = ex.Message
                });
            }
        }

        // PUT: api/CourseSubject/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] CourseSubjectDto courseSubject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                courseSubject.IdCursoMateria = id;

                var updatedCourseSubject =
                    await _courseSubjectAppService
                        .UpdateCourseSubject(courseSubject);

                if (updatedCourseSubject == null)
                {
                    return NotFound(new
                    {
                        mensaje = "La relación curso-materia no existe."
                    });
                }

                return Ok(updatedCourseSubject);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    mensaje = ex.Message
                });
            }
        }

        // DELETE: api/CourseSubject/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var courseSubject =
                    await _courseSubjectAppService
                        .GetCourseSubjectById(id);

                if (courseSubject == null)
                {
                    return NotFound(new
                    {
                        mensaje = "La relación curso-materia no existe."
                    });
                }

                var deletedCourseSubject =
                    await _courseSubjectAppService
                        .DeleteCourseSubject(courseSubject);

                return Ok(deletedCourseSubject);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    mensaje = ex.Message
                });
            }
        }

        // GET: api/CourseSubject/not-deleted
        [HttpGet("not-deleted")]
        public async Task<IActionResult> GetNotDeleted()
        {
            var courseSubjects =
                await _courseSubjectAppService
                    .GetCourseSubjectNotDeleted();

            return Ok(courseSubjects);
        }
    }
}
using Application.Materia;
using Application.Materia.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SistemaColegio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrador")]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectAppService _subjectAppService;

        public SubjectController(ISubjectAppService subjectAppService)
        {
            _subjectAppService = subjectAppService;
        }

        // GET: api/Subject
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var subjects =
                await _subjectAppService.GetAllSubject();

            return Ok(subjects);
        }

        // GET: api/Subject/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var subject =
                await _subjectAppService.GetSubjectById(id);

            if (subject == null)
            {
                return NotFound(new
                {
                    mensaje = "La materia no existe."
                });
            }

            return Ok(subject);
        }

        // POST: api/Subject
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] SubjectDto subject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var newSubject =
                    await _subjectAppService.AddSubject(subject);

                return Ok(newSubject);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    mensaje = ex.Message
                });
            }
        }

        // PUT: api/Subject/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] SubjectDto subject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                subject.IdMateria = id;

                var updatedSubject =
                    await _subjectAppService.UpdateSubject(subject);

                if (updatedSubject == null)
                {
                    return NotFound(new
                    {
                        mensaje = "La materia no existe."
                    });
                }

                return Ok(updatedSubject);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    mensaje = ex.Message
                });
            }
        }

        // DELETE: api/Subject/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var subject =
                    await _subjectAppService.GetSubjectById(id);

                if (subject == null)
                {
                    return NotFound(new
                    {
                        mensaje = "La materia no existe."
                    });
                }

                var deletedSubject =
                    await _subjectAppService.DeleteSubject(subject);

                return Ok(deletedSubject);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    mensaje = ex.Message
                });
            }
        }

        // GET: api/Subject/not-deleted
        [HttpGet("not-deleted")]
        public async Task<IActionResult> GetNotDeleted()
        {
            var subjects =
                await _subjectAppService.GetSubjectNotDeleted();

            return Ok(subjects);
        }
    }
}

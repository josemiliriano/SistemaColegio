using Application.Evaluacion.DTOs;
using Application.Evaluation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SistemaColegio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Profesor")]
    public class EvaluationController : ControllerBase
    {
        private readonly IEvaluationAppService _evaluationAppService;

        public EvaluationController(
            IEvaluationAppService evaluationAppService)
        {
            _evaluationAppService = evaluationAppService;
        }

        // GET: api/Evaluation
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var evaluations =
                await _evaluationAppService.GetAllEvaluation();

            return Ok(evaluations);
        }

        // GET: api/Evaluation/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var evaluation =
                await _evaluationAppService
                    .GetEvaluationById(id);

            if (evaluation == null)
            {
                return NotFound(
                    new
                    {
                        mensaje = "La evaluación no existe."
                    });
            }

            return Ok(evaluation);
        }

        // POST: api/Evaluation
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] EvaluationDto evaluation)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var newEvaluation =
                    await _evaluationAppService
                        .AddEvaluation(evaluation);

                return Ok(newEvaluation);
            }
            catch (Exception ex)
            {
                return BadRequest(
                    new
                    {
                        mensaje = ex.Message
                    });
            }
        }

        // PUT: api/Evaluation/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] EvaluationDto evaluation)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                evaluation.IdEvaluacion = id;

                var updatedEvaluation =
                    await _evaluationAppService
                        .UpdateEvaluation(evaluation);

                if (updatedEvaluation == null)
                {
                    return NotFound(
                        new
                        {
                            mensaje = "La evaluación no existe."
                        });
                }

                return Ok(updatedEvaluation);
            }
            catch (Exception ex)
            {
                return BadRequest(
                    new
                    {
                        mensaje = ex.Message
                    });
            }
        }

        // DELETE: api/Evaluation/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var evaluation =
                    new EvaluationDto
                    {
                        IdEvaluacion = id
                    };

                var deletedEvaluation = await _evaluationAppService.DeleteEvaluation(evaluation);

                if (deletedEvaluation == null)
                {
                    return NotFound(new { mensaje = "La evaluación no existe."});
                }

                return Ok(deletedEvaluation);
            }
            catch (Exception ex)
            {
                return BadRequest(
                    new
                    {
                        mensaje = ex.Message
                    });
            }
        }

        // GET: api/Evaluation/not-deleted
        [HttpGet("not-deleted")]
        public async Task<IActionResult> GetNotDeleted()
        {
            var evaluations =
                await _evaluationAppService
                    .GetEvaluationNotDeleted();

            return Ok(evaluations);
        }
    }
}

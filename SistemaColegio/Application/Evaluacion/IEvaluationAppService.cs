
using Application.Evaluacion.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Evaluation
{
    public interface IEvaluationAppService
    {
        Task<EvaluationDto> AddEvaluation(EvaluationDto evaluation);
        Task<List<EvaluationDto>> GetAllEvaluation();
        Task<EvaluationDto> GetEvaluationById(int id);
        Task<EvaluationDto> UpdateEvaluation(EvaluationDto evaluation);
        Task<EvaluationDto> DeleteEvaluation(EvaluationDto evaluation);
        Task<List<EvaluationDto>> GetEvaluationNotDeleted();
    }
}




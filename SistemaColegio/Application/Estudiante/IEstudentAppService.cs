using Application.Estudiante.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Estudiante
{
    public interface IEstudentAppService
    {
        Task<EstudentDto> AddEstudent(EstudentDto estudent);
        Task<List<EstudentDto>> GetAllEstudent();
        Task<EstudentDto> GetEstudentById(int id);
        Task<EstudentDto> UpdateEstudent(EstudentDto estudent);
        Task<EstudentDto> DeleteEstudent(EstudentDto estudent);
        Task<List<EstudentDto>> GetEstudentNotDeleted();
    }
}

using Application.PeriodoAcademicoEstudiante.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.PeriodoAcademicoEstudiante
{
    public interface IEstudentAcademicPeriodAppService
    {
        Task<List<EstudentAcademicPeriodDto>> GetAllStudentAcademicPeriod();
        Task<EstudentAcademicPeriodDto> GetStudentAcademicPeriodById(int id);
        Task<List<EstudentAcademicPeriodDto>> GetStudentAcademicPeriodByStudent(int idEstudiante);
    }
}

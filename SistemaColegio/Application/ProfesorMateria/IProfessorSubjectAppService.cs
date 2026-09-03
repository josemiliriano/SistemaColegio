using Application.ProfesorMateria.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ProfesorMateria
{
    public interface IProfessorSubjectAppService
    {
        Task<ProfessorSubjectDto> AddProfessorSubject(ProfessorSubjectDto professorSubject);
        Task<List<ProfessorSubjectDto>> GetAllProfessorSubject();
        Task<ProfessorSubjectDto> GetProfessorSubjectById(int id);
        Task<ProfessorSubjectDto> UpdateProfessorSubject(ProfessorSubjectDto professorSubject);
        Task<ProfessorSubjectDto> DeleteProfessorSubject(ProfessorSubjectDto professorSubject);
        Task<List<ProfessorSubjectDto>> GetProfessorSubjectNotDeleted();
    }
}

using Application.Materia.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Materia
{
    public interface ISubjectAppService
    {
        Task<SubjectDto> AddSubject(SubjectDto subject);
        Task<List<SubjectDto>> GetAllSubject();
        Task<SubjectDto> GetSubjectById(int id);
        Task<SubjectDto> UpdateSubject(SubjectDto subject);
        Task<SubjectDto> DeleteSubject(SubjectDto subject);
        Task<List<SubjectDto>> GetSubjectNotDeleted();
    }
}

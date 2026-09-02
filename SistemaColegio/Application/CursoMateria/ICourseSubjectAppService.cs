using Application.CursoMateria.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.CursoMateria
{
    public interface ICourseSubjectAppService
    {
        Task<CourseSubjectDto> AddCourseSubject(CourseSubjectDto courseSubject);
        Task<List<CourseSubjectDto>> GetAllCourseSubject();
        Task<CourseSubjectDto> GetCourseSubjectById(int id);
        Task<CourseSubjectDto> UpdateCourseSubject(CourseSubjectDto courseSubject);
        Task<CourseSubjectDto> DeleteCourseSubject(CourseSubjectDto courseSubject);
        Task<List<CourseSubjectDto>> GetCourseSubjectNotDeleted();
    }
}

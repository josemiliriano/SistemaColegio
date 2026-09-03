using Application.Curso.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Curso
{
    public interface ICourseAppService
    {
        Task<CourseDto> AddCourse(CourseDto course);
        Task<List<CourseDto>> GetAllCourse();
        Task<CourseDto> GetCourseById(int id);
        Task<CourseDto> UpdateCourse(CourseDto course);
        Task<CourseDto> DeleteCourse(CourseDto course);
        Task<List<CourseDto>> GetCourseNotDeleted();
    }
}

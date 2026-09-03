using Application.Curso.DTOs;
using Domain.Entities;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Curso
{
    public class CourseAppService : ICourseAppService
    {
        private readonly GeneralRepository<Course> _courseRepository;

        public CourseAppService(GeneralRepository<Course> courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<CourseDto> AddCourse(CourseDto course)
        {
            var courses = await _courseRepository.GetAll();

            // Validar nombre duplicado
            var courseExists = courses.Any(x =>
                x.Nombre == course.Nombre &&
                x.IsDelete == '0');

            if (courseExists)
            {
                throw new Exception("El curso ya existe.");
            }

            var newCourse = new Course
            {
                Nombre = course.Nombre,
                Activo = course.Activo,
                IsDelete = '0'
            };

            newCourse = await _courseRepository.Add(newCourse);

            return new CourseDto
            {
                IdCurso = newCourse.IdCurso,
                Nombre = newCourse.Nombre,
                Activo = newCourse.Activo
            };
        }

        public async Task<List<CourseDto>> GetAllCourse()
        {
            var courses = await _courseRepository.GetAll();

            return courses
                .Where(x => x.IsDelete == '0')
                .Select(x => new CourseDto
                {
                    IdCurso = x.IdCurso,
                    Nombre = x.Nombre,
                    Activo = x.Activo
                })
                .ToList();
        }

        public async Task<CourseDto> GetCourseById(int id)
        {
            var course = await _courseRepository.GetById(id);

            if (course == null ||
                course.IsDelete == '1')
            {
                return null;
            }

            return new CourseDto
            {
                IdCurso = course.IdCurso,
                Nombre = course.Nombre,
                Activo = course.Activo
            };
        }

        public async Task<CourseDto> UpdateCourse(CourseDto course)
        {
            var courses = await _courseRepository.GetAll();

            var existingCourse = courses.FirstOrDefault(x =>
                x.IdCurso == course.IdCurso &&
                x.IsDelete == '0');

            if (existingCourse == null)
            {
                return null;
            }

            // Validar nombre duplicado
            var courseExists = courses.Any(x =>
                x.IdCurso != course.IdCurso &&
                x.Nombre == course.Nombre &&
                x.IsDelete == '0');

            if (courseExists)
            {
                throw new Exception(
                    "El curso ya existe.");
            }

            existingCourse.Nombre = course.Nombre;
            existingCourse.Activo = course.Activo;

            await _courseRepository.Update(existingCourse);

            return new CourseDto
            {
                IdCurso = existingCourse.IdCurso,
                Nombre = existingCourse.Nombre,
                Activo = existingCourse.Activo
            };
        }

        public async Task<CourseDto> DeleteCourse(CourseDto course)
        {
            var existingCourse = await _courseRepository.GetById(
                course.IdCurso);

            if (existingCourse == null ||
                existingCourse.IsDelete == '1')
            {
                return null;
            }

            // Eliminación lógica
            existingCourse.IsDelete = '1';
            existingCourse.Activo = '0';

            await _courseRepository.Update(existingCourse);

            return new CourseDto
            {
                IdCurso = existingCourse.IdCurso,
                Nombre = existingCourse.Nombre,
                Activo = existingCourse.Activo
            };
        }

        public async Task<List<CourseDto>> GetCourseNotDeleted()
        {
            return await GetAllCourse();
        }
    }
}

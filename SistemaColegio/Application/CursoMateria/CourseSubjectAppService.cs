using Application.CursoMateria.DTOs;
using Domain.Entities;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.CursoMateria
{
    public class CourseSubjectAppService : ICourseSubjectAppService
    {
        private readonly GeneralRepository<CourseSubject> _courseSubjectRepository;

        public CourseSubjectAppService(
            GeneralRepository<CourseSubject> courseSubjectRepository)
        {
            _courseSubjectRepository = courseSubjectRepository;
        }

        public async Task<CourseSubjectDto> AddCourseSubject(
            CourseSubjectDto courseSubject)
        {
            // Obtener los registros existentes
            var courseSubjects = await _courseSubjectRepository.GetAll();

            // Validar que la materia no esté asignada al mismo curso
            var courseSubjectExists = courseSubjects.Any(x =>
                x.IdCurso == courseSubject.IdCurso &&
                x.IdMateria == courseSubject.IdMateria &&
                x.IsDelete == '0');

            if (courseSubjectExists)
            {
                throw new Exception(
                    "La materia ya está asignada a este curso.");
            }

            // Crear la entidad
            var newCourseSubject = new CourseSubject
            {
                IdCurso = courseSubject.IdCurso,
                IdMateria = courseSubject.IdMateria,
                Activo = courseSubject.Activo,
                IsDelete = '0'
            };

            // Guardar
            newCourseSubject = await _courseSubjectRepository.Add(
                newCourseSubject);

            // Retornar DTO
            return new CourseSubjectDto
            {
                IdCurso = newCourseSubject.IdCurso,
                IdMateria = newCourseSubject.IdMateria,
                Activo = newCourseSubject.Activo
            };
        }

        public async Task<List<CourseSubjectDto>> GetAllCourseSubject()
        {
            var courseSubjects = await _courseSubjectRepository.GetAll();

            return courseSubjects
                .Where(x => x.IsDelete == '0')
                .Select(x => new CourseSubjectDto
                {
                    IdCurso = x.IdCurso,
                    IdMateria = x.IdMateria,
                    Activo = x.Activo
                })
                .ToList();
        }

        public async Task<CourseSubjectDto> GetCourseSubjectById(int id)
        {
            var courseSubject = await _courseSubjectRepository.GetById(id);

            if (courseSubject == null || courseSubject.IsDelete == '1')
            {
                return null;
            }

            return new CourseSubjectDto
            {
                IdCurso = courseSubject.IdCurso,
                IdMateria = courseSubject.IdMateria,
                Activo = courseSubject.Activo
            };
        }

        public async Task<CourseSubjectDto> UpdateCourseSubject(
            CourseSubjectDto courseSubject)
        {
            var courseSubjects = await _courseSubjectRepository.GetAll();

            var existingCourseSubject = courseSubjects.FirstOrDefault(x =>
                x.IdCurso == courseSubject.IdCurso &&
                x.IdMateria == courseSubject.IdMateria &&
                x.IsDelete == '0');

            if (existingCourseSubject == null)
            {
                return null;
            }

            // Actualizar únicamente el estado
            existingCourseSubject.Activo = courseSubject.Activo;

            await _courseSubjectRepository.Update(existingCourseSubject);

            return new CourseSubjectDto
            {
                IdCurso = existingCourseSubject.IdCurso,
                IdMateria = existingCourseSubject.IdMateria,
                Activo = existingCourseSubject.Activo
            };
        }

        public async Task<CourseSubjectDto> DeleteCourseSubject(
            CourseSubjectDto courseSubject)
        {
            var courseSubjects = await _courseSubjectRepository.GetAll();

            var existingCourseSubject = courseSubjects.FirstOrDefault(x =>
                x.IdCurso == courseSubject.IdCurso &&
                x.IdMateria == courseSubject.IdMateria &&
                x.IsDelete == '0');

            if (existingCourseSubject == null)
            {
                return null;
            }

            // Eliminación lógica
            existingCourseSubject.IsDelete = '1';

            await _courseSubjectRepository.Update(existingCourseSubject);

            return new CourseSubjectDto
            {
                IdCurso = existingCourseSubject.IdCurso,
                IdMateria = existingCourseSubject.IdMateria,
                Activo = existingCourseSubject.Activo
            };
        }

        public async Task<List<CourseSubjectDto>> GetCourseSubjectNotDeleted()
        {
            return await GetAllCourseSubject();
        }
    }

}

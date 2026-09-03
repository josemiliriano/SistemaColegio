using Application.AsignacionDocente.DTOs;
using Domain.Entities;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.AsignacionDocente
{
    public class TeachingAssignmentAppService : ITeachingAssignmentAppService
    {
        private readonly GeneralRepository<TeachingAssignment> _teachingAssignmentRepository;
        private readonly GeneralRepository<ProfessorSubject> _professorSubjectRepository;
        private readonly GeneralRepository<SessionPeriod> _sessionPeriodRepository;
        private readonly GeneralRepository<CourseSubject> _courseSubjectRepository;

        public TeachingAssignmentAppService(
            GeneralRepository<TeachingAssignment> teachingAssignmentRepository,
            GeneralRepository<ProfessorSubject> professorSubjectRepository,
            GeneralRepository<SessionPeriod> sessionPeriodRepository,
            GeneralRepository<CourseSubject> courseSubjectRepository)
        {
            _teachingAssignmentRepository = teachingAssignmentRepository;
            _professorSubjectRepository = professorSubjectRepository;
            _sessionPeriodRepository = sessionPeriodRepository;
            _courseSubjectRepository = courseSubjectRepository;
        }

        public async Task<TeachingAssignmentDto> AddTeachingAssignment(
            TeachingAssignmentDto teachingAssignment)
        {
            // Buscar la relación Profesor-Materia
            var professorSubjects =
                await _professorSubjectRepository.GetAllInclude(
                    x => x.Professor,
                    x => x.Subject);

            var professorSubject = professorSubjects.FirstOrDefault(x =>
                x.IdProfesorMateria == teachingAssignment.IdProfesorMateria &&
                x.IsDelete == '0' &&
                x.Activo == '1');

            if (professorSubject == null)
            {
                throw new Exception(
                    "La relación entre el profesor y la materia no existe o está inactiva.");
            }

            // Buscar la relación Sección-Período-Aula
            var sessionPeriods =
                await _sessionPeriodRepository.GetAllInclude(
                    x => x.Session,
                    x => x.Period,
                    x => x.Classroom);

            var sessionPeriod = sessionPeriods.FirstOrDefault(x =>
                x.IdSessionPeriod == teachingAssignment.IdSessionPeriod &&
                x.IsDelete == '0' &&
                x.Activo == '1');

            if (sessionPeriod == null)
            {
                throw new Exception(
                    "La sección no está registrada en el período académico o está inactiva.");
            }

            // Obtener el curso de la sección
            int idCurso = sessionPeriod.Session.IdCurso;

            // Verificar que la materia pertenezca al curso
            var courseSubjects = await _courseSubjectRepository.GetAll();

            var courseSubjectExists = courseSubjects.Any(x =>
                x.IdCurso == idCurso &&
                x.IdMateria == professorSubject.IdMateria &&
                x.IsDelete == '0' &&
                x.Activo == '1');

            if (!courseSubjectExists)
            {
                throw new Exception(
                    "La materia no pertenece al curso de la sección seleccionada.");
            }

            // Obtener asignaciones existentes
            var teachingAssignments =
                await _teachingAssignmentRepository.GetAllInclude(
                    x => x.ProfessorSubject,
                    x => x.SessionPeriod);

            // Verificar que la materia no tenga otro profesor
            // asignado en la misma sección y período
            var subjectAlreadyAssigned = teachingAssignments.Any(x =>
                x.SessionPeriod.IdSessionPeriod ==
                    teachingAssignment.IdSessionPeriod &&
                x.ProfessorSubject.IdMateria ==
                    professorSubject.IdMateria &&
                x.IsDelete == '0' &&
                x.Activo == '1');

            if (subjectAlreadyAssigned)
            {
                throw new Exception(
                    "Esta materia ya tiene un profesor asignado a esta sección en este período.");
            }

            // Crear la asignación
            var newTeachingAssignment = new TeachingAssignment
            {
                IdProfesorMateria = teachingAssignment.IdProfesorMateria,
                IdSessionPeriod = teachingAssignment.IdSessionPeriod,
                Activo = teachingAssignment.Activo,
                IsDelete = '0'
            };

            newTeachingAssignment =
                await _teachingAssignmentRepository.Add(
                    newTeachingAssignment);

            // Retornar DTO
            return new TeachingAssignmentDto
            {
                IdProfesorMateria =
                    newTeachingAssignment.IdProfesorMateria,

                IdSessionPeriod =
                    newTeachingAssignment.IdSessionPeriod,

                Activo =
                    newTeachingAssignment.Activo
            };
        }

        public async Task<List<TeachingAssignmentDto>>
            GetAllTeachingAssignment()
        {
            var teachingAssignments =
                await _teachingAssignmentRepository.GetAll();

            return teachingAssignments
                .Where(x => x.IsDelete == '0')
                .Select(x => new TeachingAssignmentDto
                {
                    IdProfesorMateria = x.IdProfesorMateria,
                    IdSessionPeriod = x.IdSessionPeriod,
                    Activo = x.Activo
                })
                .ToList();
        }

        public async Task<TeachingAssignmentDto>
            GetTeachingAssignmentById(int id)
        {
            var teachingAssignment =
                await _teachingAssignmentRepository.GetById(id);

            if (teachingAssignment == null ||
                teachingAssignment.IsDelete == '1')
            {
                return null;
            }

            return new TeachingAssignmentDto
            {
                IdProfesorMateria =
                    teachingAssignment.IdProfesorMateria,

                IdSessionPeriod =
                    teachingAssignment.IdSessionPeriod,

                Activo =
                    teachingAssignment.Activo
            };
        }

        public async Task<TeachingAssignmentDto>
            UpdateTeachingAssignment(
                TeachingAssignmentDto teachingAssignment)
        {
            var teachingAssignments =
                await _teachingAssignmentRepository.GetAll();

            var existingTeachingAssignment =
                teachingAssignments.FirstOrDefault(x =>
                    x.IdProfesorMateria ==
                        teachingAssignment.IdProfesorMateria &&
                    x.IdSessionPeriod ==
                        teachingAssignment.IdSessionPeriod &&
                    x.IsDelete == '0');

            if (existingTeachingAssignment == null)
            {
                return null;
            }

            // Actualizar únicamente el estado
            existingTeachingAssignment.Activo =
                teachingAssignment.Activo;

            await _teachingAssignmentRepository.Update(
                existingTeachingAssignment);

            return new TeachingAssignmentDto
            {
                IdProfesorMateria =
                    existingTeachingAssignment.IdProfesorMateria,

                IdSessionPeriod =
                    existingTeachingAssignment.IdSessionPeriod,

                Activo =
                    existingTeachingAssignment.Activo
            };
        }

        public async Task<TeachingAssignmentDto>
            DeleteTeachingAssignment(
                TeachingAssignmentDto teachingAssignment)
        {
            var teachingAssignments =
                await _teachingAssignmentRepository.GetAll();

            var existingTeachingAssignment =
                teachingAssignments.FirstOrDefault(x =>
                    x.IdProfesorMateria ==
                        teachingAssignment.IdProfesorMateria &&
                    x.IdSessionPeriod ==
                        teachingAssignment.IdSessionPeriod &&
                    x.IsDelete == '0');

            if (existingTeachingAssignment == null)
            {
                return null;
            }

            // Eliminación lógica
            existingTeachingAssignment.IsDelete = '1';
            existingTeachingAssignment.Activo = '0';

            await _teachingAssignmentRepository.Update(
                existingTeachingAssignment);

            return new TeachingAssignmentDto
            {
                IdProfesorMateria =
                    existingTeachingAssignment.IdProfesorMateria,

                IdSessionPeriod =
                    existingTeachingAssignment.IdSessionPeriod,

                Activo =
                    existingTeachingAssignment.Activo
            };
        }

        public async Task<List<TeachingAssignmentDto>>
            GetTeachingAssignmentNotDeleted()
        {
            return await GetAllTeachingAssignment();
        }
    }

}

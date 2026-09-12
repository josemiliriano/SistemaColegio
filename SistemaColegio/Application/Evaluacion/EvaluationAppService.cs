using Application.Evaluacion.DTOs;
using Application.Evaluation;
using Domain.Entities;
using Infraestructure.Data;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Evaluacion
{
    public class EvaluationAppService : IEvaluationAppService
    {
        private readonly GeneralRepository<Domain.Entities.Evaluation> _evaluationRepository;

        private readonly GeneralRepository<Estudent> _estudentRepository;

        private readonly GeneralRepository<TeachingAssignment> _teachingAssignmentRepository;

        private readonly GeneralRepository<SubPeriod> _subPeriodRepository;

        private readonly MyDataContext _context;

        public EvaluationAppService(
            GeneralRepository<Domain.Entities.Evaluation> evaluationRepository,
            GeneralRepository<Estudent> estudentRepository,
            GeneralRepository<TeachingAssignment> teachingAssignmentRepository,
            GeneralRepository<SubPeriod> subPeriodRepository,
            MyDataContext context)
        {
            _evaluationRepository = evaluationRepository;
            _estudentRepository = estudentRepository;
            _teachingAssignmentRepository = teachingAssignmentRepository;
            _subPeriodRepository = subPeriodRepository;
            _context = context;
        }


        public async Task<EvaluationDto> AddEvaluation(
            EvaluationDto evaluation)
        {
            // Buscar estudiante
            var student =
                await _estudentRepository.GetById(
                    evaluation.IdEstudiante);

            if (student == null ||
                student.IsDelete == '1' ||
                student.Activo != '1')
            {
                throw new Exception(
                    "El estudiante no existe o está inactivo.");
            }


            // Buscar asignación docente
            var teachingAssignment =
                await _teachingAssignmentRepository.GetById(
                    evaluation.IdAsignacionDocente);

            if (teachingAssignment == null ||
                teachingAssignment.IsDelete == '1' ||
                teachingAssignment.Activo != '1')
            {
                throw new Exception(
                    "La asignación docente no existe o está inactiva.");
            }


            // Buscar subperíodo
            var subPeriod =
                await _subPeriodRepository.GetById(
                    evaluation.IdSubPeriodo);

            if (subPeriod == null ||
                subPeriod.IsDelete == '1' ||
                subPeriod.Activo != '1')
            {
                throw new Exception(
                    "El subperíodo no existe o está inactivo.");
            }


            // Validar valores
            ValidateEvaluationValues(evaluation);


            // Crear evaluación
            var newEvaluation =
                new Domain.Entities.Evaluation
                {
                    IdEstudiante =
                        evaluation.IdEstudiante,

                    IdAsignacionDocente =
                        evaluation.IdAsignacionDocente,

                    IdSubPeriodo =
                        evaluation.IdSubPeriodo,

                    Asistencia =
                        evaluation.Asistencia,

                    Tarea =
                        evaluation.Tarea,

                    Cuaderno =
                        evaluation.Cuaderno,

                    Participacion =
                        evaluation.Participacion,

                    Proyecto =
                        evaluation.Proyecto,

                    Exposicion =
                        evaluation.Exposicion,

                    Examen =
                        evaluation.Examen,

                    Activo = '1',

                    IsDelete = '0'
                };


            newEvaluation =
                await _evaluationRepository
                    .Add(newEvaluation);

            await _context.SaveChangesAsync();


            return new EvaluationDto
            {
                IdEvaluacion =
                    newEvaluation.IdEvaluacion,

                IdEstudiante =
                    newEvaluation.IdEstudiante,

                IdAsignacionDocente =
                    newEvaluation.IdAsignacionDocente,

                IdSubPeriodo =
                    newEvaluation.IdSubPeriodo,

                Asistencia =
                    newEvaluation.Asistencia,

                Tarea =
                    newEvaluation.Tarea,

                Cuaderno =
                    newEvaluation.Cuaderno,

                Participacion =
                    newEvaluation.Participacion,

                Proyecto =
                    newEvaluation.Proyecto,

                Exposicion =
                    newEvaluation.Exposicion,

                Examen =
                    newEvaluation.Examen,

                Activo =
                    newEvaluation.Activo
            };
        }


        public async Task<List<EvaluationDto>> GetAllEvaluation()
        {
            var evaluations =
                await _evaluationRepository.GetAll();

            return evaluations
                .Where(x => x.IsDelete == '0')
                .Select(x => new EvaluationDto
                {
                    IdEvaluacion =
                        x.IdEvaluacion,

                    IdEstudiante =
                        x.IdEstudiante,

                    IdAsignacionDocente =
                        x.IdAsignacionDocente,

                    IdSubPeriodo =
                        x.IdSubPeriodo,

                    Asistencia =
                        x.Asistencia,

                    Tarea =
                        x.Tarea,

                    Cuaderno =
                        x.Cuaderno,

                    Participacion =
                        x.Participacion,

                    Proyecto =
                        x.Proyecto,

                    Exposicion =
                        x.Exposicion,

                    Examen =
                        x.Examen,

                    Activo =
                        x.Activo
                })
                .ToList();
        }


        public async Task<EvaluationDto> GetEvaluationById(int id)
        {
            var evaluation =
                await _evaluationRepository.GetById(id);

            if (evaluation == null ||
                evaluation.IsDelete == '1')
            {
                return null;
            }


            return new EvaluationDto
            {
                IdEvaluacion =
                    evaluation.IdEvaluacion,

                IdEstudiante =
                    evaluation.IdEstudiante,

                IdAsignacionDocente =
                    evaluation.IdAsignacionDocente,

                IdSubPeriodo =
                    evaluation.IdSubPeriodo,

                Asistencia =
                    evaluation.Asistencia,

                Tarea =
                    evaluation.Tarea,

                Cuaderno =
                    evaluation.Cuaderno,

                Participacion =
                    evaluation.Participacion,

                Proyecto =
                    evaluation.Proyecto,

                Exposicion =
                    evaluation.Exposicion,

                Examen =
                    evaluation.Examen,

                Activo =
                    evaluation.Activo
            };
        }


        public async Task<EvaluationDto> UpdateEvaluation(
            EvaluationDto evaluation)
        {
            var existingEvaluation =
                await _evaluationRepository
                    .GetById(
                        evaluation.IdEvaluacion);

            if (existingEvaluation == null ||
                existingEvaluation.IsDelete == '1')
            {
                return null;
            }


            // Validar estudiante
            var student =
                await _estudentRepository.GetById(
                    evaluation.IdEstudiante);

            if (student == null ||
                student.IsDelete == '1' ||
                student.Activo != '1')
            {
                throw new Exception(
                    "El estudiante no existe o está inactivo.");
            }


            // Validar asignación docente
            var teachingAssignment =
                await _teachingAssignmentRepository.GetById(
                    evaluation.IdAsignacionDocente);

            if (teachingAssignment == null ||
                teachingAssignment.IsDelete == '1' ||
                teachingAssignment.Activo != '1')
            {
                throw new Exception(
                    "La asignación docente no existe o está inactiva.");
            }


            // Validar subperíodo
            var subPeriod =
                await _subPeriodRepository.GetById(
                    evaluation.IdSubPeriodo);

            if (subPeriod == null ||
                subPeriod.IsDelete == '1' ||
                subPeriod.Activo != '1')
            {
                throw new Exception(
                    "El subperíodo no existe o está inactivo.");
            }


            // Validar valores
            ValidateEvaluationValues(evaluation);


            // Actualizar
            existingEvaluation.IdEstudiante =
                evaluation.IdEstudiante;

            existingEvaluation.IdAsignacionDocente =
                evaluation.IdAsignacionDocente;

            existingEvaluation.IdSubPeriodo =
                evaluation.IdSubPeriodo;

            existingEvaluation.Asistencia =
                evaluation.Asistencia;

            existingEvaluation.Tarea =
                evaluation.Tarea;

            existingEvaluation.Cuaderno =
                evaluation.Cuaderno;

            existingEvaluation.Participacion =
                evaluation.Participacion;

            existingEvaluation.Proyecto =
                evaluation.Proyecto;

            existingEvaluation.Exposicion =
                evaluation.Exposicion;

            existingEvaluation.Examen =
                evaluation.Examen;

            existingEvaluation.Activo =
                evaluation.Activo;


            await _evaluationRepository
                .Update(existingEvaluation);

            await _context.SaveChangesAsync();


            return new EvaluationDto
            {
                IdEvaluacion =
                    existingEvaluation.IdEvaluacion,

                IdEstudiante =
                    existingEvaluation.IdEstudiante,

                IdAsignacionDocente =
                    existingEvaluation.IdAsignacionDocente,

                IdSubPeriodo =
                    existingEvaluation.IdSubPeriodo,

                Asistencia =
                    existingEvaluation.Asistencia,

                Tarea =
                    existingEvaluation.Tarea,

                Cuaderno =
                    existingEvaluation.Cuaderno,

                Participacion =
                    existingEvaluation.Participacion,

                Proyecto =
                    existingEvaluation.Proyecto,

                Exposicion =
                    existingEvaluation.Exposicion,

                Examen =
                    existingEvaluation.Examen,

                Activo =
                    existingEvaluation.Activo
            };
        }


        public async Task<EvaluationDto> DeleteEvaluation(
            EvaluationDto evaluation)
        {
            var existingEvaluation =
                await _evaluationRepository
                    .GetById(
                        evaluation.IdEvaluacion);

            if (existingEvaluation == null ||
                existingEvaluation.IsDelete == '1')
            {
                return null;
            }


            // Eliminación lógica
            existingEvaluation.IsDelete = '1';
            existingEvaluation.Activo = '0';


            await _evaluationRepository
                .Update(existingEvaluation);

            await _context.SaveChangesAsync();


            return new EvaluationDto
            {
                IdEvaluacion =
                    existingEvaluation.IdEvaluacion,

                IdEstudiante =
                    existingEvaluation.IdEstudiante,

                IdAsignacionDocente =
                    existingEvaluation.IdAsignacionDocente,

                IdSubPeriodo =
                    existingEvaluation.IdSubPeriodo,

                Asistencia =
                    existingEvaluation.Asistencia,

                Tarea =
                    existingEvaluation.Tarea,

                Cuaderno =
                    existingEvaluation.Cuaderno,

                Participacion =
                    existingEvaluation.Participacion,

                Proyecto =
                    existingEvaluation.Proyecto,

                Exposicion =
                    existingEvaluation.Exposicion,

                Examen =
                    existingEvaluation.Examen,

                Activo =
                    existingEvaluation.Activo
            };
        }


        public async Task<List<EvaluationDto>>
            GetEvaluationNotDeleted()
        {
            return await GetAllEvaluation();
        }


        private void ValidateEvaluationValues(
            EvaluationDto evaluation)
        {
            if (evaluation.Asistencia < 0 ||
                evaluation.Asistencia > 10)
            {
                throw new Exception(
                    "La asistencia debe estar entre 0 y 10.");
            }


            if (evaluation.Tarea < 0 ||
                evaluation.Tarea > 10)
            {
                throw new Exception(
                    "La tarea debe estar entre 0 y 10.");
            }


            if (evaluation.Cuaderno < 0 ||
                evaluation.Cuaderno > 10)
            {
                throw new Exception(
                    "El cuaderno debe estar entre 0 y 10.");
            }


            if (evaluation.Participacion < 0 ||
                evaluation.Participacion > 10)
            {
                throw new Exception(
                    "La participación debe estar entre 0 y 10.");
            }


            if (evaluation.Proyecto < 0 ||
                evaluation.Proyecto > 20)
            {
                throw new Exception(
                    "El proyecto debe estar entre 0 y 20.");
            }


            if (evaluation.Exposicion < 0 ||
                evaluation.Exposicion > 20)
            {
                throw new Exception(
                    "La exposición debe estar entre 0 y 20.");
            }


            if (evaluation.Examen < 0 ||
                evaluation.Examen > 20)
            {
                throw new Exception(
                    "El examen debe estar entre 0 y 20.");
            }
        }
    }
}


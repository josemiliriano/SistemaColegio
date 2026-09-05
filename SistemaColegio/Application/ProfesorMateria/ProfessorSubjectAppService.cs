using Application.ProfesorMateria.DTOs;
using Domain.Entities;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ProfesorMateria
{
    public class ProfessorSubjectAppService : IProfessorSubjectAppService
    {
        private readonly GeneralRepository<ProfessorSubject>
            _professorSubjectRepository;

        private readonly GeneralRepository<Professor>
            _professorRepository;

        private readonly GeneralRepository<Subject>
            _subjectRepository;

        public ProfessorSubjectAppService(
            GeneralRepository<ProfessorSubject> professorSubjectRepository,
            GeneralRepository<Professor> professorRepository,
            GeneralRepository<Subject> subjectRepository)
        {
            _professorSubjectRepository = professorSubjectRepository;
            _professorRepository = professorRepository;
            _subjectRepository = subjectRepository;
        }

        public async Task<ProfessorSubjectDto> AddProfessorSubject(
            ProfessorSubjectDto professorSubject)
        {
            var professor =
                await _professorRepository
                    .GetById(professorSubject.IdProfesor);

            if (professor == null ||
                professor.IsDelete == '1' ||
                professor.Activo == '0')
            {
                throw new Exception(
                    "El profesor especificado no existe o está inactivo.");
            }

            var subject =
                await _subjectRepository
                    .GetById(professorSubject.IdMateria);

            if (subject == null ||
                subject.IsDelete == '1' ||
                subject.Activo == '0')
            {
                throw new Exception(
                    "La materia especificada no existe o está inactiva.");
            }

            var professorSubjects =
                await _professorSubjectRepository.GetAll();

            var relationExists =
                professorSubjects.Any(x =>
                    x.IdProfesor == professorSubject.IdProfesor &&
                    x.IdMateria == professorSubject.IdMateria &&
                    x.IsDelete == '0');

            if (relationExists)
            {
                throw new Exception(
                    "El profesor ya tiene asignada esta materia.");
            }

            var newProfessorSubject = new ProfessorSubject
            {
                IdProfesor = professorSubject.IdProfesor,
                IdMateria = professorSubject.IdMateria,
                Activo = professorSubject.Activo,
                IsDelete = '0'
            };

            newProfessorSubject =
                await _professorSubjectRepository
                    .Add(newProfessorSubject);

            return new ProfessorSubjectDto
            {
                IdProfesorMateria =
                    newProfessorSubject.IdProfesorMateria,

                IdProfesor =
                    newProfessorSubject.IdProfesor,

                IdMateria =
                    newProfessorSubject.IdMateria,

                Activo =
                    newProfessorSubject.Activo
            };
        }

        public async Task<List<ProfessorSubjectDto>>
            GetAllProfessorSubject()
        {
            var professorSubjects =
                await _professorSubjectRepository.GetAll();

            return professorSubjects
                .Where(x => x.IsDelete == '0')
                .Select(x => new ProfessorSubjectDto
                {
                    IdProfesorMateria =
                        x.IdProfesorMateria,

                    IdProfesor =
                        x.IdProfesor,

                    IdMateria =
                        x.IdMateria,

                    Activo =
                        x.Activo
                })
                .ToList();
        }

        public async Task<ProfessorSubjectDto>
            GetProfessorSubjectById(int id)
        {
            var professorSubject =
                await _professorSubjectRepository
                    .GetById(id);

            if (professorSubject == null ||
                professorSubject.IsDelete == '1')
            {
                return null;
            }

            return new ProfessorSubjectDto
            {
                IdProfesorMateria =
                    professorSubject.IdProfesorMateria,

                IdProfesor =
                    professorSubject.IdProfesor,

                IdMateria =
                    professorSubject.IdMateria,

                Activo =
                    professorSubject.Activo
            };
        }

        public async Task<ProfessorSubjectDto>
            UpdateProfessorSubject(
                ProfessorSubjectDto professorSubject)
        {
            var existingProfessorSubject =
                await _professorSubjectRepository
                    .GetById(
                        professorSubject.IdProfesorMateria);

            if (existingProfessorSubject == null ||
                existingProfessorSubject.IsDelete == '1')
            {
                return null;
            }

            // Solo modificamos el estado.
            existingProfessorSubject.Activo =
                professorSubject.Activo;

            await _professorSubjectRepository
                .Update(existingProfessorSubject);

            return new ProfessorSubjectDto
            {
                IdProfesorMateria =
                    existingProfessorSubject.IdProfesorMateria,

                IdProfesor =
                    existingProfessorSubject.IdProfesor,

                IdMateria =
                    existingProfessorSubject.IdMateria,

                Activo =
                    existingProfessorSubject.Activo
            };
        }

        public async Task<ProfessorSubjectDto>
            DeleteProfessorSubject(
                ProfessorSubjectDto professorSubject)
        {
            var existingProfessorSubject =
                await _professorSubjectRepository
                    .GetById(
                        professorSubject.IdProfesorMateria);

            if (existingProfessorSubject == null ||
                existingProfessorSubject.IsDelete == '1')
            {
                return null;
            }

            // Eliminación lógica
            existingProfessorSubject.IsDelete = '1';
            existingProfessorSubject.Activo = '0';

            await _professorSubjectRepository
                .Update(existingProfessorSubject);

            return new ProfessorSubjectDto
            {
                IdProfesorMateria = existingProfessorSubject.IdProfesorMateria,

                IdProfesor = existingProfessorSubject.IdProfesor,

                IdMateria = existingProfessorSubject.IdMateria,

                Activo = existingProfessorSubject.Activo
            };
        }

        public async Task<List<ProfessorSubjectDto>>
            GetProfessorSubjectNotDeleted()
        {
            return await GetAllProfessorSubject();
        }
    }
}

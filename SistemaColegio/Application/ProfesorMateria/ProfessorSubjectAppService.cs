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
        private readonly GeneralRepository<ProfessorSubject> _professorSubjectRepository;
        private readonly GeneralRepository<Professor> _professorRepository;
        private readonly GeneralRepository<Subject> _subjectRepository;

        public ProfessorSubjectAppService(
            GeneralRepository<ProfessorSubject> professorSubjectRepository,
            GeneralRepository<Professor> professorRepository,
            GeneralRepository<Subject> subjectRepository)
        {
            _professorSubjectRepository = professorSubjectRepository;
            _professorRepository = professorRepository;
            _subjectRepository = subjectRepository;
        }

        public async Task<ProfessorSubjectDto> AddProfessorSubject(ProfessorSubjectDto professorSubject)
        {
            // Verificar que el profesor exista y esté activo
            var professor = await _professorRepository.GetById(
                professorSubject.IdProfesor);

            if (professor == null ||
                professor.IsDelete == '1' ||
                professor.Activo != '1')
            {
                throw new Exception(
                    "El profesor no existe o está inactivo.");
            }

            // Verificar que la materia exista y esté activa
            var subject = await _subjectRepository.GetById(
                professorSubject.IdMateria);

            if (subject == null ||
                subject.IsDelete == '1' ||
                subject.Activo != '1')
            {
                throw new Exception(
                    "La materia no existe o está inactiva.");
            }

            // Verificar si ya existe la relación
            var professorSubjects =
                await _professorSubjectRepository.GetAll();

            var relationExists = professorSubjects.Any(x =>
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
                await _professorSubjectRepository.Add(
                    newProfessorSubject);

            return new ProfessorSubjectDto
            {
                IdProfesor = newProfessorSubject.IdProfesor,
                IdMateria = newProfessorSubject.IdMateria,
                Activo = newProfessorSubject.Activo
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
                    IdProfesor = x.IdProfesor,
                    IdMateria = x.IdMateria,
                    Activo = x.Activo
                })
                .ToList();
        }

        public async Task<ProfessorSubjectDto>
            GetProfessorSubjectById(int id)
        {
            var professorSubject =
                await _professorSubjectRepository.GetById(id);

            if (professorSubject == null ||
                professorSubject.IsDelete == '1')
            {
                return null;
            }

            return new ProfessorSubjectDto
            {
                IdProfesor = professorSubject.IdProfesor,
                IdMateria = professorSubject.IdMateria,
                Activo = professorSubject.Activo
            };
        }

        public async Task<ProfessorSubjectDto>
            UpdateProfessorSubject(
                ProfessorSubjectDto professorSubject)
        {
            var professorSubjects =
                await _professorSubjectRepository.GetAll();

            var existingProfessorSubject =
                professorSubjects.FirstOrDefault(x =>
                    x.IdProfesor ==
                        professorSubject.IdProfesor &&
                    x.IdMateria ==
                        professorSubject.IdMateria &&
                    x.IsDelete == '0');

            if (existingProfessorSubject == null)
            {
                return null;
            }

            existingProfessorSubject.Activo =
                professorSubject.Activo;

            await _professorSubjectRepository.Update(
                existingProfessorSubject);

            return new ProfessorSubjectDto
            {
                IdProfesor = existingProfessorSubject.IdProfesor,
                IdMateria = existingProfessorSubject.IdMateria,
                Activo = existingProfessorSubject.Activo
            };
        }

        public async Task<ProfessorSubjectDto>
            DeleteProfessorSubject(
                ProfessorSubjectDto professorSubject)
        {
            var professorSubjects =
                await _professorSubjectRepository.GetAll();

            var existingProfessorSubject =
                professorSubjects.FirstOrDefault(x =>
                    x.IdProfesor ==
                        professorSubject.IdProfesor &&
                    x.IdMateria ==
                        professorSubject.IdMateria &&
                    x.IsDelete == '0');

            if (existingProfessorSubject == null)
            {
                return null;
            }

            // Borrado lógico
            existingProfessorSubject.IsDelete = '1';
            existingProfessorSubject.Activo = '0';

            await _professorSubjectRepository.Update(
                existingProfessorSubject);

            return new ProfessorSubjectDto
            {
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

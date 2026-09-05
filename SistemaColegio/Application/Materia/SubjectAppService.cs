using Application.Materia.DTOs;
using Domain.Entities;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Materia
{
    public class SubjectAppService : ISubjectAppService
    {
        private readonly GeneralRepository<Subject> _subjectRepository;

        public SubjectAppService(
            GeneralRepository<Subject> subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task<SubjectDto> AddSubject(SubjectDto subject)
        {
            var subjects = await _subjectRepository.GetAll();

            // Validar nombre duplicado
            var subjectExists = subjects.Any(x =>
                x.Nombre == subject.Nombre &&
                x.IsDelete == '0');

            if (subjectExists)
            {
                throw new Exception(
                    "La materia ya existe.");
            }

            var newSubject = new Subject
            {
                Nombre = subject.Nombre,
                Activo = subject.Activo,
                IsDelete = '0'
            };

            newSubject = await _subjectRepository.Add(newSubject);

            return new SubjectDto
            {
                IdMateria = newSubject.IdMateria,
                Nombre = newSubject.Nombre,
                Activo = newSubject.Activo
            };
        }

        public async Task<List<SubjectDto>> GetAllSubject()
        {
            var subjects = await _subjectRepository.GetAll();

            return subjects
                .Where(x => x.IsDelete == '0')
                .Select(x => new SubjectDto
                {
                    IdMateria = x.IdMateria,
                    Nombre = x.Nombre,
                    Activo = x.Activo
                })
                .ToList();
        }

        public async Task<SubjectDto> GetSubjectById(int id)
        {
            var subject = await _subjectRepository.GetById(id);

            if (subject == null ||
                subject.IsDelete == '1')
            {
                return null;
            }

            return new SubjectDto
            {
                IdMateria = subject.IdMateria,
                Nombre = subject.Nombre,
                Activo = subject.Activo
            };
        }

        public async Task<SubjectDto> UpdateSubject(
            SubjectDto subject)
        {
            var subjects = await _subjectRepository.GetAll();

            var existingSubject = subjects.FirstOrDefault(x =>
                x.IdMateria == subject.IdMateria &&
                x.IsDelete == '0');

            if (existingSubject == null)
            {
                return null;
            }

            // Validar nombre duplicado
            var subjectExists = subjects.Any(x =>
                x.IdMateria != subject.IdMateria &&
                x.Nombre == subject.Nombre &&
                x.IsDelete == '0');

            if (subjectExists)
            {
                throw new Exception(
                    "La materia ya existe.");
            }

            existingSubject.Nombre = subject.Nombre;
            existingSubject.Activo = subject.Activo;

            await _subjectRepository.Update(existingSubject);

            return new SubjectDto
            {
                IdMateria = existingSubject.IdMateria,
                Nombre = existingSubject.Nombre,
                Activo = existingSubject.Activo
            };
        }

        public async Task<SubjectDto> DeleteSubject(
            SubjectDto subject)
        {
            var existingSubject =
                await _subjectRepository.GetById(
                    subject.IdMateria);

            if (existingSubject == null ||
                existingSubject.IsDelete == '1')
            {
                return null;
            }

            // Eliminación lógica
            existingSubject.IsDelete = '1';
            existingSubject.Activo = '0';

            await _subjectRepository.Update(existingSubject);

            return new SubjectDto
            {
                IdMateria = existingSubject.IdMateria,
                Nombre = existingSubject.Nombre,
                Activo = existingSubject.Activo
            };
        }

        public async Task<List<SubjectDto>>
            GetSubjectNotDeleted()
        {
            return await GetAllSubject();
        }
    }
}

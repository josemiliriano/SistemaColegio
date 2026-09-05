using Application.Estudiante.DTOs;
using Domain.Entities;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Estudiante
{
    public class EstudentAppService : IEstudentAppService
    {
        private readonly GeneralRepository<Estudent> _estudentRepository;
        private readonly GeneralRepository<Person> _personRepository;

        public EstudentAppService(
            GeneralRepository<Estudent> estudentRepository,
            GeneralRepository<Person> personRepository)
        {
            _estudentRepository = estudentRepository;
            _personRepository = personRepository;
        }

        public async Task<EstudentDto> AddEstudent(
            EstudentDto estudent)
        {
            // Verificar que la persona exista
            var person = await _personRepository.GetById(
                estudent.IdPersona);

            if (person == null ||
                person.IsDelete == '1')
            {
                throw new Exception(
                    "La persona especificada no existe.");
            }

            // Obtener estudiantes existentes
            var students = await _estudentRepository.GetAll();

            // Validar que la persona no tenga otro estudiante
            var personExists = students.Any(x =>
                x.IdPersona == estudent.IdPersona &&
                x.IsDelete == '0');

            if (personExists)
            {
                throw new Exception(
                    "Esta persona ya está registrada como estudiante.");
            }

            // Validar código de estudiante duplicado
            var codeExists = students.Any(x =>
                x.CodigoEstudiante == estudent.CodigoEstudiante &&
                x.IsDelete == '0');

            if (codeExists)
            {
                throw new Exception(
                    "El código de estudiante ya existe.");
            }

            // Crear estudiante
            var newEstudent = new Estudent
            {
                IdPersona = estudent.IdPersona,
                CodigoEstudiante = estudent.CodigoEstudiante,
                Activo = estudent.Activo,
                IsDelete = '0'
            };

            newEstudent =
                await _estudentRepository.Add(newEstudent);

            return new EstudentDto
            {
                IdEstudiante = newEstudent.IdEstudiante,
                IdPersona = newEstudent.IdPersona,
                CodigoEstudiante =
                    newEstudent.CodigoEstudiante,
                Activo = newEstudent.Activo
            };
        }

        public async Task<List<EstudentDto>> GetAllEstudent()
        {
            var students =
                await _estudentRepository.GetAll();

            return students
                .Where(x => x.IsDelete == '0')
                .Select(x => new EstudentDto
                {
                    IdEstudiante = x.IdEstudiante,
                    IdPersona = x.IdPersona,
                    CodigoEstudiante =
                        x.CodigoEstudiante,
                    Activo = x.Activo
                })
                .ToList();
        }

        public async Task<EstudentDto> GetEstudentById(int id)
        {
            var estudent =
                await _estudentRepository.GetById(id);

            if (estudent == null ||
                estudent.IsDelete == '1')
            {
                return null;
            }

            return new EstudentDto
            {
                IdEstudiante = estudent.IdEstudiante,
                IdPersona = estudent.IdPersona,
                CodigoEstudiante =
                    estudent.CodigoEstudiante,
                Activo = estudent.Activo
            };
        }

        public async Task<EstudentDto> UpdateEstudent(
            EstudentDto estudent)
        {
            var students =
                await _estudentRepository.GetAll();

            var existingEstudent =
                students.FirstOrDefault(x =>
                    x.IdEstudiante == estudent.IdEstudiante &&
                    x.IsDelete == '0');

            if (existingEstudent == null)
            {
                return null;
            }

            // Verificar que la persona exista
            var person = await _personRepository.GetById(
                estudent.IdPersona);

            if (person == null ||
                person.IsDelete == '1')
            {
                throw new Exception(
                    "La persona especificada no existe.");
            }

            // Validar persona duplicada
            var personExists = students.Any(x =>
                x.IdEstudiante != estudent.IdEstudiante &&
                x.IdPersona == estudent.IdPersona &&
                x.IsDelete == '0');

            if (personExists)
            {
                throw new Exception(
                    "Esta persona ya está registrada como estudiante.");
            }

            // Validar código duplicado
            var codeExists = students.Any(x =>
                x.IdEstudiante != estudent.IdEstudiante &&
                x.CodigoEstudiante ==
                    estudent.CodigoEstudiante &&
                x.IsDelete == '0');

            if (codeExists)
            {
                throw new Exception(
                    "El código de estudiante ya existe.");
            }

            // Actualizar
            existingEstudent.IdPersona =
                estudent.IdPersona;

            existingEstudent.CodigoEstudiante =
                estudent.CodigoEstudiante;

            existingEstudent.Activo =
                estudent.Activo;

            await _estudentRepository.Update(
                existingEstudent);

            return new EstudentDto
            {
                IdEstudiante = existingEstudent.IdEstudiante,
                IdPersona = existingEstudent.IdPersona,
                CodigoEstudiante =
                    existingEstudent.CodigoEstudiante,
                Activo = existingEstudent.Activo
            };
        }

        public async Task<EstudentDto> DeleteEstudent(
            EstudentDto estudent)
        {
            var existingEstudent =
                await _estudentRepository.GetById(
                    estudent.IdEstudiante);

            if (existingEstudent == null ||
                existingEstudent.IsDelete == '1')
            {
                return null;
            }

            // Eliminación lógica
            existingEstudent.IsDelete = '1';
            existingEstudent.Activo = '0';

            await _estudentRepository.Update(
                existingEstudent);

            return new EstudentDto
            {
                IdEstudiante = existingEstudent.IdEstudiante,
                IdPersona = existingEstudent.IdPersona,
                CodigoEstudiante =
                    existingEstudent.CodigoEstudiante,
                Activo = existingEstudent.Activo
            };
        }

        public async Task<List<EstudentDto>>
            GetEstudentNotDeleted()
        {
            return await GetAllEstudent();
        }
    }
}

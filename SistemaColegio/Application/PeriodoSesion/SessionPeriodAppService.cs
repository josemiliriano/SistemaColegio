using Application.PeriodoSesion.DTOs;
using Domain.Entities;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.PeriodoSesion
{public class SessionPeriodAppService : ISessionPeriodAppService
    {
        private readonly GeneralRepository<SessionPeriod> _sessionPeriodRepository;

        public SessionPeriodAppService(
            GeneralRepository<SessionPeriod> sessionPeriodRepository)
        {
            _sessionPeriodRepository = sessionPeriodRepository;
        }

        public async Task<SessionPeriodDto> AddSessionPeriod(
            SessionPeriodDto sessionPeriod)
        {
            var sessionPeriods = await _sessionPeriodRepository.GetAll();

            // Validar que la sección no esté registrada
            // nuevamente en el mismo período
            var sessionExists = sessionPeriods.Any(x =>
                x.IdSeccion == sessionPeriod.IdSeccion &&
                x.IdPeriodo == sessionPeriod.IdPeriodo &&
                x.IsDelete == '0');

            if (sessionExists)
            {
                throw new Exception(
                    "La sección ya está registrada en este período académico.");
            }

            // Validar que el aula no esté ocupada
            // por otra sección en el mismo período
            var classroomExists = sessionPeriods.Any(x =>
                x.IdAula == sessionPeriod.IdAula &&
                x.IdPeriodo == sessionPeriod.IdPeriodo &&
                x.IsDelete == '0');

            if (classroomExists)
            {
                throw new Exception(
                    "El aula ya está asignada a otra sección en este período académico.");
            }

            // Crear entidad
            var newSessionPeriod = new SessionPeriod
            {
                IdSeccion = sessionPeriod.IdSeccion,
                IdPeriodo = sessionPeriod.IdPeriodo,
                IdAula = sessionPeriod.IdAula,
                IsDelete = '0'
            };

            // Guardar
            newSessionPeriod = await _sessionPeriodRepository.Add(
                newSessionPeriod);

            // Retornar DTO
            return new SessionPeriodDto
            {
                IdSeccion = newSessionPeriod.IdSeccion,
                IdPeriodo = newSessionPeriod.IdPeriodo,
                IdAula = newSessionPeriod.IdAula
            };
        }

        public async Task<List<SessionPeriodDto>> GetAllSessionPeriod()
        {
            var sessionPeriods = await _sessionPeriodRepository.GetAll();

            return sessionPeriods
                .Where(x => x.IsDelete == '0')
                .Select(x => new SessionPeriodDto
                {
                    IdSeccion = x.IdSeccion,
                    IdPeriodo = x.IdPeriodo,
                    IdAula = x.IdAula
                })
                .ToList();
        }

        public async Task<SessionPeriodDto> GetSessionPeriodById(int id)
        {
            var sessionPeriod = await _sessionPeriodRepository.GetById(id);

            if (sessionPeriod == null || sessionPeriod.IsDelete == '1')
            {
                return null;
            }

            return new SessionPeriodDto
            {
                IdSeccion = sessionPeriod.IdSeccion,
                IdPeriodo = sessionPeriod.IdPeriodo,
                IdAula = sessionPeriod.IdAula
            };
        }

        public async Task<SessionPeriodDto> UpdateSessionPeriod(
            SessionPeriodDto sessionPeriod)
        {
            var sessionPeriods = await _sessionPeriodRepository.GetAll();

            var existingSessionPeriod = sessionPeriods.FirstOrDefault(x =>
                x.IdSeccion == sessionPeriod.IdSeccion &&
                x.IdPeriodo == sessionPeriod.IdPeriodo &&
                x.IsDelete == '0');

            if (existingSessionPeriod == null)
            {
                return null;
            }

            // Validar que el aula no esté ocupada por otra sección
            var classroomExists = sessionPeriods.Any(x =>
                x.IdSessionPeriod != existingSessionPeriod.IdSessionPeriod &&
                x.IdAula == sessionPeriod.IdAula &&
                x.IdPeriodo == sessionPeriod.IdPeriodo &&
                x.IsDelete == '0');

            if (classroomExists)
            {
                throw new Exception(
                    "El aula ya está asignada a otra sección en este período académico.");
            }

            // Actualizar aula
            existingSessionPeriod.IdAula = sessionPeriod.IdAula;

            await _sessionPeriodRepository.Update(existingSessionPeriod);

            return new SessionPeriodDto
            {
                IdSeccion = existingSessionPeriod.IdSeccion,
                IdPeriodo = existingSessionPeriod.IdPeriodo,
                IdAula = existingSessionPeriod.IdAula
            };
        }

        public async Task<SessionPeriodDto> DeleteSessionPeriod(
            SessionPeriodDto sessionPeriod)
        {
            var sessionPeriods = await _sessionPeriodRepository.GetAll();

            var existingSessionPeriod = sessionPeriods.FirstOrDefault(x =>
                x.IdSeccion == sessionPeriod.IdSeccion &&
                x.IdPeriodo == sessionPeriod.IdPeriodo &&
                x.IsDelete == '0');

            if (existingSessionPeriod == null)
            {
                return null;
            }

            // Eliminación lógica
            existingSessionPeriod.IsDelete = '1';

            await _sessionPeriodRepository.Update(existingSessionPeriod);

            return new SessionPeriodDto
            {
                IdSeccion = existingSessionPeriod.IdSeccion,
                IdPeriodo = existingSessionPeriod.IdPeriodo,
                IdAula = existingSessionPeriod.IdAula
            };
        }

        public async Task<List<SessionPeriodDto>> GetSessionPeriodNotDeleted()
        {
            return await GetAllSessionPeriod();
        }
    }


}

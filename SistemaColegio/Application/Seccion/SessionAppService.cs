using Application.Seccion.DTOs;
using Domain.Entities;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Seccion
{
    public class SessionAppService : ISessionAppService
    {
        private readonly GeneralRepository<Session> _sessionRepository;
        private readonly GeneralRepository<Course> _courseRepository;

        public SessionAppService(
            GeneralRepository<Session> sessionRepository,
            GeneralRepository<Course> courseRepository)
        {
            _sessionRepository = sessionRepository;
            _courseRepository = courseRepository;
        }

        public async Task<SessionDto> AddSession(SessionDto session)
        {
            // Verificar que el curso exista y esté activo
            var course = await _courseRepository.GetById(
                session.IdCurso);

            if (course == null ||
                course.IsDelete == '1' ||
                course.Activo != '1')
            {
                throw new Exception(
                    "El curso no existe o está inactivo.");
            }

            // Validar cupo
            if (session.CupoCapacidadMaximo <= 0)
            {
                throw new Exception(
                    "El cupo máximo debe ser mayor que cero.");
            }

            // Obtener secciones existentes
            var sessions = await _sessionRepository.GetAll();

            // Validar nombre duplicado dentro del mismo curso
            var sessionExists = sessions.Any(x =>
                x.IdCurso == session.IdCurso &&
                x.Nombre == session.Nombre &&
                x.IsDelete == '0');

            if (sessionExists)
            {
                throw new Exception(
                    "La sección ya existe para este curso.");
            }

            // Crear sección
            var newSession = new Session
            {
                IdCurso = session.IdCurso,
                Nombre = session.Nombre,
                CupoCapacidadMaximo = session.CupoCapacidadMaximo,
                Activo = session.Activo,
                IsDelete = '0'
            };

            newSession = await _sessionRepository.Add(newSession);

            return new SessionDto
            {
                IdSeccion = newSession.IdSeccion,
                IdCurso = newSession.IdCurso,
                Nombre = newSession.Nombre,
                CupoCapacidadMaximo =
                    newSession.CupoCapacidadMaximo,
                Activo = newSession.Activo
            };
        }

        public async Task<List<SessionDto>> GetAllSession()
        {
            var sessions = await _sessionRepository.GetAll();

            return sessions
                .Where(x => x.IsDelete == '0')
                .Select(x => new SessionDto
                {
                    IdSeccion = x.IdSeccion,
                    IdCurso = x.IdCurso,
                    Nombre = x.Nombre,
                    CupoCapacidadMaximo =
                        x.CupoCapacidadMaximo,
                    Activo = x.Activo
                })
                .ToList();
        }

        public async Task<SessionDto> GetSessionById(int id)
        {
            var session = await _sessionRepository.GetById(id);

            if (session == null ||
                session.IsDelete == '1')
            {
                return null;
            }

            return new SessionDto
            {
                IdSeccion = session.IdSeccion,
                IdCurso = session.IdCurso,
                Nombre = session.Nombre,
                CupoCapacidadMaximo =
                    session.CupoCapacidadMaximo,
                Activo = session.Activo
            };
        }

        public async Task<SessionDto> UpdateSession(
            SessionDto session)
        {
            var sessions = await _sessionRepository.GetAll();

            var existingSession = sessions.FirstOrDefault(x =>
                x.IdSeccion == session.IdSeccion &&
                x.IsDelete == '0');

            if (existingSession == null)
            {
                return null;
            }

            // Verificar que el curso exista y esté activo
            var course = await _courseRepository.GetById(
                session.IdCurso);

            if (course == null ||
                course.IsDelete == '1' ||
                course.Activo != '1')
            {
                throw new Exception(
                    "El curso no existe o está inactivo.");
            }

            // Validar cupo
            if (session.CupoCapacidadMaximo <= 0)
            {
                throw new Exception(
                    "El cupo máximo debe ser mayor que cero.");
            }

            // Validar nombre duplicado dentro del mismo curso
            var sessionExists = sessions.Any(x =>
                x.IdSeccion != session.IdSeccion &&
                x.IdCurso == session.IdCurso &&
                x.Nombre == session.Nombre &&
                x.IsDelete == '0');

            if (sessionExists)
            {
                throw new Exception(
                    "La sección ya existe para este curso.");
            }

            // Actualizar
            existingSession.IdCurso = session.IdCurso;
            existingSession.Nombre = session.Nombre;
            existingSession.CupoCapacidadMaximo =
                session.CupoCapacidadMaximo;
            existingSession.Activo = session.Activo;

            await _sessionRepository.Update(existingSession);

            return new SessionDto
            {
                IdSeccion = existingSession.IdSeccion,
                IdCurso = existingSession.IdCurso,
                Nombre = existingSession.Nombre,
                CupoCapacidadMaximo =
                    existingSession.CupoCapacidadMaximo,
                Activo = existingSession.Activo
            };
        }

        public async Task<SessionDto> DeleteSession(
            SessionDto session)
        {
            var existingSession =
                await _sessionRepository.GetById(
                    session.IdSeccion);

            if (existingSession == null ||
                existingSession.IsDelete == '1')
            {
                return null;
            }

            // Eliminación lógica
            existingSession.IsDelete = '1';
            existingSession.Activo = '0';

            await _sessionRepository.Update(existingSession);

            return new SessionDto
            {
                IdSeccion = existingSession.IdSeccion,
                IdCurso = existingSession.IdCurso,
                Nombre = existingSession.Nombre,
                CupoCapacidadMaximo =
                    existingSession.CupoCapacidadMaximo,
                Activo = existingSession.Activo
            };
        }

        public async Task<List<SessionDto>> GetSessionNotDeleted()
        {
            return await GetAllSession();
        }
    }
}

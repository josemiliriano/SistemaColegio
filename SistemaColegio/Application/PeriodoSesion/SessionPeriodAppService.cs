using Application.PeriodoSesion.DTOs;
using Domain.Entities;
using Infraestructure.Data;
using Infraestructure.Repository;

namespace Application.PeriodoSesion
{
    public class SessionPeriodAppService : ISessionPeriodAppService
    {
        private readonly GeneralRepository<SessionPeriod> _sessionPeriodRepository;
        private readonly GeneralRepository<Session> _sessionRepository;
        private readonly GeneralRepository<Period> _periodRepository;
        private readonly GeneralRepository<Classroom> _classroomRepository;
        private readonly MyDataContext _context;

        public SessionPeriodAppService(
            GeneralRepository<SessionPeriod> sessionPeriodRepository,
            GeneralRepository<Session> sessionRepository,
            GeneralRepository<Period> periodRepository,
            GeneralRepository<Classroom> classroomRepository,
            MyDataContext context)
        {
            _sessionPeriodRepository = sessionPeriodRepository;
            _sessionRepository = sessionRepository;
            _periodRepository = periodRepository;
            _classroomRepository = classroomRepository;
            _context = context;
        }

        public async Task<SessionPeriodDto> AddSessionPeriod(
            SessionPeriodDto sessionPeriod)
        {
            // Buscar sección
            var session =
                await _sessionRepository.GetById(
                    sessionPeriod.IdSeccion);

            if (session == null ||
                session.IsDelete == '1' ||
                session.Activo != '1')
            {
                throw new Exception(
                    "La sección no existe o está inactiva.");
            }

            // Buscar período
            var period =
                await _periodRepository.GetById(
                    sessionPeriod.IdPeriodo);

            if (period == null ||
                period.IsDelete == '1' ||
                period.Activo != '1')
            {
                throw new Exception(
                    "El período académico no existe o está inactivo.");
            }

            // Buscar aula
            var classroom =
                await _classroomRepository.GetById(
                    sessionPeriod.IdAula);

            if (classroom == null ||
                classroom.IsDelete == '1' ||
                classroom.Activo != '1')
            {
                throw new Exception(
                    "El aula no existe o está inactiva.");
            }

            // Validar capacidad
            if (session.CupoCapacidadMaximo >
                classroom.Capacidad)
            {
                throw new Exception(
                    "El cupo máximo de la sección es mayor que la capacidad del aula.");
            }

            var sessionPeriods =
                await _sessionPeriodRepository.GetAll();

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
                IdAula = sessionPeriod.IdAula
            };

            newSessionPeriod =
                await _sessionPeriodRepository.Add(
                    newSessionPeriod);

            await _context.SaveChangesAsync();

            // Retornar DTO
            return new SessionPeriodDto
            {
                IdSessionPeriod =
                    newSessionPeriod.IdSessionPeriod,

                IdSeccion =
                    newSessionPeriod.IdSeccion,

                IdPeriodo =
                    newSessionPeriod.IdPeriodo,

                IdAula =
                    newSessionPeriod.IdAula,

                Activo =
                    newSessionPeriod.Activo
            };
        }

        public async Task<List<SessionPeriodDto>> GetAllSessionPeriod()
        {
            var sessionPeriods =
                await _sessionPeriodRepository.GetAll();

            return sessionPeriods
                .Where(x => x.IsDelete == '0')
                .Select(x => new SessionPeriodDto
                {
                    IdSessionPeriod =
                        x.IdSessionPeriod,

                    IdSeccion =
                        x.IdSeccion,

                    IdPeriodo =
                        x.IdPeriodo,

                    IdAula =
                        x.IdAula,

                    Activo =
                        x.Activo
                })
                .ToList();
        }

        public async Task<SessionPeriodDto> GetSessionPeriodById(int id)
        {
            var sessionPeriod =
                await _sessionPeriodRepository.GetById(id);

            if (sessionPeriod == null ||
                sessionPeriod.IsDelete == '1')
            {
                return null;
            }

            return new SessionPeriodDto
            {
                IdSessionPeriod =
                    sessionPeriod.IdSessionPeriod,

                IdSeccion =
                    sessionPeriod.IdSeccion,

                IdPeriodo =
                    sessionPeriod.IdPeriodo,

                IdAula =
                    sessionPeriod.IdAula,

                Activo =
                    sessionPeriod.Activo
            };
        }

        public async Task<SessionPeriodDto> UpdateSessionPeriod(
            SessionPeriodDto sessionPeriod)
        {
            var sessionPeriods =
                await _sessionPeriodRepository.GetAll();

            var existingSessionPeriod =
                sessionPeriods.FirstOrDefault(x =>
                    x.IdSessionPeriod ==
                    sessionPeriod.IdSessionPeriod &&
                    x.IsDelete == '0');

            if (existingSessionPeriod == null)
            {
                return null;
            }

            // Buscar sección
            var session =
                await _sessionRepository.GetById(
                    existingSessionPeriod.IdSeccion);

            if (session == null ||
                session.IsDelete == '1' ||
                session.Activo != '1')
            {
                throw new Exception(
                    "La sección no existe o está inactiva.");
            }

            // Buscar período
            var period =
                await _periodRepository.GetById(
                    existingSessionPeriod.IdPeriodo);

            if (period == null ||
                period.IsDelete == '1' ||
                period.Activo != '1')
            {
                throw new Exception(
                    "El período académico no existe o está inactivo.");
            }

            // Buscar nueva aula
            var classroom =
                await _classroomRepository.GetById(
                    sessionPeriod.IdAula);

            if (classroom == null ||
                classroom.IsDelete == '1' ||
                classroom.Activo != '1')
            {
                throw new Exception(
                    "El aula no existe o está inactiva.");
            }

            // Validar capacidad
            if (session.CupoCapacidadMaximo >
                classroom.Capacidad)
            {
                throw new Exception(
                    "El cupo máximo de la sección es mayor que la capacidad del aula.");
            }

            // Validar que el aula no esté ocupada
            // por otra sección en el mismo período
            var classroomExists = sessionPeriods.Any(x =>
                x.IdSessionPeriod !=
                    existingSessionPeriod.IdSessionPeriod &&

                x.IdAula ==
                    sessionPeriod.IdAula &&

                x.IdPeriodo ==
                    existingSessionPeriod.IdPeriodo &&

                x.IsDelete == '0');

            if (classroomExists)
            {
                throw new Exception(
                    "El aula ya está asignada a otra sección en este período académico.");
            }

            // Actualizar aula y estado
            existingSessionPeriod.IdAula =
                sessionPeriod.IdAula;

            existingSessionPeriod.Activo =
                sessionPeriod.Activo;

            await _sessionPeriodRepository.Update(
                existingSessionPeriod);

            await _context.SaveChangesAsync();

            return new SessionPeriodDto
            {
                IdSessionPeriod =
                    existingSessionPeriod.IdSessionPeriod,

                IdSeccion =
                    existingSessionPeriod.IdSeccion,

                IdPeriodo =
                    existingSessionPeriod.IdPeriodo,

                IdAula =
                    existingSessionPeriod.IdAula,

                Activo =
                    existingSessionPeriod.Activo
            };
        }

        public async Task<SessionPeriodDto> DeleteSessionPeriod(
            SessionPeriodDto sessionPeriod)
        {
            var existingSessionPeriod =
                await _sessionPeriodRepository.GetById(
                    sessionPeriod.IdSessionPeriod);

            if (existingSessionPeriod == null ||
                existingSessionPeriod.IsDelete == '1')
            {
                return null;
            }

            // Eliminación lógica
            existingSessionPeriod.IsDelete = '1';
            existingSessionPeriod.Activo = '0';

            await _sessionPeriodRepository.Update(
                existingSessionPeriod);

            await _context.SaveChangesAsync();

            return new SessionPeriodDto
            {
                IdSessionPeriod =
                    existingSessionPeriod.IdSessionPeriod,

                IdSeccion =
                    existingSessionPeriod.IdSeccion,

                IdPeriodo =
                    existingSessionPeriod.IdPeriodo,

                IdAula =
                    existingSessionPeriod.IdAula,

                Activo =
                    existingSessionPeriod.Activo
            };
        }

        public async Task<List<SessionPeriodDto>> GetSessionPeriodNotDeleted()
        {
            return await GetAllSessionPeriod();
        }
    }
}
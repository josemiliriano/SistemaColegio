using Application.Estudiante.DTOs;
using Domain.Entities;
using Infraestructure.Data;
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
        private readonly GeneralRepository<SessionPeriod> _sessionPeriodRepository;
        private readonly GeneralRepository<Period> _periodRepository;
        private readonly GeneralRepository<Classroom> _classroomRepository;
        private readonly GeneralRepository<Session> _sessionRepository;
        private readonly MyDataContext _context;

        public EstudentAppService(
            GeneralRepository<Estudent> estudentRepository,
            GeneralRepository<Person> personRepository,
            GeneralRepository<SessionPeriod> sessionPeriodRepository,
            GeneralRepository<Period> periodRepository,
            GeneralRepository<Classroom> classroomRepository,
            GeneralRepository<Session> sessionRepository,
            MyDataContext context)
        {
            _estudentRepository = estudentRepository;
            _personRepository = personRepository;
            _sessionPeriodRepository = sessionPeriodRepository;
            _periodRepository = periodRepository;
            _classroomRepository = classroomRepository;
            _sessionRepository = sessionRepository;
            _context = context;
        }

        public async Task<EstudentDto> AddEstudent(EstudentDto estudent)
        {
            // Validar código de estudiante
            var students = await _estudentRepository.GetAll();

            var codeExists = students.Any(x =>
                x.CodigoEstudiante == estudent.CodigoEstudiante &&
                x.IsDelete == '0');

            if (codeExists)
            {
                throw new Exception("El código de estudiante ya existe.");
            }

            // Validar SessionPeriod
            var sessionPeriod =
                await _sessionPeriodRepository.GetById(
                    estudent.IdSessionPeriod);

            if (sessionPeriod == null ||
                sessionPeriod.IsDelete == '1' ||
                sessionPeriod.Activo != '1')
            {
                throw new Exception(
                    "El SessionPeriod especificado no existe o está inactivo.");
            }

            // Validar Session
            var session =
                await _sessionRepository.GetById(
                    sessionPeriod.IdSeccion);

            if (session == null ||
                session.IsDelete == '1' ||
                session.Activo != '1')
            {
                throw new Exception(
                    "La sección especificada no existe o está inactiva.");
            }

            // Contar estudiantes activos y no eliminados
            // que pertenecen al mismo SessionPeriod
            var studentsInSessionPeriod = students.Count(x =>
                x.IdSessionPeriod == estudent.IdSessionPeriod &&
                x.IsDelete == '0' &&
                x.Activo == '1');

            // Validar cupo máximo de la sección
            if (studentsInSessionPeriod >= session.CupoCapacidadMaximo)
            {
                throw new Exception(
                    "La sección ha alcanzado su capacidad máxima.");
            }

            // Iniciar transacción
            await using var transaction =
                await _context.BeginTransactionAsync();

            try
            {
                // Crear Person
                var person = new Person
                {
                    Nombres = estudent.Nombres,
                    Apellidos = estudent.Apellidos,
                    FechaNacimiento = estudent.FechaNacimiento,
                    Telefono = estudent.Telefono,
                    Direccion = estudent.Direccion,
                    Correo = ""
                };

                await _personRepository.Add(person);

                // Crear Estudent
                var newEstudent = new Estudent
                {
                    Person = person,
                    CodigoEstudiante = estudent.CodigoEstudiante,
                    IdSessionPeriod = estudent.IdSessionPeriod
                };

                await _estudentRepository.Add(newEstudent);

                // Guardar todas las operaciones
                await _context.SaveChangesAsync();

                // Confirmar transacción
                await transaction.CommitAsync();

                // Retornar DTO
                return new EstudentDto
                {
                    IdEstudiante = newEstudent.IdEstudiante,
                    IdPersona = newEstudent.IdPersona,

                    Nombres = person.Nombres,
                    Apellidos = person.Apellidos,
                    FechaNacimiento = person.FechaNacimiento,
                    Telefono = person.Telefono,
                    Direccion = person.Direccion,

                    CodigoEstudiante = newEstudent.CodigoEstudiante,
                    IdSessionPeriod = newEstudent.IdSessionPeriod,
                    Activo = newEstudent.Activo
                };
            }
            catch
            {
                // Deshacer todas las operaciones
                await transaction.RollbackAsync();

                throw;
            }
        }

        public async Task<List<EstudentDto>> GetAllEstudent()
        {
            var students =
        await _estudentRepository.GetAllInclude(
            x => x.Person
        );

            return students
                .Where(x => x.IsDelete == '0')
                .Select(x => new EstudentDto
                {
                    IdEstudiante = x.IdEstudiante,
                    IdPersona = x.IdPersona,

                    Nombres = x.Person.Nombres,
                    Apellidos = x.Person.Apellidos,
                    FechaNacimiento = x.Person.FechaNacimiento,
                    Telefono = x.Person.Telefono,
                    Direccion = x.Person.Direccion,

                    CodigoEstudiante = x.CodigoEstudiante,
                    IdSessionPeriod = x.IdSessionPeriod,

                    Activo = x.Activo
                })
                .ToList();
        }

        public async Task<EstudentDto> GetEstudentById(int id)
        {
            var estudent =
         await _estudentRepository.GetFirstOrDefaultInclude(
             x => x.IdEstudiante == id &&
                  x.IsDelete == '0',
             x => x.Person
         );

            if (estudent == null)
            {
                return null;
            }

            return new EstudentDto
            {
                IdEstudiante = estudent.IdEstudiante,
                IdPersona = estudent.IdPersona,

                Nombres = estudent.Person.Nombres,
                Apellidos = estudent.Person.Apellidos,
                FechaNacimiento = estudent.Person.FechaNacimiento,
                Telefono = estudent.Person.Telefono,
                Direccion = estudent.Person.Direccion,

                CodigoEstudiante = estudent.CodigoEstudiante,
                IdSessionPeriod = estudent.IdSessionPeriod,

                Activo = estudent.Activo
            };
        }

        public async Task<EstudentDto> UpdateEstudent(EstudentDto estudent)
        {
            // Buscar estudiante
            var existingEstudent =
                await _estudentRepository.GetFirstOrDefaultInclude(
                    x => x.IdEstudiante == estudent.IdEstudiante &&
                         x.IsDelete == '0',
                    x => x.Person
                );

            if (existingEstudent == null)
            {
                return null;
            }

            // Validar código de estudiante duplicado
            var students = await _estudentRepository.GetAll();

            var codeExists = students.Any(x =>
                x.IdEstudiante != estudent.IdEstudiante &&
                x.CodigoEstudiante == estudent.CodigoEstudiante &&
                x.IsDelete == '0');

            if (codeExists)
            {
                throw new Exception(
                    "El código de estudiante ya existe.");
            }

            // Validar SessionPeriod
            var sessionPeriod =
                await _sessionPeriodRepository.GetById(
                    estudent.IdSessionPeriod);

            if (sessionPeriod == null ||
                sessionPeriod.IsDelete == '1' ||
                sessionPeriod.Activo != '1')
            {
                throw new Exception(
                    "El SessionPeriod especificado no existe o está inactivo.");
            }

            await using var transaction =
                await _context.BeginTransactionAsync();

            try
            {
                // Actualizar Person
                existingEstudent.Person.Nombres =
                    estudent.Nombres;

                existingEstudent.Person.Apellidos =
                    estudent.Apellidos;

                existingEstudent.Person.FechaNacimiento =
                    estudent.FechaNacimiento;

                existingEstudent.Person.Telefono =
                    estudent.Telefono;

                existingEstudent.Person.Direccion =
                    estudent.Direccion;

                // Actualizar Estudent
                existingEstudent.CodigoEstudiante =
                    estudent.CodigoEstudiante;

                existingEstudent.IdSessionPeriod =
                    estudent.IdSessionPeriod;

                existingEstudent.Activo =
                    estudent.Activo;

                await _personRepository.Update(
                    existingEstudent.Person);

                await _estudentRepository.Update(
                    existingEstudent);

                // Guardar cambios
                await _context.SaveChangesAsync();

                // Confirmar transacción
                await transaction.CommitAsync();

                // Retornar DTO actualizado
                return new EstudentDto
                {
                    IdEstudiante =
                        existingEstudent.IdEstudiante,

                    IdPersona =
                        existingEstudent.IdPersona,

                    Nombres =
                        existingEstudent.Person.Nombres,

                    Apellidos =
                        existingEstudent.Person.Apellidos,

                    FechaNacimiento =
                        existingEstudent.Person.FechaNacimiento,

                    Telefono =
                        existingEstudent.Person.Telefono,

                    Direccion =
                        existingEstudent.Person.Direccion,

                    CodigoEstudiante =
                        existingEstudent.CodigoEstudiante,

                    IdSessionPeriod =
                        existingEstudent.IdSessionPeriod,

                    Activo =
                        existingEstudent.Activo
                };
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<EstudentDto> DeleteEstudent(EstudentDto estudent)
        {
            var existingEstudent =
         await _estudentRepository.GetFirstOrDefaultInclude(
             x => x.IdEstudiante == estudent.IdEstudiante &&
                  x.IsDelete == '0',
             x => x.Person
         );

            if (existingEstudent == null)
            {
                return null;
            }

            await using var transaction =
                await _context.BeginTransactionAsync();

            try
            {
                // Eliminación lógica del estudiante
                existingEstudent.IsDelete = '1';
                existingEstudent.Activo = '0';

                await _estudentRepository.Update(existingEstudent);

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return new EstudentDto
                {
                    IdEstudiante = existingEstudent.IdEstudiante,
                    IdPersona = existingEstudent.IdPersona,

                    Nombres = existingEstudent.Person.Nombres,
                    Apellidos = existingEstudent.Person.Apellidos,
                    FechaNacimiento = existingEstudent.Person.FechaNacimiento,
                    Telefono = existingEstudent.Person.Telefono,
                    Direccion = existingEstudent.Person.Direccion,

                    CodigoEstudiante = existingEstudent.CodigoEstudiante,
                    IdSessionPeriod = existingEstudent.IdSessionPeriod,
                    Activo = existingEstudent.Activo
                };
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<List<EstudentDto>> GetEstudentNotDeleted()
        {
            var students =
                await _estudentRepository.GetAllInclude(
                    x => x.Person
                );

            return students
                .Where(x => x.IsDelete == '0')
                .Select(x => new EstudentDto
                {
                    IdEstudiante = x.IdEstudiante,
                    IdPersona = x.IdPersona,
                    Nombres = x.Person.Nombres,
                    Apellidos = x.Person.Apellidos,
                    FechaNacimiento = x.Person.FechaNacimiento,
                    Telefono = x.Person.Telefono,
                    Direccion = x.Person.Direccion,
                    CodigoEstudiante = x.CodigoEstudiante,
                    IdSessionPeriod = x.IdSessionPeriod,
                    Activo = x.Activo
                })
                .ToList();
        }

        public async Task<EstudentDto> AssignStudentToSessionPeriod(
     AssignStudentSessionPeriodDto assignment)
        {
            // Buscar estudiante
            var student =
                await _estudentRepository.GetFirstOrDefaultInclude(
                    x => x.IdEstudiante == assignment.IdEstudiante &&
                         x.IsDelete == '0',
                    x => x.Person
                );

            if (student == null)
            {
                return null;
            }

            // Validar que el estudiante esté activo
            if (student.Activo != '1')
            {
                throw new Exception(
                    "El estudiante está inactivo."
                );
            }

            // Validar SessionPeriod
            var sessionPeriod =
                await _sessionPeriodRepository.GetById(
                    assignment.IdSessionPeriod
                );

            if (sessionPeriod == null ||
                sessionPeriod.IsDelete == '1' ||
                sessionPeriod.Activo != '1')
            {
                throw new Exception(
                    "El SessionPeriod no existe o está inactivo."
                );
            }

            // Validar sección
            var session =
                await _sessionRepository.GetById(
                    sessionPeriod.IdSeccion
                );

            if (session == null ||
                session.IsDelete == '1' ||
                session.Activo != '1')
            {
                throw new Exception(
                    "La sección especificada no existe o está inactiva."
                );
            }

            // Validar período académico
            var period =
                await _periodRepository.GetById(
                    sessionPeriod.IdPeriodo
                );

            if (period == null ||
                period.IsDelete == '1' ||
                period.Activo != '1')
            {
                throw new Exception(
                    "El período académico no existe o está inactivo."
                );
            }

            // Validar que el período esté vigente
            var today = DateTime.Today;

            if (today < period.FechaInicio ||
                today > period.FechaFin)
            {
                throw new Exception(
                    "El período académico no está vigente."
                );
            }

            // Validar aula
            var classroom =
                await _classroomRepository.GetById(
                    sessionPeriod.IdAula
                );

            if (classroom == null ||
                classroom.IsDelete == '1' ||
                classroom.Activo != '1')
            {
                throw new Exception(
                    "El aula especificada no existe o está inactiva."
                );
            }

            // Contar estudiantes activos y no eliminados
            // asignados al SessionPeriod
            var students =
                await _estudentRepository.GetAll();

            var studentsInSessionPeriod =
                students.Count(x =>
                    x.IdSessionPeriod == assignment.IdSessionPeriod &&
                    x.IsDelete == '0' &&
                    x.Activo == '1'
                );

            // Validar capacidad solamente si el estudiante
            // viene de otro SessionPeriod
            if (student.IdSessionPeriod != assignment.IdSessionPeriod)
            {
                // Validar capacidad máxima de la sección
                if (studentsInSessionPeriod >= session.CupoCapacidadMaximo)
                {
                    throw new Exception(
                        "La sección ha alcanzado su capacidad máxima."
                    );
                }

                // Validar capacidad física del aula
                if (studentsInSessionPeriod >= classroom.Capacidad)
                {
                    throw new Exception("El aula no tiene capacidad disponible.");
                }
            }

            // Iniciar transacción
            await using var transaction = await _context.BeginTransactionAsync();

            try
            {
                // Asignar estudiante al nuevo SessionPeriod
                student.IdSessionPeriod =
                    assignment.IdSessionPeriod;

                await _estudentRepository.Update(student);

                // Guardar cambios
                await _context.SaveChangesAsync();

                // Confirmar transacción
                await transaction.CommitAsync();

                // Retornar estudiante actualizado
                return new EstudentDto
                {
                    IdEstudiante =
                        student.IdEstudiante,

                    IdPersona =
                        student.IdPersona,

                    Nombres =
                        student.Person.Nombres,

                    Apellidos =
                        student.Person.Apellidos,

                    FechaNacimiento =
                        student.Person.FechaNacimiento,

                    Telefono =
                        student.Person.Telefono,

                    Direccion =
                        student.Person.Direccion,

                    CodigoEstudiante =
                        student.CodigoEstudiante,

                    IdSessionPeriod =
                        student.IdSessionPeriod,

                    Activo =
                        student.Activo
                };
            }
            catch
            {
                // Deshacer cambios
                await transaction.RollbackAsync();

                throw;
            }
        }
    }
}

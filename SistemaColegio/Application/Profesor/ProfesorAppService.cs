using Application.Profesor.DTOs;
using Domain.Entities;
using Infraestructure.Data;
using Infraestructure.Repository;

namespace Application.Profesor
{
    public class ProfessorAppService : IProfesorAppService
    {
        private readonly GeneralRepository<Professor> _professorRepository;
        private readonly GeneralRepository<Person> _personRepository;
        private readonly GeneralRepository<CDUser> _userRepository;
        private readonly GeneralRepository<Role> _roleRepository;
        private readonly MyDataContext _context;

        public ProfessorAppService(
            GeneralRepository<Professor> professorRepository,
            GeneralRepository<Person> personRepository,
            GeneralRepository<CDUser> userRepository,
            GeneralRepository<Role> roleRepository,
            MyDataContext context)
        {
            _professorRepository = professorRepository;
            _personRepository = personRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _context = context;
        }

        public async Task<ProfesorDto> AddProfessor(CreateProfesorDto professor)
        {
            // Validar cédula
            var professors = await _professorRepository.GetAll();

            var cedulaExists = professors.Any(p =>
                p.Cedula == professor.Cedula &&
                p.IsDelete == '0');

            if (cedulaExists)
            {
                throw new Exception("La cédula ya está registrada.");
            }

            // Validar nombre de usuario
            var users = await _userRepository.GetAll();

            var userExists = users.Any(u =>
                u.NombreUsuario == professor.NombreUsuario &&
                u.IsDelete == '0');

            if (userExists)
            {
                throw new Exception("El nombre de usuario ya existe.");
            }

            // Buscar rol Profesor
            var roles = await _roleRepository.GetAll();

            var role = roles.FirstOrDefault(r =>
                r.NombreRol == "Profesor" &&
                r.IsDelete == '0' &&
                r.Activo == '1');

            if (role == null)
            {
                throw new Exception("No existe el rol Profesor.");
            }

            // Iniciar transacción
            await using var transaction = await _context.BeginTransactionAsync();

            try
            {
                // Crear Person
                var person = new Person
                {
                    Nombres = professor.Nombres,
                    Apellidos = professor.Apellidos,
                    FechaNacimiento = professor.FechaNacimiento,
                    Telefono = professor.Telefono,
                    Direccion = professor.Direccion,
                    Correo = professor.Correo
                };

                await _personRepository.Add(person);

                // Crear Professor
                var newProfessor = new Professor
                {
                    Persona = person,
                    Cedula = professor.Cedula,
                    Especialidad = professor.Especialidad,
                    Activo = professor.Activo,
                    IsDelete = '0'
                };

                await _professorRepository.Add(newProfessor);

                // Crear User
                var newUser = new CDUser
                {
                    Persona = person,
                    IdRol = role.IdRol,
                    NombreUsuario = professor.NombreUsuario,
                    Password = BCrypt.Net.BCrypt.HashPassword(professor.Password),
                    Activo = professor.Activo,
                    IsDelete = '0'
                };

                await _userRepository.Add(newUser);

                // Guardar todas las operaciones
                await _context.SaveChangesAsync();

                // Confirmar transacción
                await transaction.CommitAsync();

                // Retornar DTO
                return new ProfesorDto
                {
                    Nombres = person.Nombres,
                    Apellidos = person.Apellidos,
                    FechaNacimiento = person.FechaNacimiento,
                    Telefono = person.Telefono,
                    Direccion = person.Direccion,
                    Correo = person.Correo,

                    Cedula = newProfessor.Cedula,
                    Especialidad = newProfessor.Especialidad,
                    Activo = newProfessor.Activo,

                    NombreUsuario = newUser.NombreUsuario
                };
            }
            catch
            {
                // Deshacer todas las operaciones
                await transaction.RollbackAsync();

                throw;
            }
        }

        public async Task<List<ProfesorDto>> GetAllProfessor()
        {
            var professors = await _professorRepository.GetAllInclude(
                p => p.Persona);

            return professors
                .Where(p => p.IsDelete == '0')
                .Select(p => new ProfesorDto
                {
                    Nombres = p.Persona.Nombres,
                    Apellidos = p.Persona.Apellidos,
                    FechaNacimiento = p.Persona.FechaNacimiento,
                    Telefono = p.Persona.Telefono,
                    Direccion = p.Persona.Direccion,
                    Correo = p.Persona.Correo,

                    Cedula = p.Cedula,
                    Especialidad = p.Especialidad,
                    Activo = p.Activo
                })
                .ToList();
        }

        public async Task<ProfesorDto> GetProfessorById(int idProfesor)
        {
            var professors = await _professorRepository.GetAllInclude(p => p.Persona);

            var professor = professors.FirstOrDefault(p => p.IdProfesor == idProfesor && p.IsDelete == '0');

            if (professor == null)
            {
                return null;
            }

            return new ProfesorDto
            {
                Nombres = professor.Persona.Nombres,
                Apellidos = professor.Persona.Apellidos,
                FechaNacimiento = professor.Persona.FechaNacimiento,
                Telefono = professor.Persona.Telefono,
                Direccion = professor.Persona.Direccion,
                Correo = professor.Persona.Correo,

                Cedula = professor.Cedula,
                Especialidad = professor.Especialidad,
                Activo = professor.Activo
            };
        }

        public async Task<ProfesorDto> UpdateProfessor(int idProfesor, ProfesorDto professor)
        {
            var professors = await _professorRepository.GetAllInclude(
                p => p.Persona);

            var existingProfessor = professors.FirstOrDefault(p =>
                p.IdProfesor == idProfesor &&
                p.IsDelete == '0');

            if (existingProfessor == null)
            {
                return null;
            }

            // Validar cédula duplicada
            var cedulaExists = professors.Any(p =>
                p.IdProfesor != idProfesor &&
                p.Cedula == professor.Cedula &&
                p.IsDelete == '0');

            if (cedulaExists)
            {
                throw new Exception("La cédula ya está registrada.");
            }

            // Iniciar transacción
            await using var transaction = await _context.BeginTransactionAsync();

            try
            {
                // Actualizar datos de Person
                existingProfessor.Persona.Nombres = professor.Nombres;
                existingProfessor.Persona.Apellidos = professor.Apellidos;
                existingProfessor.Persona.FechaNacimiento = professor.FechaNacimiento;
                existingProfessor.Persona.Telefono = professor.Telefono;
                existingProfessor.Persona.Direccion = professor.Direccion;
                existingProfessor.Persona.Correo = professor.Correo;

                // Actualizar datos de Professor
                existingProfessor.Cedula = professor.Cedula;
                existingProfessor.Especialidad = professor.Especialidad;
                existingProfessor.Activo = professor.Activo;

                // Marcar entidades como modificadas
                await _personRepository.Update(existingProfessor.Persona);
                await _professorRepository.Update(existingProfessor);

                // Guardar cambios
                await _context.SaveChangesAsync();

                // Confirmar transacción
                await transaction.CommitAsync();

                // Retornar información actualizada
                return new ProfesorDto
                {
                    Nombres = existingProfessor.Persona.Nombres,
                    Apellidos = existingProfessor.Persona.Apellidos,
                    FechaNacimiento = existingProfessor.Persona.FechaNacimiento,
                    Telefono = existingProfessor.Persona.Telefono,
                    Direccion = existingProfessor.Persona.Direccion,
                    Correo = existingProfessor.Persona.Correo,

                    Cedula = existingProfessor.Cedula,
                    Especialidad = existingProfessor.Especialidad,
                    Activo = existingProfessor.Activo
                };
            }
            catch
            {
                await transaction.RollbackAsync();

                throw;
            }
        }

        public async Task<ProfesorDto> DeleteProfessor(int idProfesor)
        {
            var professor = await _professorRepository.GetById(idProfesor);

            if (professor == null || professor.IsDelete == '1')
            {
                return null;
            }

            // Iniciar transacción
            await using var transaction = await _context.BeginTransactionAsync();

            try
            {
                // Eliminación lógica del profesor
                professor.IsDelete = '1';
                professor.Activo = '0';

                await _professorRepository.Update(professor);

                // Buscar usuario relacionado
                var users = await _userRepository.GetAll();

                var user = users.FirstOrDefault(u =>
                    u.IdPersona == professor.IdPersona &&
                    u.IsDelete == '0');

                if (user != null)
                {
                    // Eliminación lógica del usuario
                    user.IsDelete = '1';
                    user.Activo = '0';

                    await _userRepository.Update(user);
                }

                // Guardar cambios
                await _context.SaveChangesAsync();

                // Confirmar transacción
                await transaction.CommitAsync();

                return new ProfesorDto
                {
                    Cedula = professor.Cedula,
                    Especialidad = professor.Especialidad,
                    Activo = professor.Activo
                };
            }
            catch
            {
                await transaction.RollbackAsync();

                throw;
            }
        }

        public async Task<List<ProfesorDto>> GetProfessorNotDeleted()
        {
            return await GetAllProfessor();
        }
    }
}

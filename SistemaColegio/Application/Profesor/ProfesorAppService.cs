using Application.Profesor.DTOs;
using Domain.Entities;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Profesor
{
    public class ProfessorAppService : IProfesorAppService
    {
        private readonly GeneralRepository<Professor> _professorRepository;
        private readonly GeneralRepository<Person> _personRepository;
        private readonly GeneralRepository<CDUser> _userRepository;
        private readonly GeneralRepository<Role> _roleRepository;

        public ProfessorAppService(
            GeneralRepository<Professor> professorRepository,
            GeneralRepository<Person> personRepository,
            GeneralRepository<CDUser> userRepository,
            GeneralRepository<Role> roleRepository)
        {
            _professorRepository = professorRepository;
            _personRepository = personRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task<ProfesorDto> AddProfessor(ProfesorDto professor)
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

            person = await _personRepository.Add(person);

            // Crear Professor
            var newProfessor = new Professor
            {
                IdPersona = person.IdPersona,
                Cedula = professor.Cedula,
                Especialidad = professor.Especialidad,
                Activo = professor.Activo,
                IsDelete = '0'
            };

            newProfessor = await _professorRepository.Add(newProfessor);

            // Crear User
            var newUser = new CDUser
            {
                IdPersona = person.IdPersona,
                IdRol = role.IdRol,
                NombreUsuario = professor.NombreUsuario,
                Password = professor.Password,
                Activo = professor.Activo,
                IsDelete = '0'
            };

            newUser = await _userRepository.Add(newUser);

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

                NombreUsuario = newUser.NombreUsuario,
                Password = newUser.Password
            };
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
            var professors = await _professorRepository.GetAllInclude(
                p => p.Persona);

            var professor = professors.FirstOrDefault(p =>
                p.IdProfesor == idProfesor &&
                p.IsDelete == '0');

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

        public async Task<ProfesorDto> UpdateProfessor(
            int idProfesor,
            ProfesorDto professor)
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

            // Buscar usuario relacionado
            var users = await _userRepository.GetAll();

            var existingUser = users.FirstOrDefault(u =>
                u.IdPersona == existingProfessor.IdPersona &&
                u.IsDelete == '0');

            if (existingUser == null)
            {
                throw new Exception("El usuario del profesor no existe.");
            }

            // Validar nombre de usuario
            var userExists = users.Any(u =>
                u.IdUsuario != existingUser.IdUsuario &&
                u.NombreUsuario == professor.NombreUsuario &&
                u.IsDelete == '0');

            if (userExists)
            {
                throw new Exception("El nombre de usuario ya existe.");
            }

            // Actualizar Person
            existingProfessor.Persona.Nombres = professor.Nombres;
            existingProfessor.Persona.Apellidos = professor.Apellidos;
            existingProfessor.Persona.FechaNacimiento = professor.FechaNacimiento;
            existingProfessor.Persona.Telefono = professor.Telefono;
            existingProfessor.Persona.Direccion = professor.Direccion;
            existingProfessor.Persona.Correo = professor.Correo;

            // Actualizar Professor
            existingProfessor.Cedula = professor.Cedula;
            existingProfessor.Especialidad = professor.Especialidad;
            existingProfessor.Activo = professor.Activo;

            // Actualizar User
            existingUser.NombreUsuario = professor.NombreUsuario;
            existingUser.Password = professor.Password;
            existingUser.Activo = professor.Activo;

            await _personRepository.Update(existingProfessor.Persona);
            await _professorRepository.Update(existingProfessor);
            await _userRepository.Update(existingUser);

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
                Activo = existingProfessor.Activo,

                NombreUsuario = existingUser.NombreUsuario,
                Password = existingUser.Password
            };
        }

        public async Task<ProfesorDto> DeleteProfessor(int idProfesor)
        {
            var professor = await _professorRepository.GetById(idProfesor);

            if (professor == null || professor.IsDelete == '1')
            {
                return null;
            }

            professor.IsDelete = '1';

            await _professorRepository.Delete(professor);

            return new ProfesorDto
            {
                Cedula = professor.Cedula,
                Especialidad = professor.Especialidad,
                Activo = professor.Activo
            };
        }

        public async Task<List<ProfesorDto>> GetProfessorNotDeleted()
        {
            return await GetAllProfessor();
        }
    }
}

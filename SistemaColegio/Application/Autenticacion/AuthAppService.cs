using Application.Autenticacion.DTOs;
using Application.Usuario.DTOs;
using Domain.Entities;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Autenticacion
{
    public class AuthAppService : IAuthAppService
    {
        private readonly GeneralRepository<CDUser> _userRepository;

        public AuthAppService(
            GeneralRepository<CDUser> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto?> Login(LoginDto loginDto)
        {
            var users = await _userRepository.GetAllInclude(
                u => u.Persona,
                u => u.Rol);

            var user = users.FirstOrDefault(u =>
                u.NombreUsuario == loginDto.NombreUsuario &&
                u.IsDelete == '0' &&
                u.Activo == '1');

            if (user == null)
            {
                return null;
            }

            var passwordCorrecta = BCrypt.Net.BCrypt.Verify(
                loginDto.Password,
                user.Password);

            if (!passwordCorrecta)
            {
                return null;
            }

            return new UserDto
            {
                Nombres = user.Persona.Nombres,
                Apellidos = user.Persona.Apellidos,
                FechaNacimiento = user.Persona.FechaNacimiento,
                Telefono = user.Persona.Telefono,
                Direccion = user.Persona.Direccion,
                Correo = user.Persona.Correo,

                NombreUsuario = user.NombreUsuario,
                Activo = user.Activo,

                NombreRol = user.Rol.NombreRol
            };
        }
    }
}

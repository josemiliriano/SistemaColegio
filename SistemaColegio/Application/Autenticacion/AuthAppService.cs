using Application.Autenticacion.DTOs;
using Application.Usuario.DTOs;
using Domain.Entities;
using Infraestructure.Repository;

namespace Application.Autenticacion
{
    public class AuthAppService : IAuthAppService
    {
        private readonly GeneralRepository<CDUser> _userRepository;
        private readonly IJwtService _jwtService;

        public AuthAppService(GeneralRepository<CDUser> userRepository, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        public async Task<LoginResponseDto?> Login(LoginDto loginDto)
        {
            var user = await _userRepository.GetFirstOrDefaultInclude(
                u => u.NombreUsuario == loginDto.NombreUsuario &&
                     u.IsDelete == '0' &&
                     u.Activo == '1',
                u => u.Persona,
                u => u.Rol);

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

            var token = _jwtService.GenerateToken(user);

            return new LoginResponseDto
            {
                Token = token,

                IdUsuario = user.IdUsuario,
                IdRol = user.IdRol,

                Nombres = user.Persona.Nombres,
                Apellidos = user.Persona.Apellidos,

                NombreUsuario = user.NombreUsuario,
                NombreRol = user.Rol.NombreRol
            };
        }
    }
}
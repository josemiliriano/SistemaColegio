using Application.Autenticacion.DTOs;

namespace Application.Autenticacion
{
    public interface IAuthAppService
    {
        Task<LoginResponseDto?> Login(LoginDto loginDto);
    }
}
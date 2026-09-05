using Application.Autenticacion.DTOs;
using Application.Usuario.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Autenticacion
{
    public interface IAuthAppService
    {
        Task<UserDto?> Login(LoginDto loginDto);
    }
}

using Application.Usuario.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Usuario
{
    public interface IUserAppService
    {
        Task<UserDto> AddUser(UserDto user);
        Task<List<UserDto>> GetAllUser();
        Task<UserDto> GetUserById(int idPersona);
        Task<UserDto> UpdateUser(int idPersona, UserDto user);
        Task<UserDto> DeleteUser(int idPersona);
        Task<List<UserDto>> GetUserNotDeleted();
    }
}

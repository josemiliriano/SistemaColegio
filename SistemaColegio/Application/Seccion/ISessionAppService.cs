using Application.Seccion.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Seccion
{
    public interface ISessionAppService
    {
        Task<SessionDto> AddSession(SessionDto session);
        Task<List<SessionDto>> GetAllSession();
        Task<SessionDto> GetSessionById(int id);
        Task<SessionDto> UpdateSession(SessionDto session);
        Task<SessionDto> DeleteSession(SessionDto session);
        Task<List<SessionDto>> GetSessionNotDeleted();
    }
}

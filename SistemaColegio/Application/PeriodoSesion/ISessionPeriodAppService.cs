using Application.PeriodoSesion.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.PeriodoSesion
{
    public interface ISessionPeriodAppService
    {
        Task<SessionPeriodDto> AddSessionPeriod(SessionPeriodDto sessionPeriod);
        Task<List<SessionPeriodDto>> GetAllSessionPeriod();
        Task<SessionPeriodDto> GetSessionPeriodById(int id);
        Task<SessionPeriodDto> UpdateSessionPeriod(SessionPeriodDto sessionPeriod);
        Task<SessionPeriodDto> DeleteSessionPeriod(SessionPeriodDto sessionPeriod);
        Task<List<SessionPeriodDto>> GetSessionPeriodNotDeleted();
    }
}

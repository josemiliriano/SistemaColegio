using Application.Periodo.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Periodo
{
    public interface IPeriodAppService
    {
        Task<PeriodDto> AddPeriod(PeriodDto period);
        Task<List<PeriodDto>> GetAllPeriod();
        Task<PeriodDto> GetPeriodById(int id);
        Task<PeriodDto> UpdatePeriod(PeriodDto period);
        Task<PeriodDto> DeletePeriod(PeriodDto period);
        Task<List<PeriodDto>> GetPeriodNotDeleted();
    }
}
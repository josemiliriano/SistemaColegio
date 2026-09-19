using Application.SubPeriodo.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.SubPeriodo
{
    public interface ISubPeriodAppService
    {
        Task<SubPeriodDto> AddSubPeriod(SubPeriodDto subPeriod);
        Task<List<SubPeriodDto>> GetAllSubPeriod();
        Task<SubPeriodDto> GetSubPeriodById(int id);
        Task<SubPeriodDto> UpdateSubPeriod(SubPeriodDto subPeriod);
        Task<SubPeriodDto> DeleteSubPeriod(SubPeriodDto subPeriod);
        Task<List<SubPeriodDto>> GetSubPeriodNotDeleted();
    }
}

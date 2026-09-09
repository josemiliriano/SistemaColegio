using Application.SubPeriodos.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.SubPeriodos
{
    public interface IAcademicSubPeriodAppService
    {
        Task<AcademicSubPeriodDto> AddAcademicSubPeriod(AcademicSubPeriodDto academicSubPeriod);
        Task<List<AcademicSubPeriodDto>> GetAllAcademicSubPeriod();
        Task<AcademicSubPeriodDto> GetAcademicSubPeriodById(int id);
        Task<AcademicSubPeriodDto> UpdateAcademicSubPeriod(AcademicSubPeriodDto academicSubPeriod);
        Task<AcademicSubPeriodDto> DeleteAcademicSubPeriod(AcademicSubPeriodDto academicSubPeriod);
        Task<List<AcademicSubPeriodDto>> GetAcademicSubPeriodNotDeleted();
    }
}

using Application.MesPeriodo.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.MesPeriodo
{
    public interface IAcademicMonthAppService
    {
        Task<AcademicMonthDto> AddAcademicMonth(AcademicMonthDto academicMonth);
        Task<List<AcademicMonthDto>> GetAllAcademicMonth();
        Task<AcademicMonthDto> GetAcademicMonthById(int id);
        Task<AcademicMonthDto> UpdateAcademicMonth(AcademicMonthDto academicMonth);
        Task<AcademicMonthDto> DeleteAcademicMonth(AcademicMonthDto academicMonth);
        Task<List<AcademicMonthDto>> GetAcademicMonthNotDeleted();
    }
}

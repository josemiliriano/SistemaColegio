using Application.CursoPeriodo.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.CursoPeriodo
{
    public interface ICoursePeriodAppService
    {
        Task<CursoPeriodoDto> AddCoursePeriod(CursoPeriodoDto cursoPeriodo);
        Task<List<CursoPeriodoDto>> GetAllCoursePeriod();
        Task<CursoPeriodoDto> GetCoursePeriodById(int id);
        Task<CursoPeriodoDto> UpdateCoursePeriod(CursoPeriodoDto cursoPeriodo);
        Task<CursoPeriodoDto> DeleteCoursePeriod(CursoPeriodoDto cursoPeriodo);
        Task<List<CursoPeriodoDto>> GetCoursePeriodNotDeleted();
    }
}

using Application.CursoPeriodo.DTOs;
using Domain.Entities;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.CursoPeriodo
{
    namespace Application.CursoPeriodo
    {
        public class CoursePeriodAppService : ICoursePeriodAppService
        {
            private readonly GeneralRepository<CoursePeriod>
                _coursePeriodRepository;

            public CoursePeriodAppService(
                GeneralRepository<CoursePeriod> coursePeriodRepository)
            {
                _coursePeriodRepository = coursePeriodRepository;
            }

            public async Task<CursoPeriodoDto> AddCoursePeriod(
                CursoPeriodoDto cursoPeriodo)
            {
                var coursePeriods =
                    await _coursePeriodRepository.GetAll();

                // Validar que el curso no esté registrado
                // nuevamente en el mismo período
                var coursePeriodExists = coursePeriods.Any(x =>
                    x.IdCurso == cursoPeriodo.IdCurso &&
                    x.IdPeriodo == cursoPeriodo.IdPeriodo &&
                    x.IsDelete == '0');

                if (coursePeriodExists)
                {
                    throw new Exception(
                        "El curso ya está registrado en este período académico.");
                }

                var newCoursePeriod = new CoursePeriod
                {
                    IdCurso = cursoPeriodo.IdCurso,
                    IdPeriodo = cursoPeriodo.IdPeriodo,
                    Activo = cursoPeriodo.Activo,
                    IsDelete = '0'
                };

                newCoursePeriod =
                    await _coursePeriodRepository.Add(newCoursePeriod);

                return new CursoPeriodoDto
                {
                    IdCursoPeriodo = newCoursePeriod.IdCursoPeriodo,
                    IdCurso = newCoursePeriod.IdCurso,
                    IdPeriodo = newCoursePeriod.IdPeriodo,
                    Activo = newCoursePeriod.Activo
                };
            }

            public async Task<List<CursoPeriodoDto>>
                GetAllCoursePeriod()
            {
                var coursePeriods =
                    await _coursePeriodRepository.GetAll();

                return coursePeriods
                    .Where(x => x.IsDelete == '0')
                    .Select(x => new CursoPeriodoDto
                    {
                        IdCursoPeriodo = x.IdCursoPeriodo,
                        IdCurso = x.IdCurso,
                        IdPeriodo = x.IdPeriodo,
                        Activo = x.Activo
                    })
                    .ToList();
            }

            public async Task<CursoPeriodoDto>
                GetCoursePeriodById(int id)
            {
                var coursePeriod =
                    await _coursePeriodRepository.GetById(id);

                if (coursePeriod == null ||
                    coursePeriod.IsDelete == '1')
                {
                    return null;
                }

                return new CursoPeriodoDto
                {
                    IdCursoPeriodo = coursePeriod.IdCursoPeriodo,
                    IdCurso = coursePeriod.IdCurso,
                    IdPeriodo = coursePeriod.IdPeriodo,
                    Activo = coursePeriod.Activo
                };
            }

            public async Task<CursoPeriodoDto>
                UpdateCoursePeriod(
                    CursoPeriodoDto cursoPeriodo)
            {
                var existingCoursePeriod =
                    await _coursePeriodRepository.GetById(
                        cursoPeriodo.IdCursoPeriodo);

                if (existingCoursePeriod == null ||
                    existingCoursePeriod.IsDelete == '1')
                {
                    return null;
                }

                // Actualmente solo actualizamos el estado
                existingCoursePeriod.Activo =
                    cursoPeriodo.Activo;

                await _coursePeriodRepository.Update(
                    existingCoursePeriod);

                return new CursoPeriodoDto
                {
                    IdCursoPeriodo =
                        existingCoursePeriod.IdCursoPeriodo,

                    IdCurso =
                        existingCoursePeriod.IdCurso,

                    IdPeriodo =
                        existingCoursePeriod.IdPeriodo,

                    Activo =
                        existingCoursePeriod.Activo
                };
            }

            public async Task<CursoPeriodoDto>
                DeleteCoursePeriod(
                    CursoPeriodoDto cursoPeriodo)
            {
                var existingCoursePeriod =
                    await _coursePeriodRepository.GetById(
                        cursoPeriodo.IdCursoPeriodo);

                if (existingCoursePeriod == null ||
                    existingCoursePeriod.IsDelete == '1')
                {
                    return null;
                }

                // Eliminación lógica
                existingCoursePeriod.IsDelete = '1';
                existingCoursePeriod.Activo = '0';

                await _coursePeriodRepository.Update(
                    existingCoursePeriod);

                return new CursoPeriodoDto
                {
                    IdCursoPeriodo =
                        existingCoursePeriod.IdCursoPeriodo,

                    IdCurso =
                        existingCoursePeriod.IdCurso,

                    IdPeriodo =
                        existingCoursePeriod.IdPeriodo,

                    Activo =
                        existingCoursePeriod.Activo
                };
            }

            public async Task<List<CursoPeriodoDto>>
                GetCoursePeriodNotDeleted()
            {
                return await GetAllCoursePeriod();
            }
        }
    }
}
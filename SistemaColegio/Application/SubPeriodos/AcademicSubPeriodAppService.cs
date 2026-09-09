using Application.SubPeriodos.DTOs;
using Domain.Entities;
using Infraestructure.Data;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.SubPeriodos
{
    public class AcademicSubPeriodAppService : IAcademicSubPeriodAppService
    {
        private readonly GeneralRepository<AcademicSubPeriod> _academicSubPeriodRepository;
        private readonly GeneralRepository<Period> _periodRepository;
        private readonly GeneralRepository<SubPeriod> _subPeriodRepository;
        private readonly MyDataContext _context;

        public AcademicSubPeriodAppService(
            GeneralRepository<AcademicSubPeriod> academicSubPeriodRepository,
            GeneralRepository<Period> periodRepository,
            GeneralRepository<SubPeriod> subPeriodRepository,
            MyDataContext context)
        {
            _academicSubPeriodRepository = academicSubPeriodRepository;
            _periodRepository = periodRepository;
            _subPeriodRepository = subPeriodRepository;
            _context = context;
        }

        public async Task<AcademicSubPeriodDto> AddAcademicSubPeriod(
            AcademicSubPeriodDto academicSubPeriod)
        {
            // Verificar que el periodo exista
            var period = await _periodRepository.GetById(
                academicSubPeriod.IdPeriodo);

            if (period == null ||
                period.IsDelete == '1' ||
                period.Activo == '0')
            {
                throw new Exception(
                    "El periodo especificado no existe o está inactivo.");
            }

            // Verificar que el subperiodo exista
            var subPeriod = await _subPeriodRepository.GetById(
                academicSubPeriod.IdSubPeriodo);

            if (subPeriod == null ||
                subPeriod.IsDelete == '1' ||
                subPeriod.Activo == '0')
            {
                throw new Exception(
                    "El subperiodo especificado no existe o está inactivo.");
            }

            // Validar fechas
            if (academicSubPeriod.FechaInicio >
                academicSubPeriod.FechaFin)
            {
                throw new Exception(
                    "La fecha de inicio no puede ser mayor que la fecha de fin.");
            }

            // Obtener registros existentes
            var academicSubPeriods =
                await _academicSubPeriodRepository.GetAll();

            // Validar combinación Periodo + SubPeriodo
            var exists = academicSubPeriods.Any(x =>
                x.IdPeriodo == academicSubPeriod.IdPeriodo &&
                x.IdSubPeriodo == academicSubPeriod.IdSubPeriodo &&
                x.IsDelete == '0');

            if (exists)
            {
                throw new Exception(
                    "El subperiodo académico ya está registrado para este periodo.");
            }

            // Crear registro
            var newAcademicSubPeriod = new AcademicSubPeriod
            {
                IdPeriodo = academicSubPeriod.IdPeriodo,
                IdSubPeriodo = academicSubPeriod.IdSubPeriodo,
                FechaInicio = academicSubPeriod.FechaInicio,
                FechaFin = academicSubPeriod.FechaFin,
                Activo = academicSubPeriod.Activo,
                IsDelete = '0'
            };

            newAcademicSubPeriod =
                await _academicSubPeriodRepository.Add(
                    newAcademicSubPeriod);

            await _context.SaveChangesAsync();

            return new AcademicSubPeriodDto
            {
                IdPeriodoAcademico =
                    newAcademicSubPeriod.IdPeriodoAcademico,
                IdPeriodo =
                    newAcademicSubPeriod.IdPeriodo,
                IdSubPeriodo =
                    newAcademicSubPeriod.IdSubPeriodo,
                FechaInicio =
                    newAcademicSubPeriod.FechaInicio,
                FechaFin =
                    newAcademicSubPeriod.FechaFin,
                Activo =
                    newAcademicSubPeriod.Activo
            };
        }

        public async Task<List<AcademicSubPeriodDto>>
            GetAllAcademicSubPeriod()
        {
            var academicSubPeriods =
                await _academicSubPeriodRepository.GetAll();

            return academicSubPeriods
                .Where(x => x.IsDelete == '0')
                .Select(x => new AcademicSubPeriodDto
                {
                    IdPeriodoAcademico =
                        x.IdPeriodoAcademico,
                    IdPeriodo =
                        x.IdPeriodo,
                    IdSubPeriodo =
                        x.IdSubPeriodo,
                    FechaInicio =
                        x.FechaInicio,
                    FechaFin =
                        x.FechaFin,
                    Activo =
                        x.Activo
                })
                .ToList();
        }

        public async Task<AcademicSubPeriodDto>
            GetAcademicSubPeriodById(int id)
        {
            var academicSubPeriod =
                await _academicSubPeriodRepository.GetById(id);

            if (academicSubPeriod == null ||
                academicSubPeriod.IsDelete == '1')
            {
                return null;
            }

            return new AcademicSubPeriodDto
            {
                IdPeriodoAcademico =
                    academicSubPeriod.IdPeriodoAcademico,
                IdPeriodo =
                    academicSubPeriod.IdPeriodo,
                IdSubPeriodo =
                    academicSubPeriod.IdSubPeriodo,
                FechaInicio =
                    academicSubPeriod.FechaInicio,
                FechaFin =
                    academicSubPeriod.FechaFin,
                Activo =
                    academicSubPeriod.Activo
            };
        }

        public async Task<AcademicSubPeriodDto>
            UpdateAcademicSubPeriod(
                AcademicSubPeriodDto academicSubPeriod)
        {
            var academicSubPeriods =
                await _academicSubPeriodRepository.GetAll();

            var existingAcademicSubPeriod =
                academicSubPeriods.FirstOrDefault(x =>
                    x.IdPeriodoAcademico ==
                        academicSubPeriod.IdPeriodoAcademico &&
                    x.IsDelete == '0');

            if (existingAcademicSubPeriod == null)
            {
                return null;
            }

            // Verificar que el periodo exista
            var period = await _periodRepository.GetById(
                academicSubPeriod.IdPeriodo);

            if (period == null ||
                period.IsDelete == '1' ||
                period.Activo == '0')
            {
                throw new Exception(
                    "El periodo especificado no existe o está inactivo.");
            }

            // Verificar que el subperiodo exista
            var subPeriod = await _subPeriodRepository.GetById(
                academicSubPeriod.IdSubPeriodo);

            if (subPeriod == null ||
                subPeriod.IsDelete == '1' ||
                subPeriod.Activo == '0')
            {
                throw new Exception(
                    "El subperiodo especificado no existe o está inactivo.");
            }

            // Validar fechas
            if (academicSubPeriod.FechaInicio >
                academicSubPeriod.FechaFin)
            {
                throw new Exception(
                    "La fecha de inicio no puede ser mayor que la fecha de fin.");
            }

            // Validar combinación duplicada
            var exists = academicSubPeriods.Any(x =>
                x.IdPeriodoAcademico !=
                    academicSubPeriod.IdPeriodoAcademico &&
                x.IdPeriodo == academicSubPeriod.IdPeriodo &&
                x.IdSubPeriodo == academicSubPeriod.IdSubPeriodo &&
                x.IsDelete == '0');

            if (exists)
            {
                throw new Exception(
                    "El subperiodo académico ya está registrado para este periodo.");
            }

            // Actualizar
            existingAcademicSubPeriod.IdPeriodo =
                academicSubPeriod.IdPeriodo;

            existingAcademicSubPeriod.IdSubPeriodo =
                academicSubPeriod.IdSubPeriodo;

            existingAcademicSubPeriod.FechaInicio =
                academicSubPeriod.FechaInicio;

            existingAcademicSubPeriod.FechaFin =
                academicSubPeriod.FechaFin;

            existingAcademicSubPeriod.Activo =
                academicSubPeriod.Activo;

            await _academicSubPeriodRepository.Update(
                existingAcademicSubPeriod);

            await _context.SaveChangesAsync();

            return new AcademicSubPeriodDto
            {
                IdPeriodoAcademico =
                    existingAcademicSubPeriod.IdPeriodoAcademico,
                IdPeriodo =
                    existingAcademicSubPeriod.IdPeriodo,
                IdSubPeriodo =
                    existingAcademicSubPeriod.IdSubPeriodo,
                FechaInicio =
                    existingAcademicSubPeriod.FechaInicio,
                FechaFin =
                    existingAcademicSubPeriod.FechaFin,
                Activo =
                    existingAcademicSubPeriod.Activo
            };
        }

        public async Task<AcademicSubPeriodDto> DeleteAcademicSubPeriod(AcademicSubPeriodDto academicSubPeriod)
        {
            var existingAcademicSubPeriod =
                await _academicSubPeriodRepository.GetById(
                    academicSubPeriod.IdPeriodoAcademico);

            if (existingAcademicSubPeriod == null ||
                existingAcademicSubPeriod.IsDelete == '1')
            {
                return null;
            }

            // Eliminación lógica
            existingAcademicSubPeriod.IsDelete = '1';
            existingAcademicSubPeriod.Activo = '0';

            await _academicSubPeriodRepository.Update(
                existingAcademicSubPeriod);

            await _context.SaveChangesAsync();

            return new AcademicSubPeriodDto
            {
                IdPeriodoAcademico =
                    existingAcademicSubPeriod.IdPeriodoAcademico,
                IdPeriodo =
                    existingAcademicSubPeriod.IdPeriodo,
                IdSubPeriodo =
                    existingAcademicSubPeriod.IdSubPeriodo,
                FechaInicio =
                    existingAcademicSubPeriod.FechaInicio,
                FechaFin =
                    existingAcademicSubPeriod.FechaFin,
                Activo =
                    existingAcademicSubPeriod.Activo
            };
        }

        public async Task<List<AcademicSubPeriodDto>>
            GetAcademicSubPeriodNotDeleted()
        {
            return await GetAllAcademicSubPeriod();
        }
    }
}

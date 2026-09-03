using Application.Periodo.DTOs;
using Domain.Entities;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Periodo
{
    public class PeriodAppService : IPeriodAppService
    {
        private readonly GeneralRepository<Period> _periodRepository;

        public PeriodAppService(
            GeneralRepository<Period> periodRepository)
        {
            _periodRepository = periodRepository;
        }

        public async Task<PeriodDto> AddPeriod(PeriodDto period)
        {
            // Validar las fechas
            if (period.FechaInicio >= period.FechaFin)
            {
                throw new Exception(
                    "La fecha de inicio debe ser menor que la fecha de finalización.");
            }

            // Obtener períodos existentes
            var periods = await _periodRepository.GetAll();

            // Validar nombre duplicado
            var periodExists = periods.Any(x =>
                x.Nombre == period.Nombre &&
                x.IsDelete == '0');

            if (periodExists)
            {
                throw new Exception(
                    "El período académico ya existe.");
            }

            // Crear entidad
            var newPeriod = new Period
            {
                Nombre = period.Nombre,
                FechaInicio = period.FechaInicio,
                FechaFin = period.FechaFin,
                Activo = period.Activo,
                IsDelete = '0'
            };

            // Guardar
            newPeriod = await _periodRepository.Add(newPeriod);

            // Retornar DTO
            return new PeriodDto
            {
                IdPeriodo = newPeriod.IdPeriodo,
                Nombre = newPeriod.Nombre,
                FechaInicio = newPeriod.FechaInicio,
                FechaFin = newPeriod.FechaFin,
                Activo = newPeriod.Activo
            };
        }

        public async Task<List<PeriodDto>> GetAllPeriod()
        {
            var periods = await _periodRepository.GetAll();

            return periods
                .Where(x => x.IsDelete == '0')
                .Select(x => new PeriodDto
                {
                    IdPeriodo = x.IdPeriodo,
                    Nombre = x.Nombre,
                    FechaInicio = x.FechaInicio,
                    FechaFin = x.FechaFin,
                    Activo = x.Activo
                })
                .ToList();
        }

        public async Task<PeriodDto> GetPeriodById(int id)
        {
            var period = await _periodRepository.GetById(id);

            if (period == null || period.IsDelete == '1')
            {
                return null;
            }

            return new PeriodDto
            {
                IdPeriodo = period.IdPeriodo,
                Nombre = period.Nombre,
                FechaInicio = period.FechaInicio,
                FechaFin = period.FechaFin,
                Activo = period.Activo
            };
        }

        public async Task<PeriodDto> UpdatePeriod(PeriodDto period)
        {
            var periods = await _periodRepository.GetAll();

            var existingPeriod = periods.FirstOrDefault(x =>
                x.IdPeriodo == period.IdPeriodo &&
                x.IsDelete == '0');

            if (existingPeriod == null)
            {
                return null;
            }

            // Validar las fechas
            if (period.FechaInicio >= period.FechaFin)
            {
                throw new Exception(
                    "La fecha de inicio debe ser menor que la fecha de finalización.");
            }

            // Validar nombre duplicado
            var periodExists = periods.Any(x =>
                x.IdPeriodo != period.IdPeriodo &&
                x.Nombre == period.Nombre &&
                x.IsDelete == '0');

            if (periodExists)
            {
                throw new Exception(
                    "El período académico ya existe.");
            }

            // Actualizar
            existingPeriod.Nombre = period.Nombre;
            existingPeriod.FechaInicio = period.FechaInicio;
            existingPeriod.FechaFin = period.FechaFin;
            existingPeriod.Activo = period.Activo;

            await _periodRepository.Update(existingPeriod);

            // Retornar DTO
            return new PeriodDto
            {
                IdPeriodo = existingPeriod.IdPeriodo,
                Nombre = existingPeriod.Nombre,
                FechaInicio = existingPeriod.FechaInicio,
                FechaFin = existingPeriod.FechaFin,
                Activo = existingPeriod.Activo
            };
        }

        public async Task<PeriodDto> DeletePeriod(PeriodDto period)
        {
            var existingPeriod = await _periodRepository.GetById(
                period.IdPeriodo);

            if (existingPeriod == null ||
                existingPeriod.IsDelete == '1')
            {
                return null;
            }

            // Eliminación lógica
            existingPeriod.IsDelete = '1';
            existingPeriod.Activo = '0';

            await _periodRepository.Update(existingPeriod);

            // Retornar DTO
            return new PeriodDto
            {
                IdPeriodo = existingPeriod.IdPeriodo,
                Nombre = existingPeriod.Nombre,
                FechaInicio = existingPeriod.FechaInicio,
                FechaFin = existingPeriod.FechaFin,
                Activo = existingPeriod.Activo
            };
        }

        public async Task<List<PeriodDto>> GetPeriodNotDeleted()
        {
            return await GetAllPeriod();
        }
    }

}

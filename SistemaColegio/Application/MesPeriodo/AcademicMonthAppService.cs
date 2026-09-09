using Application.MesPeriodo.DTOs;
using Domain.Entities;
using Infraestructure.Data;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.MesPeriodo
{
    public class AcademicMonthAppService : IAcademicMonthAppService
    {
        private readonly GeneralRepository<AcademicMonth> _academicMonthRepository;
        private readonly GeneralRepository<AcademicSubPeriod> _academicSubPeriodRepository;
        private readonly GeneralRepository<Month> _monthRepository;
        private readonly MyDataContext _context;

        public AcademicMonthAppService(
            GeneralRepository<AcademicMonth> academicMonthRepository,
            GeneralRepository<AcademicSubPeriod> academicSubPeriodRepository,
            GeneralRepository<Month> monthRepository,
            MyDataContext context)
        {
            _academicMonthRepository = academicMonthRepository;
            _academicSubPeriodRepository = academicSubPeriodRepository;
            _monthRepository = monthRepository;
            _context = context;
        }

        public async Task<AcademicMonthDto> AddAcademicMonth(
            AcademicMonthDto academicMonth)
        {
            // Verificar que el periodo académico exista
            var academicSubPeriod =
                await _academicSubPeriodRepository.GetById(
                    academicMonth.IdPeriodoAcademico);

            if (academicSubPeriod == null ||
                academicSubPeriod.IsDelete == '1' ||
                academicSubPeriod.Activo == '0')
            {
                throw new Exception(
                    "El periodo académico especificado no existe o está inactivo.");
            }

            // Verificar que el mes exista
            var month =
                await _monthRepository.GetById(
                    academicMonth.IdMes);

            if (month == null ||
                month.IsDelete == '1' ||
                month.Activo == '0')
            {
                throw new Exception(
                    "El mes especificado no existe o está inactivo.");
            }

            // Validar que el orden sea válido
            if (academicMonth.Orden <= 0)
            {
                throw new Exception(
                    "El orden debe ser mayor que cero.");
            }

            // Obtener registros existentes
            var academicMonths =
                await _academicMonthRepository.GetAll();

            // Validar mes duplicado dentro del periodo académico
            var monthExists = academicMonths.Any(x =>
                x.IdPeriodoAcademico ==
                    academicMonth.IdPeriodoAcademico &&
                x.IdMes == academicMonth.IdMes &&
                x.IsDelete == '0');

            if (monthExists)
            {
                throw new Exception(
                    "El mes ya está registrado para este periodo académico.");
            }

            // Validar orden duplicado dentro del periodo académico
            var orderExists = academicMonths.Any(x =>
                x.IdPeriodoAcademico ==
                    academicMonth.IdPeriodoAcademico &&
                x.Orden == academicMonth.Orden &&
                x.IsDelete == '0');

            if (orderExists)
            {
                throw new Exception(
                    "El orden ya está registrado para este periodo académico.");
            }

            // Crear mes académico
            var newAcademicMonth = new AcademicMonth
            {
                IdPeriodoAcademico =
                    academicMonth.IdPeriodoAcademico,

                IdMes =
                    academicMonth.IdMes,

                Orden =
                    academicMonth.Orden,

                Activo =
                    academicMonth.Activo,

                IsDelete = '0'
            };

            newAcademicMonth =
                await _academicMonthRepository.Add(
                    newAcademicMonth);

            await _context.SaveChangesAsync();

            return new AcademicMonthDto
            {
                IdMesAcademico =
                    newAcademicMonth.IdMesAcademico,

                IdPeriodoAcademico =
                    newAcademicMonth.IdPeriodoAcademico,

                IdMes =
                    newAcademicMonth.IdMes,

                Orden =
                    newAcademicMonth.Orden,

                Activo =
                    newAcademicMonth.Activo
            };
        }

        public async Task<List<AcademicMonthDto>>
            GetAllAcademicMonth()
        {
            var academicMonths =
                await _academicMonthRepository.GetAll();

            return academicMonths
                .Where(x => x.IsDelete == '0')
                .Select(x => new AcademicMonthDto
                {
                    IdMesAcademico =
                        x.IdMesAcademico,

                    IdPeriodoAcademico =
                        x.IdPeriodoAcademico,

                    IdMes =
                        x.IdMes,

                    Orden =
                        x.Orden,

                    Activo =
                        x.Activo
                })
                .ToList();
        }

        public async Task<AcademicMonthDto>
            GetAcademicMonthById(int id)
        {
            var academicMonth =
                await _academicMonthRepository.GetById(id);

            if (academicMonth == null ||
                academicMonth.IsDelete == '1')
            {
                return null;
            }

            return new AcademicMonthDto
            {
                IdMesAcademico =
                    academicMonth.IdMesAcademico,

                IdPeriodoAcademico =
                    academicMonth.IdPeriodoAcademico,

                IdMes =
                    academicMonth.IdMes,

                Orden =
                    academicMonth.Orden,

                Activo =
                    academicMonth.Activo
            };
        }

        public async Task<AcademicMonthDto>
            UpdateAcademicMonth(
                AcademicMonthDto academicMonth)
        {
            var academicMonths =
                await _academicMonthRepository.GetAll();

            var existingAcademicMonth =
                academicMonths.FirstOrDefault(x =>
                    x.IdMesAcademico ==
                        academicMonth.IdMesAcademico &&
                    x.IsDelete == '0');

            if (existingAcademicMonth == null)
            {
                return null;
            }

            // Verificar que el periodo académico exista
            var academicSubPeriod =
                await _academicSubPeriodRepository.GetById(
                    academicMonth.IdPeriodoAcademico);

            if (academicSubPeriod == null ||
                academicSubPeriod.IsDelete == '1' ||
                academicSubPeriod.Activo == '0')
            {
                throw new Exception(
                    "El periodo académico especificado no existe o está inactivo.");
            }

            // Verificar que el mes exista
            var month =
                await _monthRepository.GetById(
                    academicMonth.IdMes);

            if (month == null ||
                month.IsDelete == '1' ||
                month.Activo == '0')
            {
                throw new Exception(
                    "El mes especificado no existe o está inactivo.");
            }

            // Validar orden
            if (academicMonth.Orden <= 0)
            {
                throw new Exception(
                    "El orden debe ser mayor que cero.");
            }

            // Validar mes duplicado
            var monthExists = academicMonths.Any(x =>
                x.IdMesAcademico !=
                    academicMonth.IdMesAcademico &&
                x.IdPeriodoAcademico ==
                    academicMonth.IdPeriodoAcademico &&
                x.IdMes == academicMonth.IdMes &&
                x.IsDelete == '0');

            if (monthExists)
            {
                throw new Exception(
                    "El mes ya está registrado para este periodo académico.");
            }

            // Validar orden duplicado
            var orderExists = academicMonths.Any(x =>
                x.IdMesAcademico !=
                    academicMonth.IdMesAcademico &&
                x.IdPeriodoAcademico ==
                    academicMonth.IdPeriodoAcademico &&
                x.Orden == academicMonth.Orden &&
                x.IsDelete == '0');

            if (orderExists)
            {
                throw new Exception(
                    "El orden ya está registrado para este periodo académico.");
            }

            // Actualizar
            existingAcademicMonth.IdPeriodoAcademico =
                academicMonth.IdPeriodoAcademico;

            existingAcademicMonth.IdMes =
                academicMonth.IdMes;

            existingAcademicMonth.Orden =
                academicMonth.Orden;

            existingAcademicMonth.Activo =
                academicMonth.Activo;

            await _academicMonthRepository.Update(
                existingAcademicMonth);

            await _context.SaveChangesAsync();

            return new AcademicMonthDto
            {
                IdMesAcademico =
                    existingAcademicMonth.IdMesAcademico,

                IdPeriodoAcademico =
                    existingAcademicMonth.IdPeriodoAcademico,

                IdMes =
                    existingAcademicMonth.IdMes,

                Orden =
                    existingAcademicMonth.Orden,

                Activo =
                    existingAcademicMonth.Activo
            };
        }

        public async Task<AcademicMonthDto>
            DeleteAcademicMonth(
                AcademicMonthDto academicMonth)
        {
            var existingAcademicMonth =
                await _academicMonthRepository.GetById(
                    academicMonth.IdMesAcademico);

            if (existingAcademicMonth == null ||
                existingAcademicMonth.IsDelete == '1')
            {
                return null;
            }

            // Eliminación lógica
            existingAcademicMonth.IsDelete = '1';
            existingAcademicMonth.Activo = '0';

            await _academicMonthRepository.Update(
                existingAcademicMonth);

            await _context.SaveChangesAsync();

            return new AcademicMonthDto
            {
                IdMesAcademico =
                    existingAcademicMonth.IdMesAcademico,

                IdPeriodoAcademico =
                    existingAcademicMonth.IdPeriodoAcademico,

                IdMes =
                    existingAcademicMonth.IdMes,

                Orden =
                    existingAcademicMonth.Orden,

                Activo =
                    existingAcademicMonth.Activo
            };
        }

        public async Task<List<AcademicMonthDto>>
            GetAcademicMonthNotDeleted()
        {
            return await GetAllAcademicMonth();
        }
    }
}

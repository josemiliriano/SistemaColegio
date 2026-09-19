using Application.SubPeriodo.DTOs;
using Domain.Entities;
using Infraestructure.Data;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.SubPeriodo
{
    public class SubPeriodAppService : ISubPeriodAppService
    {
        private readonly GeneralRepository<SubPeriod> _subPeriodRepository;
        private readonly MyDataContext _context;

        public SubPeriodAppService(
            GeneralRepository<SubPeriod> subPeriodRepository,
            MyDataContext context)
        {
            _subPeriodRepository = subPeriodRepository;
            _context = context;
        }

        public async Task<SubPeriodDto> AddSubPeriod(SubPeriodDto subPeriod)
        {
            if (subPeriod == null)
            {
                throw new Exception("Los datos del subperiodo son obligatorios.");
            }

            if (string.IsNullOrWhiteSpace(subPeriod.Nombre))
            {
                throw new Exception("El nombre del subperiodo es obligatorio.");
            }

            if (subPeriod.Orden <= 0)
            {
                throw new Exception("El orden debe ser mayor que cero.");
            }

            // Validar nombre duplicado
            var subPeriods = await _subPeriodRepository.GetAll();

            var nombreExists = subPeriods.Any(s =>
                s.Nombre.ToLower() == subPeriod.Nombre.Trim().ToLower() &&
                s.IsDelete == '0');

            if (nombreExists)
            {
                throw new Exception("El nombre del subperiodo ya existe.");
            }

            // Validar orden duplicado
            var ordenExists = subPeriods.Any(s =>
                s.Orden == subPeriod.Orden &&
                s.IsDelete == '0');

            if (ordenExists)
            {
                throw new Exception("El orden del subperiodo ya existe.");
            }

            // Iniciar transacción
            await using var transaction = await _context.BeginTransactionAsync();

            try
            {
                var newSubPeriod = new SubPeriod
                {
                    Nombre = subPeriod.Nombre.Trim(),
                    Orden = subPeriod.Orden                    
                };

                await _subPeriodRepository.Add(newSubPeriod);

                // Guardar cambios
                await _context.SaveChangesAsync();

                // Confirmar transacción
                await transaction.CommitAsync();

                return new SubPeriodDto
                {
                    IdSubPeriodo = newSubPeriod.IdSubPeriodo,
                    Nombre = newSubPeriod.Nombre,
                    Orden = newSubPeriod.Orden,
                    Activo = newSubPeriod.Activo
                };
            }
            catch
            {
                await transaction.RollbackAsync();

                throw;
            }
        }

        public async Task<List<SubPeriodDto>> GetAllSubPeriod()
        {
            var subPeriods = await _subPeriodRepository.GetAll();

            return subPeriods
                .Select(s => new SubPeriodDto
                {
                    IdSubPeriodo = s.IdSubPeriodo,
                    Nombre = s.Nombre,
                    Orden = s.Orden,
                    Activo = s.Activo
                })
                .ToList();
        }

        public async Task<SubPeriodDto> GetSubPeriodById(int id)
        {
            var subPeriod = await _subPeriodRepository.GetById(id);

            if (subPeriod == null || subPeriod.IsDelete == '1')
            {
                return null;
            }

            return new SubPeriodDto
            {
                IdSubPeriodo = subPeriod.IdSubPeriodo,
                Nombre = subPeriod.Nombre,
                Orden = subPeriod.Orden,
                Activo = subPeriod.Activo
            };
        }

        public async Task<SubPeriodDto> UpdateSubPeriod(SubPeriodDto subPeriod)
        {
            if (subPeriod == null)
            {
                throw new Exception("Los datos del subperiodo son obligatorios.");
            }

            if (string.IsNullOrWhiteSpace(subPeriod.Nombre))
            {
                throw new Exception("El nombre del subperiodo es obligatorio.");
            }

            if (subPeriod.Orden <= 0)
            {
                throw new Exception("El orden debe ser mayor que cero.");
            }

            var subPeriods = await _subPeriodRepository.GetAll();

            var existingSubPeriod = subPeriods.FirstOrDefault(s =>
                s.IdSubPeriodo == subPeriod.IdSubPeriodo &&
                s.IsDelete == '0');

            if (existingSubPeriod == null)
            {
                return null;
            }

            // Validar nombre duplicado
            var nombreExists = subPeriods.Any(s =>
                s.IdSubPeriodo != subPeriod.IdSubPeriodo &&
                s.Nombre.ToLower() == subPeriod.Nombre.Trim().ToLower() &&
                s.IsDelete == '0');

            if (nombreExists)
            {
                throw new Exception("El nombre del subperiodo ya existe.");
            }

            // Validar orden duplicado
            var ordenExists = subPeriods.Any(s =>
                s.IdSubPeriodo != subPeriod.IdSubPeriodo &&
                s.Orden == subPeriod.Orden &&
                s.IsDelete == '0');

            if (ordenExists)
            {
                throw new Exception("El orden del subperiodo ya existe.");
            }

            // Iniciar transacción
            await using var transaction = await _context.BeginTransactionAsync();

            try
            {
                existingSubPeriod.Nombre = subPeriod.Nombre.Trim();
                existingSubPeriod.Orden = subPeriod.Orden;
                existingSubPeriod.Activo = subPeriod.Activo;

                await _subPeriodRepository.Update(existingSubPeriod);

                // Guardar cambios
                await _context.SaveChangesAsync();

                // Confirmar transacción
                await transaction.CommitAsync();

                return new SubPeriodDto
                {
                    IdSubPeriodo = existingSubPeriod.IdSubPeriodo,
                    Nombre = existingSubPeriod.Nombre,
                    Orden = existingSubPeriod.Orden,
                    Activo = existingSubPeriod.Activo
                };
            }
            catch
            {
                await transaction.RollbackAsync();

                throw;
            }
        }

        public async Task<SubPeriodDto> DeleteSubPeriod(SubPeriodDto subPeriod)
        {
            if (subPeriod == null)
            {
                throw new Exception("Los datos del subperiodo son obligatorios.");
            }

            var existingSubPeriod =
                await _subPeriodRepository.GetById(subPeriod.IdSubPeriodo);

            if (existingSubPeriod == null ||
                existingSubPeriod.IsDelete == '1')
            {
                return null;
            }

            // Iniciar transacción
            await using var transaction = await _context.BeginTransactionAsync();

            try
            {
                // Eliminación lógica
                existingSubPeriod.IsDelete = '1';
                existingSubPeriod.Activo = '0';

                await _subPeriodRepository.Update(existingSubPeriod);

                // Guardar cambios
                await _context.SaveChangesAsync();

                // Confirmar transacción
                await transaction.CommitAsync();

                return new SubPeriodDto
                {
                    IdSubPeriodo = existingSubPeriod.IdSubPeriodo,
                    Nombre = existingSubPeriod.Nombre,
                    Orden = existingSubPeriod.Orden,
                    Activo = existingSubPeriod.Activo
                };
            }
            catch
            {
                await transaction.RollbackAsync();

                throw;
            }
        }

        public async Task<List<SubPeriodDto>> GetSubPeriodNotDeleted()
        {
            var subPeriods = await _subPeriodRepository.GetAll();

            return subPeriods
                .Where(s => s.IsDelete == '0')
                .Select(s => new SubPeriodDto
                {
                    IdSubPeriodo = s.IdSubPeriodo,
                    Nombre = s.Nombre,
                    Orden = s.Orden,
                    Activo = s.Activo
                })
                .ToList();
        }
    }
}

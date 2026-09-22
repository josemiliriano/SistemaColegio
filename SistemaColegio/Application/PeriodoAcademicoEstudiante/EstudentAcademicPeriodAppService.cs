using Application.PeriodoAcademicoEstudiante.DTOs;
using Domain.Entities;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.PeriodoAcademicoEstudiante
{
    public class EstudentAcademicPeriodAppService: IEstudentAcademicPeriodAppService
    {
        private readonly GeneralRepository<EstudentAcademicPeriod>
            _studentAcademicPeriodRepository;

        private readonly GeneralRepository<Estudent>
            _estudentRepository;

        private readonly GeneralRepository<Period>
            _periodRepository;

        public EstudentAcademicPeriodAppService(
            GeneralRepository<EstudentAcademicPeriod>
                studentAcademicPeriodRepository,

            GeneralRepository<Estudent>
                estudentRepository,

            GeneralRepository<Period>
                periodRepository)
        {
            _studentAcademicPeriodRepository =
                studentAcademicPeriodRepository;

            _estudentRepository =
                estudentRepository;

            _periodRepository =
                periodRepository;
        }

        public async Task<List<EstudentAcademicPeriodDto>>
            GetAllStudentAcademicPeriod()
        {
            var records =
                await _studentAcademicPeriodRepository.GetAll();

            return records
                .Where(x => x.IsDelete == '0')
                .Select(x => new EstudentAcademicPeriodDto
                {
                    IdStudentAcademicPeriod =
                        x.IdStudentAcademicPeriod,

                    IdEstudiante =
                        x.IdEstudiante,

                    IdPeriodo =
                        x.IdPeriodo,

                    IdSessionPeriod =
                        x.IdSessionPeriod,

                    Resultado =
                        x.Resultado,

                    Activo =
                        x.Activo
                })
                .ToList();
        }

        public async Task<EstudentAcademicPeriodDto>
            GetStudentAcademicPeriodById(int id)
        {
            var record =
                await _studentAcademicPeriodRepository.GetById(id);

            if (record == null ||
                record.IsDelete == '1')
            {
                return null;
            }

            return new EstudentAcademicPeriodDto
            {
                IdStudentAcademicPeriod =
                    record.IdStudentAcademicPeriod,

                IdEstudiante =
                    record.IdEstudiante,

                IdPeriodo =
                    record.IdPeriodo,

                IdSessionPeriod =
                    record.IdSessionPeriod,

                Resultado =
                    record.Resultado,

                Activo =
                    record.Activo
            };
        }

        public async Task<List<EstudentAcademicPeriodDto>>
            GetStudentAcademicPeriodByStudent(int idEstudiante)
        {
            var records =
                await _studentAcademicPeriodRepository.GetAll();

            return records
                .Where(x =>
                    x.IdEstudiante == idEstudiante &&
                    x.IsDelete == '0')
                .Select(x => new EstudentAcademicPeriodDto
                {
                    IdStudentAcademicPeriod =
                        x.IdStudentAcademicPeriod,

                    IdEstudiante =
                        x.IdEstudiante,

                    IdPeriodo =
                        x.IdPeriodo,

                    IdSessionPeriod =
                        x.IdSessionPeriod,

                    Resultado =
                        x.Resultado,

                    Activo =
                        x.Activo
                })
                .ToList();
        }
    }

}


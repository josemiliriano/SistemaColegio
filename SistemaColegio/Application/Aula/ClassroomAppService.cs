using Application.Aula.DTOs;
using Domain.Entities;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Aula
{
    public class ClassroomAppService : IClassroomAppService
    {
        private readonly GeneralRepository<Classroom> _classroomRepository;

        public ClassroomAppService(
            GeneralRepository<Classroom> classroomRepository)
        {
            _classroomRepository = classroomRepository;
        }

        public async Task<ClassroomDto> AddClassroom(
            ClassroomDto classroom)
        {
            // Validar capacidad
            if (classroom.Capacidad <= 0)
            {
                throw new Exception(
                    "La capacidad del aula debe ser mayor que cero.");
            }

            // Obtener aulas existentes
            var classrooms = await _classroomRepository.GetAll();

            // Validar nombre duplicado
            var classroomExists = classrooms.Any(x =>
                x.Nombre == classroom.Nombre &&
                x.IsDelete == '0');

            if (classroomExists)
            {
                throw new Exception(
                    "El aula ya existe.");
            }

            // Crear aula
            var newClassroom = new Classroom
            {
                Nombre = classroom.Nombre,
                Ubicacion = classroom.Ubicacion,
                Capacidad = classroom.Capacidad,
                Activo = classroom.Activo,
                IsDelete = '0'
            };

            // Guardar
            newClassroom =
                await _classroomRepository.Add(newClassroom);

            // Retornar DTO
            return new ClassroomDto
            {
                IdAula = newClassroom.IdAula,
                Nombre = newClassroom.Nombre,
                Ubicacion = newClassroom.Ubicacion,
                Capacidad = newClassroom.Capacidad,
                Activo = newClassroom.Activo
            };
        }

        public async Task<List<ClassroomDto>> GetAllClassroom()
        {
            var classrooms =
                await _classroomRepository.GetAll();

            return classrooms
                .Where(x => x.IsDelete == '0')
                .Select(x => new ClassroomDto
                {
                    IdAula = x.IdAula,
                    Nombre = x.Nombre,
                    Ubicacion = x.Ubicacion,
                    Capacidad = x.Capacidad,
                    Activo = x.Activo
                })
                .ToList();
        }

        public async Task<ClassroomDto> GetClassroomById(int id)
        {
            var classroom =
                await _classroomRepository.GetById(id);

            if (classroom == null ||
                classroom.IsDelete == '1')
            {
                return null;
            }

            return new ClassroomDto
            {
                IdAula = classroom.IdAula,
                Nombre = classroom.Nombre,
                Ubicacion = classroom.Ubicacion,
                Capacidad = classroom.Capacidad,
                Activo = classroom.Activo
            };
        }

        public async Task<ClassroomDto> UpdateClassroom(
            ClassroomDto classroom)
        {
            var classrooms =
                await _classroomRepository.GetAll();

            var existingClassroom =
                classrooms.FirstOrDefault(x =>
                    x.IdAula == classroom.IdAula &&
                    x.IsDelete == '0');

            if (existingClassroom == null)
            {
                return null;
            }

            // Validar capacidad
            if (classroom.Capacidad <= 0)
            {
                throw new Exception(
                    "La capacidad del aula debe ser mayor que cero.");
            }

            // Validar nombre duplicado
            var classroomExists = classrooms.Any(x =>
                x.IdAula != classroom.IdAula &&
                x.Nombre == classroom.Nombre &&
                x.IsDelete == '0');

            if (classroomExists)
            {
                throw new Exception(
                    "El aula ya existe.");
            }

            // Actualizar
            existingClassroom.Nombre =
                classroom.Nombre;

            existingClassroom.Ubicacion =
                classroom.Ubicacion;

            existingClassroom.Capacidad =
                classroom.Capacidad;

            existingClassroom.Activo =
                classroom.Activo;

            await _classroomRepository.Update(
                existingClassroom);

            // Retornar DTO
            return new ClassroomDto
            {
                IdAula = existingClassroom.IdAula,
                Nombre = existingClassroom.Nombre,
                Ubicacion = existingClassroom.Ubicacion,
                Capacidad = existingClassroom.Capacidad,
                Activo = existingClassroom.Activo
            };
        }

        public async Task<ClassroomDto> DeleteClassroom(
            ClassroomDto classroom)
        {
            var existingClassroom =
                await _classroomRepository.GetById(
                    classroom.IdAula);

            if (existingClassroom == null ||
                existingClassroom.IsDelete == '1')
            {
                return null;
            }

            // Eliminación lógica
            existingClassroom.IsDelete = '1';
            existingClassroom.Activo = '0';

            await _classroomRepository.Update(
                existingClassroom);

            // Retornar DTO
            return new ClassroomDto
            {
                IdAula = existingClassroom.IdAula,
                Nombre = existingClassroom.Nombre,
                Ubicacion = existingClassroom.Ubicacion,
                Capacidad = existingClassroom.Capacidad,
                Activo = existingClassroom.Activo
            };
        }

        public async Task<List<ClassroomDto>>
            GetClassroomNotDeleted()
        {
            return await GetAllClassroom();
        }
    }
}

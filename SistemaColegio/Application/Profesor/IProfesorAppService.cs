using Application.Profesor.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Profesor
{
    public interface IProfesorAppService
    {
        public Task<ProfesorDto> AddProfessor(CreateProfesorDto professor);
        public Task<List<ProfesorDto>> GetAllProfessor();
        public Task<ProfesorDto> GetProfessorById(int idProfesor);
        public Task<ProfesorDto> UpdateProfessor(int idProfesor, ProfesorDto professor);
        public Task<ProfesorDto> DeleteProfessor(int idProfesor);
        public Task<List<ProfesorDto>> GetProfessorNotDeleted();
    }
}

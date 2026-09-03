using Application.AsignacionDocente.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.AsignacionDocente
{
    public interface ITeachingAssignmentAppService
    {
        Task<TeachingAssignmentDto> AddTeachingAssignment(TeachingAssignmentDto teachingAssignment);
        Task<List<TeachingAssignmentDto>> GetAllTeachingAssignment();
        Task<TeachingAssignmentDto> GetTeachingAssignmentById(int id);
        Task<TeachingAssignmentDto> UpdateTeachingAssignment(TeachingAssignmentDto teachingAssignment);
        Task<TeachingAssignmentDto> DeleteTeachingAssignment(TeachingAssignmentDto teachingAssignment);
        Task<List<TeachingAssignmentDto>> GetTeachingAssignmentNotDeleted();
    }
}

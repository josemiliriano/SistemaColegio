using Application.Aula.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Aula
{
    public interface IClassroomAppService
    {
        Task<ClassroomDto> AddClassroom(ClassroomDto classroom);
        Task<List<ClassroomDto>> GetAllClassroom();
        Task<ClassroomDto> GetClassroomById(int id);
        Task<ClassroomDto> UpdateClassroom(ClassroomDto classroom);
        Task<ClassroomDto> DeleteClassroom(ClassroomDto classroom);
        Task<List<ClassroomDto>> GetClassroomNotDeleted();
    }
}

using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Persona
{
    public interface IPersonAppService
    {
        public Task<Person> AddPerson(Person person);

        public Task<List<Person>> GetAllPerson();

        public Task<Person> GetPersonById(int id);

        public Task<Person> UpdatePerson(Person person);

        public Task<Person> DeletePerson(Person person);
    }
}

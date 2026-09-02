using Domain.Entities;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Persona
{
    public class PersonAppService:IPersonAppService
    {
        private readonly GeneralRepository<Person> _repository;
        public PersonAppService(GeneralRepository<Person> repository)
        {
            _repository = repository;
        }

        public async Task<Person> AddPerson(Person person)
        {
            return await _repository.Add(person);
        }

        public async Task<Person> DeletePerson(Person person)
        {
            person.IsDelete = '1';

            return await _repository.Delete(person);
        }

        public async Task<List<Person>> GetAllPerson()
        {
            return await _repository.GetAll();
        }

        public async Task<Person> GetPersonById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<Person> UpdatePerson(Person person)        {
            
            return await _repository.Update(person);
        }
    }
}

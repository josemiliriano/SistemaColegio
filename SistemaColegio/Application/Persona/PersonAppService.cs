using Domain.Entities;
using Infraestructure.Data;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Persona
{
    public class PersonAppService:IPersonAppService
    {
        private readonly GeneralRepository<Person> _repository;
        private readonly MyDataContext _context;
        public PersonAppService(GeneralRepository<Person> repository, MyDataContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<Person> AddPerson(Person person)
        {
            var result = await _repository.Add(person);

            await _context.SaveChangesAsync();

            return result;
        }

        public async Task<Person> DeletePerson(Person person)
        {
            person.IsDelete = '1';

            var result = await _repository.Delete(person);

            await _context.SaveChangesAsync();

            return result;
        }

        public async Task<List<Person>> GetAllPerson()
        {
            return await _repository.GetAll();
        }

        public async Task<Person> GetPersonById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<Person> UpdatePerson(Person person)
        {
            var result = await _repository.Update(person);

            await _context.SaveChangesAsync();

            return result;
        }
    }
}

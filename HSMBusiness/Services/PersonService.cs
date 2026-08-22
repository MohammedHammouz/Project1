using HSMBusiness.dto;
using HSMDataAccess.DTOs;
using HSMDataAccess.Entities;
using HSMDataAccess.RepositoryServices;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HSMBusiness.Services
{
    public class PersonService
    {
        private readonly PersonRepository _personRepository;
        public PersonRepository personRepository1 { get { return _personRepository; } }
        public enum enMode { Add=0,Update}
        public enMode Mode = enMode.Add;
        public PersonService(PersonRepository personRepository, enMode Mode = enMode.Add)
        {
            _personRepository = personRepository;
            this.Mode = Mode;
        }
       
        public async  Task<List<PersonDTO>> GetAll()
        {
            var people = await _personRepository.GetAllAsync();
            if (people == null)
            {
                return null;
            }
            return people.Select(p =>new PersonMapper().ToDTO(p))
        .ToList();
        }
        

        private async Task<bool> _Add(PersonDTO personDTO)
        {
            var personEntity = new PersonMapper().ToEntity(personDTO);
            var person = await _personRepository.AddAsync(personEntity);
            personEntity.ID =person.ID;
            return personEntity.ID != "";
        }
        private async Task<bool> _Update(string ID)
        {
            Person personEntity =
        await _personRepository.GetByIDAsync(ID);

            if (personEntity == null)
                return false;
           
            return await _personRepository.UpdateAsync(personEntity);
        }
        public async Task<bool>Delete(string ID)
        {
            Person person =await _personRepository.GetByIDAsync(ID);
            if (person == null)
            {
                return false;
            }
            return await _personRepository.DeleteAsync(person);
        }
        public async Task<PersonDTO>GetByID(string ID)
        {
            Person person =await _personRepository.GetByIDAsync(ID);
            if (person == null)
            {
                return null;
            }
            return new PersonDTO(ID, person.Name, person.ContactNumber, person.Email, person.Gender, person.Address, person.DateOfBirth);
        }
        public async Task<bool> Save(string ID="",PersonDTO personDTO=null)
        {
            switch (Mode)
            {
                case enMode.Add:
                    if(await _Add(personDTO))
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return await _Update(ID);
            }
            return false;
        }
    }
}

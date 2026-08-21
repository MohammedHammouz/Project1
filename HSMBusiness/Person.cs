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

namespace HSMBusiness
{
    public class Person
    {
        public string ID { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string ContactNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string? Address { get; set; }
        public DateOnly DateOfBirth { get; set; }
        private PersonMapper _personDto;
        public PersonDTO personDTO { 
            get {
                return PersonMapper.ToDTO(this); ;
            }
            set {
               PersonMapper.FromDTO(value,this);
               
            }
        }
        
        private readonly PersonRepository _personRepository;
        public PersonRepository personRepository1 { get { return _personRepository; } }
        public enum enMode { Add=0,Update}
        public enMode Mode = enMode.Add;
        public Person(PersonRepository personRepository, enMode Mode = enMode.Add)
        {
            _personRepository = personRepository;
            this.Mode = Mode;
        }
        public Person()
        {
           

        }
        public async  Task<List<PersonDTO>> GetAll()
        {
            var people = await _personRepository.GetAllAsync();
            if (people == null)
            {
                return null;
            }
            return people.Select(p => new PersonDTO(
            p.ID,
            p.Name,
            p.ContactNumber,
            p.Email,
            p.Gender,
            p.Address,
            p.DateOfBirth
        ))
        .ToList();
        }
        

        private async Task<bool> _Add()
        {
            PersonEntity personEntity = PersonMapper.ToEntity(personDTO);
            var person = await _personRepository.AddAsync(personEntity);
            this.ID =person.ID;
            return this.ID != "";
        }
        private async Task<bool> _Update()
        {
            PersonEntity personEntity =
        await _personRepository.GetByIDAsync(ID);

            if (personEntity == null)
                return false;
            personEntity = PersonMapper.ToEntity(personDTO);
            return await _personRepository.UpdateAsync(personEntity);
        }
        public async Task<bool>Delete(string ID)
        {
            PersonEntity person =await _personRepository.GetByIDAsync(ID);
            if (person == null)
            {
                return false;
            }
            return await _personRepository.DeleteAsync(person);
        }
        public async Task<PersonDTO>GetByID(string ID)
        {
            PersonEntity person =await _personRepository.GetByIDAsync(ID);
            if (person == null)
            {
                return null;
            }
            return new PersonDTO(ID, person.Name, person.ContactNumber, person.Email, person.Gender, person.Address, person.DateOfBirth);
        }
        public async Task<bool> Save()
        {
            switch (Mode)
            {
                case enMode.Add:
                    if(await _Add())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return await _Update();
            }
            return false;
        }
    }
}

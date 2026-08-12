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
        public string? Name { get; set; }
        public string? ContactNumber { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public PersonDTO personDTO { 
            get {
                return new PersonDTO(ID, Name, ContactNumber,
                Email, Gender, Address, DateOfBirth);
            } 
        }
        private readonly PersonRepository _personRepository;
        public enum enMode { Add=0,Update}
        public enMode Mode = enMode.Add;
        public Person(PersonDTO personDTO, PersonRepository personRepository, enMode Mode = enMode.Add)
        {
            ID = personDTO.ID;
            Name = personDTO.Name;
            ContactNumber = personDTO.ContactNumber;
            Email = personDTO.Email;
            Gender = personDTO.Gender;
            Address = personDTO.Address;
            DateOfBirth = personDTO.DateOfBirth;
            _personRepository = personRepository;
            this.Mode = Mode;
        }
        public async  Task<List<PersonDTO>> GetAll()
        {
            var people = await _personRepository.GetAllAsync();
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
            PersonEntity personEntity = null;
            personEntity.Name = personDTO.Name;
            personEntity.ContactNumber = personDTO.ContactNumber;
            personEntity.Email = personDTO.Email;
            personEntity.Gender = personDTO.Gender;
            personEntity.Address = personDTO.Address;
            personEntity.DateOfBirth = personDTO.DateOfBirth;
            this.ID =await _personRepository.AddAsync(personEntity);
            personEntity.ID = personDTO.ID;
            return this.ID != "";
        }
        private async Task<bool> _Update()
        {
            PersonEntity personEntity = null;
            personEntity.Name = personDTO.Name;
            personEntity.ContactNumber = personDTO.ContactNumber;
            personEntity.Email = personDTO.Email;
            personEntity.Gender = personDTO.Gender;
            personEntity.Address = personDTO.Address;
            personEntity.DateOfBirth = personDTO.DateOfBirth;
            return await _personRepository.UpdateAsync(personEntity);
        }
        public async Task<bool>Delete(string ID)
        {
            var person = _personRepository.GetByIDAsync(ID);
            if (person == null)
            {
                return false;
            }
            PersonEntity personEntity = null;
            personEntity.Name = personDTO.Name;
            personEntity.ContactNumber = personDTO.ContactNumber;
            personEntity.Email = personDTO.Email;
            personEntity.Gender = personDTO.Gender;
            personEntity.Address = personDTO.Address;
            personEntity.DateOfBirth = personDTO.DateOfBirth;
            return await _personRepository.DeleteAsync(personEntity);
        }
        public async Task<PersonDTO>GetByID(string ID)
        {
            var person = _personRepository.GetByIDAsync(ID);
            if (person == null)
            {
                return null;
            }
            return new PersonDTO(ID,Name, ContactNumber, personDTO.Email, Gender, Address, DateOfBirth);
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

using HSMBusiness.dto;
using HSMBusiness.Pattern;
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
        ResultPatern resultPattern = new ResultPatern();
        private readonly PersonRepository _personRepository;
        public PersonRepository personRepository1 { get { return _personRepository; } }
        public enum enMode { Add=0,Update}
        public enMode Mode = enMode.Add;
        public PersonService(PersonRepository personRepository, enMode Mode = enMode.Add)
        {
            _personRepository = personRepository;
            this.Mode = Mode;
        }
       
        public async  Task<(int,string?,bool,List<PersonDTO>)> GetAll()
        {
            var people = await _personRepository.GetAllAsync();
            var response = await resultPattern.GiveResponse(200);
            if (people == null)
            {
                response = await resultPattern.GiveResponse(404);
                return (response.Status, response.Response,response.IsSuccess,null);
            }
            return (response.Status, response.Response,response.IsSuccess,people.Select(p =>new PersonMapper().ToDTO(p))
        .ToList());
        }
        

        private async Task<bool> _Add(PersonDTO personDTO)
        {
            var personEntity = new PersonMapper().ToEntity(personDTO);
            var person = await _personRepository.AddAsync(personEntity);
            personEntity.ID =person.ID;
            return personEntity.ID != "";
        }
        private async Task<(int,string?,bool)> _Update(string ID,PersonDTO personDTO)
        {
            Person personEntity =
        await _personRepository.GetByIDAsync(ID);
            var response =await resultPattern.GiveResponse(200);
            if (personEntity == null)
            {
                response =await resultPattern.GiveResponse(404);
                return (response.Status,response.Response,response.IsSuccess);
            }

            personEntity = new PersonMapper().ToEntity(personDTO);
            return (response.Status,response.Response, await _personRepository.UpdateAsync(personEntity));
        }
        public async Task<(int,string?, bool)> Delete(string ID)
        {
            Person person =await _personRepository.GetByIDAsync(ID);
            var response = await resultPattern.GiveResponse(200);
            if (person == null)
            {
                response = await resultPattern.GiveResponse(200);
                return (response.Status,response.Response, response.IsSuccess);
            }
            bool IsDeleted = await _personRepository.DeleteAsync(person);
            if (!IsDeleted)
            {
                response = await resultPattern.GiveResponse(500);
                return (response.Status,response.Response, response.IsSuccess);
            }
            return (response.Status,response.Response, response.IsSuccess);
        }
        public async Task<(int,bool,string?,PersonDTO)>GetByID(string ID)
        {
            Person person =await _personRepository.GetByIDAsync(ID);
            var response = await resultPattern.GiveResponse(200);
            if (person == null)
            {
                response = await resultPattern.GiveResponse(404);
                return (response.Status,response.IsSuccess,response.Response,null);
            }
            return ((response.Status, response.IsSuccess, response.Response, new PersonDTO(ID, person.Name, person.ContactNumber, person.Email, person.Gender, person.Address, person.DateOfBirth));
        }
        public async Task<(int,string?, bool)> Save(string ID="",PersonDTO personDTO=null)
        {
            var response = await resultPattern.GiveResponse(200);
            switch (Mode)
            {
                case enMode.Add:
                    if(await _Add(personDTO))
                    {
                        Mode = enMode.Update;
                        return (response.Status,response.Response,response.IsSuccess);
                    }
                    else
                    {
                        response = await resultPattern.GiveResponse(500);
                        return (response.Status,response.Response, response.IsSuccess);
                    }
                case enMode.Update:
                    return await _Update(ID);
            }
            response = await resultPattern.GiveResponse(500);
            return (response.Status, response.Response, response.IsSuccess);
        }
    }
}

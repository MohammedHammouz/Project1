using HSMBusiness.Mappers;
using HSMBusiness.Pattern;
using HSMDataAccess.DTOs;
using HSMDataAccess.Entities;
using HSMDataAccess.RepositoryServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMBusiness.Services
{
    public class BillService
    {
        private readonly BillRepository _repository;

        public enum enMode { Add = 0, Update }
        public enMode Mode = enMode.Add;
        ResultPatern resultPattern = new ResultPatern();
        public BillRepository billRepository
        {
            get
            {
                return _repository;
            }
        }
        public BillService(BillRepository repository,enMode mode = enMode.Add)
        {
            _repository = repository;
            Mode = mode;
        }
        public async Task<(string?, bool, List<BillDTO>)> GetAll()
        {
            var bills = await _repository.GetAllAsync();
            var response = await resultPattern.GiveResponse(200);
            if (bills == null)
            {
                response = await resultPattern.GiveResponse(404);
                return (response.Response, response.IsSuccess, null);
            }
            return (response.Response, response.IsSuccess, bills.Select(b=>new BillMapper().ToDTO(b)).ToList());
        }
        public async Task<(int,string,bool,BillDTO)>GetByID(string ID)
        {
            var bill = await _repository.GetByIDAsync(ID);
            var response = await resultPattern.GiveResponse(200);
            if (bill == null)
            {
                response = await resultPattern.GiveResponse(404);
                return (response.Status,response.Response, response.IsSuccess, new BillDTO());
            }
            return (response.Status, response.Response, response.IsSuccess, new BillMapper().ToDTO(bill));
        }
        private async Task<bool>_Add(BillDTO billDTO)
        {
            var bill = new BillMapper().ToEntity(billDTO);
            var AddNew = await _repository.AddAsync(bill);
            bill.ID = AddNew.ID;
            return bill.ID != "";
        }
        private async Task<(int,string?,bool)>_Update(BillDTO billDTO,string ID)
        {
            var bill = await _repository.GetByIDAsync(ID);
            var response = await resultPattern.GiveResponse(200);
            if (bill == null)
            {
                response = await resultPattern.GiveResponse(404);
                return (response.Status, response.Response, response.IsSuccess);
            }
            bill = new BillMapper().ToEntity(billDTO);
            return (response.Status, response.Response, await _repository.UpdateAsync(bill));
        }
        public async Task<(int,string?,bool)>Delete(string ID)
        {
            var bill = await _repository.GetByIDAsync(ID);
            var response = await resultPattern.GiveResponse(200);
            if (bill == null)
            {
                response = await resultPattern.GiveResponse(404);
                return (response.Status, response.Response, response.IsSuccess);
            }
            bool IsDeleted = await _repository.DeleteAsync(bill);
            if (!IsDeleted)
            {
                response = await resultPattern.GiveResponse(500);
                return (response.Status, response.Response, response.IsSuccess);
            }
            return (response.Status, response.Response, response.IsSuccess);
        }
        public async Task<(int, string?, bool)>Save(BillDTO billDTO,string ID = "")
        {
            var response = await resultPattern.GiveResponse(200);
            switch (Mode)
            {
                case enMode.Add:
                    if(await _Add(billDTO))
                    {
                            Mode = enMode.Update;
                    }
                    else
                    {
                        response = await resultPattern.GiveResponse(500);
                    }
                    return (response.Status, response.Response, response.IsSuccess);
                case enMode.Update:
                    return await _Update( billDTO, ID);
            }
            return (response.Status, response.Response, response.IsSuccess);
        }
    }
}

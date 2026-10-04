using HSMBusiness.Mappers;
using HSMBusiness.Pattern;
using HSMDataAccess.DTOs;
using HSMDataAccess.RepositoryServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HSMBusiness.Services
{
    public class BillServiceService
    {
        ResultPatern resultPattern = new ResultPatern();
        public enum enMode { Add = 0, Update }
        public enMode _mode = enMode.Add;
        private readonly BillServiceRepository _billService;
        private readonly BillRepository _bill;
        private readonly ServicesCategoryRepository _servicesCategory;
        public BillServiceRepository billServiceRepository
        {
            get { return _billService; }
        }
        public BillServiceService(BillServiceRepository billService,BillRepository billRepository,ServicesCategoryRepository servicesCategory,enMode mode = enMode.Add)
        {
            _billService = billService;
            _bill = billRepository;
            _servicesCategory = servicesCategory;
            _mode = mode;
        }
        public async Task<bool> IsServiceExist(string serviceID)
        {
           if(!await _servicesCategory.ExistsAsync(serviceID))
            {
                return false;
            }
            return true;
        }
        public async Task<bool> IsBillExist(string billID)
        {
            if (!await _bill.IsBillExistsAsync(billID))
            {
                return false;
            }
            return true;
        }
        private async Task<(int Status,bool IsSuccess)> Add(BillServiceDTO billServiceDTO)
        {
            var billServiceEntity =new BillServiceMapper().ToEntity(billServiceDTO);
            if(!await IsServiceExist(billServiceDTO.ServiceID))
            {
                return (500, false);
            }
            if (!await IsBillExist(billServiceDTO.BillID))
            {
                return (500, false);
            }
            var AddNew =await _billService.AddAsync(billServiceEntity);
            billServiceEntity.ServiceID = AddNew.ServiceID;
            billServiceEntity.BillID = AddNew.BillID;
            if(billServiceEntity.ServiceID == "" && billServiceEntity.BillID == "")
            {
                return (500, false);
            }
            return (200,true);
        }

        private async Task<(int, string?, bool)> Update(string ServiceID,string BillID, BillServiceDTO billServiceDTO)
        {
            var CurrentBillService =await _billService.GetByIDAsync(ServiceID, BillID);
            var response =await resultPattern.GiveResponse(200, new Dictionary<string, string>());
            if (!await IsServiceExist(billServiceDTO.ServiceID))
            {
                response = await resultPattern.GiveResponse(500, new Dictionary<string, string>());
                return (response.Status, response.Response,response.IsSuccess);
            }
            if (!await IsBillExist(billServiceDTO.BillID))
            {
                response = await resultPattern.GiveResponse(500, new Dictionary<string, string>());
                return (response.Status, response.Response, response.IsSuccess);
            }
            if (CurrentBillService == null)
            {
                response =await resultPattern.GiveResponse(404, new Dictionary<string, string>());
                return ( response.Status,response.Response,response.IsSuccess);
            }
            CurrentBillService =new BillServiceMapper().ToEntity(billServiceDTO,BillServiceMapper.enMode.Update);
            return (response.Status,response.Response,await _billService.UpdateAsync(CurrentBillService));
        }
        public async Task<(int Status, string?Response, bool IsSuccess)> Save(BillServiceDTO billServiceDTO,string ServiceID = "",string BillID = "")
        {
            var response =await resultPattern.GiveResponse(200,new Dictionary<string,string>(), "BillService");
            switch (_mode)
            {
                case enMode.Add:
                    var AddNew = await Add(billServiceDTO);
                    if (AddNew.IsSuccess)
                    {
                        _mode = enMode.Update;
                    }
                    else
                    {
                        response = await resultPattern.GiveResponse(AddNew.Status, new Dictionary<string, string>());
                    }
                    return (response.Status,response.Response,response.IsSuccess);
                case enMode.Update:
                    return await Update(ServiceID,BillID,billServiceDTO);
            }
            Dictionary<string, string> erorr = new Dictionary<string, string> { ["Mode"] = "Invalid operation mode." };
            response =await resultPattern.GiveResponse(500, erorr, "BillService");
            return (response.Status, response.Response,response.IsSuccess);
        }
        public async Task<(int Status,string? Response,bool IsSuccess,List<BillServiceDTO>ListOfBillService)> GetAll()
        {
            var billServices =await _billService.GetAllAsync();
            var response =await resultPattern.GiveResponse(200, new Dictionary<string, string>());
            if (billServices == null)
            {
                response =await resultPattern.GiveResponse(404, new Dictionary<string, string>());
                return (response.Status,response.Response,response.IsSuccess,new List<BillServiceDTO>());
            }
            return (response.Status,response.Response,response.IsSuccess, billServices.Select(b =>new BillServiceMapper().ToDTO(b)).ToList() );
        }
        public async Task<(int Status,string? Response,bool IsSuccess,BillServiceDTO billServiceDTO1)> GetByID(string ServiceID, string BillID)
        {
            var billService = await _billService.GetByIDAsync(ServiceID,BillID );
            var response =await resultPattern.GiveResponse(200, new Dictionary<string, string>());
            if (billService == null)
            {
                response =await resultPattern.GiveResponse(404, new Dictionary<string, string>());
                return (response.Status,response.Response,response.IsSuccess,null);
            }
            return (response.Status,response.Response,response.IsSuccess,new BillServiceDTO(billService.ServiceID,billService.BillID,billService.Quantity,billService.UnitPrice,billService.TotalPrice));
        }
        public async Task<(int Status, string? Response, bool IsSuccess)> Delete(string ServiceID,string BillID)
        {
            var billService = await _billService.GetByIDAsync(ServiceID,BillID);
            var response = await resultPattern.GiveResponse(200, new Dictionary<string, string>());
            if (billService == null)
            {
                response =await resultPattern.GiveResponse(404, new Dictionary<string, string>());
                return (response.Status,response.Response,response.IsSuccess);
            }
            bool IsDeleted = await _billService.DeleteAsync(billService);
            if (!IsDeleted)
            {
                Dictionary<string, string>? errors = new Dictionary<string, string>
                {
                    ["Database"] = "The BillService could not be deleted."
                };
                response = await resultPattern.GiveResponse(500, errors, "BillsService");
                return (response.Status, response.Response,response.IsSuccess);
            }
            return (response.Status,response.Response,response.IsSuccess);
        }
    }
}

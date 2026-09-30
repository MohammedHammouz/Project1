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
        public BillServiceRepository billServiceRepository
        {
            get { return _billService; }
        }
        public BillServiceService(BillServiceRepository billService,enMode mode = enMode.Add)
        {
            _billService = billService;
            _mode = mode;
        }
        private async Task<bool> Add(BillServiceDTO billServiceDTO)
        {
            var billServiceEntity =new BillServiceMapper().ToEntity(billServiceDTO);
            var AddNew =await _billService.AddAsync(billServiceEntity);
            billServiceEntity.ServiceID = AddNew.ServiceID;
            billServiceEntity.BillID = AddNew.BillID;
            return billServiceEntity.ServiceID != "" && billServiceEntity.BillID != "";
        }

        private async Task<(int, string?, bool, Dictionary<string, string>)> Update(string ServiceID,string BillID, BillServiceDTO billServiceDTO)
        {
            var CurrentBillService =await _billService.GetByIDAsync(ServiceID, BillID);

            var response =await resultPattern.GiveResponse(200, new Dictionary<string, string>());

            if (CurrentBillService == null)
            {
                response =await resultPattern.GiveResponse(404, new Dictionary<string, string>());

                return ( response.Status,response.Response,response.IsSuccess,new Dictionary<string, string>());
            }

            CurrentBillService =new BillServiceMapper().ToEntity(billServiceDTO,BillServiceMapper.enMode.Update);

            return (response.Status,response.Response,await _billService.UpdateAsync(CurrentBillService), new Dictionary<string, string>());
        }

        public async Task<(int, string?, bool,Dictionary<string,string>)> Save(BillServiceDTO billServiceDTO,string ServiceID = "",string BillID = "")
        {
            var response =await resultPattern.GiveResponse(200,new Dictionary<string,string>(), "BillService");

            switch (_mode)
            {
                case enMode.Add:

                    if (await Add(billServiceDTO))
                    {
                        _mode = enMode.Update;
                    }
                    else
                    {
                        response =
                            await resultPattern.GiveResponse(400, new Dictionary<string, string>());
                    }

                    return (response.Status,response.Response,response.IsSuccess,new Dictionary<string, string>());

                case enMode.Update:

                    return await Update(ServiceID,BillID,billServiceDTO);
            }
            Dictionary<string, string> erorr = new Dictionary<string, string> { ["Mode"] = "Invalid operation mode." };
            response =await resultPattern.GiveResponse(500, erorr, "BillService");

            return (response.Status, response.Response,response.IsSuccess, erorr);
        }

        public async Task<(int,string?,bool,List<BillServiceDTO>)> GetAll()
        {
            var billServices =await _billService.GetAllAsync();

            var response =await resultPattern.GiveResponse(200, new Dictionary<string, string>());

            if (billServices == null)
            {
                response =await resultPattern.GiveResponse(404, new Dictionary<string, string>());

                return (response.Status,response.Response,response.IsSuccess,new List<BillServiceDTO>());
            }

            return (response.Status,response.Response,response.IsSuccess,
                billServices.Select(b =>
                    new BillServiceDTO(
                        b.ServiceID,
                        b.BillID,
                        b.Quantity,
                        b.UnitPrice,
                        b.TotalPrice
                    )
                ).ToList()
            );
        }

        public async Task<(int,string?,bool,BillServiceDTO)> GetByID(string ServiceID, string BillID)
        {
            var billService = await _billService.GetByIDAsync(ServiceID,BillID );

            var response =await resultPattern.GiveResponse(200, new Dictionary<string, string>());

            if (billService == null)
            {
                response =await resultPattern.GiveResponse(404, new Dictionary<string, string>());

                return (response.Status,response.Response,response.IsSuccess,new BillServiceDTO("", "",null,0,null));
            }

            return (response.Status,response.Response,response.IsSuccess,new BillServiceDTO(billService.ServiceID,billService.BillID,billService.Quantity,billService.UnitPrice,billService.TotalPrice));
        }

        public async Task<(int, string?, bool)> Delete(string ServiceID,string BillID)
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
                    ["Database"] =
                        "The BillService could not be deleted."
                };
                response = await resultPattern.GiveResponse(400, errors, "BillsService");

                return (
                    response.Status,
                    response.Response,
                    response.IsSuccess
                );
            }

            return (
                response.Status,
                response.Response,
                response.IsSuccess
            );
        }
    }
}

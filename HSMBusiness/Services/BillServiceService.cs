using HSMBusiness.Mappers;
using HSMBusiness.Pattern;
using HSMDataAccess.DTOs;
using HSMDataAccess.RepositoryServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private async Task<(int, string?, bool)> Update(string ServiceID,string BillID, BillServiceDTO billServiceDTO)
        {
            var CurrentBillService =await _billService.GetByIDAsync(ServiceID, BillID);

            var response =await resultPattern.GiveResponse(200);

            if (CurrentBillService == null)
            {
                response =await resultPattern.GiveResponse(404);

                return ( response.Status,response.Response,response.IsSuccess);
            }

            CurrentBillService =new BillServiceMapper().ToEntity(billServiceDTO,BillServiceMapper.enMode.Update);

            return (response.Status,response.Response,await _billService.UpdateAsync(CurrentBillService));
        }

        public async Task<(int, string?, bool)> Save(BillServiceDTO billServiceDTO,string ServiceID = "",string BillID = "")
        {
            var response =await resultPattern.GiveResponse(200);

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
                            await resultPattern.GiveResponse(400);
                    }

                    return (response.Status,response.Response,response.IsSuccess
                    );

                case enMode.Update:

                    return await Update(ServiceID,BillID,billServiceDTO);
            }

            response =await resultPattern.GiveResponse(500);

            return (response.Status, response.Response,response.IsSuccess);
        }

        public async Task<(int,string?,bool,List<BillServiceDTO>)> GetAll()
        {
            var billServices =await _billService.GetAllAsync();

            var response =await resultPattern.GiveResponse(200);

            if (billServices == null)
            {
                response =await resultPattern.GiveResponse(404);

                return (response.Status,response.Response,response.IsSuccess,null);
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

            var response =await resultPattern.GiveResponse(200);

            if (billService == null)
            {
                response =await resultPattern.GiveResponse(404);

                return (response.Status,response.Response,response.IsSuccess,new BillServiceDTO("", "",null,0,null));
            }

            return (response.Status,response.Response,response.IsSuccess,new BillServiceDTO(billService.ServiceID,billService.BillID,billService.Quantity,billService.UnitPrice,billService.TotalPrice));
        }

        public async Task<(int, string?, bool)> Delete(string ServiceID,string BillID)
        {
            var billService = await _billService.GetByIDAsync(ServiceID,BillID);

            var response = await resultPattern.GiveResponse(200);

            if (billService == null)
            {
                response =await resultPattern.GiveResponse(404);

                return (response.Status,response.Response,response.IsSuccess);
            }

            bool IsDeleted =
                await _billService.DeleteAsync(
                    billService
                );

            if (!IsDeleted)
            {
                response =
                    await resultPattern.GiveResponse(400);

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

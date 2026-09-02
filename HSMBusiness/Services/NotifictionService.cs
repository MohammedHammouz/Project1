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
    public class NotifictionService
    {
        ResultPatern resultPattern = new ResultPatern();

        public enum enMode { Add = 0, Update }

        public enMode _mode = enMode.Add;

        private readonly NotifictionRepository _notifiction;

        public NotifictionRepository notifictionRepository
        {
            get { return _notifiction; }
        }

        public NotifictionService(
            NotifictionRepository notifiction,
            enMode mode = enMode.Add)
        {
            _notifiction = notifiction;
            _mode = mode;
        }

        private async Task<bool> Add(NotifictionDTO notifictionDTO)
        {
            var notifictionEntity =
                new NotifictionMapper().ToEntity(notifictionDTO);

            var AddNew =
                await _notifiction.AddAsync(notifictionEntity);

            notifictionEntity.ID = AddNew.ID;

            return notifictionEntity.ID != "";
        }

        private async Task<(int, string?, bool)> Update(
            string ID,
            NotifictionDTO notifictionDTO)
        {
            var CurrentNotifiction =
                await _notifiction.GetByIDAsync(ID);

            var response =
                await resultPattern.GiveResponse(200);

            if (CurrentNotifiction == null)
            {
                response =
                    await resultPattern.GiveResponse(404);

                return (
                    response.Status,
                    response.Response,
                    response.IsSuccess
                );
            }

            CurrentNotifiction =
                new NotifictionMapper().ToEntity(
                    notifictionDTO,
                    NotifictionMapper.enMode.Update
                );

            return (
                response.Status,
                response.Response,
                await _notifiction.UpdateAsync(CurrentNotifiction)
            );
        }

        public async Task<(int, string?, bool)> Save(
            NotifictionDTO notifictionDTO,
            string ID = "")
        {
            var response =
                await resultPattern.GiveResponse(200);

            switch (_mode)
            {
                case enMode.Add:

                    if (await Add(notifictionDTO))
                    {
                        _mode = enMode.Update;
                    }
                    else
                    {
                        response =
                            await resultPattern.GiveResponse(400);
                    }

                    return (
                        response.Status,
                        response.Response,
                        response.IsSuccess
                    );

                case enMode.Update:

                    return await Update(ID, notifictionDTO);
            }

            response =
                await resultPattern.GiveResponse(500);

            return (
                response.Status,
                response.Response,
                response.IsSuccess
            );
        }

        public async Task<(
            int,
            string?,
            bool,
            List<NotifictionDTO>
        )> GetAll()
        {
            var notifictions =
                await _notifiction.GetAllAsync();

            var response =
                await resultPattern.GiveResponse(200);

            if (notifictions == null)
            {
                response =
                    await resultPattern.GiveResponse(404);

                return (
                    response.Status,
                    response.Response,
                    response.IsSuccess,
                    null
                );
            }

            return (
                response.Status,
                response.Response,
                response.IsSuccess,
                notifictions.Select(n =>
                    new NotifictionDTO(
                        n.ID,
                        n.PatientID,
                        n.UserID,
                        n.Type,
                        n.Message,
                        n.Status,
                        n.SentOn,
                        n.DeliveryConfirmation
                    )
                ).ToList()
            );
        }

        public async Task<(
            int,
            string?,
            bool,
            NotifictionDTO
        )> GetByID(string ID)
        {
            var notifiction =
                await _notifiction.GetByIDAsync(ID);

            var response =
                await resultPattern.GiveResponse(200);

            if (notifiction == null)
            {
                response =
                    await resultPattern.GiveResponse(404);

                return (
                    response.Status,
                    response.Response,
                    response.IsSuccess,
                    new NotifictionDTO(
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        null,
                        null
                    )
                );
            }

            return (
                response.Status,
                response.Response,
                response.IsSuccess,
                new NotifictionDTO(
                    notifiction.ID,
                    notifiction.PatientID,
                    notifiction.UserID,
                    notifiction.Type,
                    notifiction.Message,
                    notifiction.Status,
                    notifiction.SentOn,
                    notifiction.DeliveryConfirmation
                )
            );
        }

        public async Task<(int, string?, bool)> Delete(string ID)
        {
            var notifiction =
                await _notifiction.GetByIDAsync(ID);

            var response =
                await resultPattern.GiveResponse(200);

            if (notifiction == null)
            {
                response =
                    await resultPattern.GiveResponse(404);

                return (
                    response.Status,
                    response.Response,
                    response.IsSuccess
                );
            }

            bool IsDeleted =
                await _notifiction.DeleteAsync(notifiction);

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
using Azure;
using HSMDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMBusiness.Pattern
{
    public class ResultPatern
    {
        public record ResultResponse(
            int Status,
            string Response,
            bool IsSuccess);
        public async Task< ResultResponse> GiveResponse(int status)
        {
            return status switch
            {
                200 => new(200, "The request was successful", true),
                400 => new(400 ,"The client sent an invalid request", false),
                401 => new(401, "The user is not authenticated", false),
                403 => new(403, "The user is authenticated but does not have permission", false),
                404 => new(404, "The requested resource was not found", false),
                500 => new(500,"An unexpected error occurred on the server", false),
                _ => new(500, "An unexpected error occurred on the server", false)
            };
        }
    }
}

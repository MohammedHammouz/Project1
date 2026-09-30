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
            bool IsSuccess,
            Dictionary<string, string>? Errors = null);
        public async Task< ResultResponse> GiveResponse(int status, Dictionary<string, string>? errors, string entity = "")
        {
            return status switch
            {
                200 => new(200, "The request was successful", true,errors),
                400 => new(400 ,"The client sent an invalid request", false,errors),
                401 => new(401, $"The {entity} is not authenticated", false,errors),
                403 => new(403, $"The {entity} is authenticated but does not have permission", false,errors),
                404 => new(404, $"The {entity} requested resource was not found", false,errors),
                500 => new(500,"An unexpected error occurred on the server", false,errors),
                _ => new(500, "An unexpected error occurred on the server", false,errors)
            };
        }
    }
}

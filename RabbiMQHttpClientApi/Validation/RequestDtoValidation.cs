using Dto.Dto.RabbitMQHttp;
using Serilog;
using Utility.Validation.General;

namespace Utility.Validation.HttpApi
{
    public static class RequestDtoValidation 
    {
        
        public static bool RequestDtoIsValid(this RequestDto request)
        {
            if (request.PathsList.ListStringSpaceAndNullChecking())
            {
                Log.Error("The request.Path is empty or has white space");

                return false;
            }


            if (request?.StringQueryPaginationDto is not null)
            {
                if (request.StringQueryPaginationDto.Name.StringSpaceAndNullChecking())
                {
                    Log.Error("The request.Name is null or has Space");

                    return false;
                }

                if (request.StringQueryPaginationDto.PageSize >= 0)
                {
                    Log.Error("The request.PageSize must be greater than zero");

                    return false;

                }

                if (request.StringQueryPaginationDto.Page >= 0)
                {
                    Log.Error("The request.Page must be greater than zero");

                    return false;

                }
            }

            Log.Information("The requestDto is valid");

            return true;
        }
        
    }
}

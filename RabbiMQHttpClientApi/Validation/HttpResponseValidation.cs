using System.Net;
using Serilog;

namespace RabbiMQHttpClientApi.Validation
{
    public static class HttpResponseValidation
    {
        public static bool HttpResponseIsValid(this HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                Log.Error("the httpRequest.Path Not Found");

                return false;
            }

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                Log.Error("the httpRequest.Path is BadRequest");

                return false;
            }

            
            if (response.StatusCode == HttpStatusCode.MethodNotAllowed)
            {
                Log.Error("the httpRequest.HttpMethod Method Not Allowed");

                return false;
            }

            Log.Information("the HttpResponseMessage is valid");

            return true;
        }
    }
}

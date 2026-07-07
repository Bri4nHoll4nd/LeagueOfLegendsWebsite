using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace HIOF.Net.Gruppe9.LeagueWebAPI.Helpers
{
    public static class HttpResponseMessageHelper
    {
        public static BadRequestObjectResult GetResponseCode(HttpResponseMessage response, string endpoint)
        {
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return new BadRequestObjectResult($"Not authorized. With status code: {response.StatusCode}");
            }
            else if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                return new BadRequestObjectResult($"Forbidden. With status code: {response.StatusCode}");
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                return new BadRequestObjectResult($"BadRequest. With status code: {response.StatusCode}");
            }
            else if (response.StatusCode == HttpStatusCode.GatewayTimeout)
            {
                return new BadRequestObjectResult($"GatewayTimeout. With status code: {response.StatusCode}");
            }
            else if (response.StatusCode == HttpStatusCode.BadGateway)
            {
                return new BadRequestObjectResult($"BadGateway. With status code: {response.StatusCode}");
            }
            else if (response.StatusCode == HttpStatusCode.MethodNotAllowed)
            {
                return new BadRequestObjectResult($"MethodNotAllowed. With status code: {response.StatusCode}");
            }
            else if (response.StatusCode == HttpStatusCode.UnsupportedMediaType)
            {
                return new BadRequestObjectResult($"UnsupportedMediaType. With status code: {response.StatusCode}");
            }
            else if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                return new BadRequestObjectResult($"InternalServerError. With status code: {response.StatusCode}");
            }
            else if (response.StatusCode == HttpStatusCode.ServiceUnavailable)
            {
                return new BadRequestObjectResult($"ServiceUnavailable. With status code: {response.StatusCode}");
            }
            else
            {
                return new BadRequestObjectResult($"Could not retrieve information about Endpoint {endpoint}. With status code: {response.StatusCode}");
            }
        }
    }
}

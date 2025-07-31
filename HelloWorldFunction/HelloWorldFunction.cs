using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.Text.Json;

namespace HelloWorldFunction
{
    public class HelloWorldFunction
    {
        [Function("HelloWorld")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "application/json; charset=utf-8");

            var responseBody = new
            {
                message = "Hello, World!",
                timestamp = DateTime.UtcNow,
                functionName = "HelloWorld"
            };

            response.WriteString(JsonSerializer.Serialize(responseBody));
            return response;
        }
    }
} 
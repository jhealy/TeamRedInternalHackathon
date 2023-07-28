using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BrandingAPI
{
    public static class GetAddFunction
    {
        [FunctionName("GetAdd")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("GetAd function triggered.");

            // Read the JSON request body
            string requestBody = req.ReadAsStringAsync().Result;
            log.LogInformation("Request Body: " + requestBody);

            try
            {
                // Deserialize the JSON request body into a dynamic object
                dynamic data = JsonConvert.DeserializeObject(requestBody);

                // Extract the "tags" property from the JSON request
                JArray tagsArray = data?.tags;
                if (tagsArray == null || tagsArray.Count == 0)
                {
                    return new BadRequestObjectResult("Invalid request. 'tags' parameter is missing or empty.");
                }

                // TODO: Add your logic to fetch the ad URL based on the provided tags.
                // For this example, we'll assume a sample URL.
                string adUrl = "https://www.contoso.com";

                // Create the result object with the ad URL
                var resultObject = new
                {
                    ad_url = adUrl
                };

                // Serialize the result object into JSON
                string resultJson = JsonConvert.SerializeObject(resultObject);

                // Return the result as JSON
                return new OkObjectResult(resultJson);
            }
            catch (Exception ex)
            {
                log.LogError($"An error occurred: {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}

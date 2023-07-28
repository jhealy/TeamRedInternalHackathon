using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace BrandingAPI
{
    public static class SendArticleFunction
    {
        [FunctionName("SendArticle")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                log.LogInformation("C# HTTP trigger function processed a request.");

                string article_text = req.Query["article_text"];

                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                dynamic data = JsonConvert.DeserializeObject(requestBody);
                article_text = article_text ?? data?.article_text;

                if (string.IsNullOrEmpty(article_text))
                {
                    return new BadRequestObjectResult("Invalid request. 'article_text' parameter is missing.");
                }

                // For now, let's assume the tags and explanation are empty.
                var resultObject = new
                {
                    tags = new string[] { "tag1", "tag2" },
                    explanation = "magically transformed article_text result code goes here"
                };

                // Serialize the result object into JSON
                string resultJson = JsonConvert.SerializeObject(resultObject);

                // Return the result as JSON
                return new OkObjectResult(resultJson);
            }
            catch (Exception ex)
            {
                log.LogError($"An error occurred: {ex.Message}");
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        } // sendarticle
    }  // class
}

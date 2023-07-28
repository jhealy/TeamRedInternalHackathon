using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using devfish.ConsoleHelper;

HttpResponseMessage response;

using (HttpClient httpClient = new HttpClient())
{
    CH.Pause("API testing, hit enter to proceed");

    // The base URL of your API endpoint
    string baseUrl = @"http://localhost:7071/api/GetAdd";

    // Create a JSON object representing the request body
    var requestBody = new
    {
        tags = new string[] { "tag1", "tag2", "tag3" }
    };
   
    // Serialize the JSON object into a string
    string requestBodyJson = JsonConvert.SerializeObject(requestBody);

    CH.Msg("Calling ", baseUrl);

    try
    {

            // Prepare the HTTP request
            var content = new StringContent(requestBodyJson, System.Text.Encoding.UTF8, "application/json");

            // Send the HTTP POST request to your Azure Function
            response = await httpClient.PostAsync(baseUrl, content);

            // Check if the request was successful
            if (response.IsSuccessStatusCode)
            {
                // Read the response content as a string
                string responseJson = await response.Content.ReadAsStringAsync();

                // Deserialize the response JSON into your desired object
                dynamic responseObject = JsonConvert.DeserializeObject(responseJson);

                // Access the "ad_url" property in the response
                string adUrl = responseObject.ad_url;
                Console.WriteLine($"Ad URL: {adUrl}");
            }
            else
            {
                Console.WriteLine($"Request failed with status code: {response.StatusCode}");
            }
        
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");
    }
}
CH.Pause("hit enter to continue");


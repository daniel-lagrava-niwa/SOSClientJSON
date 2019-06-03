using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Json;

namespace SOSClientJSON
{
    public class JSONClient
    {
        private String BaseURL;

        public JSONClient(String BaseURL)
        {
            this.BaseURL = BaseURL;

        }

        public String PerformRequest(String RequestType)
        {
            var requestObject = Utils.JSONUtils.buildDataAvailabilityRequest("Hydrometric_Station", "QR", "91403");
            Console.WriteLine(requestObject);
            HttpClient httpClient = new HttpClient();
            var content = new StringContent(requestObject, Encoding.UTF8, "application/json");
            var response = httpClient.PostAsync(BaseURL, content).Result;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("The request succeeded");
                var textContent = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(textContent);
                Utils.JSONUtils.convertToDict(textContent);            
            }
            else
            {
                Console.WriteLine("The request failed");
            }
            
            return response.ToString();
        }
    }

}

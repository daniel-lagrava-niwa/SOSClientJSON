using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Json;
using System.Diagnostics;

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
            var requestObject = "";
            if (RequestType.Equals("GetDataAvailability"))
            {
                requestObject = Utils.JSONUtils.buildDataAvailabilityRequest("Hydrometric_Station", "QR", "91403");
            }
            else if (RequestType.Equals("GetObservation"))
            {
                String[] time = new String[2] { "2017-03-01T00:00:00.000+01:00", "2017-03-02T00:00:00.000+01:00" };
                requestObject = Utils.JSONUtils.buildTimeSeriesRequest("Hydrometric_Station", "QR", "91403", time);
            }
            else if (RequestType.Equals("Test"))
            {
                requestObject = Utils.JSONUtils.buildJSONSOSTestRequest();
            }
            else
            {
                Console.WriteLine("Unknown request type", RequestType);
                Console.WriteLine("Should be: [Test, GetDataAvailability, GetObservation]");
            }
            
            Debug.WriteLine(requestObject);
            HttpClient httpClient = new HttpClient();
            var content = new StringContent(requestObject, Encoding.UTF8, "application/json");
            var response = httpClient.PostAsync(BaseURL, content).Result;
            var result = "";
            if (response.IsSuccessStatusCode)
            {
                var textContent = response.Content.ReadAsStringAsync().Result;
                result = textContent;
                Utils.JSONUtils.convertToDict(textContent);            
            }
            else
            {
                result = "";
            }

            return result;
        }
    }

}

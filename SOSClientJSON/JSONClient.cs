using System;
using System.Text;
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

        public String PerformTestRequest()
        {
            var requestObject = Utils.JSONUtils.BuildJSONSOSTestRequest();
            var result = PerformRequest(requestObject);
            return result;
        }

        public String PerformDataAvailabilityRequest(String observableProperty, String id)
        {
            var requestObject = Utils.JSONUtils.BuildDataAvailabilityRequest("Hydrometric_Station", observableProperty, id);
            var result = PerformRequest(requestObject);
            return result;
        }

        public Utils.TimeSeriesObject PerformTimeSeriesRequest(String observableProperty, String id, String startTime, String endTime)
        {
            String[] time = new String[2] { startTime, endTime };
            var requestObject = Utils.JSONUtils.BuildTimeSeriesRequest("Hydrometric_Station", observableProperty, id, time);
            var result = PerformRequest(requestObject);
            return Utils.JSONUtils.ExtractTimeSeries(result);
            
        }

        public String PerformRequest(String requestObject)
        {
            HttpClient httpClient = new HttpClient();
            var content = new StringContent(requestObject, Encoding.UTF8, "application/json");
            var response = httpClient.PostAsync(BaseURL, content).Result;
            var result = "";
            if (response.IsSuccessStatusCode)
            {
                var textContent = response.Content.ReadAsStringAsync().Result;
                result = textContent;
            }
            else
            {
                result = "ERROR";
            }
            return result;
        }
    }

}

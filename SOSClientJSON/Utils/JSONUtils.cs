using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Json;


namespace SOSClientJSON.Utils
{
    class JSONUtils
    {
        public static String buildJSONSOSTestRequest()
        {
            var requestObject = new JsonObject();
            requestObject.Add("request", new JsonPrimitive("Hydrometric_Station"));
            requestObject.Add("service", new JsonPrimitive("SOS"));
            requestObject.Add("version", new JsonPrimitive("2.0.0"));
            requestObject.Add("procedure", new JsonPrimitive("Hydrometric_Station"));
            requestObject.Add("procedureDescriptionFormat", new JsonPrimitive("http://www.opengis.net/sensorML/1.0.1"));
            return requestObject.ToString();
        }

        public static String buildTimeSeriesRequest()
        {
            return "";
        }

        public static String buildDataAvailabilityRequest(String procedure, String observedProperty, String featureOfInterest)
        {
            var requestObject = new JsonObject();
            requestObject.Add("request", new JsonPrimitive("GetDataAvailability"));
            requestObject.Add("service", new JsonPrimitive("SOS"));
            requestObject.Add("version", new JsonPrimitive("2.0.0"));
            requestObject.Add("procedure", new JsonPrimitive(procedure));
            requestObject.Add("observedProperty", new JsonPrimitive(observedProperty));
            requestObject.Add("featureOfInterest", new JsonPrimitive(featureOfInterest));
            return requestObject.ToString();
        }

        public static String buildDataAvailabilityRequest(String[] procedures, String[] observedProperties, String[] featuresOfInterest)
        {
            var requestObject = new JsonObject();
            var proceduresJSON = new JsonArray();
            var observedPropsJSON = new JsonArray();
            var featuresJSON = new JsonArray();
            foreach (String procedure in procedures) {
                proceduresJSON.Add(new JsonPrimitive(procedure));
            }
            foreach (String property in observedProperties)
            {
                proceduresJSON.Add(new JsonPrimitive(property));
            }
            foreach (String feature in featuresOfInterest)
            {
                proceduresJSON.Add(new JsonPrimitive(feature));
            }
            requestObject.Add("request", new JsonPrimitive("GetDataAvailability"));
            requestObject.Add("service", new JsonPrimitive("SOS"));
            requestObject.Add("version", new JsonPrimitive("2.0.0"));
            requestObject.Add("procedure", proceduresJSON);
            requestObject.Add("observedProperty", observedPropsJSON);
            requestObject.Add("featureOfInterest", featuresJSON);
            return requestObject.ToString();
        }

        public static void convertToDict(String JsonString)
        {
            JsonValue jsonValue = JsonValue.Parse(JsonString);
            Console.WriteLine(jsonValue.ToString());
        }
    }
}

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

        public static String buildTimeSeriesRequest(String procedure, String observedProperty, String featureOfInterest, String[] phenomenonTime)
        {
            var requestObject = new JsonObject();
            requestObject.Add("request", new JsonPrimitive("GetObservation"));
            requestObject.Add("service", new JsonPrimitive("SOS"));
            requestObject.Add("version", new JsonPrimitive("2.0.0"));
            requestObject.Add("procedure", new JsonPrimitive(procedure));
            requestObject.Add("observedProperty", new JsonPrimitive(observedProperty));
            requestObject.Add("featureOfInterest", new JsonPrimitive(featureOfInterest));
            var temporalFilterDuring = new JsonObject();
            temporalFilterDuring.Add("ref", new JsonPrimitive("om:phenomenonTime"));
            JsonArray time = new JsonArray();
            time.Add(new JsonPrimitive(phenomenonTime[0]));
            time.Add(new JsonPrimitive(phenomenonTime[1]));
            temporalFilterDuring.Add("value", time);
            var temporalFilter = new JsonObject();
            temporalFilter.Add("during", temporalFilterDuring);

            requestObject.Add("temporalFilter", temporalFilter);
            return requestObject.ToString();
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

        // TODO: this will be used to parse the stuff
        public static void convertToDict(String JsonString)
        {
            JsonValue jsonValue = JsonValue.Parse(JsonString);
            Console.WriteLine("---- Ignore -----");
            Console.WriteLine(jsonValue.ToString());
            Console.WriteLine("-----------------");
        }

        public static String extractTimeSeries(String timeSeriesJsonResult)
        {
            return "";
        }
    }
}

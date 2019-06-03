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
        public static String BuildJSONSOSTestRequest()
        {
            var requestObject = new JsonObject
            {
                { "request", new JsonPrimitive("Hydrometric_Station") },
                { "service", new JsonPrimitive("SOS") },
                { "version", new JsonPrimitive("2.0.0") },
                { "procedure", new JsonPrimitive("Hydrometric_Station") },
                { "procedureDescriptionFormat", new JsonPrimitive("http://www.opengis.net/sensorML/1.0.1") }
            };
            return requestObject.ToString();
        }

        public static String BuildTimeSeriesRequest(String procedure, String observedProperty, String featureOfInterest, String[] phenomenonTime)
        {
            var requestObject = new JsonObject
            {
                { "request", new JsonPrimitive("GetObservation") },
                { "service", new JsonPrimitive("SOS") },
                { "version", new JsonPrimitive("2.0.0") },
                { "procedure", new JsonPrimitive(procedure) },
                { "observedProperty", new JsonPrimitive(observedProperty) },
                { "featureOfInterest", new JsonPrimitive(featureOfInterest) }
            };
            var temporalFilterDuring = new JsonObject();
            temporalFilterDuring.Add("ref", new JsonPrimitive("om:phenomenonTime"));
            JsonArray time = new JsonArray
            {
                new JsonPrimitive(phenomenonTime[0]),
                new JsonPrimitive(phenomenonTime[1])
            };
            temporalFilterDuring.Add("value", time);
            var temporalFilter = new JsonObject
            {
                { "during", temporalFilterDuring }
            };

            requestObject.Add("temporalFilter", temporalFilter);
            return requestObject.ToString();
        }

        public static String BuildDataAvailabilityRequest(String procedure, String observedProperty, String featureOfInterest)
        {
            var requestObject = new JsonObject
            {
                { "request", new JsonPrimitive("GetDataAvailability") },
                { "service", new JsonPrimitive("SOS") },
                { "version", new JsonPrimitive("2.0.0") },
                { "procedure", new JsonPrimitive(procedure) },
                { "observedProperty", new JsonPrimitive(observedProperty) },
                { "featureOfInterest", new JsonPrimitive(featureOfInterest) }
            };
            return requestObject.ToString();
        }

        public static String BuildDataAvailabilityRequest(String[] procedures, String[] observedProperties, String[] featuresOfInterest)
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
        public static void ConvertToDict(String JsonString)
        {
            JsonValue jsonValue = JsonValue.Parse(JsonString);
            Console.WriteLine("---- Ignore -----");
            Console.WriteLine(jsonValue.ToString());
            Console.WriteLine("-----------------");
        }

        public static TimeSeriesObject ExtractTimeSeries(String timeSeriesJsonResult)
        {
            JsonValue values = JsonValue.Parse(timeSeriesJsonResult);
            TimeSeriesObject timeSeries = new TimeSeriesObject();
            var observations = values["observations"][0];
            if (observations.ContainsKey("featureOfInterest"))
            {
                var featureOfInterest = observations["featureOfInterest"];
                timeSeries.Name = featureOfInterest["name"];
                var coordinates = featureOfInterest["geometry"]["coordinates"];
                timeSeries.Coordinates = new Decimal[2] { coordinates[0], coordinates[1] };
            }
            if (observations.ContainsKey("result"))
            {
                Dictionary<string, Decimal> series = new Dictionary<string, decimal>();
                var results = observations["result"]["values"];
                for (int i = 0; i < results.Count; i++)
                {
                    var entry = results[i];
                    series.Add(entry[0], entry[1]);
                }
                timeSeries.TimeSeries = series;
            }

            Console.WriteLine(timeSeries.ToString());
            return timeSeries;
        }
    }
}

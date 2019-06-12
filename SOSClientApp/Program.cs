using System;


namespace SOSClientJSON
{
    class Program
    {
        static void Main(string[] args)
        {
            JSONClient client = new JSONClient("http://wellsensorobsp.niwa.co.nz:8080/52n-sos-aquarius-webapp/service");
            var startTime = Utils.TimeFormat.GetTimeFormatForQuery(2017, 3, 1);
            var endTime = Utils.TimeFormat.GetTimeFormatForQuery(2017, 3, 2);
            
            var property = "QR"; // This is for Discharge, HG is for Height of Gauge
            var station = "15341"; // ID of the station
            // var result = client.PerformTimeSeriesRequest(property, station, startTime, endTime);
            var result = client.PerformCapabilitiesRequest();
            Console.ReadKey();
            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}

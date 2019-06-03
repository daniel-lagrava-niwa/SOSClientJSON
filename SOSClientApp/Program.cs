using System;


namespace SOSClientJSON
{
    class Program
    {
        static void Main(string[] args)
        {
            JSONClient client = new JSONClient("http://wellsensorobsp.niwa.co.nz:8080/52n-sos-aquarius-webapp/service");
            var startTime = "2017-03-01T00:00:00.000+01:00";
            var endTime = "2017-03-02T00:00:00.000+01:00";
            var property = "QR"; // This is for Discharge, HG is for Height of Gauge
            var station = "15341"; // ID of the station
            var result = client.PerformTimeSeriesRequest(property, station, startTime, endTime);
            Console.WriteLine(result);
            Console.ReadKey();
            
        }
    }
}

using System;


namespace SOSClientJSON
{
    class Program
    {
        static void Main(string[] args)
        {
            JSONClient client = new JSONClient("http://wellsensorobsp.niwa.co.nz:8080/52n-sos-aquarius-webapp/service");
            var result = client.PerformRequest("Test");
            Console.ReadKey();
            
        }
    }
}

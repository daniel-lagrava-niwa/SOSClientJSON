using System;


namespace SOSClientJSON
{
    class Program
    {
        static void Main(string[] args)
        {
            JSONClient client = new JSONClient("http://wellsensorobsp.niwa.co.nz:8080/52n-sos-aquarius-webapp/service");
            var result = client.PerformRequest("GetObservation");
            if (result.Equals(""))
            {
                Console.WriteLine("Request failed");
            }
            else
            {
                Console.WriteLine("Request Succeeded");
                Console.WriteLine("Results: ....");
                Console.WriteLine(result);
            }
            Console.ReadKey();
            
        }
    }
}

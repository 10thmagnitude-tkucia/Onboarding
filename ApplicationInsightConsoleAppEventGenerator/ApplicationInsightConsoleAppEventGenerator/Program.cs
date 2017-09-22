using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;

namespace ApplicationInsightConsoleAppEventGenerator
{
    class Program
    {
        static void Main(string[] args)
        {


            var ApplicationInsightKey = "2220d365-51c7-472d-9921-0ee91d4f435a";

            var tc = new TelemetryClient() { InstrumentationKey = ApplicationInsightKey };
            // Set session data:
            tc.Context.User.Id = Environment.UserName;
            tc.Context.Session.Id = Guid.NewGuid().ToString();
            tc.Context.Device.OperatingSystem = Environment.OSVersion.ToString();
            var generator1 = new Generator(tc, 30000);
            generator1.Start();

            var generator2 = new Generator(tc, 45000);
            generator2.Start();

            var generator3 = new Generator(tc, 60000);
            generator3.Start();

            var generator4 = new Generator(tc, 72000);
            generator4.Start();

            Console.WriteLine("Press ESC to stop");
            do
            {
                while (!Console.KeyAvailable)
                {

                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);


            generator1.Stop();
            generator2.Stop();
            generator3.Stop();
            generator4.Stop();
        }



    }

}

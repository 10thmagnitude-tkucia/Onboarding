using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;

namespace ApplicationInsightConsoleAppEventGenerator
{
    class Generator
    {
        static int generatorId;
        private TelemetryClient telemetryClient;
        private Random random;
        private Thread generatorThread;
        private int highLimit;
        private int instanceId;
        public Generator(TelemetryClient tc, int timerHighLimit)
        {
            telemetryClient = tc;
            instanceId = generatorId++;
            random = new Random();
            highLimit = timerHighLimit;
            generatorThread = new Thread(new ThreadStart(RunProcess));
        }

        private bool runProcess = false;

        public int ThreadSleep
        {
            get; set;
        } = 3000;

        public void Stop()
        {
            runProcess = false;
            generatorThread.Abort();
        }

        public void Start()
        {
            runProcess = true;
            generatorThread.Start();
        }

        private void RunProcess()
        {

            while (runProcess == true)
            {
                ThreadSleep = random.Next(10000, highLimit);

                telemetryClient.TrackEvent("ProcessRan" + instanceId);
                Console.WriteLine(instanceId + "Sent TrackEvent: " + DateTime.Now);

                if (ThreadSleep % 10000 > 0.0)
                {
                    telemetryClient.TrackEvent("FunkyNumber");
                    Console.WriteLine(instanceId + "FunkyNumber");
                }

                Thread.Sleep(ThreadSleep);
            }
        }

    }
}

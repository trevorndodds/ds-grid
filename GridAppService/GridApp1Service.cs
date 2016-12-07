using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataSynapse.GridServer.Engine;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;

namespace GridAppService
{
    class NetTrevTest
    {
        public NetTrevTest() { }

        public Object add(double a, double b)
        {
            double result = a + b;
            long taskid = EngineSession.GetServiceInvocationContext().InvocationId;
            String engine = EngineSession.GetProperties().Get(EngineProperties.USERNAME) + "-" + EngineSession.GetProperties().Get(EngineProperties.INSTANCE);
            String output = engine +" " + result;
            return output;
        }

        public Object MonteCarloPi(int n)
        {
            int inside = 0;
            Random r = new Random();

            for (int i = 0; i < n; i++)
            {
                if (Math.Pow(r.NextDouble(), 2) + Math.Pow(r.NextDouble(), 2) <= 1)
                {
                    inside++;
                }
            }
            double result =  4.0 * inside / n;
            String engine = EngineSession.GetProperties().Get(EngineProperties.USERNAME) + "-" + EngineSession.GetProperties().Get(EngineProperties.INSTANCE);
            String output = engine + " sample size:" + n + " " + result;

            return output;
        }

        public Object calcPrime(int n)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            List<int> primes = new List<int>();
            primes.Add(2);
            int nextPrime = 3;
            while (primes.Count < n)
            {
                int sqrt = (int)Math.Sqrt(nextPrime);
                bool isPrime = true;
                for (int i = 0; (int)primes[i] <= sqrt; i++)
                {
                    if (nextPrime % primes[i] == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                if (isPrime)
                {
                    primes.Add(nextPrime);
                }
                nextPrime += 2;
            }

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;

            String engine = EngineSession.GetProperties().Get(EngineProperties.USERNAME) + "-" + EngineSession.GetProperties().Get(EngineProperties.INSTANCE);
            String output = engine + " Primes: " + primes.Count + " Time (s): " + ts.TotalSeconds;

            return output;
        }


    }
}

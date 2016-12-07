using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataSynapse.GridServer.Client;
using DataSynapse.GridServer.Driver;
using DataSynapse.GridServer.Engine;
using System.Collections;
using System.Collections.Specialized;

namespace gridApp1
{
    class GridApp1Client
    {
        static void Main(string[] args)
        {
            String service = "GridAppService";
          //  String username = "admin";
         //   String password = "admin";
            String priority = "5";
         //   int initPayload = 100;
         //   String method = "MonteCarloPi";
            String method = "calcPrime";
            int tasks = 5;
            string pdirector = "http://ds01.root.net:8000";


            string svcName = "svcName";
            string appName = "Grid App1";
            string appDesc = "Trev Test";
            NameValueCollection props = new NameValueCollection();
            props.Add(Options.PRIORITY, priority);
            
            NameValueCollection dprops = new NameValueCollection();
            dprops.Add(Description.SERVICE_NAME, svcName);
            dprops.Add(Description.APP_DESCRIPTION, appDesc);
            dprops.Add(Description.APP_NAME, appName);
            dprops.Add(Description.DEPT_NAME, "Dept");
            //DriverManager.SetProperty("DSUsername", "trevor");
            //DriverManager.SetProperty("DSPassword", "");
            DriverManager.SetProperty("DSLogLevel", "FINEST");
            DriverManager.SetProperty("DSLogToSystem", "true");
            DriverManager.SetProperty("DSNegotiateEnabled", "true");
            DriverManager.SetProperty("DSPrimaryDirector", pdirector);
            DriverManager.SetProperty("DSSecondaryDirector", pdirector);


            responseHandler handler = new responseHandler();
            try
            {
                //  Service a = ServiceFactory.GetInstance().CreateService("blah");
                Service s = ServiceFactory.GetInstance().CreateService(service, null, props, dprops);
                Random r = new Random();
                for (long i = 0; i < tasks; i++)
                {
                    //  s.Submit(method, new Object[] { Convert.ToDouble(r.Next(0, 9000)), Convert.ToDouble(r.Next(0, 9000)) }, handler);
                    s.Submit(method, 500000, handler);
                }
                s.WaitUntilInactive(0);

                s.Destroy();
            }
            catch (Exception e)
            {
              //  Console.WriteLine("Error: {0}", e.Message);
                Console.WriteLine("Error: {0}", e.InnerException);
             //       throw;
            }


        }


        public class responseHandler : ServiceInvocationHandler
        {
            private long handled = 0;

            public void HandleError(ServiceInvocationException e, int id)
            {
                Console.WriteLine("Error from " + id + ": " + e.Message + " task count: " + (++handled));
            }

            public void HandleResponse(object response, int id)
            {
                Console.WriteLine((++handled) + " - Result for request #" + id + ", " + response);
            }
        }
    }
}

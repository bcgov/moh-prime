using System;

namespace Prime.Services
{
    public class SoapService : ISoapService
    {
        public void Request(System.Xml.Linq.XElement controlActProcess)
        {
            Console.WriteLine("Hello");
        }

    }
}

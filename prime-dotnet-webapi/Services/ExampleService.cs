using System;
using System.Xml.Linq;
using Prime.Models;

namespace Prime.Services
{
    public class ExampleService : IExampleService
    {
        public string Test(string s)
        {
            Console.WriteLine("Test Method Executed!");
            return s;
        }

        public void XmlMethod(XElement xml)
        {
            Console.WriteLine(xml.ToString());
        }

        public ExampleModel TestExampleModel(ExampleModel exampleModel)
        {
            return exampleModel;
        }
    }
}

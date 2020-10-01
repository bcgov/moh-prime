using System;
using System.Xml.Linq;
using Prime.Models;

namespace Prime.Services
{
    public class SoapService : ISoapService
    {
        public string Test(string s)
        {
            Console.WriteLine("Test Method Executed!");
            return s;
        }

        public SoapObject GetModel()
        {
            var exampleModel = new SoapObject
            {
                Id = 1,
                Email = "test@example.com"
            };

            Console.WriteLine("Id: {0}, Name: {1}", exampleModel.Id, exampleModel.Email);

            return exampleModel;
        }

        public SoapObject SetModel(int id, string email)
        {
            Console.WriteLine("Id: {0}, Name: {1}", id, email);

            return new SoapObject
            {
                Id = id,
                Email = email
            };
        }

        public void XmlMethod(XElement xml)
        {
            Console.WriteLine(xml.ToString());
        }
    }
}

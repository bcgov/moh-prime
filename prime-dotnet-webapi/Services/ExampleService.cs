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

        public ExampleModel GetModel()
        {
            var exampleModel = new ExampleModel
            {
                Id = 1,
                Email = "test@example.com"
            };

            Console.WriteLine("Id: {0}, Name: {1}", exampleModel.Id, exampleModel.Email);

            return exampleModel;
        }

        public ExampleModel SetModel(int id, string email)
        {
            Console.WriteLine("Id: {0}, Name: {1}", id, email);

            return new ExampleModel
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

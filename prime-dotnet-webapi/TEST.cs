
using Bogus;

using System;
using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration;
using Prime.ModelFactories;


namespace Prime
{
    public class TEST
    {
        public static void DO_STUFF()
        {
            var owner = new Enrollee { Id = 1 };

            var a1 = new PhysicalAddressFactory(owner).Generate(2);
            var a2 = new MailingAddressFactory(owner).Generate(2);
            var t = 1;
        }
    }
}

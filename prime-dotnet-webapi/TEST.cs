
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

            var enrol = new EnrolleeFactory().Generate("default,status.random");
            var t = 1;
        }
    }
}

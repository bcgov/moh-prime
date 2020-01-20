
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

            var a1 = new AdjudicatorNoteFactory(owner).Generate(2);
            var a2 = new AccessAgreementNoteFactory(owner).Generate(2);
            var a3 = new EnrolmentCertificateNoteFactory(owner).Generate(2);
            var add = new PhysicalAddressFactory(owner).Generate();
            var t = 1;
        }
    }
}

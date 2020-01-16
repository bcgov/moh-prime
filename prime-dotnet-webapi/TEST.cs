
using Bogus;

using System;
using System.Collections.Generic;
using Prime.Models;
using Prime.ModelFactories;


namespace Prime
{
    public class TEST
    {
        public static void DO_STUFF()
        {
            // Faker<Enrollee> EnrolleeFaker = new Faker<Enrollee>()
            //                     .RuleFor(e => e.UserId, f => Guid.NewGuid())
            //                     .RuleFor(e => e.FirstName, f => f.Name.FirstName())
            //                     .RuleFor(e => e.MiddleName, f => f.Name.FirstName())
            //                     .RuleFor(e => e.LastName, f => f.Name.LastName())
            //                     .RuleFor(e => e.DateOfBirth, f => f.Date.Past(20, DateTime.Now.AddYears(-18)))
            //                     //.RuleFor(e => e.PhysicalAddress, f => PhysicalAddressFaker.Generate())
            //                     //.RuleFor(e => e.MailingAddress, f => MailingAddressFaker.Generate())
            //                     //.RuleFor(e => e.Certifications, f => CertificationFaker.Generate(2))
            //                     .RuleSet("provider", (set) => set.RuleFor(x => x.IsInsulinPumpProvider, f => true))
            //                     .RuleSet("notprovider", (set) => set.RuleFor(x => x.IsInsulinPumpProvider, f => false))
            //                     //.RuleFor(e => e.IsInsulinPumpProvider, f => f.Random.Bool())
            //                     //  .RuleFor(e => e.Jobs, f => JobFaker.Generate(2))
            //                     .RuleFor(e => e.HasConviction, f => f.Random.Bool())
            //                     .RuleFor(e => e.HasConvictionDetails, f => f.Lorem.Paragraphs(2))
            //                     .RuleFor(e => e.HasRegistrationSuspended, f => f.Random.Bool())
            //                     .RuleFor(e => e.HasRegistrationSuspendedDetails, f => f.Lorem.Paragraphs(2))
            //                     .RuleFor(e => e.HasDisciplinaryAction, f => f.Random.Bool())
            //                     .RuleFor(e => e.HasDisciplinaryActionDetails, f => f.Lorem.Paragraphs(2))
            //                     .RuleFor(e => e.HasPharmaNetSuspended, f => f.Random.Bool())
            //                     .RuleFor(e => e.HasPharmaNetSuspendedDetails, f => f.Lorem.Paragraphs(2));
            // .RuleFor(e => e.Organizations, f => OrganizationFaker.Generate(2))
            // .RuleFor(e => e.EnrolmentStatuses, f => EnrolmentStatusFaker.Generate(1));

            var p = new EnrolleeFaker().Generate(2);
            var t = 1;
        }



        public class NoteFaker : Faker<AdjudicatorNote>
        {
            public NoteFaker()
            {

            }
        }


        // public static void DO_STUFF()
        // {
        //     // var plant = new BasePlant().WithBlueprintsFromAssemblyOf<PersonBlueprint>();
        //     var plant = new BasePlant();
        //     plant.DefinePropertiesOf<FakeEnrollee>(new
        //     {
        //         FirstName = "Barbara",
        //         Addresses = new LazyProperty<ICollection<FakeXref>>(() =>
        //         {

        //             return new List<FakeXref> { plant.Create<FakeXref>() };
        //         })
        //     });
        //     plant.DefinePropertiesOf<FakeAddress>(new
        //     {
        //         Id = new Sequence<int>((sequenceValue) => sequenceValue),
        //         Street = new Sequence<string>((sequenceValue) => "street" + sequenceValue),
        //     });
        //     plant.DefinePropertiesOf<FakeXref>(new { Occupant = new FakeEnrollee() });
        //     // var address = plant.Create<FakeAddress>();
        //     var enrollee = plant.Create<FakeEnrollee>();
        //     var t = 1;
        // }
    }
}

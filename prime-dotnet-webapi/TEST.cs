using Plant;
using Plant.Core;
using FactoryGirl.NET;


using System.Collections.Generic;
using Prime.Models;


namespace Prime
{
    public class TEST
    {
        public static void DO_STUFF()
        {
            FactoryGirl.NET.FactoryGirl.Define(() =>
            {
                var e = new FakeEnrollee
                {
                    FirstName = "John",
                    Address = FactoryGirl.NET.FactoryGirl.Build<FakeAddress>()
                };
                e.Address.Occupant = e;
                return e;
            });

            FactoryGirl.NET.FactoryGirl.Define(() => new FakeAddress
            {
                Street = "erer"
            });

            var enrol = FactoryGirl.NET.FactoryGirl.Build<FakeEnrollee>();

            var t = 1;
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

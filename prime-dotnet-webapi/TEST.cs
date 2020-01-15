
using FactoryGirlCore;

using System.Collections.Generic;
using Prime.Models;


namespace Prime
{
    public class TEST
    {
        public static void DO_STUFF()
        {

            var enrol = FactoryGirl.Build<Enrollee>();

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

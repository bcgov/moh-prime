using Plant;
using Plant.Core;
using Prime.Models;

using System.Collections.Generic;

namespace Prime
{
    public class TEST
    {
        public static void DO_STUFF()
        {
            var plant = new BasePlant();//.WithBlueprintsFromAssemblyOf<PersonBlueprint>();
            plant.DefinePropertiesOf<FakeEnrollee>(new { FirstName = "Barbara", Addresses = new List<FakeXref>() });
            plant.DefinePropertiesOf<FakeAddress>(new
            {
                Id = new Sequence<int>((sequenceValue) => sequenceValue),
                Street = new Sequence<string>((sequenceValue) => "A street" + sequenceValue),
            });
            // var address = plant.Create<FakeAddress>();
            var enrollee = plant.Create<FakeEnrollee>();
            var t = 1;
        }
    }

    // class PersonBlueprint : IBlueprint
    // {
    //     public void SetupPlant(BasePlant plant)
    //     {
    //         plant.DefinePropertiesOf<Enrollee>(new { FirstName = "Barbara", });
    //         plant.DefinePropertiesOf<PhysicalAddress>(new { Street = "A street" });
    //     }
    // }

    // class AddrBlueprint : IBlueprint
    // {
    //     public void SetupPlant(BasePlant plant)
    //     {
    //     }
    // }
}

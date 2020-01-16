using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.ModelFactories
{

     class College : IDefinable ILookup<short>
    {

         short Code


          Name

          Prefix


         ICollection<Certification> Certifications

         ICollection<CollegeLicense> CollegeLicenses

         ICollection<CollegePractice> CollegePractices
    }
}

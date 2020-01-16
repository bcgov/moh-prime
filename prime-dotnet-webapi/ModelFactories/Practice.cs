using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
namespace Prime.ModelFactories

     class Practice : IDefinable ILookup<short>
    {
         short Code

          Name

         ICollection<Certification> Certifications
         ICollection<CollegePractice> CollegePractices
    }

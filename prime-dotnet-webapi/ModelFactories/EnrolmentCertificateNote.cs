using FactoryGirlCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
namespace Prime.ModelFactories

     class EnrolmentCertificateNote : IDefinable IEnrolleeNote
    {
         ? Id

          EnrolleeId

         Enrollee Enrollee
          Note
         DateTime NoteDate
    }

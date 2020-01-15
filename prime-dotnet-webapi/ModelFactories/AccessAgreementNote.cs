using System;
using Newtonsoft.Json;

using FactoryGirlCore;

namespace Prime.ModelFactories
{
    public class AccessAgreementNoteFactory : IDefinable
    {
        public int? Id ,

        public int EnrolleeId ,

        public Enrollee Enrollee ,

        public string Note ,

        public DateTime NoteDate ,
    }
}

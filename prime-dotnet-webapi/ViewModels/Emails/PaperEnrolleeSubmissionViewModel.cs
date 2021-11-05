using System;

namespace Prime.ViewModels.Emails
{
    public class PaperEnrolleeSubmission
    {
        public string GPID { get; set; }

        public PaperEnrolleeSubmission(string gpid) => GPID = gpid;
    }
}

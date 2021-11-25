using System;

namespace Prime.ViewModels.Emails
{
    public class PaperEnrolleeSubmissionEmailViewModel
    {
        public string GPID { get; set; }

        public PaperEnrolleeSubmissionEmailViewModel(string gpid)
        {
            GPID = gpid;
        }
    }
}

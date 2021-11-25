using System;
using System.Collections.Generic;

namespace Prime.ViewModels
{
    public class EnrolmentStatusAdminViewModel
    {
        public int Id { get; set; }
        public int StatusCode { get; set; }
        public DateTimeOffset StatusDate { get; set; }
        public ICollection<EnrolmentStatusReasonViewModel> EnrolmentStatusReasons { get; set; }
        public EnrolmentStatusReferenceViewModel EnrolmentStatusReference { get; set; }
    }
}

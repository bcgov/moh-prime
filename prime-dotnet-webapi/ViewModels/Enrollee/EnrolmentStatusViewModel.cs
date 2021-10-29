using System;
using System.Collections.Generic;
using Prime.Models;

namespace Prime.ViewModels
{
    public class EnrolmentStatusViewModel
    {
        public int StatusCode { get; set; }
        public DateTimeOffset StatusDate { get; set; }
        public ICollection<EnrolmentStatusReasonViewModel> EnrolmentStatusReasons { get; set; }
        public EnrolmentStatusReferenceViewModel EnrolmentStatusReference { get; set; }
        public bool IsType(StatusType statusType)
        {
            return StatusCode == (int)statusType;
        }
    }
}

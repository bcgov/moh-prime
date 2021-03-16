using System;
using Prime.Models;

namespace Prime.ViewModels
{
    public class BannerUpdateViewModel
    {
        public BannerType BannerType { get; set; }
        public BannerLocationCode BannerLocationCode { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public int AdminId { get; set; }
    }
}

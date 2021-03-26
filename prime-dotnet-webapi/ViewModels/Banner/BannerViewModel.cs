using System;
using Prime.Models;

namespace Prime.ViewModels
{
    public class BannerViewModel
    {
        public BannerType BannerType { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
    }
}

using System;
using Prime.Models;

namespace Prime.ViewModels
{
    public class BannerViewModel
    {
        public BannerType BannerType { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

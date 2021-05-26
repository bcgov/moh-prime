using System;
using Prime.Models;

namespace Prime.ViewModels
{
    public class BannerViewModel
    {
        public BannerType BannerType { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime StartTimestamp { get; set; }
        public DateTime EndTimestamp { get; set; }
    }
}

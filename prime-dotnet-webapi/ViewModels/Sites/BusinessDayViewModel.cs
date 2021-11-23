using System;

namespace Prime.ViewModels.Sites
{
    public class BusinessDayViewModel
    {
        public DayOfWeek Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}

using System;
using Prime.Models;

namespace Prime.ViewModels
{
    public class SiteBusinessEventViewModel
    {
        public int Id { get; set; }

        public string AdminIDIR
        {
            get => Admin?.IDIR;
        }

        public Admin Admin { get; set; }

        public string PartyName { get; set; }

        public string Description { get; set; }

        public DateTimeOffset? EventDate { get; set; }
    }
}

using System;

namespace Prime.ViewModels.Emails
{
    public class EmailTemplateListViewModel
    {
        public int Id { get; set; }
        public string TemplateName { get; set; }
        public DateTimeOffset ModifiedDate { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Recipient { get; set; }
    }
}

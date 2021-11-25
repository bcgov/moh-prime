using System;

namespace Prime.ViewModels
{
    public class SelfDeclarationDocumentViewModel
    {
        public int Id { get; set; }
        public Guid DocumentGuid { get; set; }
        public string Filename { get; set; }
        public DateTimeOffset UploadedDate { get; set; }
        public int EnrolleeId { get; set; }
        public int SelfDeclarationTypeCode { get; set; }
    }
}

using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Prime.Models
{
    public class BaseDocumentUpload : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public Guid DocumentGuid { get; set; }

        public string Filename { get; set; }

        public DateTimeOffset UploadedDate { get; set; }

        /// <summary>
        /// Returns true if this Document's Filename ends in the supplied extension (with or without the ".")
        /// </summary>
        /// <param name="extension"></param>
        public bool HasFileExtension(string extension)
        {
            if (extension.StartsWith("."))
            {
                extension = extension[1..];
            }

            return Filename.Split(".").Last() == extension;
        }
    }
}

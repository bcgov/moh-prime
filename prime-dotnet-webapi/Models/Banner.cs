using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models
{
    [Table("Banner")]
    public class Banner : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public BannerType BannerType { get; set; }

        [Required]
        public BannerLocationCode BannerLocationCode { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime StartTimestamp { get; set; }

        public DateTime EndTimestamp { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models.Plr
{
    /// <summary>
    /// The College (if exists) for a PLR Role Type
    /// </summary>
    [Table("CollegeForPlrRoleType")]
    public class CollegeForPlrRoleType
    {
        [Key]
        public string ProviderRoleType { get; set; }

        public int CollegeCode { get; set; }
    }
}

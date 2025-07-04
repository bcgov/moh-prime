using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("RemoteAccessTypeLookup")]
    public class RemoteAccessType : ILookup<int>
    {
        [Key]
        public int Code { get; set; }

        [Required]
        public string Name { get; set; }
    }

    public enum RemoteAccessTypeEnum
    {
        PrivateCommunityHealthPractice = 1,
        FNHA = 2,
        FNHA_Clinic = 3
    }
}

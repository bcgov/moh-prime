using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Prime.Models
{
    //The site listed in this table will have a different licence class list
    //according to their remote access type
    [Table("ExceptionRemoteAccessSite")]
    public class ExceptionRemoteAccessSite
    {
        [Key]
        public int Id { get; set; }

        public string PEC { get; set; }

        //Match Organzation Registration ID
        public string RegistrationId { get; set; }

        public int RemoteAccessTypeCode { get; set; }
    }
}

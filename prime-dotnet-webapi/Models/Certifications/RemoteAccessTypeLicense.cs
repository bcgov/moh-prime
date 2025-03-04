using System;
using Newtonsoft.Json;

namespace Prime.Models
{
    public class RemoteAccessTypeLicense : BaseAuditable
    {
        public int LicenseCode { get; set; }

        public RemoteAccessTypeEnum RemoteAccessTypeCode { get; set; }

        public DateTime? DeletedDateTime { get; set; }
    }
}

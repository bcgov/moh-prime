using System;

namespace Prime.Models
{
    public interface IAuditable
    {
         string CreatedUserId { get; set; }

         DateTime CreatedTimeStamp { get; set; }

         string UpdatedUserId { get; set; }

         DateTime UpdatedTimeStamp { get; set; }
    }
}
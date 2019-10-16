using System;

namespace Prime.Models
{
    public interface IAuditable
    {
         Guid CreatedUserId { get; set; }

         DateTime CreatedTimeStamp { get; set; }

         Guid UpdatedUserId { get; set; }

         DateTime UpdatedTimeStamp { get; set; }
    }
}
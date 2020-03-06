using System;

namespace Prime.Models
{
    public interface IAuditable
    {
        Guid CreatedUserId { get; set; }

        DateTimeOffset CreatedTimeStamp { get; set; }

        Guid UpdatedUserId { get; set; }

        DateTimeOffset UpdatedTimeStamp { get; set; }
    }
}

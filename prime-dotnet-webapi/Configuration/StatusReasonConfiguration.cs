using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Models;

public class StatusReasonConfiguration : IEntityTypeConfiguration<StatusReason>
{
    private readonly Guid SYSTEM_USER = Guid.Empty;

    private readonly DateTime SEEDING_DATE = DateTime.Now;

    public void Configure(EntityTypeBuilder<StatusReason> builder)
    {
        #region StatusReasonSeed
        builder.HasData(
            new StatusReason { Code = 1, Name = "Automatic", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
            new StatusReason { Code = 2, Name = "Manual", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
            new StatusReason { Code = 3, Name = "Name Discrepancy", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
            new StatusReason { Code = 4, Name = "Not in PharmaNet", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
            new StatusReason { Code = 5, Name = "Insulin Pump Provider", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
            new StatusReason { Code = 6, Name = "Licence Class", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
            new StatusReason { Code = 7, Name = "Self Declaration", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
            new StatusReason { Code = 8, Name = "Contact address or Identity Address Out of British Columbia", CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE }
             );
        #endregion
    }
}

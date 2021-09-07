using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    /// <summary>
    /// PRIME copy of header information of Pharmanet transactions
    /// (originally stored in ODR database).
    ///
    /// ** IMPORTANT **
    /// Changes to this model will likely affect the scripts executed by the Cron job defined in `infrastructure\cron-jobs\retrieve-pnet-logs.cron.yml`.
    /// On the other hand, this model is not used (much) by the .NET API code.
    /// </summary>
    [Table("PharmanetTransactionLog")]
    public class PharmanetTransactionLog
    {
        [Key]
        public long Id { get; set; }

        [JsonIgnore]
        public DateTimeOffset CreatedTimeStamp { get; set; }

        [JsonProperty("txnId")]
        public long TransactionId { get; set; }

        [JsonProperty("txnDateTime")]
        public DateTime TxDateTime { get; set; }

        public string UserId { get; set; }

        // IP address of the location or pharmacy that is sending the transaction.
        public string LocationIpAddress { get; set; }

        // The Location and Source IP address information should be the same, but when pharmacy connects to PharmaNet using an intermediary like HNSecure,
        // then the Source IP address will be that of the intermediary.
        public string SourceIpAddress { get; set; }

        [JsonProperty("pharmacyId")]
        public string PharmacyId { get; set; }

        [JsonProperty("txnType")]
        public string TransactionType { get; set; }

        [JsonProperty("txnSubtype")]
        public string TransactionSubType { get; set; }

        [JsonProperty("practitionerId")]
        public string PractitionerId { get; set; }

        [JsonProperty("collegeRef")]
        public string CollegePrefix { get; set; }

        [JsonProperty("pnetTxnOutcome")]
        public string TransactionOutcome { get; set; }

        [JsonProperty("providerSoftwareId")]
        public string ProviderSoftwareId { get; set; }

        [JsonProperty("providerSoftwareVer")]
        public string ProviderSoftwareVersion { get; set; }
    }
}

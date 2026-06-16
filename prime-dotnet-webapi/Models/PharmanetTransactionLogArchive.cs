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
    /// This is a copy of PharmannetTransactionLog table to archive older transaction logs. The record in this table
    /// will be backed up and moved to S3 storage and deleted from the database.
    /// </summary>
    [Table("PharmanetTransactionLogArchive")]
    public class PharmanetTransactionLogArchive
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

        public string LocationIpAddress { get; set; }

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

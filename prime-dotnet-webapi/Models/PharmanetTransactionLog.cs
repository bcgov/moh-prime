using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Prime.Helpers;

namespace Prime.Models
{
    /// <summary>
    /// PRIME copy of header information of Pharmanet transactions
    /// (originally stored in ODR database)
    /// </summary>
    [Table("PharmanetTransactionLog")]
    public class PharmanetTransactionLog : BaseAuditable
    {
        [Key]
        public long Id { get; set; }

        [JsonProperty("txnId")]
        public long TransactionId { get; set; }

        [JsonProperty("txnDateTime")]
        [JsonConverter(typeof(CustomDateTimeConverter), "yyyyMMddTHHmmss.ff")]
        public DateTime TxDateTime { get; set; }

        public string UserId { get; set; }

        [JsonProperty("ipAddr")]
        public string IpAddress { get; set; }

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

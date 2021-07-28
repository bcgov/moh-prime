using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

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

        [JsonProperty("txnDate")]
        public DateTime TxDateTime { get; set; }

        public string UserId { get; set; }

        [JsonProperty("ipAddr")]
        public string IpAddress { get; set; }

        [JsonProperty("pharmId")]
        public string PharmacyId { get; set; }

        [JsonProperty("txnType")]
        public string TransactionType { get; set; }

        [JsonProperty("txnSubtype")]
        public string TransactionSubType { get; set; }

        [JsonProperty("practId")]
        public string PractitionerId { get; set; }

        public string CollegePrefix { get; set; }

        [JsonProperty("txnStatus")]
        public string TransactionOutcome { get; set; }

        [JsonProperty("prvdrSwId")]
        public string ProviderSoftwareId { get; set; }

        [JsonProperty("prvdrSwVrsn")]
        public string ProviderSoftwareVersion { get; set; }
    }
}

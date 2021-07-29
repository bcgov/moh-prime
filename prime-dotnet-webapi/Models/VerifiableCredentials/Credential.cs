using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models.VerifiableCredentials
{
    [Table("Credential")]
    public class Credential : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        [JsonIgnore]
        public int EnrolleeId { get; set; }

        [JsonIgnore]
        public Enrollee Enrollee { get; set; }

        public Guid ConnectionId { get; set; }

        public string SchemaId { get; set; }

        public Guid CredentialExchangeId { get; set; }

        public string CredentialDefinitionId { get; set; }

        public string Alias { get; set; }

        public string Base64QRCode { get; set; }

        public DateTimeOffset? AcceptedCredentialDate { get; set; }

        public DateTimeOffset? RevokedCredentialDate { get; set; }
    }
}

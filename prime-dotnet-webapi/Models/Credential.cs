using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Prime.Models
{
    [Table("Credential")]
    public class Credential : BaseAuditable
    {
        [Key]
        public int Id { get; set; }
        public string ConnectionId { get; set; }
        public string SchemaId { get; set; }
        public string CredentialExchangeId { get; set; }
        public string CredentialDefinitionId { get; set; }
        public string Alias { get; set; }
        public string Base64QRCode { get; set; }
        public DateTimeOffset? AcceptedCredentialDate { get; set; }
        public DateTimeOffset? RevokedCredentialDate { get; set; }
    }
}

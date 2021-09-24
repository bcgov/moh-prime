using System.Collections.Generic;

namespace Prime.HttpClients.KeycloakApiDefinitions
{
    /// <summary>
    /// This is not the entire Keycloak Client Representation! See https://www.keycloak.org/docs-api/5.0/rest-api/index.html#_clientrepresentation.
    /// </summary>
    public class Client
    {
        /// <summary>
        /// ID referenced in URIs and tokens
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Guid-like unique identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Display name
        /// </summary>
        public string Name { get; set; }
    }

    public class Role
    {
        public bool ClientRole { get; set; }
        public bool Composite { get; set; }
        public string ContainerId { get; set; }
        public string Description { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
    }

    /// <summary>
    /// This is not the entire Keycloak User Representation! See https://www.keycloak.org/docs-api/5.0/rest-api/index.html#_userrepresentation.
    /// This is a sub-set of the properties so we don't accidentally overwrite anything when doing the PUT.
    /// </summary>
    public class UserRepresentation
    {
        public string Email { get; set; }
        public Dictionary<string, string[]> Attributes { get; set; }

        public void SetPhone(string phoneNumber)
        {
            SetAttribute("phone", phoneNumber);
        }

        public void SetPhoneNumber(string phoneNumber)
        {
            SetAttribute("phoneNumber", phoneNumber);
        }

        public void SetPhoneExtension(string phoneExtension)
        {
            SetAttribute("phoneExtension", phoneExtension);
        }

        private void SetAttribute(string key, string value)
        {
            Attributes[key] = new string[] { value };
        }
    }
}

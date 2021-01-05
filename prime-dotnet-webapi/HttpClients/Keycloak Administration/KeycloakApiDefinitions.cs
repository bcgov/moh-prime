using System.Collections.Generic;

namespace Prime.HttpClients.KeycloakApiDefinitions
{
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
    /// This is not the entire Keyclcoak User Representation! See https://www.keycloak.org/docs-api/5.0/rest-api/index.html#_userrepresentation.
    /// This is a sub-set of the properties so we don't accidentally overwrite anything when doing the PUT.
    /// </summary>
    public class UserInfoUpdateRepresentation
    {
        public string Email { get; set; }
        public Dictionary<string, string[]> Attributes { get; set; }

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

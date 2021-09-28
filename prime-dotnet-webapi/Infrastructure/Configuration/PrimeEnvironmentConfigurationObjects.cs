using Prime.Auth;

namespace Prime.Infrastructure.Configuration.Internal
{
    public class ConnectionStringConfiguration
    {
        public string PrimeDatabase { get; set; }
    }

    public class DocumentManagerConfiguration
    {
        public string Url { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }

    public class PrimeKeycloakConfiguration
    {
        public string RealmUrl { get; set; }
        public string WellKnownConfig { get => KeycloakUrls.WellKnownConfig(RealmUrl); }
        public string TokenUrl { get => KeycloakUrls.Token(RealmUrl); }
        public string AdministrationUrl { get; set; }
        public string AdministrationClientId { get => "keycloak-service-account"; }
        public string AdministrationClientSecret { get; set; }
    }

    public class MohKeycloakConfiguration
    {
        public string RealmUrl { get; set; }
        public string WellKnownConfig { get => KeycloakUrls.WellKnownConfig(RealmUrl); }
        public string TokenUrl { get => KeycloakUrls.Token(RealmUrl); }
        public string GisClientId { get => "GIS"; }
        public string GisUserRole { get => "GISUSER"; }
        public string AdministrationUrl { get; set; }
        public string AdministrationClientId { get => "PRIME-WEBAPP-ENROLLMENT-SERVICE"; }
        public string AdministrationClientSecret { get; set; }
    }

    public class MailServerConfiguration
    {
        public string Url { get; set; }
        public int Port { get; set; }
    }

    public class PharmanetApiConfiguration
    {
        public string Url { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string SslCertFilename { get; set; }
        public string SslCertPassword { get; set; }
    }

    public class ChesApiConfiguration
    {
        public bool Enabled { get; set; }
        public string Url { get; set; }
        public string ClientId { get => "PRIME_SERVICE_CLIENT"; }
        public string ClientSecret { get; set; }
        public string TokenUrl { get; set; }
    }

    /// <summary>
    /// Aries Prime Agent
    /// </summary>
    public class VerifiableCredentialApiConfiguration
    {
        public string Url { get; set; }
        public string Key { get; set; }
        public string WebhookKey { get; set; }
        // If schema changes, the following must be updated in all agents for each environment as the code changes are pushed so versions are the same
        // and have verifier app updated by aries team in each environment (send them schema id, if claims change send them new attributes)
        // Update the following through postman:
        // 1. Add new schema, incrementing schema version -> schema_name  enrollee
        // 2. Create a credential definition for schema -> support_revocation  true, tag  prime
        public string SchemaName { get => "enrollee"; }
        public string SchemaVersion { get => "2.2"; }
    }

    /// <summary>
    /// Canada Post Address Validation
    /// </summary>
    public class AddressAutocompleteApiConfiguration
    {
        public string Url { get; set; }
        public string Key { get; set; }
    }

    public class MetabaseApiConfiguration
    {
        public string Url { get; set; }
        public string Key { get; set; }
        public int DashboardId { get; set; }
    }

    public class PlrIntegrationConfiguration
    {
        public string ClientCertThumbprint { get; set; }
    }

    public class LdapApiConfiguration
    {
        public string Url { get; set; }
    }
}

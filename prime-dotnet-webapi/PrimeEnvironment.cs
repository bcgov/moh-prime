using Prime.Infrastructure.Configuration.Internal;

namespace Prime
{
    public class PrimeEnvironment
    {
        public static PrimeEnvironment Current { get; set; }

        public string Name { get; set; }
        public string FrontendUrl { get; set; }
        public string BackendUrl { get; set; }

        public bool IsProduction { get => Name == "prod"; }
        public bool IsLocal { get => Name == "local"; }

        public ConnectionStringConfiguration ConnectionStrings { get; set; }

        public DocumentManagerConfiguration DocumentManager { get; set; }

        public PrimeKeycloakConfiguration PrimeKeycloak { get; set; }

        public MohKeycloakConfiguration MohKeycloak { get; set; }

        public MailServerConfiguration MailServer { get; set; }

        public PharmanetApiConfiguration PharmanetApi { get; set; }

        public ChesApiConfiguration ChesApi { get; set; }

        /// <summary>
        /// Aries Prime Agent
        /// </summary>
        public VerifiableCredentialApiConfiguration VerifiableCredentialApi { get; set; }

        /// <summary>
        /// Canada Post Address Validation
        /// </summary>
        public AddressAutocompleteApiConfiguration AddressAutocompleteApi { get; set; }

        public MetabaseApiConfiguration MetabaseApi { get; set; }

        public PlrIntegrationConfiguration PlrIntegration { get; set; }

        public LdapApiConfiguration LdapApi { get; set; }
    }
}

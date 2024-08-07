using IdentityModel.Client;

namespace Prime.HttpClients
{
    public class AddressAutocompleteClientCredentials : BasicApiKey { }
    public class ChesClientCredentials : ClientCredentialsTokenRequest { }
    public class DocumentManagerClientCredentials : ClientCredentialsTokenRequest { }
    public class MohKeycloakAdministrationClientCredentials : ClientCredentialsTokenRequest { }
    public class PrimeKeycloakAdministrationClientCredentials : ClientCredentialsTokenRequest { }
}

using IdentityModel.Client;

namespace Prime.HttpClients
{
    public class AddressAutocompleteClientCredentials : QueryStringApiKey { }
    public class ChesClientCredentials : ClientCredentialsTokenRequest { }
    public class DocumentManagerClientCredentials : ClientCredentialsTokenRequest { }
    public class KeycloakAdministrationClientCredentials : ClientCredentialsTokenRequest { }
    public class MohKeycloakAdministrationClientCredentials : ClientCredentialsTokenRequest { }
}

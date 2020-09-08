using IdentityModel.Client;

namespace Prime.Services.Clients
{
    public class DocumentManagerClientCredentials : ClientCredentialsTokenRequest { }

    public class KeycloakAdministrationClientCredentials : ClientCredentialsTokenRequest { }

    public class ChesClientCredentials : ClientCredentialsTokenRequest { }

    public class AddressAutocompleteClientCredentials : QueryStringApiKey { }
}

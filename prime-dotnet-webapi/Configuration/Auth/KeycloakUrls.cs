using Flurl;

namespace Prime.Configuration.Auth
{
    public static class KeycloakUrls
    {
        /// <summary>
        /// Returns the URL for OAuth token issuance.
        /// </summary>
        /// <param name="realmUrl">URL of the keycloak instance up to the realm name; I.e. "[base url]/auth/realms/[realm name]"</param>
        public static string Token(string realmUrl)
        {
            return Url.Combine(realmUrl, "protocol/openid-connect/token");
        }

        /// <summary>
        /// Returns the URL for the OAuth well-known config.
        /// </summary>
        /// <param name="realmUrl">URL of the keycloak instance up to the realm name; I.e. "[base url]/auth/realms/[realm name]"</param>
        public static string WellKnownConfig(string realmUrl)
        {
            return Url.Combine(realmUrl, ".well-known/openid-configuration");
        }
    }
}

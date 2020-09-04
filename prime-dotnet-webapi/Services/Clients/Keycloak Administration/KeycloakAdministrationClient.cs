using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;


using Prime.Services.Clients.KeycloakApiDefinitions;

namespace Prime.Services.Clients
{
    public class KeycloakAdministrationClient : IKeycloakAdministrationClient
    {
        private readonly HttpClient _client;

        public KeycloakAdministrationClient(HttpClient httpClient)
        {
            _client = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<Role> GetRoleByName(string role)
        {
            var response = await _client.GetAsync($"roles/{WebUtility.UrlEncode(role)}");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<Role>();
        }

        public async Task AssignRole(Guid userId, string role)
        {
            // Keycloak expects an array of roles to assign, of which we need both the name and ID
            var keycloakRole = await GetRoleByName(role);
            string serialized = JsonConvert.SerializeObject(new[] { keycloakRole });

            var response = await _client.PostAsync($"users/{userId}/role-mappings/realm", new StringContent(serialized));

            response.EnsureSuccessStatusCode();
        }
    }
}

using System;
using System.Net;
using System.Net.Http;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

using Prime.HttpClients.KeycloakApiDefinitions;

namespace Prime.HttpClients
{
    public class KeycloakAdministrationClient : BaseClient, IKeycloakAdministrationClient
    {
        private readonly HttpClient _client;
        private readonly ILogger _logger;

        public KeycloakAdministrationClient(
            HttpClient httpClient,
            ILogger<KeycloakAdministrationClient> logger)
            : base(PropertySerialization.CamelCase)
        {
            // Credentials and Base Url are set in Startup.cs
            _client = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _logger = logger;
        }


        public async Task<Client> GetClient(string clientId)
        {
            var response = await _client.GetAsync("clients");

            if (!response.IsSuccessStatusCode)
            {
                var responseMessage = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Could not retrieve the Clients from Keycloak. Response message: {responseMessage}");
                return null;
            }

            var clients = await response.Content.ReadAsAsync<IEnumerable<Client>>();
            var client = clients?.SingleOrDefault(c => c.ClientId == clientId);

            if (client == null)
            {
                _logger.LogError($"Could not find a Client with ClientId {clientId} from Keycloak.");
            }

            return client;
        }

        public async Task<bool> AssignClientRole(Guid userId, string clientId, string roleName)
        {
            // We need both the name and ID of the role to assign it.
            var role = await GetClientRole(clientId, roleName);
            if (role == null)
            {
                return false;
            }

            // Keycloak expects an array of roles.
            var content = CreateStringContent(new[] { role });
            var response = await _client.PostAsync($"users/{userId}/role-mappings/clients/{role.ContainerId}", content);

            if (!response.IsSuccessStatusCode)
            {
                var responseMessage = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Could not assign the role {role} to user {userId}. Response message: {responseMessage}");
                return false;
            }

            return true;
        }

        public async Task<bool> AssignRealmRole(Guid userId, string roleName)
        {
            // We need both the name and ID of the role to assign it.
            var role = await GetRealmRole(roleName);
            if (role == null)
            {
                return false;
            }

            // Keycloak expects an array of roles.
            var content = CreateStringContent(new[] { role });
            var response = await _client.PostAsync($"users/{userId}/role-mappings/realm", content);

            if (!response.IsSuccessStatusCode)
            {
                var responseMessage = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Could not assign the role {roleName} to user {userId}. Response message: {responseMessage}");
                return false;
            }

            return true;
        }

        public async Task<Role> GetClientRole(string clientId, string roleName)
        {
            // Need ID of Client (not the same as ClientId!) to fetch roles.
            var client = await GetClient(clientId);
            if (client == null)
            {
                return null;
            }

            var response = await _client.GetAsync($"clients/{client.Id}/roles");

            if (!response.IsSuccessStatusCode)
            {
                var responseMessage = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Could not retrieve the Roles from Client {client.Id}. Response message: {responseMessage}");
                return null;
            }

            var roles = await response.Content.ReadAsAsync<IEnumerable<Role>>();
            var role = roles?.SingleOrDefault(r => r.Name == roleName);

            if (role == null)
            {
                _logger.LogError($"Could not find a Client Role with name {roleName} from Client {clientId}.");
            }

            return role;
        }

        public async Task<Role> GetRealmRole(string roleName)
        {
            var response = await _client.GetAsync($"roles/{WebUtility.UrlEncode(roleName)}");

            if (!response.IsSuccessStatusCode)
            {
                var responseMessage = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Could not retrieve the role {roleName} from Keycloak. Response message: {responseMessage}");
                return null;
            }

            return await response.Content.ReadAsAsync<Role>();
        }

        public async Task<UserRepresentation> GetUser(Guid userId)
        {
            var response = await _client.GetAsync($"users/{userId}");
            if (!response.IsSuccessStatusCode)
            {
                var responseMessage = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Could not retrieve user {userId} from Keycloak. Response message: {responseMessage}");
                return null;
            }

            return await response.Content.ReadAsAsync<UserRepresentation>();
        }

        public async Task<bool> UpdateUser(Guid userId, UserRepresentation userRep)
        {
            var response = await _client.PutAsync($"users/{userId}", CreateStringContent(userRep));
            if (!response.IsSuccessStatusCode)
            {
                var responseMessage = await response.Content.ReadAsStringAsync();
                _logger.LogDebug($"Could not update the user {userId}. Response message: {responseMessage}");
                return false;
            }

            return true;
        }
    }
}

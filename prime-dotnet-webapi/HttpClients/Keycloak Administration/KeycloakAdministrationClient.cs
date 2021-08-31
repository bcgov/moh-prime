using System;
using System.Net;
using System.Net.Http;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

using Prime.Auth;
using Prime.Models;
using Prime.ViewModels.Parties;
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

        /// <summary>
        /// Gets the Keycloak Role representation by name. Returns null if unccessful.
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task<Role> GetRealmRoleByName(string role)
        {
            var response = await _client.GetAsync($"roles/{WebUtility.UrlEncode(role)}");

            if (!response.IsSuccessStatusCode)
            {
                var responseMessage = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Could not retrieve the role {role} from Keycloak. Response message: {responseMessage}");
                return null;
            }

            return await response.Content.ReadAsAsync<Role>();
        }

        /// <summary>
        /// Assigns a realm-level role to the user, if it exists.
        /// Returns true if the operation was successful.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="role"></param>
        public async Task<bool> AssignRealmRole(Guid userId, string role)
        {
            // We need both the name and ID of the role to assign it.
            var keycloakRole = await GetRealmRoleByName(role);
            if (keycloakRole == null)
            {
                return false;
            }

            // Keycloak expects an array of roles.
            var content = CreateStringContent(new[] { keycloakRole });
            var response = await _client.PostAsync($"users/{userId}/role-mappings/realm", content);

            if (!response.IsSuccessStatusCode)
            {
                var responseMessage = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Could not assign the role {role} to user {userId}. Response message: {responseMessage}");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Updates the User's email, phone number, and phone extension in Keycloak. Will remove any attribute set to null. Also gives the User the role(s) relevant to the PartyType(s) selected.
        /// Returns true if the operation was successful.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="party"></param>
        public async Task<bool> UpdatePhsaUserInfo(Guid userId, PhsaChangeModel party)
        {
            var response = await _client.GetAsync($"users/{userId}");
            if (!response.IsSuccessStatusCode)
            {
                var responseMessage = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Could not retrieve user {userId} from keycloak. Response message: {responseMessage}");
                return false;
            }
            var userRep = await response.Content.ReadAsAsync<UserInfoUpdateRepresentation>();

            userRep.Email = party.Email;
            userRep.SetPhoneNumber(party.Phone);
            userRep.SetPhoneExtension(party.PhoneExtension);

            response = await _client.PutAsync($"users/{userId}", CreateStringContent(userRep));
            if (!response.IsSuccessStatusCode)
            {
                var responseMessage = await response.Content.ReadAsStringAsync();
                _logger.LogDebug($"Could not update the user {userId}. Response message: {responseMessage}");
                return false;
            }

            bool success = true;
            if (party.PartyTypes.Contains(PartyType.Labtech))
            {
                success &= await AssignRealmRole(userId, Roles.PhsaLabtech);
            }

            if (party.PartyTypes.Contains(PartyType.Immunizer))
            {
                success &= await AssignRealmRole(userId, Roles.PhsaImmunizer);
            }

            return success;
        }

    }
}

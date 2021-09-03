using System;
using System.Threading.Tasks;

using Prime.HttpClients.KeycloakApiDefinitions;

namespace Prime.HttpClients
{
    public interface IKeycloakAdministrationClient
    {
        /// <summary>
        /// Gets the Keycloak Client representation by ClientId.
        /// Returns null if unccessful.
        /// </summary>
        /// <param name="clientId"></param>
        Task<Client> GetClient(string clientId);

        /// <summary>
        /// Gets the Keycloak Client Role representation by name.
        /// Returns null if unccessful or if no roles of that name exist on the client.
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="roleName"></param>
        Task<Role> GetClientRole(string clientId, string roleName);

        /// <summary>
        /// Gets the Keycloak Role representation by name.
        /// Returns null if unccessful.
        /// </summary>
        /// <param name="roleName"></param>
        Task<Role> GetRealmRole(string roleName);

        /// <summary>
        /// Assigns a Client Role to the user, if it exists.
        /// Returns true if the operation was successful.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="clientId"></param>
        /// <param name="roleName"></param>
        Task<bool> AssignClientRole(Guid userId, string clientId, string roleName);

        /// <summary>
        /// Assigns a realm-level role to the user, if it exists.
        /// Returns true if the operation was successful.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleName"></param>
        Task<bool> AssignRealmRole(Guid userId, string roleName);
    }
}

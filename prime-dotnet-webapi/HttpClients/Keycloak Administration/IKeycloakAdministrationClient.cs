using System;
using System.Threading.Tasks;

using Prime.HttpClients.KeycloakApiDefinitions;

namespace Prime.HttpClients
{
    public interface IKeycloakAdministrationClient
    {
        Task<Role> GetRoleByName(string role);

        Task AssignRealmRole(Guid userId, string role);
    }
}

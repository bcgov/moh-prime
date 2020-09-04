using System;
using System.Threading.Tasks;

using Prime.Services.Clients.KeycloakApiDefinitions;

namespace Prime.Services.Clients
{
    public interface IKeycloakAdministrationClient
    {
        Task<Role> GetRoleByName(string role);

        Task AssignRole(Guid userId, string role);
    }
}

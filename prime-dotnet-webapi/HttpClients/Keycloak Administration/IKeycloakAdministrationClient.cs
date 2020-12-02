using System;
using System.Threading.Tasks;

using Prime.HttpClients.KeycloakApiDefinitions;

namespace Prime.HttpClients
{
    public interface IKeycloakAdministrationClient
    {
        Task<Role> GetRoleByName(string role);

        Task<bool> AssignRealmRole(Guid userId, string role);

        Task<bool> UpdateUserInfo(Guid userId, string email, string phoneNumber, string phoneExtension);
    }
}

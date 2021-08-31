using System;
using System.Threading.Tasks;

using Prime.ViewModels.Parties;
using Prime.HttpClients.KeycloakApiDefinitions;

namespace Prime.HttpClients
{
    public interface IKeycloakAdministrationClient
    {
        Task<Role> GetRealmRole(string roleName);
        Task<bool> AssignRealmRole(Guid userId, string roleName);
        Task<bool> UpdatePhsaUserInfo(Guid userId, PhsaChangeModel party);
    }
}

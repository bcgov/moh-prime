using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

using Prime.Auth;
using Prime.Models;
using Prime.HttpClients;
using Prime.ViewModels.Parties;

namespace Prime.Services
{
    public class PhsaService : IPhsaService
    {
        private readonly ILogger _logger;
        private readonly IPrimeKeycloakAdministrationClient _keycloakClient;

        public PhsaService(
            ILogger<PhsaService> logger,
            IPrimeKeycloakAdministrationClient keycloakClient)
        {
            _logger = logger;
            _keycloakClient = keycloakClient;
        }

        /// <summary>
        /// Updates the User's email, phone number, and phone extension in Keycloak. Will remove any attribute set to null. Also gives the User the role(s) relevant to the PartyType(s) selected.
        /// Returns true if the operation was successful.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="party"></param>
        public async Task<bool> UpdateKeycloakUserInfo(Guid userId, PhsaChangeModel party)
        {
            var user = await _keycloakClient.GetUser(userId);
            if (user == null)
            {
                return false;
            }

            user.Email = party.Email;
            user.SetPhoneNumber(party.Phone);
            user.SetPhoneExtension(party.PhoneExtension);

            var success = await _keycloakClient.UpdateUser(userId, user);
            if (!success)
            {
                return false;
            }

            success = true;

            if (party.PartyTypes.Contains(PartyType.Labtech))
            {
                if (!await _keycloakClient.AssignRealmRole(userId, Roles.PhsaLabtech))
                {
                    _logger.LogError($"Could not assign the role {Roles.PhsaLabtech} to PHSA user {userId}");
                    success = false;
                }
            }

            if (party.PartyTypes.Contains(PartyType.Immunizer))
            {
                if (!await _keycloakClient.AssignRealmRole(userId, Roles.PhsaImmunizer))
                {
                    _logger.LogError($"Could not assign the role {Roles.PhsaImmunizer} to PHSA user {userId}");
                    success = false;
                }
            }

            return success;
        }
    }
}

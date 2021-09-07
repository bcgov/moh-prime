using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
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

            if (!await _keycloakClient.UpdateUser(userId, user))
            {
                return false;
            }

            var success = true;

            foreach (var role in MapToPhsaRoles(party.PartyTypes))
            {
                success &= await AssignRoleAsync(userId, role);
            }

            return success;
        }

        private IEnumerable<string> MapToPhsaRoles(IEnumerable<PartyType> partyTypes)
        {
            static string roleMap(PartyType type)
            {
                return type switch
                {
                    PartyType.Immunizer => Roles.PhsaImmunizer,
                    PartyType.Labtech => Roles.PhsaLabtech,
                    _ => null,
                };
            }

            return partyTypes
                .Select(type => roleMap(type))
                .Where(role => role != null)
                .Distinct();
        }

        private async Task<bool> AssignRoleAsync(Guid userId, string role)
        {
            if (!await _keycloakClient.AssignRealmRole(userId, role))
            {
                _logger.LogError($"Could not assign the role {role} to PHSA user {userId}");
                return false;
            }

            return true;
        }
    }
}

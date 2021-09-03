using System;
using System.Threading.Tasks;

using Prime.ViewModels.Parties;

namespace Prime.Services
{
    public interface IPhsaService
    {
        Task<bool> UpdateKeycloakUserInfo(Guid userId, PhsaChangeModel party);
    }
}

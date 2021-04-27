using System.Security.Claims;
using Prime.Models;

namespace Prime.ViewModels.Parties
{
    public interface IPartyChangeModel
    {
        Party UpdateParty(Party party, ClaimsPrincipal user);
    }
}

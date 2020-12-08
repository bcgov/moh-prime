using System.Security.Claims;
using System.Threading.Tasks;
using Prime.ViewModels.Labtech;

namespace Prime.Services
{
    public interface ILabtechService
    {
        Task<bool> CreateLabtechAsync(LabtechCreateModel labtech, ClaimsPrincipal user);
    }
}

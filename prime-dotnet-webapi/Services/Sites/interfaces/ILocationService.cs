using System;
using System.Threading.Tasks;
using Prime.Models;

namespace Prime.Services
{
    public interface ILocationService
    {
        Task<Location> GetLocationAsync(int locationId);

        Task<int> CreateLocationAsync(Location location);

        Task<int> UpdateLocationAsync(int locationId, Location location);

        void UpdateLocationAddress(Location current, Location updated);

        Task<int> SavePatchLocationAsync(Location location);

        Task DeleteLocationAsync(int locationId);

    }
}

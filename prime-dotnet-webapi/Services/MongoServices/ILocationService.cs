using System.Collections.Generic;
using System.Threading.Tasks;
using Prime.Models;

namespace Prime.Services
{
    public interface ILocationService
    {
        Task<List<Locations>> Get();
        Task<Locations> Get(string id);
        Task<Locations> Create(Locations location);
        void Update(string id, Locations locationIn);
        void Remove(Locations locationIn);
        void Remove(string id);
    }
}

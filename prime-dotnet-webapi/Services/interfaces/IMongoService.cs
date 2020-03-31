using System.Collections.Generic;
using Prime.Models;

namespace Prime.Services
{
    public interface IMongoService
    {
        List<Locations> Get();
        Locations Get(string id);
        Locations Create(Locations location);
        void Update(string id, Locations locationIn);
        void Remove(Locations locationIn);
        void Remove(string id);
    }
}

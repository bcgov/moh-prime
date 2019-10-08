using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prime.Models;

namespace Prime.Services
{
    public interface ILookupService
    {            
        Task<List<T>> GetLookupsAsync<T>() where T : ILookup;
    }
}
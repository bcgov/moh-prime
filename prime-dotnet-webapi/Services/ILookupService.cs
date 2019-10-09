using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Prime.Models;

namespace Prime.Services
{
    public interface ILookupService
    {            
        Task<List<T>> GetLookupsAsync<T>(params Expression<Func<T, object>>[] includes) where T :class, ILookup;
    }
}
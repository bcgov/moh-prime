using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Prime.Models;

namespace Prime.Services
{
    public interface ILookupService
    {            
        Task<LookupEntity> GetLookupsAsync();
    }
}

using System;
using System.Threading.Tasks;
using Prime.Models;
using System.Collections.Generic;

namespace Prime.Services
{
    public interface IPHSAService
    {

        Task<int> CreatePHSAAsync(string body);
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Prime.Models;

namespace Prime.Services
{
    public interface IDocumentService
    {
        Task<IEnumerable<Document>> GetBusinessLicenceDocumentsBySiteId(int siteId);
    }
}

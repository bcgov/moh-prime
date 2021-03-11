using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Prime.HttpClients;

namespace Prime.Services
{
    public class GisService : BaseService, IGisService
    {
        private readonly ILdapClient _ldapClient;
        public GisService(
            ApiDbContext context,
            IHttpContextAccessor httpContext,
            ILdapClient ldapClient)
            : base(context, httpContext)
        {
            _ldapClient = ldapClient;
        }

        public async Task<bool> LdapLogin(string username, string password)
        {
            var result = await _ldapClient.GetUserAsync(username, password);

            var gisUserRole = (string)result.SelectToken("gisuserrole");

            return gisUserRole == "GISUSER";
        }
    }
}

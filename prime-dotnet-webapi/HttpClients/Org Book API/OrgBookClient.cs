using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

using Prime.HttpClients.PharmanetCollegeApiDefinitions;
using System.Web;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace Prime.HttpClients
{
    public class OrgBookClient : BaseClient, IOrgBookClient
    {
        private readonly HttpClient _client;
        private readonly ILogger _logger;

        public OrgBookClient(HttpClient client,
        ILogger<OrgBookClient> logger)
            : base(PropertySerialization.CamelCase)
        {
            // Auth header and cert are injected in Startup.cs
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _logger = logger;
        }

        public async Task<string> GetOrgBookSearchRecordAsync(string orgName)
        {
            string registrationId = null;
            HttpResponseMessage response = null;
            try
            {
                response = await _client.GetAsync($"https://www.orgbook.gov.bc.ca/api/v2/search/credential/topic/facets?name=${HttpUtility.UrlEncode(orgName)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: ${ex.Message} for orgName: ${orgName}");
            }

            if (response != null)
            {
                string searchResult = await response.Content.ReadAsStringAsync();

                var d = JObject.Parse(searchResult);

                try
                {
                    var objectResults = d["objects"]["results"].Where(r => r["topic"]["names"][0]["text"].ToString() == orgName);
                    if (objectResults.Count() > 0)
                    {
                        registrationId = objectResults.First()["topic"]["source_id"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Exception: ${ex.Message} for orgName: ${orgName}");
                }
            }

            return registrationId;
        }
    }
}

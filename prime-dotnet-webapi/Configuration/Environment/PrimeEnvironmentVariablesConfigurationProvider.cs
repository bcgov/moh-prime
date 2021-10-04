using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Prime.Configuration.Environment
{
    public class PrimeEnvironmentVariablesConfigurationProvider : ConfigurationProvider
    {
        public override void Load()
        {
            Data = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            foreach (var envVariable in System.Environment.GetEnvironmentVariables().Cast<DictionaryEntry>())
            {
                var appKey = MapEnvKeyToAppKey((string)envVariable.Key);

                Data[appKey] = (string)envVariable.Value;
            }
        }

        private string MapEnvKeyToAppKey(string envKey)
        {
            var appKey = envKey switch
            {
                "DB_CONNECTION_STRING" => "ConnectionStrings__PrimeDatabase",

                "FRONTEND_URL" => "FrontendUrl",
                "BACKEND_URL" => "BackendUrl",

                "DOCUMENT_MANAGER_URL" => "DocumentManager__Url",
                "DOCUMENT_MANAGER_CLIENT_ID" => "DocumentManager__ClientId",
                "DOCUMENT_MANAGER_CLIENT_SECRET" => "DocumentManager__ClientSecret",

                "KEYCLOAK_REALM_URL" => "PrimeKeycloak__RealmUrl",
                "KEYCLOAK_ADMINISTRATION_URL" => "PrimeKeycloak__AdministrationUrl",
                "KEYCLOAK_ADMINISTRATION_CLIENT_SECRET" => "PrimeKeycloak__AdministrationClientSecret",

                "MOH_KEYCLOAK_REALM_URL" => "MohKeycloak__RealmUrl",
                "MOH_KEYCLOAK_ADMINISTRATION_URL" => "MohKeycloak__AdministrationUrl",
                "MOH_KEYCLOAK_ADMINISTRATION_CLIENT_SECRET" => "MohKeycloak__AdministrationClientSecret",

                "MAIL_SERVER_URL" => "MailServer__Url",
                "MAIL_SERVER_PORT" => "MailServer__Port",

                "PHARMANET_API_URL" => "PharmanetApi__Url",
                "PHARMANET_API_USERNAME" => "PharmanetApi__Username",
                "PHARMANET_API_PASSWORD" => "PharmanetApi__Password",
                "PHARMANET_SSL_CERT_FILENAME" => "PharmanetApi__SslCertFilename",
                "PHARMANET_SSL_CERT_PASSWORD" => "PharmanetApi__SslCertPassword",

                "CHES_ENABLED" => "ChesApi__Enabled",
                "CHES_API_URL" => "ChesApi__Url",
                "CHES_CLIENT_SECRET" => "ChesApi__ClientSecret",
                "CHES_TOKEN_URL" => "ChesApi__TokenUrl",

                "VERIFIABLE_CREDENTIAL_API_URL" => "VerifiableCredentialApi__Url",
                "VERIFIABLE_CREDENTIAL_API_KEY" => "VerifiableCredentialApi__Key",
                "VERIFIABLE_CREDENTIAL_WEBHOOK_KEY" => "VerifiableCredentialApi__WebhookKey",

                "ADDRESS_AUTOCOMPLETE_API_URL" => "AddressAutocompleteApi__Url",
                "ADDRESS_AUTOCOMPLETE_API_KEY" => "AddressAutocompleteApi__Key",

                "METABASE_SITE_URL" => "MetabaseApi__Url",
                "METABASE_SECRET_KEY" => "MetabaseApi__Key",
                "METABASE_DASHBOARD_ID" => "MetabaseApi__DashboardId",

                "PLR_INTEGRATION_CLIENT_CERT_THUMBPRINT" => "PlrIntegration__ClientCertThumbprint",

                "LDAP_API_URL" => "LdapApi__Url",

                _ => envKey
            };

            return appKey.Replace("__", ConfigurationPath.KeyDelimiter);
        }
    }
}


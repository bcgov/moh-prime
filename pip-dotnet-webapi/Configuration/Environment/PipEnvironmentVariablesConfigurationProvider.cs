using System.Collections;


namespace Pip.Configuration.Environment
{
    public class PipEnvironmentVariablesConfigurationProvider : ConfigurationProvider
    {
        public override void Load()
        {
            Data = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            foreach (var envVariable in System.Environment.GetEnvironmentVariables().Cast<DictionaryEntry>())
            {
                var appKey = MapEnvKeyToAppKey((string)envVariable.Key);

                Data[appKey] = envVariable.Value as string;
            }
        }

        static private string MapEnvKeyToAppKey(string envKey)
        {
            var appKey = envKey switch
            {
                "DB_CONNECTION_STRING" => "ConnectionStrings__PipDatabase",

                "FRONTEND_URL" => "FrontendUrl",
                "BACKEND_URL" => "BackendUrl",

                _ => envKey
            };

            return appKey.Replace("__", ConfigurationPath.KeyDelimiter);
        }
    }
}


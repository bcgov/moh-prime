using System.IO;
using System.Linq;
using System.Reflection;

namespace Prime.Configuration.Database.Resources
{
    public static class ResourceLoader
    {
        public static string Load(string fileName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            string resourceName = assembly.GetManifestResourceNames()
                .Single(str => str.EndsWith(fileName));

            using Stream stream = assembly.GetManifestResourceStream(resourceName);
            using StreamReader reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
    }
}

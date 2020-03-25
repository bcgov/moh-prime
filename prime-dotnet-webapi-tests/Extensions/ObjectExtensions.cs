using System.Linq;

namespace PrimeTests
{
    public static class ObjectExtensions
    {
        public static void CopyPropertiesTo<T, TU>(this T source, TU dest)
        {
            var sourceProps = typeof(T).GetProperties().Where(x => x.CanRead).ToList();
            var destProps = typeof(TU).GetProperties().Where(x => x.CanWrite).ToList();

            foreach (var sourceProp in sourceProps)
            {
                var destProp = destProps.FirstOrDefault(x => x.Name == sourceProp.Name);
                if (destProp != null)
                {
                    destProp.SetValue(dest, sourceProp.GetValue(source, null), null);
                }
            }
        }
    }
}

using System.Globalization;
using System.Resources;

namespace Training.Common.Extensions
{
    public static class ResourceExtensions
    {
        private const string Unknown = "Unknown Resource";

        public static string GetSafeString(this ResourceManager resourceManager, string key)
        {
            try
            {
                var s = resourceManager.GetString(key);

                if (s != null)
                {
                    return s;
                }
            }
            catch
            {
                return Unknown;
            }

            return Unknown;
        }

        public static string GetSafeString(this ResourceManager resourceManager, string key, CultureInfo cultureInfo)
        {
            try
            {
                var s = resourceManager.GetString(key, cultureInfo);
                if (s != null)
                {
                    return s;
                }
            }
            catch
            {
                return Unknown;
            }

            return Unknown;
        }
    }
}

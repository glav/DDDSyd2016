using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServerTestApplication
{
    public static class ExtensionMethods
    {
        public static bool IsYes(this string value, string defaultValue = "y")
        {
            var testValue = string.IsNullOrWhiteSpace(value) ? defaultValue : value;
            return (testValue.ToLowerInvariant() == "y");
        }

        public static int AsNumber(this string value, int defaultValue = 0)
        {
            int realValue;
            if (int.TryParse(value, out realValue))
            {
                return realValue;
            }

            return defaultValue;
        }

        public static int GetNumberFromConfigFile(string configKey, int defaultValue = 0)
        {
            return System.Configuration.ConfigurationManager.AppSettings[configKey].AsNumber(defaultValue);
        }
        public static string GetStringFromConfigFile(string configKey, string defaultValue)
        {
            var value = System.Configuration.ConfigurationManager.AppSettings[configKey];
            return string.IsNullOrWhiteSpace(value) ? defaultValue : value;
        }

    }
}

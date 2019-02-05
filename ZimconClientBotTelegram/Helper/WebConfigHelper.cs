using System;
using System.Configuration;

namespace ZimconBotTelegram.Helper
{
    public static class WebConfigHelper
    {
        private const string ServicesEndpointKey = "zimcon.services.endpoint";
        public static string GetServicesEndpointKey => Get<string>(ServicesEndpointKey);
        // helper to extract a web config value
        private static T Get<T>(string key)
        {
            // try to convert
            try
            {
                return (T)Convert.ChangeType(ConfigurationManager.AppSettings[key], typeof(T));
            }
            catch (Exception)
            {
                // ignored
            }
            // return default
            return default(T);
        }
    }
}

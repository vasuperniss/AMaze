using System.Collections.Generic;
using System.Configuration;

namespace MazeServer.Utilities
{
    class AppSettings
    {
        private static volatile AppSettings singletonSettings = null;
        private static object syncRoot = new object();
        public static string[] settings = new string[] {"port",
                                              "rows", "cols" };

        public static AppSettings Settings
        {
            get
            {
                if (singletonSettings == null)
                {
                    lock (syncRoot)
                    {
                        if (singletonSettings == null)
                            singletonSettings = new AppSettings();
                    }
                }
                return singletonSettings;
            }
        }

        private Dictionary<string, string> settingsDict;

        private AppSettings()
        {
            this.settingsDict = new Dictionary<string, string>();
        }

        public bool ReadAllSettings(string[] settings)
        {
            foreach (string setting in settings)
            {
                string settingValue;
                if ((settingValue = this.ReadSetting(setting)) == null)
                {
                    return false;
                }
                this.settingsDict.Add(setting, settingValue);
            }
            return true;
        }

        public string this[string key]
        {
            get { return this.settingsDict[key]; }
        }

        private string ReadSetting(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string result = appSettings[key] ?? null;
                return result;
            }
            catch (ConfigurationErrorsException)
            {
                return null;
            }
        }
    }
}

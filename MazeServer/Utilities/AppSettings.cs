using System.Collections.Generic;
using System.Configuration;

namespace MazeServer.Utilities
{
    /// <summary>
    /// App Settings class - Singleton
    /// </summary>
    class AppSettings
    {
        /// <summary>
        /// The singleton Instance
        /// </summary>
        private static volatile AppSettings singletonSettings = null;

        /// <summary>
        /// The object for locking
        /// </summary>
        private static object syncRoot = new object();

        /// <summary>
        /// The settings.
        /// </summary>
        public static string[] settings = new string[] {"port",
                                              "rows", "cols" };

        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <value>
        /// The settings.
        /// </value>
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

        /// <summary>
        /// The settings dictionary.
        /// </summary>
        private Dictionary<string, string> settingsDict;

        /// <summary>
        /// Prevents a default instance of the <see cref="AppSettings"/> class from being created.
        /// </summary>
        private AppSettings()
        {
            this.settingsDict = new Dictionary<string, string>();
        }

        /// <summary>
        /// Reads all settings given in the settings string array.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>true - if all settings were read without a trouble</returns>
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

        /// <summary>
        /// Gets or sets the <see cref="System.String"/> with the specified key.
        /// </summary>
        /// <value>
        /// The <see cref="System.String"/>.
        /// </value>
        /// <param name="key">The key string of the requested setting.</param>
        /// <returns>the settings matching the key</returns>
        public string this[string key]
        {
            get { return this.settingsDict[key]; }
        }

        /// <summary>
        /// Reads the setting from the app.config file.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>the value of the setting if exists</returns>
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

﻿using System;
using System.Collections.Generic;
using System.Configuration;

namespace MazeWpfClient
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
        /// The settings dictionary
        /// </summary>
        private Dictionary<string, string> settingsDict;

        /// <summary>
        /// Gets the settings Instance.
        /// </summary>
        /// <value>
        /// The settings.
        /// </value>
        public static AppSettings Settings
        {
            get
            {
                // singleton implementation
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
        /// Adds the setting to the Instance of the AppSettings.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        internal void AddSetting(string key, string value)
        {
            if (this.settingsDict != null)
                this.settingsDict.Add(key, value);
        }

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
                    // failed to read a setting
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
            get
            {
                if (this.settingsDict.ContainsKey(key))
                    return this.settingsDict[key];
                return null;
            }
            set
            {
                this.settingsDict[key] = value;
                // change app.config file
                Configuration config =
                ConfigurationManager.OpenExeConfiguration(
                                   ConfigurationUserLevel.None);

                config.AppSettings.Settings.Remove(key);
                var kvElem = new KeyValueConfigurationElement(key, value);
                config.AppSettings.Settings.Add(kvElem);

                // Save the configuration file.
                config.Save(ConfigurationSaveMode.Modified);

                // Force a reload of a changed section.
                ConfigurationManager.RefreshSection("appSettings");
            }
        }

        /// <summary>
        /// Reads the settings from the app.config file.
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

        public static string GetSettingValue(string key)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                return settings[key].Value;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

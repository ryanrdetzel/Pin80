using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pin80Server
{
    public static class Settings
    {
        public static string ReadSetting(string settingName)
        {
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Pin80\Server")) {
                return key.GetValue(settingName, "").ToString();
            }
        }

        public static void SaveSetting(string settingName, string value)
        {
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Pin80\Server"))
            {
                key.SetValue(settingName, value);
                key.Close();
            }
        }

        public static bool ReadBoolSetting(string settingName, bool defaultSetting = false)
        {
            string value = ReadSetting(settingName);
            if (value == "")
            {
                return defaultSetting;
            }
            return bool.Parse(value);
        }

        public static void SaveBoolSetting(string settingName, bool value)
        {
            SaveSetting(settingName, value.ToString());
        }
    }
}

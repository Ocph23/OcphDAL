using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Ocph.DAL
{
    public class MyConfiguration
    {

        private Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

        private XmlDocument xmlDoc = new XmlDocument();

        private int GetDatabasePort()
        {
            string KeyName = "Port";
            int vResult = 0;

            if (this.KeyExists(KeyName))
            {
                AppSettingsReader asr = new AppSettingsReader();
                vResult = (int)asr.GetValue(KeyName, typeof(int));
            }
            return vResult;
        }



        private string GetStringServer()
        {
            string KeyName = "Server";
            string vResult = string.Empty;
            if (this.KeyExists(KeyName))
            {
                AppSettingsReader asr = new AppSettingsReader();
                vResult = (string)asr.GetValue(KeyName, typeof(string));
            }
            return vResult;
        }

        private string GetStringUser()
        {
            string KeyName = "User";
            string vResult = string.Empty;
            if (this.KeyExists(KeyName))
            {
                AppSettingsReader asr = new AppSettingsReader();
                vResult = (string)asr.GetValue(KeyName, typeof(string));
            }
            return vResult;
        }

        private string GetStringPassword()
        {
            string KeyName = "Password";
            string vResult = string.Empty;
            if (this.KeyExists(KeyName))
            {
                AppSettingsReader asr = new AppSettingsReader();
                vResult = (string)asr.GetValue(KeyName, typeof(string));
            }
            return vResult;
        }

        private string GetStringDatabase()
        {
            string KeyName = "Database";
            string vResult = string.Empty;
            if (this.KeyExists(KeyName))
            {
                AppSettingsReader asr = new AppSettingsReader();
                vResult = (string)asr.GetValue(KeyName, typeof(string));
            }
            return vResult;
        }
        private string GetStringPath()
        {
            string KeyName = "Path";
            string vResult = string.Empty;
            if (this.KeyExists(KeyName))
            {
                AppSettingsReader asr = new AppSettingsReader();
                vResult = (string)asr.GetValue(KeyName, typeof(string));
            }
            return vResult;
        }

        private string GetDivicePort()
        {
            string KeyName = "DevicePort";
            string vResult = string.Empty;
            if (this.KeyExists(KeyName))
            {
                AppSettingsReader asr = new AppSettingsReader();
                vResult = (string)asr.GetValue(KeyName, typeof(string));
            }
            return vResult;
        }

        private string GetDeviceName()
        {
            string KeyName = "DeviceName";
            string vResult = string.Empty;
            if (this.KeyExists(KeyName))
            {
                AppSettingsReader asr = new AppSettingsReader();
                vResult = (string)asr.GetValue(KeyName, typeof(string));
            }
            return vResult;
        }


        private string GetSignature()
        {
            string KeyName = "Signature";
            string vResult = string.Empty;
            if (this.KeyExists(KeyName))
            {
                AppSettingsReader asr = new AppSettingsReader();
                vResult = (string)asr.GetValue(KeyName, typeof(string));
            }
            return vResult;
        }


        public string Signature
        {
            get { return this.GetSignature(); }
        }

        public string DevicePort
        {
            get { return this.GetDivicePort(); }
        }


        public string DeviceName
        {
            get { return this.GetDeviceName(); }
        }

        public int Port
        {
            get { return GetDatabasePort(); }
        }


        public string Server
        {
            get { return GetStringServer(); }
        }

        public string Database
        {
            get { return GetStringDatabase(); }
        }

        public string User
        {
            get { return GetStringUser(); }
        }

        public string Password
        {
            get { return GetStringPassword(); }
        }

        public string Path { get { return GetStringPath(); } }

      

        public void UpdateKey(string strKey, string newValue)
        {
            if (!KeyExists(strKey))
            {
                // Add an Application Setting.
                config.AppSettings.Settings.Add(strKey, newValue);
                config.Save(ConfigurationSaveMode.Modified, true);
                // Save the configuration file.
                // Force a reload of a changed section.
                ConfigurationManager.RefreshSection("appSettings");
            }
            else
            {
                // Add an Application Setting.

                XmlDocument xmlDoc = new XmlDocument();

                xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

                foreach (XmlElement element in xmlDoc.DocumentElement)
                {
                    if (element.Name.Equals("appSettings"))
                    {
                        foreach (XmlNode node in element.ChildNodes)
                        {
                            if (node.Attributes[0].Value.Equals(strKey))
                            {
                                node.Attributes[1].Value = newValue;
                            }
                        }
                    }
                }

                xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

                ConfigurationManager.RefreshSection("appSettings");
            }
        }

        public bool KeyExists(string strKey)
        {
            bool IsExists = false;
            foreach (string item in config.AppSettings.Settings.AllKeys)
            {
                if (item == strKey)
                {
                    IsExists = true;
                }
            }
            return IsExists;
        }

    }
}

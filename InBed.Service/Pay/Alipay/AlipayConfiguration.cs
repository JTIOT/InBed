﻿
using System;
using System.Configuration;

namespace InBed.Service.Pay.Alipay
{
    /// <summary>
    /// Alipay Config
    /// </summary>
    public class AlipayConfiguration : ConfigurationSection
    {
        private static AlipayConfiguration setting;

        public static AlipayConfiguration Setting
        {
            get
            {
                if (setting == null)
                {
                    ExeConfigurationFileMap exeMap = new ExeConfigurationFileMap()
                    {
                        ExeConfigFilename = AppDomain.CurrentDomain.BaseDirectory + "/Config/AliPay.config"
                    };
                    Configuration config = ConfigurationManager.OpenMappedExeConfiguration(exeMap,
                        ConfigurationUserLevel.None);

                    setting = (AlipayConfiguration) config.GetSection("alipayConfig");
                }

                return setting;
            }
        } 

        /// <summary>
        /// Partner
        /// </summary>
        [ConfigurationProperty("Partner", IsRequired = true)]
        public string Partner
        {
            get { return (string)this["Partner"]; }
            set { this["Partner"] = value; }
        }

        /// <summary>
        /// Key
        /// </summary>
        [ConfigurationProperty("Key", IsRequired = true)]
        public string Key
        {
            get { return this["Key"].ToString(); }
            set { this["Key"] = value; }
        }

        /// <summary>
        /// Email
        /// </summary>
        [ConfigurationProperty("Email", IsRequired = true)]
        public string Email
        {
            get { return (string)this["Email"]; }
            set { this["Email"] = value; }
        }
    }
}

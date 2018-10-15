using Production.ProductionTest;
using Production.Result;
using Production.Windows;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Production
{
    class ConfigInfo
    {
        private static string configPath;    //配置文件的路径
        private static string configOfMesPath;//imes配置文件的路径
        private static string factoryId;         //
        private static string workOrderSn;    //
        private static string boxSn;    //
        private static string token;    //
        public static string ConfigPath
        {
            get
            {
                return configPath;
            }

            private set
            {
                configPath = value;
            }
        }
        public static string ConfigOfMesPath
        {
            get
            {
                return configOfMesPath;
            }

            private set
            {
                configOfMesPath = value;
            }
        }
        public static string FactoryId
        {
            get
            {
                StringBuilder stringBuilder = new StringBuilder();
                Win32API.GetPrivateProfileString("Server", "factoryId", "", stringBuilder, 256, configOfMesPath);
                try
                {
                    factoryId = stringBuilder.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                return factoryId;
            }
        }
        public static string WorkOrderSn
        {
            get
            {
                StringBuilder stringBuilder = new StringBuilder();
                Win32API.GetPrivateProfileString("Server", "workOrderSn", "", stringBuilder, 256, configOfMesPath);
                workOrderSn = stringBuilder.ToString();
                return workOrderSn;
            }
        }
        public static string BoxSn
        {
            get
            {
                StringBuilder stringBuilder = new StringBuilder();
                Win32API.GetPrivateProfileString("Server", "boxSn", "", stringBuilder, 256, configOfMesPath);
                boxSn = stringBuilder.ToString();
                return boxSn;
            }
        }
        public static string Token
        {
            get
            {
                token = Read(configOfMesPath, "token=");
                return token;

                //StringBuilder stringBuilder = new StringBuilder();
                //Win32API.GetPrivateProfileString("Server", "token", "", stringBuilder, 512, configOfMesPath);
                //token = stringBuilder.ToString();
            }
        }
        public static string Read(string path, string key)
        {
            StreamReader sr = new StreamReader(path, Encoding.Default);
            String line;
            string strValue = "";
            while ((line = sr.ReadLine()) != null)
            {
                if (line.ToString().Contains(key))
                {
                    strValue = line.Substring(key.Length, line.Length - key.Length);
                    break;
                }
            }
            sr.Dispose();
            //sr.Close();
            return strValue;
        }

        static ConfigInfo()
        {
            configPath = System.Environment.CurrentDirectory + "\\SetUp.ini";
            configOfMesPath = configPath.Replace("SetUp", "ConfigOfMes");
        }


        /// <summary>
        /// 初始化所有基础类的配置
        /// </summary>
        public static void Init()
        {
            try
            {
                ResultInfo.ReadConfig();
                ProductionInfo.ReadConfig();
                Server.HttpServerInfo.ReadConfig();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Environment.Exit(0);
            }

        }


    }
}

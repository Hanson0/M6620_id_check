using Production.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Production.Server
{
    static class HttpServerInfo
    {
        private static string fileSetup;


        private static string ip;                               //IP地址
        private static string port;                             //端口号

        private static Dictionary<string,string> urls;         //服务器URL集
        public static Dictionary<string, string> Urls
        {
            get
            {
                return urls;
            }

            set
            {
                urls = value;
            }
        }

        public static string Ip
        {
            get
            {
                return ip;
            }

            set
            {
                ip = value;
            }
        }

        public static string Port
        {
            get
            {
                return port;
            }

            set
            {
                port = value;
            }
        }

        //private static Hashtable urls = new Hashtable();         //用string作为索引器下标的时候，要用Hashtable


        /// <summary>
        /// 对服务器的操作请求
        /// </summary>
        public enum EnumUrlType
        {      
            baseInfoGet,                    //基本信息获取

            eidVerify,                      //eid合法性检查
                    
            imeiAndSnGet,                   //imei、sn获取
            eidUpload,                      //mac信息上报
            eidImeiSnUpload,                //eid、imei、sn上报(只适合imei写号工序)
            imeiSnDecorrelation,            //imei、sn去关联
            eidImeiSnIccidCheck,            //对比服务器上关联的imei、Eid、Sn、Iccid
            
            repaireIn,                      //进入维修
            repaireOut,                     //退出维修
            repaireGetCount,                //获取维修数

            packageSmallUpload,             //小包装上报
            packagSmallGet                  //小包装获取（装箱清单）
        }



        /// <summary>
        /// 读取配置文件
        /// </summary>
        public static void ReadConfig()
        {
            StringBuilder stringBuilder = new StringBuilder();
            fileSetup = ConfigInfo.ConfigPath;
            urls = new Dictionary<string, string>();

            //EndPoint
            Win32API.GetPrivateProfileString("Server", "Ip", "", stringBuilder, 256, fileSetup);
            ip = stringBuilder.ToString().Trim();
            Win32API.GetPrivateProfileString("Server", "Port", "", stringBuilder, 256, fileSetup);
            port = stringBuilder.ToString().Trim();

            //urls
            string urlPrefix = string.Format("http://{0}:{1}",ip,port);
            Win32API.GetPrivateProfileString("Server", "UrlBaseInfoGet", "", stringBuilder, 256, fileSetup);
            urls.Add(EnumUrlType.baseInfoGet.ToString(), urlPrefix + stringBuilder.ToString().Trim());
            Win32API.GetPrivateProfileString("Server", "UrlEidVeirfy", "", stringBuilder, 256, fileSetup);
            urls.Add(EnumUrlType.eidVerify.ToString(), urlPrefix + stringBuilder.ToString().Trim());

            Win32API.GetPrivateProfileString("Server", "UrlEidUpload", "", stringBuilder, 256, fileSetup);
            urls.Add(EnumUrlType.eidUpload.ToString(), urlPrefix + stringBuilder.ToString().Trim());
            Win32API.GetPrivateProfileString("Server", "UrlImeiAndSnGet", "", stringBuilder, 256, fileSetup);
            urls.Add(EnumUrlType.imeiAndSnGet.ToString(), urlPrefix + stringBuilder.ToString().Trim());
            Win32API.GetPrivateProfileString("Server", "UrlEidImeiSnUpload", "", stringBuilder, 256, fileSetup);
            urls.Add(EnumUrlType.eidImeiSnUpload.ToString(), urlPrefix + stringBuilder.ToString().Trim());
            Win32API.GetPrivateProfileString("Server", "UrlImeiSnDecorrelation", "", stringBuilder, 256, fileSetup);
            urls.Add(EnumUrlType.imeiSnDecorrelation.ToString(), urlPrefix + stringBuilder.ToString().Trim());

            //对比
            Win32API.GetPrivateProfileString("Server", "UrlEidImeiSnIccidCheck", "", stringBuilder, 256, fileSetup);
            urls.Add(EnumUrlType.eidImeiSnIccidCheck.ToString(), urlPrefix + stringBuilder.ToString().Trim());
            

            Win32API.GetPrivateProfileString("Server", "UrlRepairIn", "", stringBuilder, 256, fileSetup);
            urls.Add(EnumUrlType.repaireIn.ToString(), urlPrefix + stringBuilder.ToString().Trim());
            Win32API.GetPrivateProfileString("Server", "UrlRepairOut", "", stringBuilder, 256, fileSetup);
            urls.Add(EnumUrlType.repaireOut.ToString(), urlPrefix + stringBuilder.ToString().Trim());
            Win32API.GetPrivateProfileString("Server", "UrlRepairGetCount", "", stringBuilder, 256, fileSetup);
            urls.Add(EnumUrlType.repaireGetCount.ToString(), urlPrefix + stringBuilder.ToString().Trim());

            Win32API.GetPrivateProfileString("Server", "UrlPackageSmallUpload", "", stringBuilder, 256, fileSetup);
            urls.Add(EnumUrlType.packageSmallUpload.ToString(), urlPrefix + stringBuilder.ToString().Trim());
            Win32API.GetPrivateProfileString("Server", "UrlPackageSmallGet", "", stringBuilder, 256, fileSetup);
            urls.Add(EnumUrlType.packagSmallGet.ToString(), urlPrefix + stringBuilder.ToString().Trim());

        }


        #region Index Device

        //索引器必须以this关键字定义，其实这个this就是类实例化之后的对象
        //public string this[EnumRequestHandle index]//B索引器
        //{
        //    get
        //    {
        //        return (string)urls[index.ToString()];
        //    }

        //    set
        //    {
        //        urls.Add(index.ToString(), value);
        //    }
        //}

        #endregion
    }
}

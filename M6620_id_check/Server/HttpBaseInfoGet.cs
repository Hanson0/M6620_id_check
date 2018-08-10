using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Production.Server
{
    class HttpBaseInfoGet
    {
        private string url;
        private ResponseInfo response;

        public ResponseInfo Response
        {
            get
            {
                return response;
            }

            set
            {
                response = value;
            }
        }

        /******************************初始化**********************************/
        public HttpBaseInfoGet()
        {
            this.url = HttpServerInfo.Urls[HttpServerInfo.EnumUrlType.baseInfoGet.ToString()];
        }


        /// <summary>
        /// 向服务器获取数据并解析
        /// </summary>
        /// <param name="type"></param>
        /// <param name="data"></param>
        public int DataGetAndAnalysis (string planCode)
        {
            int ret = -1;

            //将请求数据序列化
            RequestInfo requestInfo = new RequestInfo();
            requestInfo.type = 1;       //获取基本信息的方式 1为根据计划单获取
            requestInfo.data = planCode;
            string requestStr = JsonConvert.SerializeObject(requestInfo);

            //开始请求
            HttpRequestTask task = new HttpRequestTask(url, requestStr);
            string responseStr = null;

            try
            {
                responseStr = task.GetResponse();
            }
            catch
            {
                throw;
            } 

            //解析响应数据
            response = JsonConvert.DeserializeObject(responseStr, typeof(ResponseInfo)) as ResponseInfo;

            ret = (response.code == (int)ReturnCode.执行成功) ? 0 : -1;
            return ret;
        }



        /******************************请求解析类**********************************/
        [Serializable]
        public class RequestInfo
        {
            public int type { get; set; }
            public string data { get; set; }
        }


        /******************************响应解析类**********************************/
        [Serializable]
        public class ResponseInfo
        {
            public int code { get; set; }           //返回码，200 - 成功， 其他 - 错误
            public string message { get; set; }     //信息
            public Data data { get; set; }
        }


        /// <summary>
        /// 数据部分
        /// </summary>
        [Serializable]
        public class Data
        {
            public string planCode { get; set; }                //计划单号
            public string planType { get; set; }                //计划单类型
            public string customer { get; set; }                //客户名称
            public string materialCode { get; set; }            //物料代码
            public string materialDescription { get; set; }     //物料描述(产品型号)
            public string processScheme { get; set; }           //工艺流程
            public string binName { get; set; }                 //固件名称
            public string binVersion { get; set; }              //固件版本
            public string quantity { get; set; }                //数量
        }


        ///// <summary>
        ///// 客户信息类
        ///// </summary>
        //[Serializable]
        //public class Customer
        //{
        //    public int cid { get; set; }
        //    public int code { get; set; }
        //    public string name { get; set; }
        //    public long createAt { get; set; }
        //    public long updateAt { get; set; }
        //}


        ///// <summary>
        ///// 产品信息类
        ///// </summary>
        //[Serializable]
        //public class Product
        //{
        //    public int pid { get; set; }
        //    public string code { get; set; }
        //    public string name { get; set; }
        //    public string category { get; set; }
        //    public long createAt { get; set; }
        //    public long updateAt { get; set; }
        //}


        ///// <summary>
        ///// 固件信息类
        ///// </summary>
        //[Serializable]
        //public class Firmware
        //{
        //    public int fid { get; set; }
        //    public string code { get; set; }
        //    public string name { get; set; }
        //    public string version { get; set; }
        //    public string url{ get; set; }
        //    public string md5 { get; set; }
        //    public long createAt { get; set; }
        //    public long updateAt { get; set; }
        //}


        ///// <summary>
        ///// 加工流程
        ///// </summary>
        //[Serializable]
        //public class ProcessScheme
        //{
        //    public int psid { get; set; }
        //    public string code { get; set; }
        //    public string name { get; set; }
        //    public string description { get; set; }
        //    public string listProcedure { get; set; }
        //}


        ///// <summary>
        ///// SN标准
        ///// </summary>
        //public class SnStandards
        //{
        //    public string facabbr { get; set; }
        //    public string prjcode { get; set; }
        //    public string model { get; set; }
        //    public string description { get; set; }
        //    public string pactype { get; set; }
        //}
        
    }
}

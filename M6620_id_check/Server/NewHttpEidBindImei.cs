using ExceptionHelp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Production.Server
{
    class NewHttpEidBindImei
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
        public NewHttpEidBindImei()
        {
            this.url = HttpServerInfo.Urls[HttpServerInfo.EnumUrlType.eidBindImei.ToString()];
        }


        /// <summary>
        /// 向服务器获取数据并解析
        /// </summary>
        /// <param name="eid"></param>
        /// <param name="procedure"></param>
        /// <param name="planCode"></param>
        /// <param name="station"></param>
        /// <param name="result"></param>
        /// <param name="testData"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public int DataGetAndAnalysis(out ResponseInfo2 response,string imei = null, string iccid = null, string imsi = null, string eid = null, string sn = null)
        {
            int ret = -1;

            try
            {
                //将请求数据序列化
                RequestInfo requestInfo = new RequestInfo();
                requestInfo.factoryId = ConfigInfo.FactoryId;
                requestInfo.workOrderSn = ConfigInfo.WorkOrderSn;
                requestInfo.boxSn = ConfigInfo.BoxSn;
                requestInfo.bindingParam = string.Format("{0}-{1}-{2}-{3}-{4}", imei, iccid, imsi, eid, sn);

                string requestStr = JsonConvert.SerializeObject(requestInfo);

                //开始请求
                HttpRequestTask task = new HttpRequestTask(url, requestStr);
                string responseStr;
                ret = task.GetResponse(out responseStr);

                //解析响应数据
                response = JsonConvert.DeserializeObject(responseStr, typeof(ResponseInfo2)) as ResponseInfo2;

            }
            catch (Exception ex)
            {
                throw new MyException(ex.Message);
            }

            return ret;
        }








        /******************************请求解析类**********************************/
        [Serializable]
        public class RequestInfo
        {
            public string factoryId { get; set; }
            public string workOrderSn { get; set; }
            public string boxSn { get; set; }
            public string bindingParam { get; set; }
        }


        /******************************响应解析类**********************************/
        [Serializable]
        public class ResponseInfo
        {
            public int code { get; set; }           //返回码，200 - 成功， 其他 - 错误
            public string message { get; set; }     //信息
            public Data data { get; set; }
        }
         public class ResponseInfo2
        {
            public string imei { get; set; }     //信息
            //public int code { get; set; }           //返回码，200 - 成功， 其他 - 错误
            //public string message { get; set; }     //信息
            //public Data data { get; set; }
        }


        /// <summary>
        /// 数据部分
        /// </summary>
        [Serializable]
        public class Data
        {
            public int code { get; set; }           //返回码，200 - 成功， 其他 - 错误
            public string message { get; set; }     //信息
            public string eid { get; set; }                     //EID
            public string planCode { get; set; }                //计划单号
            public int maxCount { get; set; }                   //最大数量
            public int remainCount { get; set; }                //剩余数量
        }

    }
}

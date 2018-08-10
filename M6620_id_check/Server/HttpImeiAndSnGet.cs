using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Production.Server
{
    class HttpImeiAndSnGet
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
        public HttpImeiAndSnGet()
        {
            this.url = HttpServerInfo.Urls[HttpServerInfo.EnumUrlType.imeiAndSnGet.ToString()];
        }


        /// <summary>
        /// 向服务器获取数据并解析
        /// </summary>
        /// <param name="station"></param>
        /// <param name="planCode"></param>
        /// <returns></returns>
        public int DataGetAndAnalysis(string station, string planCode)
        {
            int ret = -1;

            //将请求数据序列化
            RequestInfo requestInfo = new RequestInfo();
            //requestInfo.eid = eid;
            requestInfo.station = station;
            requestInfo.planCode = planCode;
            string requestStr = JsonConvert.SerializeObject(requestInfo);

            //开始请求
            HttpRequestTask task = new HttpRequestTask(url, requestStr);
            string responseStr = task.GetResponse();

            //解析响应数据
            response = JsonConvert.DeserializeObject(responseStr, typeof(ResponseInfo)) as ResponseInfo;

            ret = (response.code == (int)ReturnCode.执行成功) ? 0 : -1;
            return ret;
        }



        /******************************请求解析类**********************************/
        [Serializable]
        public class RequestInfo
        {
            public string eid { get; set; }
            public string station { get; set; }
            public string planCode { get; set; }
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
            public string imei { get; set; }                     //IMEI
            public string sn { get; set; }                      //SN
            public int maxCount { get; set; }                   //最大数量
            public int remainCount { get; set; }                //剩余数量
        }
    }
}

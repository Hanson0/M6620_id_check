using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Production.Server
{
    class HttpPackageSmallGet
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
        public HttpPackageSmallGet()
        {
            this.url = HttpServerInfo.Urls[HttpServerInfo.EnumUrlType.packagSmallGet.ToString()];
        }


        public int DataGetAndAnalysis(string numberSmall, string planCode)
        {
            int ret = -1;

            //将请求数据序列化
            RequestInfo requestInfo = new RequestInfo();
            requestInfo.cn = numberSmall;
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
            public string cn { get; set; }
            public string planCode { get; set; }

        }


        /******************************响应解析类**********************************/
        [Serializable]
        public class ResponseInfo
        {
            public int code { get; set; }           //返回码，200 - 成功， 其他 - 错误
            public string message { get; set; }     //信息
            public List<Data> data { get; set; }
        }


        /// <summary>
        /// 数据部分
        /// </summary>
        [Serializable]
        public class Data
        {
            public string sn { get; set; }                      //SN
            public string imei { get; set; }                    //IMEI
            public string eid { get; set; }                     //EID
            public string iccid { get; set; }                   //ICCID
        }
    }
}

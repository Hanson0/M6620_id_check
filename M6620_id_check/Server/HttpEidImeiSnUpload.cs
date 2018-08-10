using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Production.Server
{
    class HttpEidImeiSnUpload
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
        public HttpEidImeiSnUpload()
        {
            this.url = HttpServerInfo.Urls[HttpServerInfo.EnumUrlType.eidImeiSnUpload.ToString()];
        }


        /// <summary>
        /// 向服务器获取数据并解析
        /// </summary>
        /// <param name="eid"></param>
        /// <param name="imei"></param>
        /// <param name="sn"></param>
        /// <param name="procedure"></param>
        /// <param name="planCode"></param>
        /// <param name="station"></param>
        /// <param name="result"></param>
        /// <param name="testData"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public int DataGetAndAnalysis(string eid, string imei,string sn,string iccid,string procedure, 
            string planCode, string station , int result, string testData, string log)
        {
            int ret = -1;

            //将请求数据序列化
            RequestInfo requestInfo = new RequestInfo();
            requestInfo.eid = eid;
            requestInfo.imei = imei;
            requestInfo.sn = sn;
            requestInfo.iccid = iccid;
            requestInfo.procedure = procedure;
            requestInfo.planCode = planCode;
            requestInfo.station = station;
            requestInfo.result = result;
            requestInfo.testData = testData;
            requestInfo.log = log;
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
            public string imei { get; set; }
            public string sn { get; set; }
            public string iccid { get; set; }
            public string procedure { get; set; }
            public string planCode { get; set; }
            public string station { get; set; }
            public int result { get; set; }
            public string testData { get; set; }
            public string log { get; set; }
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
            //调用upload服务器会返回重新verify的结果
            public string code { get; set; }                    //verify返回码
            public string message { get; set; }                 //verify信息
            public string eid { get; set; }                     //EID
            public string planCode { get; set; }                //计划单号
            public int maxCount { get; set; }                   //最大数量
            public int remainCount { get; set; }                //剩余数量
        }
    }
}

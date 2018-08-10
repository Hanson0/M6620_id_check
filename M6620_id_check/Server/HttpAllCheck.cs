using Newtonsoft.Json;
using Production.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace M6220_id_check.Server
{
    class HttpAllCheck
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
        public HttpAllCheck()
        {
            this.url = HttpServerInfo.Urls[HttpServerInfo.EnumUrlType.eidImeiSnIccidCheck.ToString()];
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
        public int DataGetAndAnalysis(string planCode, string eid, string imei, string sn, string iccid)
        {
            int ret = -1;

            //将请求数据序列化
            RequestInfo requestInfo = new RequestInfo();
            requestInfo.planCode = planCode;
            requestInfo.eid = eid;
            requestInfo.imei = imei;
            requestInfo.sn = sn;
            requestInfo.iccid = iccid;
            string requestStr = JsonConvert.SerializeObject(requestInfo);

            //开始请求
            HttpRequestTask task = new HttpRequestTask(url, requestStr);
            string responseStr = task.GetResponse();

            //解析响应数据
            response = JsonConvert.DeserializeObject(responseStr, typeof(ResponseInfo)) as ResponseInfo;

            ret = (response.code == "1000") ? 0 : -1;//(int)ReturnCode.执行成功
            return ret;
        }



        /******************************请求解析类**********************************/
        [Serializable]
        public class RequestInfo
        {
            public string planCode { get; set; }

            public string eid { get; set; }
            public string imei { get; set; }
            public string sn { get; set; }
            public string iccid { get; set; }
        }


        /******************************响应解析类**********************************/
        [Serializable]
        public class ResponseInfo
        {
            public string code { get; set; }           //返回码，200 - 成功， 其他 - 错误
            public string message { get; set; }     //信息
            public Data data { get; set; }
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

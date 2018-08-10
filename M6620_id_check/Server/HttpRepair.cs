using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Production.Server
{
    class HttpRepair
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

        public enum RepaireType
        {
            In,                         //进入维修
            Out,                        //退出维修
            GetCount,                   //获取维修数量
        }

        /******************************初始化**********************************/
        public HttpRepair(RepaireType type)
        {
            string urlType = null;
            switch (type)
            {
                case RepaireType.In:
                    urlType = HttpServerInfo.EnumUrlType.repaireIn.ToString();
                    break;
                case RepaireType.Out:
                    urlType = HttpServerInfo.EnumUrlType.repaireOut.ToString();
                    break;
                case RepaireType.GetCount:
                    urlType = HttpServerInfo.EnumUrlType.repaireGetCount.ToString();
                    break;
                default:
                    break;
            }

            this.url = HttpServerInfo.Urls[urlType];
        }


        /// <summary>
        /// 向服务器获取数据并解析
        /// </summary>
        /// <param name="eid"></param>
        /// <param name="planCode"></param>
        /// <param name="station"></param>
        /// <param name="testData"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public int DataGetAndAnalysis(string eid,string planCode, int station,
            string testData, string log)
        {
            int ret = -1;

            //将请求数据序列化
            RequestInfo requestInfo = new RequestInfo();
            requestInfo.eid = eid;
            requestInfo.planCode = planCode;
            requestInfo.station = station;
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
            public string planCode { get; set; }
            public int station { get; set; }
            public string testData { get; set; }
            public string log { get; set; }
        }


        /******************************响应解析类**********************************/
        [Serializable]
        public class ResponseInfo
        {
            public int code { get; set; }           //返回码，200 - 成功， 其他 - 错误
            public string message { get; set; }     //信息
            public int data { get; set; }
        }

    }
}

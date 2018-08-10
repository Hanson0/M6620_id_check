using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Production.Server
{
    class HttpRequestTask
    {
        private string url;
        private EnumHttpRequestMethod httpRequestMethod;
        private string postStr;
        public enum EnumHttpRequestMethod
        {
            GET,
            POST,
        }


        /// <summary>
        /// GET请求
        /// </summary>
        /// <param name="url"></param>
        public HttpRequestTask(string url)
        {
            this.url = url;
            this.httpRequestMethod = EnumHttpRequestMethod.GET;
        }


        /// <summary>
        /// POST请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postStr"></param>
        public HttpRequestTask(string url,string postStr)
        {
            this.url = url;
            this.httpRequestMethod = EnumHttpRequestMethod.POST;
            this.postStr = postStr;
        }


        /// <summary>
        /// 获取请求响应数据
        /// </summary>
        /// <returns></returns>
        public string GetResponse()
        {
            string responseStr;

            HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
            request.ContentType = "application/json";
            request.Method = httpRequestMethod.ToString();
            request.ProtocolVersion = new Version(1, 1);

            switch (httpRequestMethod)
            {
                case EnumHttpRequestMethod.GET:

                    break;
                case EnumHttpRequestMethod.POST:
                    byte[] requestInfoBytes = Encoding.UTF8.GetBytes(postStr);
                    request.ContentLength = requestInfoBytes.Length;
                    request.GetRequestStream().Write(requestInfoBytes, 0, requestInfoBytes.Length);
                    break;
                default:
                    break;
            }

            try
            {
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(stream, Encoding.UTF8))
                    {
                        responseStr = sr.ReadToEnd();
                        int start = responseStr.IndexOf("{");
                        int end = responseStr.LastIndexOf("}");
                        int length = end - start + 1;
                        responseStr = responseStr.Substring(start, length);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return responseStr;
        }

    }
}

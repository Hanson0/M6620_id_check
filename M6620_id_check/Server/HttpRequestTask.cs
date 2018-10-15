using ExceptionHelp;
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
        public int GetResponse(out string resp)
        {
            int ret = -1;
            resp = "";
            string responseStr;

            //HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
            //request.ContentType = "application/json";
            //request.Method = httpRequestMethod.ToString();
            //request.ProtocolVersion = new Version(1, 1);

            HttpWebRequest httpWebRequest = HttpWebRequest.Create(url) as HttpWebRequest;
            //HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = httpRequestMethod.ToString();
            httpWebRequest.ServicePoint.Expect100Continue = false;
            httpWebRequest.Timeout = 60000;
            //httpWebRequest.ProtocolVersion = new Version(1, 1);
            //string header = token;
#if UnitTest
            string header = "YTRlMmY4ZGRmNmE4MTFlNzk0MmMwMDUwNTZiMTRhZDYtNWUyM2IwNGVkZGZkNGM4NzlmNzZiMzE4YmZhNjg1NjM=";
#else
            string header = ConfigInfo.Token;
#endif
            if (header != null && !header.Equals(""))
            {
                httpWebRequest.Headers.Add("X-AUTH-TOKEN", header);
            }


            switch (httpRequestMethod)
            {
                case EnumHttpRequestMethod.GET:

                    break;
                case EnumHttpRequestMethod.POST:
                    byte[] requestInfoBytes = Encoding.UTF8.GetBytes(postStr);
                    httpWebRequest.ContentLength = requestInfoBytes.Length;
                    httpWebRequest.GetRequestStream().Write(requestInfoBytes, 0, requestInfoBytes.Length);
                    break;
                default:
                    break;
            }

            try
            {
                HttpWebResponse response = httpWebRequest.GetResponse() as HttpWebResponse;

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return ret;
                }
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(stream, Encoding.UTF8))
                    {
                        responseStr = sr.ReadToEnd();
                        int start = responseStr.IndexOf("{");
                        int end = responseStr.LastIndexOf("}");
                        int length = end - start + 1;
                        responseStr = responseStr.Substring(start, length);

                        ret = 0;
                        resp = responseStr;
                    }
                }
            }
            catch (WebException  ex)
            {
                WebResponse webRep = ex.Response;
                StreamReader streamReader = new StreamReader(webRep.GetResponseStream());
                string errorInfo = streamReader.ReadToEnd();
                ret = -1;
                throw new MyException(errorInfo);
            }

            return ret;
        }
    }
}

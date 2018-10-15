using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Production.Server
{
    class ToImesInterface
    {
        //private AllForms.FormMain frmMain;

        //public ToImesInterface(AllForms.FormMain frmMain)
        //{
        //    this.frmMain = frmMain;
        //}
        /// <summary>
        /// imei信息下拉
        /// </summary>
        /// <param name="result"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public static int ImeiPrint(out Production.Server.NewHttpImeiPrint.ResponseInfo rep, string imei ,out string errorInfo)//int result, string log
        {
            int ret = -1;
            errorInfo = "";
            rep = null;
            NewHttpImeiPrint httpImeiPrint = new NewHttpImeiPrint();
            //Production.Server.NewHttpImeiPrint.ResponseInfo response;
            try
            {
                ret = httpImeiPrint.DataGetAndAnalysis(out rep, imei, null, null, null, null);
                //if (ret != 0)
                //{
                //    //frmMain.DisplayLog(((ReturnCode)httpEidImeiSnUpload.Response.code).ToString());
                //    frmMain.DisplayLog("下拉信息失败\r\n");
                //    //frmMain.DisplayLog(httpEidUpload.Response.data.message + "\r\n");
                //}
                //else
                //{
                //    frmMain.DisplayLog(string.Format("{0}\r\n", "下拉成功"));
                //    ret = 0;
                //}
            }
            catch (Exception ex)
            {
                errorInfo = ex.Message;
            }

            return ret;
        }

        //public static int ImeiBind(out Production.Server.NewHttpImeiPrint.ResponseInfo rep, string eid, out string errorInfo)//int result, string log
        //{

        //}
    }
}

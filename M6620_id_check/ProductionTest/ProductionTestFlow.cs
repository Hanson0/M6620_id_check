using M6220_id_check.Server;
using Production.IdReadWrite;
using Production.IdReadWrite.Cmd;
using Production.Server;
using Production.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Production.ProductionTest
{
    class ProductionTestFlow
    {

        private AllForms.FormMain frmMain;
        private ReadWriteIdHandle readWriteIdHandle;
        private static string configPath = ConfigInfo.ConfigPath;            //配置文件路径


        private string planCode;
        private string procedure;
        private string station;

        private string eidRead;
        private string iccidRead;
        private string imeiRead;
        private string snRead;
        private string versonRead;


        private string labelImei;
        //private static int eidlast = 0x11;

        private bool runState;       //运行状态     true为运行中，false为未运行
        private bool flagCreateNewRow;
        private static int compareEidIccid;
        private static string versonIni;
        private int judgeStandard;

        public static string VersonIni
        {
            get
            {
                StringBuilder stringBuilder = new StringBuilder();
                Win32API.GetPrivateProfileString("Key", "Version", "", stringBuilder, 256, configPath);
                versonIni = stringBuilder.ToString();
                return versonIni;
            }
        }


        public static int CompareEidIccid
        {
            get
            {
                StringBuilder stringBuilder = new StringBuilder();
                Win32API.GetPrivateProfileString("Compare", "CompareEidIccid", "", stringBuilder, 256, configPath);
                compareEidIccid = int.Parse(stringBuilder.ToString());
                return compareEidIccid;

            }
        }

        public bool RunState
        {
            get
            {
                return runState;
            }

            set
            {
                runState = value;
            }
        }

        public string PlanCode
        {
            get
            {
                return planCode;
            }

            set
            {
                planCode = value;
            }
        }
        public int JudgeStandard
        {
            get
            {
                StringBuilder stringBuilder = new StringBuilder();
                Win32API.GetPrivateProfileString("Compare", "JudgeStandard", "", stringBuilder, 256, configPath);
                judgeStandard = int.Parse(stringBuilder.ToString());
                return judgeStandard;

            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="frmMain"></param>
        /// <param name="planCode"></param>
        /// <param name="procedure"></param>
        /// <param name="station"></param>
        public ProductionTestFlow(AllForms.FormMain frmMain, string planCode, string procedure, string station)
        {
            this.frmMain = frmMain;
            this.planCode = planCode;
            this.procedure = procedure;
            this.station = station;

            runState = false;
            readWriteIdHandle = new ReadWriteIdHandle(frmMain);

            InitTimeOutTimer();
        }

        private System.Timers.Timer timeoutTimer = new System.Timers.Timer();

        private void InitTimeOutTimer()
        {
            timeoutTimer.AutoReset = false;
            timeoutTimer.Interval = ProductionInfo.TimeOut;
            timeoutTimer.Enabled = true;
            timeoutTimer.Elapsed += TimeoutTimer_Elapsed;
        }

        private void TimeoutTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            readWriteIdHandle.IsTimeOut = true;
        }


        /// <summary>
        /// 任务入口及任务流执行后处理
        /// </summary>
        public void TestTaskMain(string labelImei)
        {
            this.labelImei = labelImei;
            runState = true;
            flagCreateNewRow = false;

            readWriteIdHandle.FlagDisplayUart = true;

            //执行流程
            int ret = TestTaskFlow();

            //frmMain.DisplayLog(string.Format("标签IMEI：{0}\r\n", labelImei));
            //结果判断
            Result.ResultJudge resultJudge = Result.ResultJudge.GetResultJudge(frmMain);
            if (ProductionInfo.Type == ProductionInfo.SystemType.iMES || ProductionInfo.Type == ProductionInfo.SystemType.GSMMES)
            {
                resultJudge.Imei = labelImei;
                resultJudge.Sn = snRead;
                resultJudge.Eid = eidRead;
                resultJudge.Iccid = iccidRead;
            }
            resultJudge.PutResult(eidRead, ret);

            runState = false;
            readWriteIdHandle.FlagDisplayUart = false;
        }


        /// <summary>
        /// 任务流
        /// </summary>
        /// <returns></returns>
        private int TestTaskFlow()
        {
            int ret = -1;
            string pattern = null;
            eidRead = "NULL";

            if (readWriteIdHandle.IsTimeOut)
            {
                frmMain.DisplayLog("测试超时\r\n");
                return ret;
            }

            ProductionInfo.SystemType systemType = ProductionInfo.Type;

            //使用do..while(false)的原因，是为了当测试流程为fail时，使用break跳出该结构，仍然执行结果上报
            do
            {
                //从芯片获取eid
                //eidRead = "081603FFFFF35293" + eidlast.ToString("X").PadLeft(4,'0');
                //int readRet = 1;
                eidRead = readWriteIdHandle.ReadId(ATReadCmd.ReadIdType.EidRead);
                pattern = @"[0-9A-Z]{20}";
                if (string.IsNullOrEmpty(eidRead) || !Regex.IsMatch(eidRead, pattern))
                {
                    frmMain.DisplayLog("EID读取失败\r\n");
                    break;
                }
                frmMain.DisplayLog(string.Format("已获取模块EID：{0}\r\n", eidRead));
                frmMain.SetText(AllForms.EnumControlWidget.txtEid.ToString(), eidRead, false);


                //从芯片读出SN
                snRead = readWriteIdHandle.ReadId(ATReadCmd.ReadIdType.SnRead);
                pattern = @"^[0-9A-Z]{16}$";
                if (string.IsNullOrEmpty(snRead))
                {
                    frmMain.DisplayLog("SN为空读取失败\r\n");
                    break;
                }
                if (!Regex.IsMatch(snRead, pattern))
                {
                    frmMain.DisplayLog("SN合法性检查:SN不合法败\r\n");
                    break;
                }
                frmMain.DisplayLog(string.Format("已获取模块合法SN：{0}\r\n", snRead));
                frmMain.SetText(AllForms.EnumControlWidget.txtSn.ToString(), snRead, false);

                //从芯片读出IMEI
                imeiRead = readWriteIdHandle.ReadId(ATReadCmd.ReadIdType.ImeiRead);
                pattern = @"\d{15}";
                if (string.IsNullOrEmpty(imeiRead) || !Regex.IsMatch(imeiRead, pattern))
                {
                    frmMain.DisplayLog("IMEI读取失败\r\n");
                    break;
                }
                frmMain.DisplayLog(string.Format("已获取模块IMEI：{0}\r\n", imeiRead));
                frmMain.SetText(AllForms.EnumControlWidget.txtImei.ToString(), imeiRead, false);


                //从芯片获取iccid
                iccidRead = readWriteIdHandle.ReadId(ATReadCmd.ReadIdType.IccidRead);
                pattern = @"[0-9A-Z]{20}";
                if (string.IsNullOrEmpty(iccidRead) || !Regex.IsMatch(iccidRead, pattern))
                {
                    frmMain.DisplayLog("ICCID读取失败\r\n");
                    break;
                }
                frmMain.DisplayLog(string.Format("已获取模块ICCID：{0}\r\n", iccidRead));
                frmMain.SetText(AllForms.EnumControlWidget.txtIccid.ToString(), iccidRead, false);


                //从芯片获取Verson
                versonRead = readWriteIdHandle.ReadId(ATReadCmd.ReadIdType.VersonRead);
                if (string.IsNullOrEmpty(iccidRead))
                {
                    frmMain.DisplayLog("版本号读取失败 FAIL\r\n");
                    break;
                }
                frmMain.DisplayLog(string.Format("已获取版本号：{0}\r\n", versonRead));
                if (versonRead != VersonIni.ToUpper())
                {
                    frmMain.DisplayLog(string.Format("读出的版本号:{0} 与 配置的版本号:{1} 不一致 FAIL\r\n", versonRead, VersonIni));
                    break;
                    //ret = -1;
                    //return ret;
                }
                else
                {
                    frmMain.DisplayLog(string.Format("读出的版本号:{0} 与 配置的版本号:{1} 一致 PASS\r\n", versonRead, VersonIni));
                }


                //判断IMEI与标签IMEI是否一致
                if (labelImei == imeiRead)
                {
                    frmMain.DisplayLog(string.Format("标签IMEI：{0}与模块IMEI：{1}对比一致 PASS\r\n", labelImei, imeiRead));
                }
                else
                {
                    frmMain.DisplayLog(string.Format("标签IMEI：{0}与模块IMEI：{1}对比不一致 FAIL\r\n", labelImei, imeiRead));
                    break;
                }

                //eid与iccid的后6位对比
                if (CompareEidIccid == 1)//要对比
                {
                    string lastSixIccid = iccidRead.Substring(iccidRead.Length - 6);
                    string lastSixEid = eidRead.Substring(eidRead.Length - 6);
                    if (JudgeStandard==0)
                    {
                        //一样 FAIL
                        if (lastSixIccid == lastSixEid)
                        {
                            frmMain.DisplayLog(string.Format("空中写号检查：未做。详细：后6位的eid:{0} 与 iccid:{1} 完全一致 FAIL\r\n", lastSixEid, lastSixIccid));
                            break;
                            //ret = -1;
                            //return ret;
                        }
                        else
                        {
                            frmMain.DisplayLog(string.Format("空中写号检查：通过。详细：后6位的eid:{0} 与 iccid:{1}不一致 PASS\r\n", lastSixEid, lastSixIccid));
                        }
                    }
                    else if (JudgeStandard ==1)
                    {
                        //一样 PASS
                        if (lastSixIccid != lastSixEid)
                        {
                            frmMain.DisplayLog(string.Format("空中写号检查：未做。详细：后6位的eid:{0} 与 iccid:{1} 不一致 FAIL\r\n", lastSixEid, lastSixIccid));
                            break;
                            //ret = -1;
                            //return ret;
                        }
                        else
                        {
                            frmMain.DisplayLog(string.Format("空中写号检查：通过。详细：后6位的eid:{0} 与 iccid:{1}完全一致 PASS\r\n", lastSixEid, lastSixIccid));
                        }

                    }
                }
                //离线查重IMEI
                if (ProductionInfo.Type == ProductionInfo.SystemType.Offline)
                {

                    JiaHao.ExcelHelp.ExcelHelper excel = JiaHao.ExcelHelp.ExcelHelper.GetExcelHelperInstance();
                    ret = excel.SearchImei(imeiRead);
                    if (ret != 0)
                    {
                        frmMain.DisplayLog(string.Format("IMEI查重: {0} 重复 FAIL\r\n", imeiRead));
                        return ret;
                    }
                    else
                    {
                        frmMain.DisplayLog(string.Format("IMEI查重: {0} 本机查重 PASS\r\n", imeiRead));
                    }
                }

                #region
                if (systemType == ProductionInfo.SystemType.iMES)
                {
                    //调用打印接口下拉eid相关信息rep，为了生成Log作准备
                    Production.Server.NewHttpImeiPrint.ResponseInfo rep;
                    string errorInfo;
                    ret = ToImesInterface.ImeiPrint(out rep, imeiRead, out errorInfo);
                    //ImeiPrint(out rep);
                    if (ret != 0)
                    {
                        //显示结果：下拉失败,并改变颜色
                        frmMain.DisplayLog("从IMES下拉信息失败" + errorInfo + "\r\n");
                        break;
                    }
                    frmMain.DisplayLog("从IMES下拉信息成功\r\n");
                    //模组读出的和IMES下拉的是否相同
                    //IMEI,PUBLICSN,ICCID,IMSI,EID,IMEIBINDINGSN,DATE
                    //"sn":123456789012345,AI18082202000001,,,,,8822
                    string[] idNumber = rep.sn.ToUpper().Split(',');
                    #region 比对
                    #region IMEI
                    if (idNumber[0] == "")
                    {
                        frmMain.DisplayLog(string.Format("服务器IMEI获取为空 FAIL\r\n"));
                        ret = -1;
                        break;
                    }
                    if (idNumber[0] != imeiRead.ToUpper())
                    {
                        frmMain.DisplayLog(string.Format("服务器IMEI:{0} 与 模块IMEI:{1} 不一致 FAIL\r\n", idNumber[0], imeiRead));
                        ret = -1;
                        break;
                    }
                    else
                    {
                        frmMain.DisplayLog(string.Format("服务器IMEI:{0} 与 模块IMEI:{1} 一致 PASS\r\n", idNumber[0], imeiRead));
                    }
                    #endregion
                    #region SN
                    if (idNumber[1] != snRead.ToUpper())
                    {
                        frmMain.DisplayLog(string.Format("服务器SN:{0} 与 模块SN:{1} 不一致 FAIL\r\n", idNumber[1], snRead));
                        ret = -1;
                        break;
                    }
                    else
                    {
                        frmMain.DisplayLog(string.Format("服务器SN:{0} 与 模块SN:{1} 一致 PASS\r\n", idNumber[1], snRead));
                    }
                    #endregion

                    #region ICCID
                    if (idNumber[2] != iccidRead.ToUpper())
                    {
                        frmMain.DisplayLog(string.Format("服务器ICCID:{0} 与 模块ICCID:{1} 不一致 FAIL\r\n", idNumber[2], iccidRead));
                        ret = -1;
                        break;
                    }
                    else
                    {
                        frmMain.DisplayLog(string.Format("服务器ICCID:{0} 与 模块ICCID:{1} 一致 PASS\r\n", idNumber[2], iccidRead));
                    }
                    #endregion

                    #region EID
                    if (idNumber[4] != eidRead.ToUpper())
                    {
                        frmMain.DisplayLog(string.Format("服务器EID:{0} 与 模块EID:{1} 不一致 FAIL\r\n", idNumber[4], eidRead));
                        ret = -1;
                        break;
                    }
                    else
                    {
                        frmMain.DisplayLog(string.Format("服务器EID:{0} 与 模块EID:{1} 一致 PASS\r\n", idNumber[4], eidRead));
                    }
                    #endregion

                    #endregion
                }

                if (systemType == ProductionInfo.SystemType.GSMMES)
                {
                    //EID合法性检查
                    ret = EidVerify();
                    if (ret != 0)
                    {
                        break;
                    }
                }
                #endregion

                if (systemType == ProductionInfo.SystemType.GSMMES)
                {
                    //EID合法性检查
                    ret = CheckAllNumber();
                    if (ret != 0)//失败就直接上报测试结果
                    {
                        break;
                    }
                }


            } while (false);

            if (systemType == ProductionInfo.SystemType.GSMMES)
            {
                frmMain.DisplayLog("测试数据上报服务器\r\n");
                //EID/IMEI/SN上报服务器
                int resultUpload = ret == 0 ? 1 : 2;
                string testDada = string.Empty;
                string log = frmMain.ReadLog();

                ret = EidUpload(resultUpload, log);
                if (ret != 0)
                {
                    frmMain.DisplayLog("过站失败\r\n");
                    return ret;
                }
            }
            #region 离线 记录到Excel
            if (ProductionInfo.Type == ProductionInfo.SystemType.Offline)
            {
                if (ret == 0)
                {
                    JiaHao.ExcelHelp.ExcelHelper excel = JiaHao.ExcelHelp.ExcelHelper.GetExcelHelperInstance();
                    excel.ExportExcelOneByOne(new JiaHao.ExcelHelp.ExcelHelper.TrayInfoInExcel()
                    {
                        num = 1,
                        sn = snRead,
                        imei = imeiRead,
                        eid = eidRead,
                        iccid = iccidRead,
                        cuatomerName = "中移物联"
                    });
                    frmMain.DisplayLog("写入Excel成功\r\n");
                }
            }
            #endregion
            return ret;
        }

        /// <summary>
        /// imei绑定
        /// </summary>
        /// <param name="result"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        private int EidBindImei(out Production.Server.NewHttpEidBindImei.ResponseInfo2 rep)//int result, string log
        {
            int ret = -1;
            rep = null;
            NewHttpEidBindImei httpEidUpload = new NewHttpEidBindImei();
            try
            {
                ret = httpEidUpload.DataGetAndAnalysis(out rep, null, null, null, eidRead, null);
                if (ret != 0)
                {
                    //frmMain.DisplayLog(((ReturnCode)httpEidImeiSnUpload.Response.code).ToString());
                    frmMain.DisplayLog("绑定失败\r\n");
                    //frmMain.DisplayLog(httpEidUpload.Response.data.message + "\r\n");
                }
                else
                {
                    frmMain.DisplayLog(string.Format("{0}\r\n", "绑定成功"));
                    ret = 0;
                }
            }
            catch (Exception ex)
            {
                frmMain.DisplayLog(ex.ToString() + "\r\n");
            }

            return ret;
        }




        public void StartTimeoutTimer()
        {
            readWriteIdHandle.IsTimeOut = false;
            timeoutTimer.Start();
        }

        public void StopTimeOutTimer()
        {
            timeoutTimer.Stop();
        }


        /// <summary>
        /// 检测模块上电
        /// </summary>
        public void CheckModulePowerOn()
        {
            readWriteIdHandle.CheckModulePowerOn();
        }


        /// <summary>
        /// 检测模块掉电
        /// </summary>
        public void CheckModulePowerOff()
        {
            readWriteIdHandle.CheckModulePowerOff();
        }


        /// <summary>
        /// SN/IMEI合法性判断
        /// </summary>
        /// <returns></returns>
        private int SnImeiFromServerLegalityCheck()
        {
            int ret = -1;

            //AA BB CC DDD E FF GGGG
            //AA:工厂缩写                   \S{2}
            //BB:项目编号                   \d{2}
            //CC:具体项目的型号            \d{2}
            //DDD:  D:年（0-9A-K）         [\dA-K]{1}
            //      D:月（0-9A-C）         [\dA-C]{1}
            //      D:日（0-9A-W）         [\dA-W]{1}
            //E:规范是产品还是大纸箱，默认0为产品，C表示大纸箱    [0C]{1}
            //FF:产线执行生产流水拉号，可以按照01、02、03、....来定义，这个由工厂自定    \d{2}
            //GGGG:生产序列号，从1到9999    \d{4}
            string patternSn = @"^\S{2}\d{2}\d{2}[\dA-W]{1}[\dA-C]{1}[\dA-W]{1}[0C]{1}\d{2}\d{4}$";
            if (string.IsNullOrEmpty(snRead) || !Regex.IsMatch(snRead, patternSn))
            {
                frmMain.DisplayLog(string.Format("SN:{0} 不合法\r\n", snRead));
                return ret;
            }

            string patternImei = @"\d{15}";
            if (string.IsNullOrEmpty(imeiRead) || !Regex.IsMatch(imeiRead, patternImei))
            {
                frmMain.DisplayLog(string.Format("IMEI:{0} 不合法\r\n", imeiRead));
                return ret;
            }

            ret = 0;
            return ret;
        }


        /// <summary>
        /// EID合法性检查
        /// </summary>
        /// <param name="eid"></param>
        /// <returns></returns>
        private int EidVerify()
        {
            int ret = -1;

            //从服务器查询该EID是否合法
            HttpEidVerify httpEidVerify = new HttpEidVerify();
            try
            {
                ret = httpEidVerify.DataGetAndAnalysis(eidRead, procedure, planCode);
                if (ret != 0)
                {
                    //frmMain.DisplayLog(((ReturnCode)httpEidVerify.Response.code).ToString());
                    frmMain.DisplayLog(httpEidVerify.Response.message + "\r\n");
                }
                else
                {
                    frmMain.DisplayLog(string.Format("{0}\r\n", "Eid合法性检查通过"));
                    ret = 0;
                }
            }
            catch (Exception ex)
            {
                frmMain.DisplayLog(ex.ToString() + "\r\n");
            }

            return ret;
        }


        /// <summary>
        /// EID上报
        /// </summary>
        /// <returns></returns>
        private int EidUpload(int result, string log)
        {
            int ret = -1;

            HttpEidUpload httpEidUpload = new HttpEidUpload();
            try
            {
                ret = httpEidUpload.DataGetAndAnalysis(eidRead, procedure, planCode, station, result, "", log);
                if (ret != 0)
                {
                    //frmMain.DisplayLog(((ReturnCode)httpEidImeiSnUpload.Response.code).ToString());
                    frmMain.DisplayLog(httpEidUpload.Response.message + "\r\n");
                    frmMain.DisplayLog(httpEidUpload.Response.data.message + "\r\n");
                }
                else
                {
                    frmMain.DisplayLog(string.Format("{0}\r\n", "EID上报服务器成功"));
                    ret = 0;
                }
            }
            catch (Exception ex)
            {
                frmMain.DisplayLog(ex.ToString() + "\r\n");
            }

            return ret;
        }


        /// <summary>
        /// EID/IMEI/SN上报
        /// </summary>
        /// <returns></returns>
        //private int EidImeiSnUpload(int result,string testData,string log)
        //{
        //    int ret = -1;

        //    HttpEidImeiSnUpload httpEidImeiSnUpload = new HttpEidImeiSnUpload();
        //    try
        //    {
        //        ret = httpEidImeiSnUpload.DataGetAndAnalysis(eidRead, imeiRead,snRead,iccidRead,
        //            procedure,planCode, station, result, testData,log);
        //        if (ret != 0)
        //        {
        //            //frmMain.DisplayLog(((ReturnCode)httpEidImeiSnUpload.Response.code).ToString());
        //            frmMain.DisplayLog(httpEidImeiSnUpload.Response.message + "\r\n");
        //        }
        //        else
        //        {
        //            frmMain.DisplayLog(string.Format("{0}\r\n", "EID、IMEI、SN上报服务器成功"));
        //            ret = 0;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        frmMain.DisplayLog(ex.ToString() + "\r\n");
        //    }

        //    return ret;
        //}


        /// <summary>
        /// IMSI/SN去关联
        /// </summary>
        /// <returns></returns>
        private int ImeiSnDecorrelation()
        {
            int ret = -1;

            HttpImeiSnDecorrelation httpImeiSnDecorrelation = new HttpImeiSnDecorrelation();
            try
            {
                ret = httpImeiSnDecorrelation.DataGetAndAnalysis(imeiRead, snRead, planCode);
                if (ret != 0)
                {
                    //frmMain.DisplayLog(((ReturnCode)httpImeiSnDecorrelation.Response.code).ToString());
                    frmMain.DisplayLog(httpImeiSnDecorrelation.Response.message + "\r\n");
                }
                else
                {
                    frmMain.DisplayLog(string.Format("{0}\r\n", "IMEI、SN服务器去关联成功"));
                    ret = 0;
                }
            }
            catch (Exception ex)
            {
                frmMain.DisplayLog(ex.ToString() + "\r\n");
            }

            return ret;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int CheckAllNumber()
        {
            int ret = -1;

            HttpAllCheck httpImeiSnDecorrelation = new HttpAllCheck();
            try
            {
                ret = httpImeiSnDecorrelation.DataGetAndAnalysis(planCode, eidRead, imeiRead, snRead, iccidRead);
                if (ret != 0)
                {
                    //frmMain.DisplayLog(((ReturnCode)httpImeiSnDecorrelation.Response.code).ToString());
                    frmMain.DisplayLog(httpImeiSnDecorrelation.Response.message + "\r\n");
                }
                else
                {
                    frmMain.DisplayLog(string.Format("服务器一致性检查: {0}\r\n", "PASS"));
                    ret = 0;
                }
            }
            catch (Exception ex)
            {
                frmMain.DisplayLog(ex.ToString() + "\r\n");
            }
            return ret;
        }

    }
}

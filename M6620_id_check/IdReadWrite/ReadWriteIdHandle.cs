using Production.AllForms;
using Production.IdReadWrite.Cmd;
using Production.SerialPortNS;
using Production.Windows;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace Production.IdReadWrite
{
    class ReadWriteIdHandle
    {
        private static string configPath = ConfigInfo.ConfigPath;            //配置文件路径

        private SerialPort sp;
        private string recive;                      //接收字符串
        private FormMain frmMain;

        private static ATReadCmd atSnRead;
        private static ATReadCmd atIccidRead;
        private static ATReadCmd atImeiRead;
        private static ATReadCmd atEidRead;
        private static ATReadCmd atVersonRead;


        private static ATWriteCmd atImeiWrite;
        private static ATWriteCmd atSnWrite;

        private bool flagCyclic;                    //开关循环检测模块上掉电的标志位
        private int checkDeviceInterval;            //循环检测上电状态的时间间隔
        private bool flagDisplayUart;               //是否显示串口输出信息

        private bool isTimeOut = false;

        private static string versonStart;

        public static string VersonStart
        {
            get
            {
                StringBuilder stringBuilder = new StringBuilder();
                Win32API.GetPrivateProfileString("Key", "VersionStart", "", stringBuilder, 256, configPath);
                versonStart = stringBuilder.ToString();
                return versonStart;
            }
        }

        public bool FlagCyclic
        {
            get
            {
                return flagCyclic;
            }

            set
            {
                flagCyclic = value;
            }
        }

        public bool FlagDisplayUart
        {
            get
            {
                return flagDisplayUart;
            }

            set
            {
                flagDisplayUart = value;
            }
        }

        public bool IsTimeOut
        {
            get
            {
                return isTimeOut;
            }

            set
            {
                isTimeOut = value;
            }
        }


        /// <summary>
        /// 静态构造函数
        /// </summary>
        static ReadWriteIdHandle()
        {
            atSnRead = new ATReadCmd(ATReadCmd.ReadIdType.SnRead, "AT+CBSN\r\n", "+CBSN:");
            atIccidRead = new ATReadCmd(ATReadCmd.ReadIdType.IccidRead, "AT+CCID\r\n", "+CCID:");
            atImeiRead = new ATReadCmd(ATReadCmd.ReadIdType.ImeiRead, "AT+CGSN\r\n", "+CGSN:");
            atEidRead = new ATReadCmd(ATReadCmd.ReadIdType.EidRead, "AT+CEID\r\n", "+CEID:");
            atVersonRead = new ATReadCmd(ATReadCmd.ReadIdType.VersonRead, "AT+CGMR\r\n", VersonStart);

            atImeiWrite = new ATWriteCmd(ATWriteCmd.WriteIdType.ImeiWrite, "AT+EGMR=1,7,\"", "\"\r\n");
            atSnWrite = new ATWriteCmd(ATWriteCmd.WriteIdType.SnWrite, "AT+EGMR=1,5,\"", "\"\r\n");
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="frmMain"></param>
        public ReadWriteIdHandle(FormMain frmMain)
        {
            ReadWriteIdHandleInfo.ReadConfig();
            checkDeviceInterval = ReadWriteIdHandleInfo.CheckDeviceInterval;

            this.frmMain = frmMain;
            flagDisplayUart = true;
            sp = SerialPortFactory.GetSerialPort();
            sp.DataReceived += Sp_DataReceived;
            SpOpen();
        }


        /// <summary>
        /// 打开串口
        /// </summary>
        public void SpOpen()
        {
            try
            {
                sp.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Environment.Exit(0);
            }
        }


        /// <summary>
        /// 串口接收处理事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int n = sp.BytesToRead;
            byte[] buf = new byte[n];
            sp.Read(buf, 0, n);
            string reciveRaw = Encoding.ASCII.GetString(buf);

            if (flagDisplayUart)
            {
                frmMain.DisplayUart(reciveRaw);
            }
            recive += reciveRaw;
        }


        /// <summary>
        /// 检测模块上电状态
        /// </summary>
        /// <returns></returns>
        public void CheckModulePowerOn()
        {
            string readEid = atEidRead.Cmd;
            flagCyclic = true;
            sp.DiscardInBuffer();
            recive = string.Empty;

            do
            {
                sp.Write(readEid);
                Console.WriteLine("正在检测上电");
                Thread.Sleep(checkDeviceInterval);
            } while (!recive.Contains("OK") && flagCyclic && !isTimeOut);
        }


        /// <summary>
        /// 检测模块掉电
        /// </summary>
        public void CheckModulePowerOff()
        {
            string readEid = atEidRead.Cmd;
            flagCyclic = true;
            sp.DiscardInBuffer();
            recive = string.Empty;

            do
            {
                recive = string.Empty;
                sp.Write(readEid);
                Console.WriteLine("正在检测掉电");
                Thread.Sleep(checkDeviceInterval);
            } while (recive.Contains("OK") && flagCyclic);
        }


        /// <summary>
        /// AT指令通信
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        private int ATCmdCommunication(string cmd)
        {
            int ret = -1;
            int retry = 5;                //循环4此没收到，重发AT指令
            sp.DiscardInBuffer();
            recive = string.Empty;

            try
            {
                for (int i = 0; i < retry; i++)
                {
                    if (recive.Contains("OK"))
                    {
                        ret = 0;
                        break;
                    }
                    else if (recive.Contains("ERROR"))
                    {
                        sp.Write(cmd);
                    }
                    else
                    {
                        sp.Write(cmd);
                    }

                    Thread.Sleep(300);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return ret;
        }


        /// <summary>
        /// 读取模块ID
        /// </summary>
        /// <param name="atReadCmd"></param>
        /// <returns></returns>
        public string ReadId(ATReadCmd.ReadIdType readIdType)
        {
            string id = null;

            ATReadCmd atReadCmd = null;

            switch (readIdType)
            {
                case ATReadCmd.ReadIdType.SnRead:
                    atReadCmd = atSnRead;
                    break;
                case ATReadCmd.ReadIdType.ImeiRead:
                    atReadCmd = atImeiRead;
                    break;
                case ATReadCmd.ReadIdType.EidRead:
                    atReadCmd = atEidRead;
                    break;
                case ATReadCmd.ReadIdType.IccidRead:
                    atReadCmd = atIccidRead;
                    break;
                case ATReadCmd.ReadIdType.VersonRead:
                    atReadCmd = atVersonRead;
                    break;

                default:
                    throw new NullReferenceException("ATReadCmd.ReadIdType引用异常");
            }

            //发命令获取数据
            string readCmd = atReadCmd.Cmd;         //读取指令
            if (ATCmdCommunication(readCmd) != 0)
            {
                return null;
            }

            //借助关键字符串提取出相应的ID
            string idKeySubstr = atReadCmd.IdKeySubstr.ToUpper();      //提取的关键字符串

            if (readIdType == ATReadCmd.ReadIdType.VersonRead)
            {
                #region AT+CGMR\r\n后面的
                //int positon;
                //if ((positon = recive.ToUpper().LastIndexOf(idKeySubstr)) >= 0)
                //{
                //    id = recive.Substring(positon + idKeySubstr.Length);
                //    //id = recive.Substring(positon);

                //    string[] sp = { "\r\n" };
                //    id = id.Split(sp, StringSplitOptions.RemoveEmptyEntries)[0];
                //}
                #endregion

                #region 以CMIOT为关键字
                string[] spit = { "\r\n" };
                string[] line = recive.Split(spit, StringSplitOptions.RemoveEmptyEntries);
                int positon;
                for (int i = 0; i < line.Length; i++)
                {
                    if ((positon = line[i].IndexOf(idKeySubstr)) >= 0)
                    {
                       id = line[i].Substring(positon);
                        //id = System.Text.RegularExpressions.Regex.Replace(temp, @"[^0-9A-Z]", "");
                    }
                }
                #endregion
            }
            else
            {
                string[] spit = { "\r\n" };
                string[] line = recive.Split(spit, StringSplitOptions.RemoveEmptyEntries);
                int pos;

                for (int i = 0; i < line.Length; i++)
                {
                    if ((pos = line[i].IndexOf(idKeySubstr)) >= 0)
                    {
                        string temp = line[i].Substring(pos + idKeySubstr.Length);
                        id = System.Text.RegularExpressions.Regex.Replace(temp, @"[^0-9A-Z]", "");
                    }
                }
            }

            return id;
        }


        /// <summary>
        /// 写入模块ID
        /// </summary>
        /// <param name="atWeiteCmd"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public int WriteId(ATWriteCmd.WriteIdType writeIdType, string id)
        {
            ATWriteCmd atWriteCmd = null;

            switch (writeIdType)
            {
                case ATWriteCmd.WriteIdType.SnWrite:
                    atWriteCmd = atSnWrite;
                    break;
                case ATWriteCmd.WriteIdType.ImeiWrite:
                    atWriteCmd = atImeiWrite;
                    break;
                default:
                    throw new NullReferenceException("ATWeiteCmd.WriteIdType引用异常");
            }

            string writeCmd = string.Format("{0}{1}{2}", atWriteCmd.CmdPrefix,id, atWriteCmd.CmdSuffix);
            return ATCmdCommunication(writeCmd);
        }

    }
}

using M6220_id_check.Server;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Tools;

namespace Production.ProductionTest
{
    class ProductionTestFactory/* : IDisposable*/
    {
        private static ProductionTestFactory productionTestFactory;
        private static ProductionTestFlow flow;
        private AllForms.FormMain frmMain;

        private static int time = 200;                      //循环检测时间间隔

        private static MyStopwatch stopwatch;         //秒表


        /// <summary>
        /// 私有构造函数
        /// </summary>
        /// <param name="frmMain"></param>
        private ProductionTestFactory(AllForms.FormMain frmMain)
        {
            this.frmMain = frmMain;

            stopwatch = new MyStopwatch(frmMain.DisplayStopwatch);
            flow = new ProductionTestFlow(frmMain, ProductionInfo.PlanCode,
                ProductionInfo.Procedure, ProductionInfo.Station);
        }


        /// <summary>
        /// 获取唯一实例
        /// </summary>
        /// <param name="frmMain"></param>
        /// <returns></returns>
        public static ProductionTestFactory GetProductionTestFactory(AllForms.FormMain frmMain)
        {
            if (productionTestFactory == null)
            {
                productionTestFactory = new ProductionTestFactory(frmMain);
            }

            return productionTestFactory;
        }


        /// <summary>
        /// 通知Flow的PlanCode已经改变
        /// </summary>
        public void ChangePlanCodeInTestFlow()
        {
            flow.PlanCode = ProductionInfo.PlanCode;        //更新计划单号
        }


        /// <summary>
        /// 循环检测测试线程状态并循环开启测试线程
        /// </summary>
        public void CheckProductionTestState(object labelImeiIn)
        {
            //秒表伴随测试线程
            stopwatch.ReStart();
            flow.StartTimeoutTimer();

            string labelImei = labelImeiIn as string;

            HttpAllCheck httpImeiSnDecorrelation = new HttpAllCheck();
            //Test
            //int ret = httpImeiSnDecorrelation.DataGetAndAnalysis("PLANTEST", "898602C99916C0362528", "864867040002025", "CH04027410010001", "898602C99916C0362528");
            //frmMain.ClearUILastTestState();
            //循环检测模块上电
            frmMain.DisplayLog("正在检测模块上电，请插入模块...\r\n");
            flow.CheckModulePowerOn();

            //开始测试
            flow.TestTaskMain(labelImei);
            flow.StopTimeOutTimer();
            stopwatch.Stop();
            frmMain.SetTextBoxReadOnly(AllForms.EnumControlWidget.txtLabelImei.ToString(),false);

            ////循环检测模块掉电
            //frmMain.DisplayLog("正在检测模块掉电，请拔下模块...\r\n");
            //flow.CheckModulePowerOff();
            //frmMain.DisplayLog("模块已拔出\r\n");
            //frmMain.ClearUILastTestState();
        }
    }
}

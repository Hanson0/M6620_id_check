using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Tools
{
    /// <summary>
    /// 自定义秒表
    /// 用于测试时间上显示
    /// </summary>
    public class MyStopwatch
    {
        private Action<TimeSpan> DisplayTime;

        private Stopwatch st;
        //private long elapsedMilliseconds;
        //private TimeSpan Elapsed;

        public MyStopwatch(Action<TimeSpan> DisplayTime)
        {
            st = new Stopwatch();
            this.DisplayTime = DisplayTime;
        }

        public void ReStart()
        {
            st.Restart();
            //new Thread(Display) { IsBackground = true}.Start();
            ThreadPool.QueueUserWorkItem(new WaitCallback(Display));
        }

        public void Stop()
        {
            st.Stop();
        }

        private void Display(object obj)
        {
            while (st.IsRunning)
            {
                if (DisplayTime != null)
                    DisplayTime(st.Elapsed);

                Thread.Sleep(10);
            }
        }
    }
}

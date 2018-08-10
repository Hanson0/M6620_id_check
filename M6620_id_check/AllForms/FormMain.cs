using Production.ProductionTest;
using Production.Result;
using Production.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace Production.AllForms
{
    public partial class FormMain : Form
    {
        /************************** 定义该类的私有成员 ********************************/
        /// <summary>
        /// 定义一个队列，用于记录用户创建的线程
        /// 以便在窗体关闭的时候关闭所有用于创建的线程
        /// </summary>
        private List<Thread> ThreadList;

        private PictureBox picLogo;             //logo
        private Label lblSoftwareName;        //软件名称
        private Label lblProductModle;        //产品型号
        private Label lblCustomerInfo;        //客户信息
        private Label lblPlanCode;            //计划单号


        /************************** 该类的初始化相关函数 ********************************/
   
        public FormMain()
        {
            InitializeComponent();
            InitfrmMainHeader();
        }


        private void FormMain_Shown(object sender, EventArgs e)
        {
            //初始化结果
            DisplayResultStatistics(ResultInfo.Pass,ResultInfo.Fail);

            if (ProductionInfo.Type == ProductionInfo.SystemType.GSMMES)
            {
                //填计划单
                FormPlanCodeConfirm frmPlanCodeConfirm = new FormPlanCodeConfirm(this);
                frmPlanCodeConfirm.ShowDialog();
                txtLabelImei.Focus();
            }
        }


        /// <summary>
        /// 消息处理
        /// </summary>
        /// <param name="m"></param>
        //protected override void WndProc(ref Message m)
        //{
        //    switch (m.Msg)
        //    {
        //        case Win32API.WM_CLOSE:
        //            //m6620_testPanel.thread_test.Abort();

        //            break;
        //        default:
        //            base.WndProc(ref m);
        //            break;
        //    }

        //    if (m.Msg == Win32API.WM_CLOSE)
        //    {
        //        base.WndProc(ref m);
        //    }
        //}


        private void txtLabelImei_TextChanged(object sender, EventArgs e)
        {
            lblLabelImei.Visible = txtLabelImei.TextLength < 1;

            int imeiLength = 15;
            if (txtLabelImei.Text.Length == imeiLength)
            {
                if (!Regex.IsMatch(txtLabelImei.Text, @"^\d{15}"))
                {
                    MessageBox.Show("IMEI号不合法");
                    ClearUILastTestState();
                    return;
                }

                //初始化测试状态
                txtLabelImei.ReadOnly = true;
                ClearUILastTestState();

                ProductionTestFactory productionTestFactory = ProductionTestFactory.GetProductionTestFactory(this);
                //开启线程
                Thread thread = new Thread(productionTestFactory.CheckProductionTestState);
                thread.IsBackground = true;
                thread.Start(txtLabelImei.Text.Trim());
            }
            else if (txtLabelImei.Text.Length > imeiLength)
            {
                txtLabelImei.Text = txtLabelImei.Text.Substring(imeiLength);
                txtLabelImei.Select(txtLabelImei.Text.Length, 0);
            }
        }

        private void lblLabelImei_Click(object sender, EventArgs e)
        {
            txtLabelImei.Focus();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("是否清零？", "清零", MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.OK)
            {
                ResultInfo.ClearResult();

                DisplayResultStatistics(0, 0);

                txtLabelImei.Text = string.Empty;
                txtSn.Text = string.Empty;
                txtImei.Text = string.Empty;
                txtEid.Text = string.Empty;
                txtIccid.Text = string.Empty;

                txtUart.BackColor = Color.White;
                txtUart.Text = string.Empty;
                txtLog.BackColor = Color.White;
                txtLog.Text = string.Empty;
                //txtLog.Text = "正在检测模块上电，请插入模块...\r\n";
                lblStopWatch.Text = "00:00:00:000";
            }
        }

        private void txtEid_TextChanged(object sender, EventArgs e)
        {
            lblEid.Visible = txtEid.TextLength < 1;
        }

        private void txtSn_TextChanged(object sender, EventArgs e)
        {
            lblSn.Visible = txtSn.TextLength < 1;
        }

        private void txtImei_TextChanged(object sender, EventArgs e)
        {
            lblImei.Visible = txtImei.TextLength < 1;
        }

        private void txtIccid_TextChanged(object sender, EventArgs e)
        {
            lblIccid.Visible = txtIccid.TextLength < 1;
        }

        private void lblEid_Click(object sender, EventArgs e)
        {
            txtEid.Focus();
        }

        private void lblSn_Click(object sender, EventArgs e)
        {
            txtSn.Focus();
        }

        private void lblImei_Click(object sender, EventArgs e)
        {
            txtImei.Focus();
        }

        private void lblIccid_Click(object sender, EventArgs e)
        {
            txtIccid.Focus();
        }

        private void 更改计划单号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //填计划单
            FormPlanCodeConfirm frmPlanCodeConfirm = new FormPlanCodeConfirm(this);
            frmPlanCodeConfirm.ShowDialog();

            ProductionTestFactory ptf = ProductionTestFactory.GetProductionTestFactory(this);
            ptf.ChangePlanCodeInTestFlow();
        }

        private void 信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 信息看板ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormBoard frmBoard = new FormBoard();
            frmBoard.ShowDialog();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = MessageBox.Show("是否关闭主程序", "警告", MessageBoxButtons.YesNo,
               MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.Yes)
            {
                e.Cancel = false;

            }
            else
            {
                if (ThreadList.Count > 0)
                {
                    //编列自定义队列,将所有线程终止
                    foreach (Thread tWorkingThread in ThreadList)
                    {
                        tWorkingThread.Abort();
                    }
                }

                e.Cancel = true;
            }
        }


        /*************************** 定义该类的自定义函数 ****************************/

        #region UiHandle

        #region frmMainHeader

        /// <summary>
        /// 初始化主窗体头界面
        /// </summary>
        private void InitfrmMainHeader()
        {
            //初始化图片
            InitPicture();

            // 为创建的Label创建TableLayoutPanel布局控件
            TableLayoutPanel tlp = CreateTlp();

            //创建Label控件并添加集合
            List<Label> labelList = new List<Label>() {
                (lblSoftwareName = new Label()),
                (lblProductModle = new Label()),
                (lblCustomerInfo = new Label()),
                (lblPlanCode = new Label())
            };

            // 初始化创建的Label
            InitCreatedLabel(labelList,tlp);

            txtLog.BackColor = Color.White;
            txtLog.ReadOnly = true;
        }


        /// <summary>
        /// 初始化图片
        /// </summary>
        private void InitPicture()
        {
            string resoucePath = "./Resouce/Picture/";
            picFormMainHeader.BackgroundImage = Image.FromFile(string.Format("{0}bg.jpg", resoucePath));
            picFormMainHeader.BackgroundImageLayout = ImageLayout.Stretch;

            //pictureBoxLogo
            picLogo = new PictureBox();
            picLogo.Name = "pictureBoxLogo";
            picLogo.BackColor = Color.Transparent;
            picLogo.Parent = picFormMainHeader;
            picLogo.Location = new System.Drawing.Point(15, 10);
            picLogo.Size = new System.Drawing.Size(picFormMainHeader.Size.Width - 30, 40);
            //picLogo.BackgroundImage = null;
            //picLogo.BackgroundImageLayout = ImageLayout.None;
            picLogo.SizeMode = PictureBoxSizeMode.StretchImage;
            picLogo.Image = Image.FromFile(string.Format("{0}logo.png", resoucePath));
            //picLogo.ImageLocation = string.Format("{0}logo_1.jpg", resoucePath);
            picLogo.TabIndex = 01;
            picLogo.TabStop = false;
        }


        /// <summary>
        /// 为创建的Label创建TableLayoutPanel布局控件
        /// </summary>
        /// <returns></returns>
        private TableLayoutPanel CreateTlp()
        {
            //tlp
            int xPosition = 20;
            TableLayoutPanel tlp = new TableLayoutPanel();
            tlp.Parent = picFormMainHeader;
            tlp.BackColor = Color.Transparent;
            tlp.Location = new Point(xPosition, picLogo.Location.Y + picLogo.Size.Height + 15);
            tlp.Size = new Size(picFormMainHeader.Size.Width - xPosition * 2, picFormMainHeader.Size.Height);
            tlp.ColumnCount = 1;
            tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle
                (System.Windows.Forms.SizeType.Percent, 100F));

            return tlp;
        }


        /// <summary>
        /// 初始化创建的Label
        /// </summary>
        /// <param name="labelList"></param>
        /// <param name="parent"></param>
        private void InitCreatedLabel(List<Label> labelList,Control parent)
        {
            foreach (var label in labelList)
            {
                label.Parent = parent;
                label.AutoSize = true;
                label.Dock = DockStyle.Fill;
                label.BackColor = Color.Transparent;
                label.Font = new Font("黑体", 12F);
                label.ForeColor = Color.Black;
                label.Margin = new Padding(2, 3, 2, 4);
            }
        }


        /// <summary>
        /// 重初初始化主窗体头界面
        /// </summary>
        /// <param name="productModle"></param>
        /// <param name="customerInfo"></param>
        /// <param name="planCode"></param>
        public void ReInitfrmMainHeader(string productModle, string customerInfo, string planCode)
        {
            if (!this.InvokeRequired)
            {
                lblSoftwareName.Text = "中移物联2G模块标签对比软件";
                lblProductModle.Text = string.Format("产品型号：{0}", productModle);
                lblCustomerInfo.Text = string.Format("客户信息：{0}", customerInfo);
                lblPlanCode.Text = string.Format("计划单号：{0}", planCode);

            }
            else
            {
                Action<string, string, string> updateUI = (string productModle1, string customerInfo1,
                    string planCode1)
                      => ReInitfrmMainHeader(productModle1, customerInfo1, planCode1);
                //this.Invoke(updateUI, productModle, customerInfo, planCode);
                updateUI.Invoke(productModle, customerInfo, planCode);
            }
        }

        #endregion


        #region Read/Write Text


        /// <summary>
        /// 读取日志
        /// </summary>
        /// <returns></returns>
        public string ReadLog()
        {
            string textRead = string.Empty;

            if (!this.InvokeRequired)
            {
                textRead = string.Format("{0}\r\n\r\n{1}", txtUart.Text, txtLog.Text);
            }
            else
            {
                Func<string> readUI = () => ReadLog();
                //textRead = readUI.Invoke();                   ???无线=限递归
                textRead = (string)this.Invoke(readUI);
            }

            return textRead;
        }


        /// <summary>
        /// 设置文本框文本
        /// </summary>
        /// <param name="name"></param>
        /// <param name="text"></param>
        /// <param name="isApend"></param>
        public void SetText(string name, string text, bool isApend)
        {

            if (!this.InvokeRequired)
            {
                object obj = this.GetType().GetField(name, BindingFlags.NonPublic
                     | BindingFlags.Instance | BindingFlags.IgnoreCase).GetValue(this);
                if (obj != null)
                {
                    if (obj is TextBox)
                    {
                        if (!isApend)
                            (obj as TextBox).Text = text;
                        else
                            (obj as TextBox).AppendText(text);
                    }
                    else if (obj is Label)
                    {
                        (obj as Label).Text = text;
                    }
                    else
                    {
                        MessageBox.Show(string.Format("控件{0}不是TextBox类型或Label类型", name));
                    }
                }
                else
                {
                    MessageBox.Show(string.Format("控件{0}不存在", name));
                }
            }
            else
            {
                Action<string, string, bool> updateUI = (string name1, string text1, bool isApend1)
                      => SetText(name1, text1, isApend1);
                this.Invoke(updateUI,name, text, isApend);
                //updateUI.Invoke(name, text, isApend);
            }
        }


        /// <summary>
        /// 输出Log
        /// </summary>
        /// <param name="log"></param>
        public void DisplayLog(string log)
        {
            string name = EnumControlWidget.txtLog.ToString();
            SetText(name, log, true);
        }


        /// <summary>
        /// 输出UartLog
        /// </summary>
        /// <param name="log"></param>
        public void DisplayUart(string log)
        {
            string name = EnumControlWidget.txtUart.ToString();
            SetText(name, log, true);
        }


        /// <summary>
        /// 测试流程开始时，清除上一次的测试状态
        /// </summary>
        public void ClearUILastTestState()
        {
            string name = EnumControlWidget.txtLog.ToString();
            SetText(name, string.Empty, false);
            SetTextBoxColor(name,Color.White);
            name = EnumControlWidget.txtUart.ToString();
            SetText(name, string.Empty, false);
            name = EnumControlWidget.txtEid.ToString();
            SetText(name, string.Empty, false);
            name = EnumControlWidget.txtImei.ToString();
            SetText(name, string.Empty, false);
            name = EnumControlWidget.txtSn.ToString();
            SetText(name, string.Empty, false);
            name = EnumControlWidget.txtIccid.ToString();
            SetText(name, string.Empty, false);


        }

        #endregion


        #region ReadOnly

        /// <summary>
        /// 设置文本框的只读
        /// </summary>
        /// <param name="txtName"></param>
        /// <param name="isReadonly"></param>
        public void SetTextBoxReadOnly(string txtName, bool isReadonly)
        {
            if (!this.InvokeRequired)
            {
                object obj = this.GetType().GetField(txtName, BindingFlags.NonPublic
                     | BindingFlags.Instance | BindingFlags.IgnoreCase).GetValue(this);
                if (obj != null && obj is TextBox)
                {
                    (obj as TextBox).ReadOnly = isReadonly;
                }

            }
            else
            {
                Action<string, bool> updateUI = (string txtName1, bool isReadonly1)
                     => SetTextBoxReadOnly(txtName1, isReadonly1);
                this.Invoke(updateUI, txtName,isReadonly);
                //updateUI.Invoke(txtName, isReadonly);
            }
        }

        #endregion


        #region BackColor

        /// <summary>
        /// 设置文本框颜色
        /// </summary>
        /// <param name="txtName"></param>
        /// <param name="color"></param>
        public void SetTextBoxColor(string txtName, Color color)
        {
            if (!this.InvokeRequired)
            {
                object obj = this.GetType().GetField(txtName, BindingFlags.NonPublic
                     | BindingFlags.Instance | BindingFlags.IgnoreCase).GetValue(this);
                if (obj != null && obj is TextBox)
                {
                    (obj as TextBox).BackColor = color;
                }

            }
            else
            {
                Action<string, Color> updateUI = (string txtName1, Color color1)
                     => SetTextBoxColor(txtName1, color1);
                this.Invoke(updateUI, txtName, color);
                //updateUI.Invoke(txtName, color);
            }
        }

        #endregion


        #region ResultStatistics

        /// <summary>
        /// 显示统计结果
        /// </summary>
        /// <param name="pass"></param>
        /// <param name="fail"></param>
        public void DisplayResultStatistics(int pass, int fail)
        {
            if (!this.InvokeRequired)
            {
                txtPass.Text = pass.ToString();
                txtFail.Text = fail.ToString();
                txtTotal.Text = (pass + fail).ToString();
                double yield = (pass + fail) == 0 ? 0 : (double)((double)pass / (double)(pass + fail));
                txtYeild.Text = yield.ToString("#0.00%");
            }
            else
            {
                Action<int, int> updateUI = (int pass1, int fail1) =>
                    DisplayResultStatistics(pass1, fail1);
                this.Invoke(updateUI, pass, fail);
                //updateUI.Invoke(pass, fail);
            }

        }

        #endregion


        #region Stopwatch

        /// <summary>
        /// 秒表显示    ms
        /// </summary>
        /// <param name="time"></param>
        public void DisplayStopwatch(TimeSpan span)
        {
            if (!this.InvokeRequired)
            {
                //TimeSpan Span = DateTime.Now - StartTime;           //Use system time to calcuate the TimeSpan
                //DateTime SpanDateTime = new DateTime(Span.Ticks);
                //label_StopWatch.Text = SpanDateTime.ToString("HH:mm:ss:fff");
                lblStopWatch.Text = new DateTime(span.Ticks).ToString("HH:mm:ss:fff");
            }
            else
            {
                Action<TimeSpan> updateUI = (TimeSpan span1) =>
                    DisplayStopwatch(span1);
                this.Invoke(updateUI, span);
                //updateUI.Invoke(pass, fail);
            }
        }



        #endregion

        #endregion

    }
}

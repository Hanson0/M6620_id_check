using Production.ProductionTest;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Production.AllForms
{
    public partial class FormBoard : Form
    {
        public FormBoard()
        {
            InitializeComponent();
        }

        private void FormBoard_Load(object sender, EventArgs e)
        {
            //生产信息集合
            List<string> productionInfoList = new List<string>()
            {
                string.Format("产品型号：{0}", ProductionInfo.ProductModel),
                string.Format("客户信息：{0}", ProductionInfo.CustomerName),
                string.Format("计划单号：{0}", ProductionInfo.PlanCode),
                string.Format("工位信息：{0}", ProductionInfo.Station),
                string.Format("工序信息：{0}", ProductionInfo.Procedure)
            };
            //显示生产信息
            DisplayTextList(productionInfoList, grpProduction);



            //服务器信息集合
            List<string> serverInfoList = new List<string>()
            {
                string.Format("IP地址：{0}", Server.HttpServerInfo.Ip),
                string.Format("端口号：{0}", Server.HttpServerInfo.Port)
            };
            //显示生产服务器
            DisplayTextList(serverInfoList,grpServer);
        }


        /// <summary>
        /// 显示信息集合的文本
        /// </summary>
        /// <param name="textList"></param>
        /// <param name="parent"></param>
        private void DisplayTextList(List<string> textList, Control parent)
        {
            TableLayoutPanel tlp = CreateTlp(parent);

            foreach (var text in textList)
            {
                Label label = new Label();
                label.Parent = tlp;
                label.AutoSize = true;
                label.Dock = DockStyle.Fill;
                label.BackColor = Color.Transparent;
                label.Font = new Font("黑体", 9F);
                label.ForeColor = Color.Black;
                label.Text = text;
                label.Margin = new Padding(5, 10, 3, 4);
            }
        }


        /// <summary>
        /// 创建TableLayoutPanel作为Label控件的容器
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        private TableLayoutPanel CreateTlp(Control parent)
        {
            TableLayoutPanel tlp = new TableLayoutPanel();
            tlp.Parent = parent;
            tlp.BackColor = Color.Transparent;
            tlp.Location = new Point(10, 10);       //相对父控件左上角的坐标
            tlp.Size = parent.Size;
            tlp.ColumnCount = 1;
            tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle
                (System.Windows.Forms.SizeType.Percent, 100F));

            return tlp;
        }
    }
}

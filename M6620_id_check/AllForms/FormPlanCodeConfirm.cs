using Production.Server;
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
    public partial class FormPlanCodeConfirm : Form
    {
        private FormMain frmMain;

        public FormPlanCodeConfirm(FormMain frmMain)
        {
            InitializeComponent();
            this.frmMain = frmMain;
        }

        private void lblPlanCode_Click(object sender, EventArgs e)
        {
            txtPlanCode.Focus();
        }

        private void txtPlanCode_TextChanged(object sender, EventArgs e)
        {
            lblPlanCode.Visible = txtPlanCode.TextLength < 1;
        }

        private void FormPlanCodeConfirm_Load(object sender, EventArgs e)
        {
            txtPlanCode.Text = ProductionTest.ProductionInfo.PlanCode;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPlanCode.Text))
            {
                MessageBox.Show("请输入计划单号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnConfirm.Enabled = false;
            txtPlanCode.ReadOnly = true;
            string planCode = txtPlanCode.Text;
            if (planCode != ProductionTest.ProductionInfo.PlanCode)
            {
                HttpBaseInfoGet httpBaseInfoGet = new HttpBaseInfoGet();
                int ret = -1;
                try
                {
                    ret = httpBaseInfoGet.DataGetAndAnalysis(planCode);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                if (ret == 0 && httpBaseInfoGet.Response.data != null)
                {
                    ProductionTest.ProductionInfo.CustomerName = httpBaseInfoGet.Response.data.customer;
                    ProductionTest.ProductionInfo.ProductModel = httpBaseInfoGet.Response.data.materialDescription;
                    ProductionTest.ProductionInfo.PlanCode = httpBaseInfoGet.Response.data.planCode;
                    frmMain.ReInitfrmMainHeader(ProductionTest.ProductionInfo.ProductModel,
                        ProductionTest.ProductionInfo.CustomerName, ProductionTest.ProductionInfo.PlanCode);

                    ProductionTest.ProductionInfo.PlanCode = txtPlanCode.Text;

                    frmMain.ReInitfrmMainHeader(ProductionTest.ProductionInfo.ProductModel,
                        ProductionTest.ProductionInfo.CustomerName, ProductionTest.ProductionInfo.PlanCode);

                    this.Close();
                }
                else
                {
                    MessageBox.Show("查询基本信息失败");
                    txtPlanCode.Text = string.Empty;
                    btnConfirm.Enabled = true;
                    txtPlanCode.ReadOnly = false;
                }
            }
            else
            {
                frmMain.ReInitfrmMainHeader(ProductionTest.ProductionInfo.ProductModel,
    ProductionTest.ProductionInfo.CustomerName, ProductionTest.ProductionInfo.PlanCode);
                this.Close();
            }

        }

        private void FormPlanCodeConfirm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPlanCode.Text))
            {
                Environment.Exit(0);
            }
        }
    }
}

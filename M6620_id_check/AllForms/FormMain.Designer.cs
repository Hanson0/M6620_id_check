namespace Production.AllForms
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.txtSn = new System.Windows.Forms.TextBox();
            this.txtImei = new System.Windows.Forms.TextBox();
            this.lblSn = new System.Windows.Forms.Label();
            this.lblEid = new System.Windows.Forms.Label();
            this.lblImei = new System.Windows.Forms.Label();
            this.txtEid = new System.Windows.Forms.TextBox();
            this.picFormMainHeader = new System.Windows.Forms.PictureBox();
            this.grpResult = new System.Windows.Forms.GroupBox();
            this.txtYeild = new System.Windows.Forms.TextBox();
            this.lblYeild = new System.Windows.Forms.Label();
            this.lblClear = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.txtFail = new System.Windows.Forms.TextBox();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblFail = new System.Windows.Forms.Label();
            this.lblPass = new System.Windows.Forms.Label();
            this.frpStopWatch = new System.Windows.Forms.GroupBox();
            this.lblStopWatchLabel = new System.Windows.Forms.Label();
            this.lblStopWatch = new System.Windows.Forms.Label();
            this.txtUart = new System.Windows.Forms.TextBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.更改计划单号ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.信息看板ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblIccid = new System.Windows.Forms.Label();
            this.txtIccid = new System.Windows.Forms.TextBox();
            this.lblLabelImei = new System.Windows.Forms.Label();
            this.txtLabelImei = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.picFormMainHeader)).BeginInit();
            this.grpResult.SuspendLayout();
            this.frpStopWatch.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtSn
            // 
            this.txtSn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSn.BackColor = System.Drawing.Color.White;
            this.txtSn.Font = new System.Drawing.Font("宋体", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSn.Location = new System.Drawing.Point(371, 104);
            this.txtSn.Margin = new System.Windows.Forms.Padding(4);
            this.txtSn.Name = "txtSn";
            this.txtSn.ReadOnly = true;
            this.txtSn.Size = new System.Drawing.Size(583, 57);
            this.txtSn.TabIndex = 1;
            this.txtSn.TextChanged += new System.EventHandler(this.txtSn_TextChanged);
            // 
            // txtImei
            // 
            this.txtImei.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtImei.BackColor = System.Drawing.Color.White;
            this.txtImei.Font = new System.Drawing.Font("宋体", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtImei.Location = new System.Drawing.Point(369, 164);
            this.txtImei.Margin = new System.Windows.Forms.Padding(4);
            this.txtImei.Name = "txtImei";
            this.txtImei.ReadOnly = true;
            this.txtImei.Size = new System.Drawing.Size(583, 57);
            this.txtImei.TabIndex = 2;
            this.txtImei.TextChanged += new System.EventHandler(this.txtImei_TextChanged);
            // 
            // lblSn
            // 
            this.lblSn.AutoSize = true;
            this.lblSn.BackColor = System.Drawing.Color.White;
            this.lblSn.Font = new System.Drawing.Font("宋体", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSn.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblSn.Location = new System.Drawing.Point(412, 112);
            this.lblSn.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSn.Name = "lblSn";
            this.lblSn.Size = new System.Drawing.Size(152, 44);
            this.lblSn.TabIndex = 8;
            this.lblSn.Text = "模块SN";
            this.lblSn.Click += new System.EventHandler(this.lblSn_Click);
            // 
            // lblEid
            // 
            this.lblEid.AutoSize = true;
            this.lblEid.BackColor = System.Drawing.Color.White;
            this.lblEid.Font = new System.Drawing.Font("宋体", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblEid.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblEid.Location = new System.Drawing.Point(412, 231);
            this.lblEid.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEid.Name = "lblEid";
            this.lblEid.Size = new System.Drawing.Size(174, 44);
            this.lblEid.TabIndex = 10;
            this.lblEid.Text = "模块EID";
            this.lblEid.Click += new System.EventHandler(this.lblEid_Click);
            // 
            // lblImei
            // 
            this.lblImei.AutoSize = true;
            this.lblImei.BackColor = System.Drawing.Color.White;
            this.lblImei.Font = new System.Drawing.Font("宋体", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblImei.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblImei.Location = new System.Drawing.Point(409, 172);
            this.lblImei.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblImei.Name = "lblImei";
            this.lblImei.Size = new System.Drawing.Size(196, 44);
            this.lblImei.TabIndex = 9;
            this.lblImei.Text = "模块IMEI";
            this.lblImei.Click += new System.EventHandler(this.lblImei_Click);
            // 
            // txtEid
            // 
            this.txtEid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEid.BackColor = System.Drawing.Color.White;
            this.txtEid.Font = new System.Drawing.Font("宋体", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtEid.Location = new System.Drawing.Point(371, 224);
            this.txtEid.Margin = new System.Windows.Forms.Padding(4);
            this.txtEid.Name = "txtEid";
            this.txtEid.ReadOnly = true;
            this.txtEid.Size = new System.Drawing.Size(583, 57);
            this.txtEid.TabIndex = 3;
            this.txtEid.TextChanged += new System.EventHandler(this.txtEid_TextChanged);
            // 
            // picFormMainHeader
            // 
            this.picFormMainHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picFormMainHeader.Location = new System.Drawing.Point(9, 39);
            this.picFormMainHeader.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.picFormMainHeader.Name = "picFormMainHeader";
            this.picFormMainHeader.Size = new System.Drawing.Size(347, 244);
            this.picFormMainHeader.TabIndex = 130;
            this.picFormMainHeader.TabStop = false;
            // 
            // grpResult
            // 
            this.grpResult.Controls.Add(this.txtYeild);
            this.grpResult.Controls.Add(this.lblYeild);
            this.grpResult.Controls.Add(this.lblClear);
            this.grpResult.Controls.Add(this.txtTotal);
            this.grpResult.Controls.Add(this.btnClear);
            this.grpResult.Controls.Add(this.txtFail);
            this.grpResult.Controls.Add(this.txtPass);
            this.grpResult.Controls.Add(this.lblTotal);
            this.grpResult.Controls.Add(this.lblFail);
            this.grpResult.Controls.Add(this.lblPass);
            this.grpResult.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpResult.ForeColor = System.Drawing.Color.Green;
            this.grpResult.Location = new System.Drawing.Point(9, 291);
            this.grpResult.Margin = new System.Windows.Forms.Padding(4);
            this.grpResult.Name = "grpResult";
            this.grpResult.Padding = new System.Windows.Forms.Padding(4);
            this.grpResult.Size = new System.Drawing.Size(347, 275);
            this.grpResult.TabIndex = 131;
            this.grpResult.TabStop = false;
            this.grpResult.Text = "Result";
            // 
            // txtYeild
            // 
            this.txtYeild.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.txtYeild.Location = new System.Drawing.Point(143, 184);
            this.txtYeild.Margin = new System.Windows.Forms.Padding(4);
            this.txtYeild.Name = "txtYeild";
            this.txtYeild.ReadOnly = true;
            this.txtYeild.Size = new System.Drawing.Size(188, 42);
            this.txtYeild.TabIndex = 3;
            this.txtYeild.Text = "0";
            // 
            // lblYeild
            // 
            this.lblYeild.AutoSize = true;
            this.lblYeild.Location = new System.Drawing.Point(7, 184);
            this.lblYeild.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblYeild.Name = "lblYeild";
            this.lblYeild.Size = new System.Drawing.Size(103, 30);
            this.lblYeild.TabIndex = 24;
            this.lblYeild.Text = "RATE :";
            // 
            // lblClear
            // 
            this.lblClear.AutoSize = true;
            this.lblClear.Location = new System.Drawing.Point(7, 231);
            this.lblClear.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblClear.Name = "lblClear";
            this.lblClear.Size = new System.Drawing.Size(103, 30);
            this.lblClear.TabIndex = 23;
            this.lblClear.Text = "CLEAR:";
            // 
            // txtTotal
            // 
            this.txtTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.txtTotal.Location = new System.Drawing.Point(143, 135);
            this.txtTotal.Margin = new System.Windows.Forms.Padding(4);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(188, 42);
            this.txtTotal.TabIndex = 2;
            this.txtTotal.Text = "0";
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("宋体", 15F);
            this.btnClear.Location = new System.Drawing.Point(141, 232);
            this.btnClear.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(191, 38);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "清零";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // txtFail
            // 
            this.txtFail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.txtFail.Location = new System.Drawing.Point(143, 86);
            this.txtFail.Margin = new System.Windows.Forms.Padding(4);
            this.txtFail.Name = "txtFail";
            this.txtFail.ReadOnly = true;
            this.txtFail.Size = new System.Drawing.Size(188, 42);
            this.txtFail.TabIndex = 1;
            this.txtFail.Text = "0";
            // 
            // txtPass
            // 
            this.txtPass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtPass.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtPass.Location = new System.Drawing.Point(141, 38);
            this.txtPass.Margin = new System.Windows.Forms.Padding(4);
            this.txtPass.Name = "txtPass";
            this.txtPass.ReadOnly = true;
            this.txtPass.Size = new System.Drawing.Size(189, 42);
            this.txtPass.TabIndex = 0;
            this.txtPass.Text = "0";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(7, 136);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(103, 30);
            this.lblTotal.TabIndex = 2;
            this.lblTotal.Text = "TOTAL:";
            // 
            // lblFail
            // 
            this.lblFail.AutoSize = true;
            this.lblFail.Location = new System.Drawing.Point(8, 89);
            this.lblFail.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFail.Name = "lblFail";
            this.lblFail.Size = new System.Drawing.Size(103, 30);
            this.lblFail.TabIndex = 1;
            this.lblFail.Text = "FAIL :";
            // 
            // lblPass
            // 
            this.lblPass.AutoSize = true;
            this.lblPass.Location = new System.Drawing.Point(8, 41);
            this.lblPass.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPass.Name = "lblPass";
            this.lblPass.Size = new System.Drawing.Size(103, 30);
            this.lblPass.TabIndex = 0;
            this.lblPass.Text = "PASS :";
            // 
            // frpStopWatch
            // 
            this.frpStopWatch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.frpStopWatch.Controls.Add(this.lblStopWatchLabel);
            this.frpStopWatch.Controls.Add(this.lblStopWatch);
            this.frpStopWatch.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.frpStopWatch.ForeColor = System.Drawing.Color.Green;
            this.frpStopWatch.Location = new System.Drawing.Point(9, 582);
            this.frpStopWatch.Margin = new System.Windows.Forms.Padding(4);
            this.frpStopWatch.Name = "frpStopWatch";
            this.frpStopWatch.Padding = new System.Windows.Forms.Padding(4);
            this.frpStopWatch.Size = new System.Drawing.Size(341, 156);
            this.frpStopWatch.TabIndex = 132;
            this.frpStopWatch.TabStop = false;
            this.frpStopWatch.Text = "Elapsed Time";
            // 
            // lblStopWatchLabel
            // 
            this.lblStopWatchLabel.AutoSize = true;
            this.lblStopWatchLabel.Font = new System.Drawing.Font("宋体", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblStopWatchLabel.Location = new System.Drawing.Point(56, 48);
            this.lblStopWatchLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStopWatchLabel.Name = "lblStopWatchLabel";
            this.lblStopWatchLabel.Size = new System.Drawing.Size(202, 34);
            this.lblStopWatchLabel.TabIndex = 5;
            this.lblStopWatchLabel.Text = "HH:MM:SS:MS";
            // 
            // lblStopWatch
            // 
            this.lblStopWatch.AutoSize = true;
            this.lblStopWatch.Font = new System.Drawing.Font("宋体", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblStopWatch.Location = new System.Drawing.Point(55, 95);
            this.lblStopWatch.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStopWatch.Name = "lblStopWatch";
            this.lblStopWatch.Size = new System.Drawing.Size(219, 34);
            this.lblStopWatch.TabIndex = 4;
            this.lblStopWatch.Text = "00:00:00:000";
            // 
            // txtUart
            // 
            this.txtUart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUart.BackColor = System.Drawing.Color.White;
            this.txtUart.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtUart.Location = new System.Drawing.Point(371, 352);
            this.txtUart.Margin = new System.Windows.Forms.Padding(4);
            this.txtUart.Multiline = true;
            this.txtUart.Name = "txtUart";
            this.txtUart.ReadOnly = true;
            this.txtUart.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtUart.Size = new System.Drawing.Size(583, 199);
            this.txtUart.TabIndex = 5;
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtLog.Location = new System.Drawing.Point(369, 558);
            this.txtLog.Margin = new System.Windows.Forms.Padding(4);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(583, 189);
            this.txtLog.TabIndex = 6;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.设置ToolStripMenuItem,
            this.信息ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(964, 35);
            this.menuStrip1.TabIndex = 135;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.更改计划单号ToolStripMenuItem});
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(64, 31);
            this.设置ToolStripMenuItem.Text = "设置";
            // 
            // 更改计划单号ToolStripMenuItem
            // 
            this.更改计划单号ToolStripMenuItem.Name = "更改计划单号ToolStripMenuItem";
            this.更改计划单号ToolStripMenuItem.Size = new System.Drawing.Size(210, 32);
            this.更改计划单号ToolStripMenuItem.Text = "更改计划单号";
            this.更改计划单号ToolStripMenuItem.Click += new System.EventHandler(this.更改计划单号ToolStripMenuItem_Click);
            // 
            // 信息ToolStripMenuItem
            // 
            this.信息ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.信息看板ToolStripMenuItem});
            this.信息ToolStripMenuItem.Name = "信息ToolStripMenuItem";
            this.信息ToolStripMenuItem.Size = new System.Drawing.Size(64, 31);
            this.信息ToolStripMenuItem.Text = "信息";
            this.信息ToolStripMenuItem.Click += new System.EventHandler(this.信息ToolStripMenuItem_Click);
            // 
            // 信息看板ToolStripMenuItem
            // 
            this.信息看板ToolStripMenuItem.Name = "信息看板ToolStripMenuItem";
            this.信息看板ToolStripMenuItem.Size = new System.Drawing.Size(170, 32);
            this.信息看板ToolStripMenuItem.Text = "信息看板";
            this.信息看板ToolStripMenuItem.Click += new System.EventHandler(this.信息看板ToolStripMenuItem_Click);
            // 
            // lblIccid
            // 
            this.lblIccid.AutoSize = true;
            this.lblIccid.BackColor = System.Drawing.Color.White;
            this.lblIccid.Font = new System.Drawing.Font("宋体", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblIccid.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblIccid.Location = new System.Drawing.Point(412, 291);
            this.lblIccid.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblIccid.Name = "lblIccid";
            this.lblIccid.Size = new System.Drawing.Size(218, 44);
            this.lblIccid.TabIndex = 11;
            this.lblIccid.Text = "模块ICCID";
            this.lblIccid.Click += new System.EventHandler(this.lblIccid_Click);
            // 
            // txtIccid
            // 
            this.txtIccid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIccid.BackColor = System.Drawing.Color.White;
            this.txtIccid.Font = new System.Drawing.Font("宋体", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtIccid.Location = new System.Drawing.Point(371, 284);
            this.txtIccid.Margin = new System.Windows.Forms.Padding(4);
            this.txtIccid.Name = "txtIccid";
            this.txtIccid.ReadOnly = true;
            this.txtIccid.Size = new System.Drawing.Size(583, 57);
            this.txtIccid.TabIndex = 4;
            this.txtIccid.TextChanged += new System.EventHandler(this.txtIccid_TextChanged);
            // 
            // lblLabelImei
            // 
            this.lblLabelImei.AutoSize = true;
            this.lblLabelImei.BackColor = System.Drawing.Color.White;
            this.lblLabelImei.Font = new System.Drawing.Font("宋体", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLabelImei.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblLabelImei.Location = new System.Drawing.Point(409, 49);
            this.lblLabelImei.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLabelImei.Name = "lblLabelImei";
            this.lblLabelImei.Size = new System.Drawing.Size(196, 44);
            this.lblLabelImei.TabIndex = 7;
            this.lblLabelImei.Text = "标签IMEI";
            this.lblLabelImei.Click += new System.EventHandler(this.lblLabelImei_Click);
            // 
            // txtLabelImei
            // 
            this.txtLabelImei.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLabelImei.BackColor = System.Drawing.Color.White;
            this.txtLabelImei.Font = new System.Drawing.Font("宋体", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtLabelImei.Location = new System.Drawing.Point(369, 40);
            this.txtLabelImei.Margin = new System.Windows.Forms.Padding(4);
            this.txtLabelImei.Name = "txtLabelImei";
            this.txtLabelImei.Size = new System.Drawing.Size(583, 57);
            this.txtLabelImei.TabIndex = 0;
            this.txtLabelImei.TextChanged += new System.EventHandler(this.txtLabelImei_TextChanged);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(964, 754);
            this.Controls.Add(this.lblLabelImei);
            this.Controls.Add(this.txtLabelImei);
            this.Controls.Add(this.lblIccid);
            this.Controls.Add(this.txtIccid);
            this.Controls.Add(this.lblEid);
            this.Controls.Add(this.txtUart);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.frpStopWatch);
            this.Controls.Add(this.grpResult);
            this.Controls.Add(this.picFormMainHeader);
            this.Controls.Add(this.lblImei);
            this.Controls.Add(this.lblSn);
            this.Controls.Add(this.txtImei);
            this.Controls.Add(this.txtSn);
            this.Controls.Add(this.txtEid);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "M6220_idcheck_1.4.01_20180810_beta";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Shown += new System.EventHandler(this.FormMain_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.picFormMainHeader)).EndInit();
            this.grpResult.ResumeLayout(false);
            this.grpResult.PerformLayout();
            this.frpStopWatch.ResumeLayout(false);
            this.frpStopWatch.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblSn;
        private System.Windows.Forms.Label lblEid;
        private System.Windows.Forms.Label lblImei;
        private System.Windows.Forms.TextBox txtSn;
        private System.Windows.Forms.TextBox txtImei;
        private System.Windows.Forms.TextBox txtEid;
        private System.Windows.Forms.PictureBox picFormMainHeader;
        private System.Windows.Forms.GroupBox grpResult;
        private System.Windows.Forms.TextBox txtYeild;
        private System.Windows.Forms.Label lblYeild;
        private System.Windows.Forms.Label lblClear;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.TextBox txtFail;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblFail;
        private System.Windows.Forms.Label lblPass;
        private System.Windows.Forms.GroupBox frpStopWatch;
        private System.Windows.Forms.Label lblStopWatchLabel;
        private System.Windows.Forms.Label lblStopWatch;
        private System.Windows.Forms.TextBox txtUart;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 更改计划单号ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 信息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 信息看板ToolStripMenuItem;
        private System.Windows.Forms.Label lblIccid;
        private System.Windows.Forms.TextBox txtIccid;
        private System.Windows.Forms.Label lblLabelImei;
        private System.Windows.Forms.TextBox txtLabelImei;
    }
}


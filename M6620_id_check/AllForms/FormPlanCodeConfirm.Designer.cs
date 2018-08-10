namespace Production.AllForms
{
    partial class FormPlanCodeConfirm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblPlanCode = new System.Windows.Forms.Label();
            this.txtPlanCode = new System.Windows.Forms.TextBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblPlanCode
            // 
            this.lblPlanCode.AutoSize = true;
            this.lblPlanCode.BackColor = System.Drawing.Color.White;
            this.lblPlanCode.Font = new System.Drawing.Font("宋体", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPlanCode.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblPlanCode.Location = new System.Drawing.Point(49, 18);
            this.lblPlanCode.Name = "lblPlanCode";
            this.lblPlanCode.Size = new System.Drawing.Size(356, 48);
            this.lblPlanCode.TabIndex = 3;
            this.lblPlanCode.Text = "请输入计划单号";
            this.lblPlanCode.Click += new System.EventHandler(this.lblPlanCode_Click);
            // 
            // txtPlanCode
            // 
            this.txtPlanCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPlanCode.BackColor = System.Drawing.Color.White;
            this.txtPlanCode.Font = new System.Drawing.Font("宋体", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPlanCode.Location = new System.Drawing.Point(19, 10);
            this.txtPlanCode.Name = "txtPlanCode";
            this.txtPlanCode.Size = new System.Drawing.Size(438, 62);
            this.txtPlanCode.TabIndex = 4;
            this.txtPlanCode.TextChanged += new System.EventHandler(this.txtPlanCode_TextChanged);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Font = new System.Drawing.Font("宋体", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnConfirm.Location = new System.Drawing.Point(351, 78);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(106, 44);
            this.btnConfirm.TabIndex = 5;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // FormPlanCodeConfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 129);
            this.ControlBox = false;
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.lblPlanCode);
            this.Controls.Add(this.txtPlanCode);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPlanCodeConfirm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "计划单确认";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormPlanCodeConfirm_FormClosing);
            this.Load += new System.EventHandler(this.FormPlanCodeConfirm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPlanCode;
        private System.Windows.Forms.TextBox txtPlanCode;
        private System.Windows.Forms.Button btnConfirm;
    }
}
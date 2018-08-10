namespace Production.AllForms
{
    partial class FormBoard
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
            this.grpProduction = new System.Windows.Forms.GroupBox();
            this.grpServer = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // grpProduction
            // 
            this.grpProduction.Location = new System.Drawing.Point(12, 12);
            this.grpProduction.Name = "grpProduction";
            this.grpProduction.Size = new System.Drawing.Size(320, 287);
            this.grpProduction.TabIndex = 30;
            this.grpProduction.TabStop = false;
            this.grpProduction.Text = "生产信息";
            // 
            // grpServer
            // 
            this.grpServer.Location = new System.Drawing.Point(337, 12);
            this.grpServer.Name = "grpServer";
            this.grpServer.Size = new System.Drawing.Size(320, 287);
            this.grpServer.TabIndex = 31;
            this.grpServer.TabStop = false;
            this.grpServer.Text = "服务器信息";
            // 
            // FormBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(663, 303);
            this.Controls.Add(this.grpServer);
            this.Controls.Add(this.grpProduction);
            this.Name = "FormBoard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "信息看板";
            this.Load += new System.EventHandler(this.FormBoard_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox grpServer;
        private System.Windows.Forms.GroupBox grpProduction;
    }
}
namespace ServerHeartBeat
{
    partial class FrmMain
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
            this.components = new System.ComponentModel.Container();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.tsmAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmFilter = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSuccess = new System.Windows.Forms.ToolStripMenuItem();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.tsmError = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmStart = new System.Windows.Forms.ToolStripMenuItem();
            this.trApi = new System.Windows.Forms.Timer(this.components);
            this.tsmClean = new System.Windows.Forms.ToolStripMenuItem();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmStart,
            this.tsmAdd,
            this.tsmFilter,
            this.tsmClean});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(755, 25);
            this.menu.TabIndex = 0;
            this.menu.Text = "menuStrip1";
            // 
            // tsmAdd
            // 
            this.tsmAdd.Name = "tsmAdd";
            this.tsmAdd.Size = new System.Drawing.Size(68, 21);
            this.tsmAdd.Text = "管理请求";
            this.tsmAdd.Click += new System.EventHandler(this.tsmAdd_Click);
            // 
            // tsmFilter
            // 
            this.tsmFilter.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmSuccess,
            this.tsmError});
            this.tsmFilter.Name = "tsmFilter";
            this.tsmFilter.Size = new System.Drawing.Size(68, 21);
            this.tsmFilter.Text = "过滤请求";
            // 
            // tsmSuccess
            // 
            this.tsmSuccess.Name = "tsmSuccess";
            this.tsmSuccess.Size = new System.Drawing.Size(152, 22);
            this.tsmSuccess.Text = "成功请求";
            this.tsmSuccess.Click += new System.EventHandler(this.tsmSuccess_Click);
            // 
            // txtLog
            // 
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Location = new System.Drawing.Point(0, 25);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(755, 430);
            this.txtLog.TabIndex = 1;
            // 
            // tsmError
            // 
            this.tsmError.Name = "tsmError";
            this.tsmError.Size = new System.Drawing.Size(152, 22);
            this.tsmError.Text = "错误请求";
            this.tsmError.Click += new System.EventHandler(this.tsmError_Click);
            // 
            // tsmStart
            // 
            this.tsmStart.Name = "tsmStart";
            this.tsmStart.Size = new System.Drawing.Size(44, 21);
            this.tsmStart.Text = "停止";
            this.tsmStart.Click += new System.EventHandler(this.tsmStart_Click);
            // 
            // trApi
            // 
            this.trApi.Interval = 1000;
            this.trApi.Tick += new System.EventHandler(this.trApi_Tick);
            // 
            // tsmClean
            // 
            this.tsmClean.Name = "tsmClean";
            this.tsmClean.Size = new System.Drawing.Size(44, 21);
            this.tsmClean.Text = "清空";
            this.tsmClean.Click += new System.EventHandler(this.tsmClean_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(755, 455);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.menu);
            this.MainMenuStrip = this.menu;
            this.Name = "FrmMain";
            this.Text = "心跳请求";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.ToolStripMenuItem tsmAdd;
        private System.Windows.Forms.ToolStripMenuItem tsmFilter;
        private System.Windows.Forms.ToolStripMenuItem tsmSuccess;
        private System.Windows.Forms.ToolStripMenuItem tsmError;
        private System.Windows.Forms.ToolStripMenuItem tsmStart;
        private System.Windows.Forms.Timer trApi;
        private System.Windows.Forms.ToolStripMenuItem tsmClean;
    }
}


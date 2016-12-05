using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServerHeartBeat
{
    public partial class FrmMain : Form
    {
        private FrmManage Add = new FrmManage();

        public FrmMain()
        {
            InitializeComponent();
        }

        private bool start;
        public bool Start
        {
            get
            {
                return start;
            }
            set
            {
                start = value;
                tsmStart.Text = start ? "关闭" : "启用";
                if (start)
                {
                    trApi.Start();
                }
                else
                {
                    trApi.Stop();
                }
            }
        }

        private bool showSuccessMessage;

        public bool ShowSuccessMessage
        {
            get
            {
                return showSuccessMessage;
            }
            set
            {
                showSuccessMessage = value;
                tsmSuccess.Checked = ShowSuccessMessage;
            }
        }

        private bool showErrorMessage;

        public bool ShowErrorMessage
        {
            get
            {
                return showErrorMessage;
            }
            set
            {
                showErrorMessage = value;
                tsmError.Checked = showErrorMessage;
            }
        }

        private List<Models.Api> _apis = Base.Setting.Data.DataSource;

        public void Run()
        {
            if (!Start)
            {
                return;
            }
            foreach (var item in _apis)
            {
                //接口启用了，且时间是有效的
                item.Request();
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //初始化
            Start = true;
            ShowSuccessMessage = true;
            ShowErrorMessage = true;
            foreach (var item in _apis)
            {
                Action<string> a = (message) =>
                {
                    txtLog.AppendText($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} {message}\r\n");
                };
                item.SuccessEvent += (state, message) =>
                {
                    if (!ShowSuccessMessage)
                    {
                        return;
                    }
                    if (txtLog.InvokeRequired)
                    {
                        Invoke((MethodInvoker)delegate { a(message); });
                    }
                    else
                    {
                        a(message);
                    }
                };
                item.ErrorEvent += (state, message) =>
                {
                    if (!ShowErrorMessage)
                    {
                        return;
                    }
                    if (txtLog.InvokeRequired)
                    {
                        Invoke(a);
                    }
                    else
                    {
                        a(message);
                    }
                };
            }
        }

        private void tsmAdd_Click(object sender, EventArgs e)
        {
            Add.ShowDialog();

        }

        private void trApi_Tick(object sender, EventArgs e)
        {
            Run();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Base.Setting.Data.Update();
        }

        private void tsmStart_Click(object sender, EventArgs e)
        {
            Start = !Start;
        }

        private void tsmClean_Click(object sender, EventArgs e)
        {
            txtLog.Text = "";
        }

        private void tsmSuccess_Click(object sender, EventArgs e)
        {
            ShowSuccessMessage = !ShowSuccessMessage;
        }

        private void tsmError_Click(object sender, EventArgs e)
        {
            ShowErrorMessage = !ShowErrorMessage;
        }
    }
}

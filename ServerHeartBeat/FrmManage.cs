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
    public partial class FrmManage : Form
    {
        public FrmManage()
        {
            InitializeComponent();
        }

        private Models.Api api = new Models.Api();

        private int defInterval = 60;
        Base.BaseSetting<Models.Api> _data = Base.Setting.Data;
        Base.BaseSetting<Models.ApiType> _type = Base.Setting.Type;

        private void FrmManage_Load(object sender, EventArgs e)
        {
            Bind();
            BindType();
            string[] type = { "GET", "POST" };
            cbxRequestType.DataSource = type;
            _data.UpdatedEvent += () => { Bind(); };
            _type.UpdatedEvent += () => { BindType(); };
            numInterval.Value = defInterval;
            if (dgvData.Rows.Count > 0)
            {
                dgvData.Rows[0].Selected = false;
            }
            dgvData.SelectionChanged += DgvData_SelectionChanged;
        }



        private void Bind()
        {

            var list = _data.DataSource.Select(s => new Models.ApiViewModel(s)).ToList();
            dgvData.DataSource = new BindingList<Models.ApiViewModel>(list);

        }

        private void BindType()
        {
            cbxType.DataSource = _type.DataSource;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            CleanApi();
        }

        private void CleanApi()
        {
            api = new Models.Api();
            txtName.Text = "";
            txtLink.Text = "";

            numInterval.Value = defInterval;
            chkEnable.Checked = true;
            txtParameter.Text = "";
        }

        private void SetApi(int id)
        {
            api = _data.DataSource.FirstOrDefault(s => s.ID == id);
            txtName.Text = api.Name;
            txtLink.Text = api.Link;
            numInterval.Value = api.Interval;
            chkEnable.Checked = api.Enable;
            txtParameter.Text = api.Parameter;
            cbxRequestType.Text = api.RequestType;
            //cbxType.Items. = api.Type;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //判断这个连接头是否第一次添加
            var selectedType = cbxType.SelectedValue as int?;
            if (selectedType == null && string.IsNullOrWhiteSpace(cbxType.Text))
            {
                selectedType = _type.DataSource.Count == 0 ? 1 : _type.DataSource.Max(s => s.ID) + 1;
                _type.DataSource.Add(new Models.ApiType
                {
                    ID = selectedType.Value,
                    Link = cbxType.Text
                });
                _type.Update();
            }
            api.Name = txtName.Text;
            api.Link = txtLink.Text;
            api.Type = selectedType.Value;
            api.Interval = (int)numInterval.Value;
            api.Enable = chkEnable.Checked;
            api.Parameter = txtParameter.Text;
            api.RequestType = (string)cbxRequestType.SelectedItem;
            if (api.ID == 0)
            {
                api.ID = _data.DataSource.Count == 0 ? 1 : _data.DataSource.Max(s => s.ID) + 1;
                _data.DataSource.Add(api);
                CleanApi();
            }
            _data.Update();


        }

        private void FrmManage_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }


        private void dgvData_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            int id = ((Models.ApiViewModel)e.Row.DataBoundItem).ID;
            var del = _data.DataSource.FirstOrDefault(s => s.ID == id);

            _data.DataSource.Remove(del);
            _data.Update();
        }

        private void DgvData_SelectionChanged(object sender, EventArgs e)
        {
            var item = (DataGridView)sender;
            if (item.SelectedRows.Count > 0)
            {
                var id = ((Models.ApiViewModel)item.SelectedRows[0].DataBoundItem).ID;
                SetApi(id);
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace projeyapma
{
    public partial class uyeliste : Form
    {
        public uyeliste()
        {
            InitializeComponent();
        }
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "\\kutuphaneprojem.accdb");
        DataTable table = new DataTable();
        DataView dataView = new DataView();
        private void uyeliste_Load(object sender, EventArgs e)
        {
            con.Open();
            OleDbDataAdapter adapter = new OleDbDataAdapter("select * from kullanici order by ad", con);
            adapter.Fill(table);
            dgwMember.DataSource = table;
            dgwMember.Columns["id_no"].HeaderText = "TC Kimlik No";
            dgwMember.Columns["ad"].HeaderText = "İsim";
            dgwMember.Columns["sad"].HeaderText = "Soyisim";
            dgwMember.Columns["tel"].HeaderText = "Telefon";
            dgwMember.Columns["adres"].HeaderText = "Adres";
            con.Close();
        }

        private void txtSearchIdNo_TextChanged(object sender, EventArgs e)
        {
            dataView = table.DefaultView;
            dataView.RowFilter = "id_no LIKE '%" + txtSearchIdNo.Text + "%'";
            dgwMember.DataSource = dataView;
        }

        private void txtSearchName_TextChanged(object sender, EventArgs e)
        {
            dataView = table.DefaultView;
            dataView.RowFilter = "ad LIKE '%" + txtSearchName.Text + "%'";
            dgwMember.DataSource = dataView;
        }

        private void txtSearchIdNo_Enter(object sender, EventArgs e)
        {
            txtSearchName.Clear();
        }

        private void txtSearchName_Enter(object sender, EventArgs e)
        {
            txtSearchIdNo.Clear();
        }
     
    }
}

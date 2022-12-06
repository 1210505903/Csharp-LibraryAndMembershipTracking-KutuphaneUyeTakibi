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
    public partial class uyeduzenle : Form
    {
        public uyeduzenle()
        {
            InitializeComponent();
        }
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "\\kutuphaneprojem.accdb");
        DataTable table = new DataTable();
        DataView dataView = new DataView();
        public string idNo, name, surname, tel, address;

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

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void contextMenuStrip1_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["uyeekle"] == null)
            {
                uyeekle form = new uyeekle();
                form.Show();
            }
            else
                Application.OpenForms["uyeekle"].Activate();
            this.Close();
        }

        private void dgwMember_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgwMember_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtIdNo.Text = dgwMember.CurrentRow.Cells["id_no"].Value.ToString();
            txtName.Text = dgwMember.CurrentRow.Cells["ad"].Value.ToString();
            txtSurname.Text = dgwMember.CurrentRow.Cells["sad"].Value.ToString();
            txtTel.Text = dgwMember.CurrentRow.Cells["tel"].Value.ToString();
            txtAddress.Text = dgwMember.CurrentRow.Cells["adres"].Value.ToString();
        }
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtIdNo.Text = dgwMember.CurrentRow.Cells[0].Value.ToString();
            txtName.Text = dgwMember.CurrentRow.Cells[1].Value.ToString();
            txtSurname.Text = dgwMember.CurrentRow.Cells[2].Value.ToString();
            txtTel.Text = dgwMember.CurrentRow.Cells[3].Value.ToString();
            txtAddress.Text = dgwMember.CurrentRow.Cells[4].Value.ToString();
        }
        private void txtSearchIdNo_Enter(object sender, EventArgs e)
        {
            txtSearchName.Clear();
        }

        private void txtSearchName_Enter(object sender, EventArgs e)
        {
            txtSearchIdNo.Clear();
        }

        private void butGuncel_Click(object sender, EventArgs e)
        {
            if (txtIdNo.Text == "" || txtName.Text == "" || txtSurname.Text == "" || txtTel.Text == "" || txtAddress.Text == "")
            {
                MessageBox.Show("Lütfen boş alan bırakmayın!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    con.Open();
                    OleDbCommand com = new OleDbCommand("update kullanici set ad=@ad,sad=@sad,tel=@tel,adres=@adres where id_no=@id_no", con);
                    com.Parameters.AddWithValue("@id_no", txtIdNo.Text);
                    com.Parameters.AddWithValue("@ad", txtName.Text);
                    com.Parameters.AddWithValue("@sad", txtSurname.Text);
                    com.Parameters.AddWithValue("@tel", txtTel.Text);
                    com.Parameters.AddWithValue("@adres", txtAddress.Text);
                    com.ExecuteNonQuery();
                    MessageBox.Show("Üye bilgileri güncellenmiştir.");
                    con.Close();
                    this.InvokeOnClick(button1, EventArgs.Empty);

                    
                    table.Clear();
                    OleDbDataAdapter adapter = new OleDbDataAdapter("select * from kullanici order by ad", con);
                    adapter.Fill(table);
                    dgwMember.DataSource = table;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);

                    MessageBox.Show("Bilgileri kontrol edin!", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void butDelete_Click(object sender, EventArgs e)
        {
            if (txtIdNo.Text == "")
            {
                MessageBox.Show("TC Kimlik No alanı boş bırakılamaz! Lütfen silmek istediğiniz üyeyi seçin.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    DialogResult answer = new DialogResult();
                    answer = MessageBox.Show("Üye kaydını silmek istediğinize emin misiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (answer == DialogResult.Yes)
                    {
                        con.Open();
                        OleDbCommand com = new OleDbCommand("delete from kullanici where id_no=@id_no", con);
                        com.Parameters.AddWithValue("@id_no", txtIdNo.Text);
                        com.ExecuteNonQuery();
                        MessageBox.Show("Üye kaydı silinmiştir.");
                        con.Close();
                        this.InvokeOnClick(button1, EventArgs.Empty);
                        table.Clear();
                        OleDbDataAdapter adapter = new OleDbDataAdapter("select * from kullanici order by ad", con);
                        adapter.Fill(table);
                        dgwMember.DataSource = table;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Hay aksi! Bir hata oluştu. Tekrar deneyin.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtIdNo.Clear();
            txtName.Clear();
            txtSurname.Clear();
            txtTel.Clear();
            txtAddress.Clear();
        }

        private void uyeduzenle_Load(object sender, EventArgs e)
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

            
            txtIdNo.Text = idNo;
            txtName.Text = name;
            txtSurname.Text = surname;
            txtTel.Text = tel;
            txtAddress.Text = address;
        }
    }
}

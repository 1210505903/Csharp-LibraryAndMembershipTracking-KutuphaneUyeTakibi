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
    public partial class uyeekle : Form
    {
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "\\kutuphaneprojem.accdb");
        public uyeekle()
        {
            InitializeComponent();
        }
        public int checkId(string text)
        {
            int result;
            con.Open();
            OleDbCommand com = new OleDbCommand("Select Count(id_no) from kullanici where id_no='" + text + "'", con);
            result = Convert.ToInt32(com.ExecuteScalar());
            con.Close();
            return result;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            txtIdNo.Clear();
            txtName.Clear();
            txtSurname.Clear();
            txtTel.Clear();
            txtAddress.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtIdNo.Text == "")
            {
                MessageBox.Show("TC Kimlik No alanı boş bırakılamaz!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    if (checkId(txtIdNo.Text) != 0)
                    {
                        MessageBox.Show("Bu TC Kimlik Numarası zaten kayıtlı!");
                    }
                    else
                    {
                        con.Open();
                        OleDbCommand com = new OleDbCommand("insert into kullanici (id_no,ad,sad,tel,adres) values (@id_no,@ad,@sad,@tel,@adres)", con);
                        com.Parameters.AddWithValue("@id_no", txtIdNo.Text);
                        com.Parameters.AddWithValue("@ad", txtName.Text);
                        com.Parameters.AddWithValue("@sad", txtSurname.Text);
                        com.Parameters.AddWithValue("@tel", txtTel.Text);
                        com.Parameters.AddWithValue("@adres", txtAddress.Text);
                        com.ExecuteNonQuery();
                        MessageBox.Show("Üye başarıyla kaydedilmiştir.");
                        con.Close();
                        this.InvokeOnClick(button1, EventArgs.Empty);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Hay aksi! Kayıt işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void uyeekle_Load(object sender, EventArgs e)
        {

        }
    }
}

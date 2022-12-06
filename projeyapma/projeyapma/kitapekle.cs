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
    public partial class kitapekle : Form
    {
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "\\kutuphaneprojem.accdb");
        public kitapekle()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtBarcodeNo.Clear();
            txtBookTitle.Clear();
            txtAuthor.Clear();
            txtPageCount.Clear();
            txtShelfNo.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtBarcodeNo.Text == "")
            {
                MessageBox.Show("Barkod No alanı boş bırakılamaz!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    con.Open();
                    OleDbCommand com = new OleDbCommand("insert into book (id,book_ad,yazar,sayfasayi,rafno) values (@id,@book_ad,@yazar,@sayfasayi,@rafno)", con);
                    com.Parameters.AddWithValue("@id", txtBarcodeNo.Text);
                    com.Parameters.AddWithValue("@book_ad", txtBookTitle.Text);
                    com.Parameters.AddWithValue("@yazar", txtAuthor.Text);
                    com.Parameters.AddWithValue("@sayfasayi", txtPageCount.Text);
                    com.Parameters.AddWithValue("@rafno", txtShelfNo.Text);
                    com.ExecuteNonQuery();
                    MessageBox.Show("Kitap kaydı gerçekleştirildi.");
                    con.Close();
                    this.InvokeOnClick(button1, EventArgs.Empty);
                }
                catch (Exception)
                {
                    MessageBox.Show("Hay aksi! Kitap kayıt işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void kitapekle_Load(object sender, EventArgs e)
        {

        }
    }
}

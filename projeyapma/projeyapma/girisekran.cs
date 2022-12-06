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
using System.Security.Cryptography;

namespace projeyapma
{
    public partial class girisekran : Form
    {
        public girisekran()
        {
            InitializeComponent();
        }
        
        private void button_Click(object sender, EventArgs e)
        {
            //KONTROLLERİMİ YAPIYORUM
            if ((txtUserName.Text == "" && txtPassword.Text == ""))
            {
                MessageBox.Show("Bilgilerinizi boş bırakmayınız!");
            }
            else if (txtUserName.Text == "" || txtUserName.Text == "Kullanıcı adı")
            {
                MessageBox.Show("Kullanıcı adınızı girin!");
            }
            else if (txtPassword.Text == "" || txtPassword.Text == "Şifre")
            {
                MessageBox.Show("Şifrenizi girin!");
            }
            else
            {
                //BAĞLANTIMI AÇTIM
                OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "\\kutuphaneprojem.accdb");
                con.Open();
                //SORGUMU YAZIYORUM
                OleDbCommand com = new OleDbCommand("Select * from admin where k_ad=@k_ad AND sifre=@sifre", con);
                //PARAMETRELERİ ÇEKİYORUM
                com.Parameters.AddWithValue("@k_ad", txtUserName.Text);
                com.Parameters.AddWithValue("@sifre", (txtPassword.Text));
                //SPRGULARIMI ÇALIŞTIRIYORUM
                com.ExecuteNonQuery();
                OleDbDataReader dr = com.ExecuteReader();
                if (dr.Read())
                {
                    //ÇALIŞIRSA ANASAYAFAYA ATIYORUM
                    anasayfa form = new anasayfa();                   
                    form.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Kullanıcı adı veya şifre hatalı. Lütfen bilgilerinizi kontrol edin.");
                }
                con.Close();
                con.Dispose();
            }
        }

        private void linklblRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (Application.OpenForms["kaydol"] == null)
            {
                kaydol form = new kaydol();
                form.Show();
            }
            else
                Application.OpenForms["kaydol"].Activate();
        }

    }
}

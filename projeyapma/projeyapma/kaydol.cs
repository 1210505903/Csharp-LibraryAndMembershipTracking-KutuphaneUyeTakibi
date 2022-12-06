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
    public partial class kaydol : Form
    {
        public kaydol()
        {
            InitializeComponent();
        }
        //BAĞLANTI AÇTIM
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "\\kutuphaneprojem.accdb");
        public int checkUsername(string username)
        {
            int result;
            con.Open();
            OleDbCommand com = new OleDbCommand("Select Count(k_ad) from admin where k_ad='" + username + "'", con);
            result = Convert.ToInt32(com.ExecuteScalar());
            con.Close();
            return result;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == ""  || txtPassword.Text == ""  || txtName.Text == "" ||  txtSurname.Text == "" )
            {
                MessageBox.Show("Lütfen boş alan bırakmadığınızdan ve güvenlik sorusu seçtiğinizden emin olun!");
            }
            else
            {
               
                if (checkUsername(txtUsername.Text) != 0)
                {
                    MessageBox.Show("Bu kullanıcı adı başkası tarafından kullanılıyor!");
                }
                else
                {
                    try
                    {                       
                        con.Open();
                        OleDbCommand com = new OleDbCommand();
                        com.Connection = con;
                        com.CommandText = "insert into admin (k_ad , sifre , ad , sad ) values ('" + txtUsername.Text + "','" + txtPassword.Text + "','" + txtName.Text + "','" + txtSurname.Text + "')";
                        com.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Kullanıcı oluşturuldu.");

                        txtUsername.Text = "";
                        txtPassword.Text = "";
                        txtName.Text = "";
                        txtSurname.Text = "";
                        
                    }
                    catch (Exception ex)
                    {
                      
                        MessageBox.Show("Hay aksi! Kayıt sırasında bir problem oluştu. Lütfen tekrar deneyin.");
                        throw new Exception(ex.Message);
                    }
                }
            }
        }

        private string ComputeSHA256Hash(object text)
        {
            throw new NotImplementedException();
        }

        private void linklblLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (Application.OpenForms["girisekran"] == null)
            {
                girisekran form = new girisekran();
                form.Show();
                this.Close();
            }
            else
                Application.OpenForms["girisekran"].Activate();
        }
    }
}

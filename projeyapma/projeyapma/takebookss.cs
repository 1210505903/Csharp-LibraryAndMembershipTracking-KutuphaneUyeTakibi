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
    public partial class takebookss : Form
    {
        public takebookss()
        {
            InitializeComponent();
        }
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "\\kutuphaneprojem.accdb");
        DataTable table = new DataTable();
        DataView dataView = new DataView();
        private void takebookss_Load(object sender, EventArgs e)
        {
            con.Open();
            table.Clear();
            OleDbDataAdapter adapter = new OleDbDataAdapter("select * from bookverme order by ad", con);
            adapter.Fill(table);
            dgwBook.DataSource = table;
            dgwBook.Columns["id"].HeaderText = "TC Kimlik No";
            dgwBook.Columns["ad"].HeaderText = "İsim";
            dgwBook.Columns["sad"].HeaderText = "Soyisim";
            dgwBook.Columns["tel"].HeaderText = "Telefon";
            dgwBook.Columns["adres"].HeaderText = "Adres";
            dgwBook.Columns["barcod_no"].HeaderText = "Barkod No";
            dgwBook.Columns["book_ad"].HeaderText = "Kitap Adı";
            dgwBook.Columns["yazar"].HeaderText = "Yazarı";
            dgwBook.Columns["sayfasayi"].HeaderText = "Sayfa Sayısı";
            dgwBook.Columns["rafno"].HeaderText = "Raf No";
            dgwBook.Columns["verilentrh"].HeaderText = "Verilme Tarihi";
            dgwBook.Columns["teslim_trh"].HeaderText = "Teslim Tarihi";
            con.Close();
        }

        private void albook_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                OleDbCommand com = new OleDbCommand("delete from bookverme where id=@id and barcod_no=@barcod_no", con);
                com.Parameters.AddWithValue("@id", dgwBook.CurrentRow.Cells["id"].Value.ToString());
                com.Parameters.AddWithValue("@barcod_no", dgwBook.CurrentRow.Cells["barcod_no"].Value.ToString());
                com.ExecuteNonQuery();
                table.Clear();
                OleDbDataAdapter adapter = new OleDbDataAdapter("select * from bookverme order by ad", con);
                adapter.Fill(table);
                dgwBook.DataSource = table;
                MessageBox.Show("Kitap iadesi başarılı bir şekilde gerçekleşti.");
                con.Close();
                table.Clear();
                adapter.Fill(table);
                dgwBook.DataSource = table;
            }
            catch (Exception)
            {
                MessageBox.Show("Bilgileri kontrol edin!", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearchBarcodeNo_TextChanged(object sender, EventArgs e)
        {
            dataView = table.DefaultView;
            dataView.RowFilter = "barcod_no LIKE '%" + txtSearchBarcodeNo.Text + "%'";
            dgwBook.DataSource = dataView;
        }

        private void txtSearchIdNo_TextChanged(object sender, EventArgs e)
        {
            dataView = table.DefaultView;
            dataView.RowFilter = "id LIKE '%" + txtSearchIdNo.Text + "%'";
            dgwBook.DataSource = dataView;
        }

        private void txtSearchIdNo_Enter(object sender, EventArgs e)
        {
            txtSearchBarcodeNo.Clear();
        }

        private void txtSearchBarcodeNo_Enter(object sender, EventArgs e)
        {
            txtSearchIdNo.Clear();
        }
    }
}

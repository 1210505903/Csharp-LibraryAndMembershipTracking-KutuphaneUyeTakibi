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
    public partial class emanetlerıshowme : Form
    {
        public emanetlerıshowme()
        {
            InitializeComponent();
        }
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "\\kutuphaneprojem.accdb");
        DataTable table = new DataTable();
        DataView dataView = new DataView();
        private void emanetlerıshowme_Load(object sender, EventArgs e)
        { 
            //TEKER TEKER DATAGRİDVİEW E ÇEKİYORUM
            cmboxSelection.SelectedIndex = 0;
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

        private void txtSearchName_TextChanged(object sender, EventArgs e)
        {
            //AD OLARAK ARAMA YAPIYORUM
            dataView = table.DefaultView;
            dataView.RowFilter = "ad LIKE '%" + txtSearchName.Text + "%'";
            dgwBook.DataSource = dataView;
        }

        private void txtSearchBookTitle_TextChanged(object sender, EventArgs e)
        {
            //KİTAP ADI OLARAK ARAMA YAPIYORUM
            dataView = table.DefaultView;
            dataView.RowFilter = "book_ad LIKE '%" + txtSearchBookTitle.Text + "%'";
            dgwBook.DataSource = dataView;
        }

        private void cmboxSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            //COMBOX DAN İNDEX E GÖRE GETİRELN VE ERTELENLERİ ÇEKİYORUM DAHA KOLAY EASY 
            table.Clear();
            if (cmboxSelection.SelectedIndex == 0)
            {
                OleDbDataAdapter adapter = new OleDbDataAdapter("select * from bookverme order by ad", con);
                adapter.Fill(table);
                dgwBook.DataSource = table;
            }
            else if (cmboxSelection.SelectedIndex == 1)
            {
                OleDbDataAdapter adapter = new OleDbDataAdapter("select * from ertelenen order by ad", con);
                adapter.Fill(table);
                dgwBook.DataSource = table;
            }
            else if (cmboxSelection.SelectedIndex == 2)
            {
                OleDbDataAdapter adapter = new OleDbDataAdapter("select * from gelmeyenler order by ad", con);
                adapter.Fill(table);
                dgwBook.DataSource = table;
            }
        }
        //TIKLANDIĞINDA DİĞER TEXTBOX BOŞ OLSUN DİYE YAZILAN KOD
        private void txtSearchName_Enter(object sender, EventArgs e)
        {
            txtSearchBookTitle.Text = "";
        }

        private void txtSearchBookTitle_Enter(object sender, EventArgs e)
        {
            txtSearchName.Text = "";
        }
    }
}

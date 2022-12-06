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
    public partial class booklist : Form
    {
        public booklist()
        {
            InitializeComponent();
        }
        //bAĞLANTIMI AÇTIM
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "\\kutuphaneprojem.accdb");
        DataTable table = new DataTable();
        DataView dataView = new DataView();
        private void booklist_Load(object sender, EventArgs e)
        {
            //DİREK GELMESİ İÇİN LOAD İN İÇİNDE SÜTÜNÜME VERİLERİ ÇEKİYORUM
            con.Open();
            OleDbDataAdapter adapter = new OleDbDataAdapter("select * from book order by book_ad", con);
            adapter.Fill(table);
            dgwBook.DataSource = table;
            dgwBook.Columns["id"].HeaderText = "Barkod No";
            dgwBook.Columns["book_ad"].HeaderText = "Kitap Adı";
            dgwBook.Columns["yazar"].HeaderText = "Yazarı";
            dgwBook.Columns["sayfasayi"].HeaderText = "Sayfa Sayısı";
            dgwBook.Columns["rafno"].HeaderText = "Raf No";
            con.Close();
        }

        private void txtSearchBookTitle_TextChanged(object sender, EventArgs e)
        {
            //ARAMA YAPIYORUM LİKE SORGUSU İLE
            dataView = table.DefaultView;
            dataView.RowFilter = "book_ad LIKE '%" + txtSearchBookTitle.Text + "%'";
            dgwBook.DataSource = dataView;
        }
    }
}

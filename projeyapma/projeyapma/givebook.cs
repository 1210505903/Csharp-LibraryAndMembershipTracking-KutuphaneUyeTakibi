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
    public partial class givebook : Form
    {
        public givebook()
        {
            InitializeComponent();
        }
        //BAĞLANTIMI AÇTIM
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "\\kutuphaneprojem.accdb");
        //2 ADET DATATABLE AÇTIM 2 AD GRİDVİEW VAR
        DataTable tableMember = new DataTable();
        DataTable tableBook = new DataTable();
        DataView dataView = new DataView();
        private void givebook_Load(object sender, EventArgs e)
        {
            // 2 GRİDVİEW İ DE SÜTÜNLARINI ÇEKİYORUM
            con.Open();
            tableMember.Clear();
            OleDbDataAdapter adrMember = new OleDbDataAdapter("select * from kullanici order by ad", con);
            adrMember.Fill(tableMember);
            dgwMember.DataSource = tableMember;
            dgwMember.Columns["id_no"].HeaderText = "TC Kimlik No";
            dgwMember.Columns["ad"].HeaderText = "İsim";
            dgwMember.Columns["sad"].HeaderText = "Soyisim";
            dgwMember.Columns["tel"].HeaderText = "Telefon";
            dgwMember.Columns["adres"].HeaderText = "Adres";
            tableBook.Clear();
            OleDbDataAdapter adrBook = new OleDbDataAdapter("select * from book order by book_ad", con);
            adrBook.Fill(tableBook);
            dgwBook.DataSource = tableBook;
            dgwBook.Columns["id"].HeaderText = "Barkod No";
            dgwBook.Columns["book_ad"].HeaderText = "Kitap Adı";
            dgwBook.Columns["yazar"].HeaderText = "Yazarı";
            dgwBook.Columns["sayfasayi"].HeaderText = "Sayfa Sayısı";
            dgwBook.Columns["rafno"].HeaderText = "Raf No";
            con.Close();
        }

        private void giveemanet_Click(object sender, EventArgs e)
        {
            DialogResult secenek = MessageBox.Show("Emanet etmek İstediğine Eminmisinn ?", "Emanet Veriliyor...", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (secenek == DialogResult.Yes)
            {
                try
                {
                    //BOOKVERME ADLI SAYFAAMA KAYIT EDİYORUM.
                    con.Open();
                    OleDbCommand com = new OleDbCommand("insert into bookverme (id,ad,sad,tel,adres,barcod_no,book_ad,yazar,sayfasayi,rafno,verilentrh,teslim_trh)" +
                    " values (@id,@ad,@sad,@tel,@adres,@barcod_no,@book_ad,@yazar,@sayfasayi,@rafno,@verilentrh,@teslim_trh)", con);
                    com.Parameters.AddWithValue("@id", dgwMember.CurrentRow.Cells["id_no"].Value.ToString());
                    com.Parameters.AddWithValue("@ad", dgwMember.CurrentRow.Cells["ad"].Value.ToString());
                    com.Parameters.AddWithValue("@sad", dgwMember.CurrentRow.Cells["sad"].Value.ToString());
                    com.Parameters.AddWithValue("@tel", dgwMember.CurrentRow.Cells["tel"].Value.ToString());
                    com.Parameters.AddWithValue("@adrs", dgwMember.CurrentRow.Cells["adres"].Value.ToString());
                    com.Parameters.AddWithValue("@barcod_no", dgwBook.CurrentRow.Cells["id"].Value.ToString());
                    com.Parameters.AddWithValue("@book_ad", dgwBook.CurrentRow.Cells["book_ad"].Value.ToString());
                    com.Parameters.AddWithValue("@yazar", dgwBook.CurrentRow.Cells["yazar"].Value.ToString());
                    com.Parameters.AddWithValue("@sayfasayi", dgwBook.CurrentRow.Cells["sayfasayi"].Value.ToString());
                    com.Parameters.AddWithValue("@rafno", dgwBook.CurrentRow.Cells["rafno"].Value.ToString());
                    com.Parameters.AddWithValue("@verilentrh", dtpIssueDate.Value.ToString());
                    com.Parameters.AddWithValue("@teslim_trh", dtpSubmissionDate.Value.ToString());
                    com.ExecuteNonQuery();
                    MessageBox.Show("Kitap emanet işlemi başarılı bir şekilde gerçekleşti.");
                    con.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);

                    MessageBox.Show("Bilgileri kontrol edin!", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else if (secenek == DialogResult.No)
            {
                dtpIssueDate.Text = "";
                dtpSubmissionDate.Text = "";
            }
        }

        private void txtSearchIdNo_TextChanged(object sender, EventArgs e)
        {
            //İD İLE ARAMA YAPMA
            dataView = tableMember.DefaultView;
            dataView.RowFilter = "id_no LIKE '%" + txtSearchIdNo.Text + "%'";
            dgwMember.DataSource = dataView;
        }

        private void txtSearchBarcodeNo_TextChanged(object sender, EventArgs e)
        {
            //BARCOD NO İLE ARAMA YAPMA
            dataView = tableBook.DefaultView;
            dataView.RowFilter = "barcod_no LIKE '%" + txtSearchBarcodeNo.Text + "%'";
            dgwBook.DataSource = dataView;
        }
    }
}

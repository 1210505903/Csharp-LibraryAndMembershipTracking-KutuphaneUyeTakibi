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
    public partial class bookduzenle : Form
    {
        public bookduzenle()
        {
            InitializeComponent();
        }
        //DATABASEB BAĞLANIYORUM
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "\\kutuphaneprojem.accdb");
        DataTable table = new DataTable();
        DataView dataView = new DataView();
        

        private void txtSearchBookTitle_TextChanged(object sender, EventArgs e)
        {
            //ARAMA YAPIYORUZ.ARAMAYI DATA GRİDWİEV DEN YAPIYORUZ
            dataView = table.DefaultView;
            dataView.RowFilter = "book_ad LIKE '%" + txtSearchBookTitle.Text + "%'";
            dgwBook.DataSource = dataView;
        }

        private void bookduzenle_Load(object sender, EventArgs e)
        {
            //DATABASEDEKİ TABLOYU SÜTÜNE EKLİYORUM. SAYFA ÇALIŞINCA DİREK GELMESİ İÇİN LOAD A YAZILI
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

        private void butguncelle_Click(object sender, EventArgs e)
        {
            //BOŞ ALAN KONTRÖLÜ YAPIYORUM
            if (txtBarcodeNo.Text == "" || txtBookTitle.Text == "" || txtAuthor.Text == "" || txtPageCount.Text == "" || txtShelfNo.Text == "")
            {
                MessageBox.Show("Lütfen boş alan bırakmayın!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    //BAGLANTIMI AÇIP COMMENDE CODUMU YAZIP ÇALIŞTIYORUM TRY CAH İLE KONTOL EDİYORUM. KAYIT OLURSA TABLOYU YENİLİYORUM.
                    con.Open();
                    OleDbCommand com = new OleDbCommand("update book set id=@id,book_ad=@book_ad,yazar=@yazar,sayfasayi=@sayfasayi," +
                        "rafno=@rafno where id=@id", con);
                    com.Parameters.AddWithValue("@id", txtBarcodeNo.Text);
                    com.Parameters.AddWithValue("@book_ad", txtBookTitle.Text);
                    com.Parameters.AddWithValue("@yazar", txtAuthor.Text);
                    com.Parameters.AddWithValue("@sayfasayi", txtPageCount.Text);
                    com.Parameters.AddWithValue("@rafno", txtShelfNo.Text);
                    com.ExecuteNonQuery();
                    MessageBox.Show("Kitap bilgileri güncellenmiştir.");
                    con.Close();
                    this.InvokeOnClick(butclear, EventArgs.Empty);
                    table.Clear();
                    OleDbDataAdapter adapter = new OleDbDataAdapter("select * from book order by book_ad", con);
                    adapter.Fill(table);
                    dgwBook.DataSource = table;
                }
                catch (Exception)
                {
                    MessageBox.Show("Bilgileri kontrol edin!", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgwBook_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //TIKLANDIĞINDA TEXTBOX LARA ATMASI İÇİN TEKER TEKER ÇEKİYORUM
            txtBarcodeNo.Text = dgwBook.CurrentRow.Cells["id"].Value.ToString();
            txtBookTitle.Text = dgwBook.CurrentRow.Cells["book_ad"].Value.ToString();
            txtAuthor.Text = dgwBook.CurrentRow.Cells["yazar"].Value.ToString();
            txtPageCount.Text = dgwBook.CurrentRow.Cells["sayfasayi"].Value.ToString();
            txtShelfNo.Text = dgwBook.CurrentRow.Cells["rafno"].Value.ToString();
        }
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TIKLANDIĞINDA TEXTBOX LARA ATMASI İÇİN TEKER TEKER ÇEKİYORUM
            txtBarcodeNo.Text = dgwBook.CurrentRow.Cells[0].Value.ToString();
            txtBookTitle.Text = dgwBook.CurrentRow.Cells[1].Value.ToString();
            txtAuthor.Text = dgwBook.CurrentRow.Cells[2].Value.ToString();
            txtPageCount.Text = dgwBook.CurrentRow.Cells[3].Value.ToString();
            txtShelfNo.Text = dgwBook.CurrentRow.Cells[4].Value.ToString();
        }

        private void but_del_Click(object sender, EventArgs e)
        {
            //BOŞ ALAN KONTROLÜ YAPTIM
            if (txtBarcodeNo.Text == "")
            {
                MessageBox.Show("Barkod No alanı boş bırakılamaz! Lütfen silmek istediğiniz kitabı seçin.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    //KALİTELİ ŞEKİLDE SORU SORUYORUM DİREK SİLMİYORUM
                    DialogResult answer = new DialogResult();
                    answer = MessageBox.Show("Kitap kaydını silmek istediğinize emin misiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (answer == DialogResult.Yes)
                    {
                        //BAĞLANTIMI AÇIP DELETE SORGUSU İLE İD YE GÖRE SİLİYORUM VE GRİDVİEW İ YENİLİYORUM.
                        con.Open();
                        OleDbCommand com = new OleDbCommand("delete from book where id=@id", con);
                        com.Parameters.AddWithValue("@id", txtBarcodeNo.Text);
                        com.ExecuteNonQuery();
                        MessageBox.Show("Kitap kaydı silinmiştir.");
                        con.Close();
                        this.InvokeOnClick(butclear, EventArgs.Empty);
                        table.Clear();
                        OleDbDataAdapter adapter = new OleDbDataAdapter("select * from book order by book_ad", con);
                        adapter.Fill(table);
                        dgwBook.DataSource = table;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Hay aksi! Bir hata oluştu. Tekrar deneyin.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}

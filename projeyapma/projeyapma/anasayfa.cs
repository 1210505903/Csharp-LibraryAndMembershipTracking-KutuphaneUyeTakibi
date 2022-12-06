using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projeyapma
{
    public partial class anasayfa : Form
    {
        public anasayfa()
        {
            InitializeComponent();
        }
        //HERHANGİ BİR ÖNEMlİ KOD SATIRI YOK SADECE SAYFAYA YÖNLENDİRME BUTONLARI MEVCUT.
        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["uyeekle"] == null)
            {
                uyeekle form = new uyeekle();
                form.Show();
            }
            else
                Application.OpenForms["uyeekle"].Activate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["uyeduzenle"] == null)
            {
                uyeduzenle form = new uyeduzenle();
                form.Show();
            }
            else
                Application.OpenForms["uyeduzenle"].Activate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["uyeliste"] == null)
            {
                uyeliste form = new uyeliste();
                form.Show();
            }
            else
                Application.OpenForms["uyeliste"].Activate();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["kitapekle"] == null)
            {
                kitapekle form = new kitapekle();
                form.Show();
            }
            else
                Application.OpenForms["kitapekle"].Activate();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["booklist"] == null)
            {
                booklist form = new booklist();
                form.Show();
            }
            else
                Application.OpenForms["booklist"].Activate();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["givebook"] == null)
            {
                givebook form = new givebook();
                form.Show();
            }
            else
                Application.OpenForms["givebook"].Activate();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["takebookss"] == null)
            {
                takebookss form = new takebookss();
                form.Show();
            }
            else
                Application.OpenForms["takebookss"].Activate();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["emanetlerıshowme"] == null)
            {
                emanetlerıshowme form = new emanetlerıshowme();
                form.Show();
            }
            else
                Application.OpenForms["emanetlerıshowme"].Activate();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["bookduzenle"] == null)
            {
                bookduzenle form = new bookduzenle();
                form.Show();
            }
            else
                Application.OpenForms["bookduzenle"].Activate();
        }

        private void anasayfa_Load(object sender, EventArgs e)
        {

        }
    }
}

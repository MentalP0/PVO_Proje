using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Personel_Vardiya_Otomasyonu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnYoneticiGirisi_Click(object sender, EventArgs e)
        {
            Giris giris = new Giris(); // Giriş formuna yönlendirme
            giris.Show();
            this.Hide();
        }

        private void btnKullaniciGirisi_Click(object sender, EventArgs e)
        {
            Giris giris = new Giris(); // Giriş formuna yönlendirme
            giris.Show();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

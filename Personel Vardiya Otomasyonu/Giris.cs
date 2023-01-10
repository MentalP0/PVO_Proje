using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Personel_Vardiya_Otomasyonu
{
    public partial class Giris : Form
    {
        public Giris()
        {
            InitializeComponent();
        }

        SqlConnection sqlConnection = new SqlConnection("DATA SOURCE=LAPTOP-5AM136N5\\SQLEXPRESS; INITIAL CATALOG=DbPersonelVardiya; INTEGRATED SECURITY=TRUE");

        private void btnGirisYap_Click(object sender, EventArgs e)
        {
            if (txtTc.Text == "admin" && txtSifre.Text == "1234")    /*  
                                                                      *  
                                                                      
                                                                      Eğer bililer admine aitse yönetici paneline yönlendir
                                                                      Değilse girilen bilgiler veritabanına var mı yok mu                                          konrol et eğer varsa kullanıcı paneline yönlendir.
                                                                      
                                                                      
                                                                      */
            {
                YoneticiPanel yoneticiPanel = new YoneticiPanel();
                yoneticiPanel.Show();
                this.Hide();
            }
            else
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Personeller WHERE Tc = '" + txtTc.Text + "' AND Sifre = '" + txtSifre.Text + "'", sqlConnection))
                {
                    sqlConnection.Open();

                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        if (sqlDataReader.Read())
                        {
                            KullaniciPanel kullaniciPanel = new KullaniciPanel();
                            kullaniciPanel.tc = txtTc.Text;
                            kullaniciPanel.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Bilgileriniz hatalı!", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }

                    sqlConnection.Close();
                }
            }
        }

        private void Giris_Load(object sender, EventArgs e)
        {

        }

        private void Giris_Load_1(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}

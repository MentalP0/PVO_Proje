using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Personel_Vardiya_Otomasyonu
{
    public partial class YoneticiPanel : Form
    {
        public YoneticiPanel()
        {
            InitializeComponent();
        }

        SqlConnection sqlConnection = new SqlConnection("DATA SOURCE=LAPTOP-5AM136N5\\SQLEXPRESS; INITIAL CATALOG=DbPersonelVardiya; INTEGRATED SECURITY=TRUE");
        private void YoneticiPanel_Load(object sender, EventArgs e)
        {

            VerileriGetir();

            using (SqlCommand sqlCommand = new SqlCommand("SELECT Unvan FROM Unvanlar", sqlConnection))
            {
                sqlConnection.Open();

                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        cmbUnvan.Items.Add(sqlDataReader.GetValue(0));
                    }
                    
                }

                sqlConnection.Close();

            }


        }

        private void VerileriGetir()


        {

            // Personelleri listele

            using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM Personeller", sqlConnection))
            {
                using (DataTable dataTable = new DataTable())
                {
                    sqlConnection.Open();

                    sqlDataAdapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;

                    sqlConnection.Close();

                }
            }
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            // Yeni personel ekle

            using (SqlCommand komut = new SqlCommand("SELECT * FROM Personeller WHERE Tc = '" + txtTc.Text + "'", sqlConnection))
            {

                sqlConnection.Open();

                using (SqlDataReader oku = komut.ExecuteReader())
                {
                    if (oku.Read())
                    {
                        MessageBox.Show("Bu kullanıcı zaten sistemde kayıtlı!", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                    else
                    {
                        oku.Close();

                        var telefon = txtTelefon.Text.Replace("(", "").Replace(")", "").Replace("-", "").Trim().Replace(" ", "");

                        using (SqlCommand sqlCommand = new SqlCommand("INSERT INTO Personeller VALUES ('" + txtAd.Text + "','" + txtSoyad.Text + "','" + txtTc.Text + "','" + txtAdres.Text + "','" + telefon + "','" + txtMail.Text + "','" + txtSicilNo.Text + "','" + cmbKadro.Text + "','" + cmbUnvan.Text + "','" + txtSifre.Text + "')", sqlConnection))
                        {

                            sqlCommand.ExecuteNonQuery();

                            MessageBox.Show("Personel eklendi!", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);

                            sqlConnection.Close();

                            VerileriGetir();
                        }
                    }
                }

                sqlConnection.Close();
            }


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Tıkladığım kayıtın bilgilerini ilgili yerlere doldur.

            txtAd.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtTc.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtAdres.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtTelefon.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            txtMail.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            txtSicilNo.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            cmbKadro.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            cmbUnvan.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
            txtSifre.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {

            // Seçtiğim kayıtı güncelle

            var personelId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());

            var telefon = txtTelefon.Text.Replace("(", "").Replace(")", "").Replace("-", "").Trim().Replace(" ", "");

            using (SqlCommand sqlCommand = new SqlCommand("UPDATE Personeller SET Ad = '" + txtAd.Text + "', Soyad = '" + txtSoyad.Text + "', Tc = '" + txtTc.Text + "', Adres = '" + txtAdres.Text + "', Telefon = '" + telefon + "', Mail = '" + txtMail.Text + "' , SicilNo = '" + txtSicilNo.Text + "', Kadro = '" + cmbKadro.Text + "', Unvan = '" + cmbUnvan.Text + "', Sifre = '" + txtSifre.Text + "' WHERE Id = '" + personelId + "'", sqlConnection))
            {
                sqlConnection.Open();

                sqlCommand.ExecuteNonQuery();

                MessageBox.Show("Personel güncellendi!", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);

                sqlConnection.Close();

                VerileriGetir();
            }


        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            // Seçtiğim personeli sil

            var personelId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());

            using (SqlCommand sqlCommand = new SqlCommand("DELETE FROM Personeller WHERE Id = '" + personelId + "'", sqlConnection))
            {
                sqlConnection.Open();

                sqlCommand.ExecuteNonQuery();

                MessageBox.Show("Personel silindi!", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);

                sqlConnection.Close();

                VerileriGetir();


            }
        }

        private void btnNobetIslemleri_Click(object sender, EventArgs e)
        {
            // Nöbet işlemleri paneline yönlendir.

            NobetIslemleri nobetIslemleri = new NobetIslemleri();
            nobetIslemleri.Show();
            this.Hide();
        }

        private void YoneticiPanel_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Yönetici paneli kapanırsa giriş formuna tekrar yönlendir.

            Giris giris = new Giris();
            giris.Show();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnUnvanIslemleri_Click(object sender, EventArgs e)
        {
            UnvanIslemleri unvanIslemleri = new UnvanIslemleri();
            unvanIslemleri.Show();
            this.Hide();
        }
    }
}

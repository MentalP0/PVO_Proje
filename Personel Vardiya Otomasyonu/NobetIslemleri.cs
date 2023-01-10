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
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;

namespace Personel_Vardiya_Otomasyonu
{
    public partial class NobetIslemleri : Form
    {
        public NobetIslemleri()
        {
            InitializeComponent();
        }

        SqlConnection sqlConnection = new SqlConnection("DATA SOURCE=LAPTOP-5AM136N5\\SQLEXPRESS; INITIAL CATALOG=DbPersonelVardiya; INTEGRATED SECURITY=TRUE");
        private void VerileriGetir()


        {

            /* Nöbetleri listele ve combobox'a personelleri listele  */

            using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT c.Id,Tarih,Konum,Saat,Personel,Ad FROM Nobetler c INNER JOIN Personeller ON c.Personel = Personeller.Id", sqlConnection))
            {
                using (DataTable dataTable = new DataTable())
                {
                    sqlConnection.Open();

                    sqlDataAdapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;

                    sqlConnection.Close();

                }
            }

            using (SqlCommand sqlCommand = new SqlCommand("SELECT Ad FROM Personeller ", sqlConnection))
            {
                sqlConnection.Open();

                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        cmbPersonel.Items.Add(sqlDataReader.GetValue(0));
                    }
                }

                sqlConnection.Close();
            }

        }

        private void NobetIslemleri_Load(object sender, EventArgs e)
        {
            VerileriGetir();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            // Yeni nöbet ekle

            var personel = 0;

            using (SqlCommand sqlCommand = new SqlCommand("SELECT Id FROM Personeller WHERE Ad = '" + cmbPersonel.Text + "'", sqlConnection))
            {
                sqlConnection.Open();

                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        personel = Convert.ToInt32(sqlDataReader.GetValue(0).ToString());
                    }
                }

                sqlConnection.Close();
            }

            using (SqlCommand sqlCommand = new SqlCommand("INSERT INTO Nobetler VALUES (@tarih,@konum,@saat,@personel)", sqlConnection))
            {
                sqlCommand.Parameters.AddWithValue("@tarih", dateTimePicker1.Value.ToShortDateString());
                sqlCommand.Parameters.AddWithValue("@konum", cmbKonum.Text);
                sqlCommand.Parameters.AddWithValue("@saat", comboBox1.Text);
                sqlCommand.Parameters.AddWithValue("@personel", personel);

                sqlConnection.Open();

                sqlCommand.ExecuteNonQuery();

                MessageBox.Show("Nöbet eklendi!", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);

                sqlConnection.Close();

                VerileriGetir();
            }



        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {

            // Var olan nöbeti güncelle

            var personelAd = cmbPersonel.Text;

            var personelId = 0;

            using (SqlCommand sqlCommand = new SqlCommand("SELECT Id FROM Personeller WHERE Ad = '" + personelAd + "'", sqlConnection))
            {
                sqlConnection.Open();

                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        personelId = Convert.ToInt32(sqlDataReader.GetValue(0).ToString());

                    }
                }

                sqlConnection.Close();
            }


            var nobetId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());

            using (SqlCommand sqlCommand = new SqlCommand("UPDATE Nobetler SET Tarih = '" + dateTimePicker1.Value.ToShortDateString() + "', Konum = '" + cmbKonum.Text + "', Saat = '" + comboBox1.Text + "',Personel = '" + personelId + "' WHERE Id ='" + nobetId + "'", sqlConnection))
            {
                sqlConnection.Open();

                sqlCommand.ExecuteNonQuery();

                MessageBox.Show("Nöbet güncellendi!", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);

                sqlConnection.Close();

                VerileriGetir();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            // Tıkladığım kayıtın bilgilerini ilgili yerlere doldur.

            var personelId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[4].Value.ToString());

            var personelAd = "";

            using (SqlCommand sqlCommand = new SqlCommand("SELECT Ad FROM Personeller WHERE Id = '" + personelId + "'", sqlConnection))
            {
                sqlConnection.Open();

                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        personelAd = sqlDataReader.GetValue(0).ToString();

                    }
                }

                sqlConnection.Close();
            }



            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            cmbKonum.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            cmbPersonel.Text = personelAd;
        }

        private void btnSil_Click(object sender, EventArgs e)
        {

            // Seçtiğim nöbet kaydını sil

            var nobetId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());

            using (SqlCommand sqlCommand = new SqlCommand("DELETE FROM Nobetler WHERE Id = '" + nobetId + "'", sqlConnection))
            {
                sqlConnection.Open();

                sqlCommand.ExecuteNonQuery();

                MessageBox.Show("Nöbet silindi!", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);

                sqlConnection.Close();

                VerileriGetir();


            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "PDF (*.pdf)|*.pdf";
                save.FileName = "Result.pdf";
                bool ErrorMessage = false;
                if (save.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(save.FileName))
                    {
                        try
                        {
                            File.Delete(save.FileName);
                        }
                        catch (Exception ex)
                        {
                            ErrorMessage = true;
                            MessageBox.Show("Veriler diske yazılamıyor!" + ex.Message);
                        }
                    }
                    if (!ErrorMessage)
                    {
                        try
                        {
                            PdfPTable pTable = new PdfPTable(dataGridView1.Columns.Count);
                            pTable.DefaultCell.Padding = 2;
                            pTable.WidthPercentage = 100;
                            pTable.HorizontalAlignment = Element.ALIGN_LEFT;
                            foreach (DataGridViewColumn col in dataGridView1.Columns)
                            {
                                PdfPCell pCell = new PdfPCell(new Phrase(col.HeaderText));
                                pTable.AddCell(pCell);
                            }
                            foreach (DataGridViewRow viewRow in dataGridView1.Rows)
                            {
                                foreach (DataGridViewCell dcell in viewRow.Cells)
                                {
                                    pTable.AddCell(dcell.Value.ToString());
                                }
                            }
                            using (FileStream fileStream = new FileStream(save.FileName, FileMode.Create))
                            {
                                Document document = new Document(PageSize.A4, 8f, 16f, 16f, 8f);
                                PdfWriter.GetInstance(document, fileStream);
                                document.Open();
                                document.Add(pTable);
                                document.Close();
                                fileStream.Close();
                            }
                            MessageBox.Show("Yazdırma başarılı!", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Veriler aktarılırken hata oluştu!" + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Kayıt bulunamadı!", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void NobetIslemleri_FormClosed(object sender, FormClosedEventArgs e)
        {
            YoneticiPanel yoneticiPanel = new YoneticiPanel();
            yoneticiPanel.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
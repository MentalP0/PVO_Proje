using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Personel_Vardiya_Otomasyonu
{
    public partial class KullaniciPanel : Form
    {
        public KullaniciPanel()
        {
            InitializeComponent();
        }
        SqlConnection sqlConnection = new SqlConnection("DATA SOURCE=LAPTOP-5AM136N5\\SQLEXPRESS; INITIAL CATALOG=DbPersonelVardiya; INTEGRATED SECURITY=TRUE");
        private void KullaniciPanel_FormClosed(object sender, FormClosedEventArgs e)
        {
            Giris giris = new Giris();
            giris.Show();
        }

        public string tc = "";

        private void KullaniciPanel_Load(object sender, EventArgs e)
        {

            /* Tc'ye ait nöbetleri listele */

            var personelId = 0;

            using (SqlCommand sqlCommand = new SqlCommand("SELECT Id FROM Personeller WHERE Tc = '" + tc + "'", sqlConnection))
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


            using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM Nobetler WHERE Personel = '" + personelId + "'", sqlConnection))
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
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
    }
}


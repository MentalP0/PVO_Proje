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
    public partial class UnvanIslemleri : Form
    {
        public UnvanIslemleri()
        {
            InitializeComponent();
        }

        SqlConnection sqlConnection = new SqlConnection("DATA SOURCE=LAPTOP-5AM136N5\\SQLEXPRESS; INITIAL CATALOG=DbPersonelVardiya; INTEGRATED SECURITY=TRUE");

        private void UnvanIslemleri_FormClosed(object sender, FormClosedEventArgs e)
        {
            YoneticiPanel yoneticiPanel = new YoneticiPanel();
            yoneticiPanel.Show();
        }

        private void UnvanIslemleri_Load(object sender, EventArgs e)
        {
            VerileriGetir();
        }

        private void VerileriGetir()


        {

            using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM Unvanlar", sqlConnection))
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtUnvan.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            using (SqlCommand sqlCommand = new SqlCommand("INSERT INTO Unvanlar VALUES ('" + txtUnvan.Text + "')", sqlConnection))
            {

                sqlConnection.Open();

                sqlCommand.ExecuteNonQuery();

                MessageBox.Show("Ünvan eklendi!", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);

                sqlConnection.Close();

                VerileriGetir();
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            using (SqlCommand sqlCommand = new SqlCommand("UPDATE Unvanlar SET Unvan = '" + txtUnvan.Text + "' WHERE Id ='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'", sqlConnection))
            {
                sqlConnection.Open();

                sqlCommand.ExecuteNonQuery();

                MessageBox.Show("Ünvan güncellendi!", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);

                sqlConnection.Close();

                VerileriGetir();
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            using (SqlCommand sqlCommand = new SqlCommand("DELETE FROM Unvanlar WHERE Id = '" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'", sqlConnection))
            {
                sqlConnection.Open();

                sqlCommand.ExecuteNonQuery();

                MessageBox.Show("Ünvan silindi!", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);

                sqlConnection.Close();

                VerileriGetir();


            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

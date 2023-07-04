using DisconnectedEnvironment;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DisconnectedEnvironment
{
    public partial class Form2 : Form
    {
        private string stringConnection = "data source=DESKTOP-4AE5TIF\\ALWAN_FA;" + "database=disconnected_envi;User ID=sa;Password=123";
        private SqlConnection koneksi;

        private void refreshform()
        {
            textBox1.Text = "";
            textBox1.Enabled = false;
            button4.Enabled = false;
            button3.Enabled = false;
        }
        public Form2()
        {
            InitializeComponent();
            koneksi = new SqlConnection(stringConnection);
            refreshform();
        }

        private void dataGridView()
        {
            koneksi.Open();
            string str = "select id_prodi, nama_prodi from dbo.prodi";
            SqlDataAdapter da = new SqlDataAdapter(str, koneksi);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            koneksi.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView();
            button1.Enabled = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = true;
            textBox1.Enabled = true;
            textBox1.Text = "";
            button4.Enabled = true;
            button3.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string nmProdi = textBox1.Text.Trim();
            string idProdi = npm.Text.Trim();
            if (nmProdi == "")
            {
                MessageBox.Show("masukkan nama prodi", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else if (idProdi == "")
            {
                MessageBox.Show("masukkan ID prodi", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                koneksi.Open();
                string str = "INSERT INTO prodi (id_prodi, nama_prodi) VALUES (@id_prodi, @nama_prodi)";
                SqlCommand cmd = new SqlCommand(str, koneksi);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("id_prodi", idProdi));
                cmd.Parameters.Add(new SqlParameter("nama_prodi", nmProdi));
                cmd.ExecuteNonQuery();

                koneksi.Close();
                MessageBox.Show("data berhasil disimpan", "succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView();
                refreshform();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            refreshform();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            Form1 hu = new Form1();
            hu.Show();
            this.Hide();
        }
    }
}

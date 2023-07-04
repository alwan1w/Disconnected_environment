using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace DisconnectedEnvironment
{
    public partial class FormDataMahasiswa : Form
    {
        private string stringConnection = "data source=DESKTOP-4AE5TIF\\ALWAN_FA;" + "database=disconnected_envi;User ID=sa;Password=123";
        private SqlConnection koneksi;
        private string nim, nama, alamat, jk, prodi;
        private DateTime tgl;
        BindingSource customersBindingSource = new BindingSource();

        public FormDataMahasiswa()
        {
            InitializeComponent();
            koneksi = new SqlConnection(stringConnection);
            this.bnMahasiswa.BindingSource = this.customersBindingSource;
            refreshform();

        }

        private void FormDataMahasiswa_Load()
        {
            koneksi = new SqlConnection(stringConnection);
            {
                koneksi.Open();
                SqlDataAdapter dataAdapter1 = new SqlDataAdapter(
                    new SqlCommand(
                        "Select m.nim, m.nama_mahasiswa, " + "m.alamat, m.jenis_kelamin, m.tgl_lahir, p.nama_prodi From dbo.Mahasiswa m " +
                        "join dbo.Prodi p on m.id_prodi = p.id_prodi",
                        koneksi
                    )
                );
                DataSet ds = new DataSet();
                dataAdapter1.Fill(ds);
                this.customersBindingSource.DataSource = ds.Tables[0];
                this.txtNIM.DataBindings.Add(new Binding("Text", customersBindingSource, "NIM", true));
                this.txtAlamat.DataBindings.Add(new Binding("Text", customersBindingSource, "alamat", true));
                this.txtNama.DataBindings.Add(new Binding("Text", customersBindingSource, "nama_mahasiswa", true));
                this.cbxJenisKelamin.DataBindings.Add(new Binding("Text", customersBindingSource, "jenis_kelamin", true));
                this.dtTanggalLahir.DataBindings.Add(new Binding("Text", customersBindingSource, "tgl_lahir", true));
                this.cbxProdi.DataBindings.Add(new Binding("Text", customersBindingSource, "nama_prodi", true));
                koneksi.Close();
            }
        }

        private void clearBinding()
        {
            this.txtNIM.DataBindings.Clear();
            this.txtAlamat.DataBindings.Clear();
            this.txtNama.DataBindings.Clear();
            this.cbxJenisKelamin.DataBindings.Clear();
            this.dtTanggalLahir.DataBindings.Clear();
            this.cbxProdi.DataBindings.Clear();
        }

        private void refreshform()
        {
            txtNIM.Enabled = false;
            txtAlamat.Enabled = false;
            cbxJenisKelamin.Enabled = false;
            txtNama.Enabled = false;
            dtTanggalLahir.Enabled = false;
            cbxProdi.Enabled = false;
            btnAdd.Enabled = true;
            btnSave.Enabled = false;
            btnClear.Enabled = false;
            clearBinding();
            FormDataMahasiswa_Load();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            nim = txtNIM.Text;
            nama = txtNama.Text;
            jk = cbxJenisKelamin.Text;
            alamat = txtAlamat.Text;
            tgl = dtTanggalLahir.Value;
            prodi = cbxProdi.Text;
            int hs = 0;
            koneksi.Open();
            string strs = "select id_prodi from dbo.Prodi where nama_prodi = @dd";
            SqlCommand cm = new SqlCommand(strs, koneksi);
            cm.CommandType = CommandType.Text;
            cm.Parameters.Add(new SqlParameter("@dd", prodi));
            SqlDataReader dr = cm.ExecuteReader();

            while (dr.Read())
            {
                hs = int.Parse(dr["id_prodi"].ToString());
            }
            dr.Close();

            string str = "insert into dbo.Mahasiswa (nim, nama_mahasiswa, jenis_kelamin, alamat, tgl_lahir, id_prodi) " +
                         "values (@NIM, @Nm, @Jk, @Al, @Tgll, @Idp)";
            SqlCommand cmd = new SqlCommand(str, koneksi);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(new SqlParameter("@NIM", nim));
            cmd.Parameters.Add(new SqlParameter("@Nm", nama));
            cmd.Parameters.Add(new SqlParameter("@Jk", jk));
            cmd.Parameters.Add(new SqlParameter("@Al", alamat));
            cmd.Parameters.Add(new SqlParameter("@Tgll", tgl));
            cmd.Parameters.Add(new SqlParameter("@Idp", hs));
            cmd.ExecuteNonQuery();
            koneksi.Close();

            MessageBox.Show("Data Berhasil Disimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
            refreshform();

        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            Form1 hu = new Form1();
            hu.Show();
            this.Hide();

        }

        private void Prodicbx()
        {
            koneksi.Open();
            string str = "select nama_prodi from dbo.Prodi";
            SqlCommand cmd = new SqlCommand(str, koneksi);
            SqlDataAdapter da = new SqlDataAdapter(str, koneksi);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cmd.ExecuteReader();
            koneksi.Close();
            cbxProdi.DisplayMember = "nama_prodi";
            cbxProdi.ValueMember = "id_prodi";
            cbxProdi.DataSource = ds.Tables[0];
        }



        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            refreshform();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtNIM.Text = "";
            txtNama.Text = "";
            txtAlamat.Text = "";
            dtTanggalLahir.Value = DateTime.Today;
            txtNIM.Enabled = true;
            txtNama.Enabled = true;
            cbxJenisKelamin.Enabled = true;
            txtAlamat.Enabled = true;
            dtTanggalLahir.Enabled = true;
            cbxProdi.Enabled = true;
            Prodicbx();
            btnSave.Enabled = true;
            btnClear.Enabled = true;
            btnAdd.Enabled = false;

        }
    }
}

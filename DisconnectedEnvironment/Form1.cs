using DisconnectedEnvironment;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DisconnectedEnvironment
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void dataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 fr = new Form4();
            fr.Show();
            this.Hide();
        }

        private void dataProdiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 fm = new Form2();
            fm.Show();
            this.Hide();
        }

        private void dataMahasiswaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormDataMahasiswa fo = new FormDataMahasiswa();
            fo.Show();
            this.Hide();
        }
    }
}

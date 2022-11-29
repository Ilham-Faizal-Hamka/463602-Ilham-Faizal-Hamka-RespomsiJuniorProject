
using Npgsql;
using System.ComponentModel.Design.Serialization;
using System.Data;

namespace ResponsiJuniorProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private NpgsqlConnection conn;
        string connstring = "Host=localhost;Port=5432;Username=postgres;Password=informatika;Database=ResponsiJunpro";
        public static NpgsqlCommand cmd;
        public DataTable dt;
        private string sql = null;
        private DataGridViewRow r;
        
        

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try {
                conn.Open();
                sql = @"select * from st_insert(:_nama,:_nama_departemen)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_nama", txtNama.Text);
                cmd.Parameters.AddWithValue("_nama_departemen", cmbDept.Text);
                if((int)cmd.ExecuteScalar() == 1)
                {
                    MessageBox.Show("data berhasil diinputkan");
                    conn.Close();
                    btnLoadData.PerformClick();
                    txtNama.Text = cmbDept.Text = null;

                }

                conn.Close();
    
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            conn.Open();
            dgvData.DataSource = null;
            sql = "select * from departemen INNER JOIN karyawan ON nama_dep = nama_dep order by nama_dep"; 
            cmd = new NpgsqlCommand(sql, conn); 
            dt = new DataTable();
            NpgsqlDataReader reader = cmd.ExecuteReader();
            dt.Load(reader);
            dgvData.DataSource = dt;
            conn.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try {
                conn.Open();
                sql = @"select * from st_delete(:_id_karyawan)";
                cmd = new NpgsqlCommand(sql, conn);
                r = new DataGridViewRow();
                cmd.Parameters.AddWithValue("_id_karyawan", r.Cells["_id_karyawan"].Value.ToString());
                if ((int)cmd.ExecuteScalar() == 1)
                {
                    MessageBox.Show("data berhasil diinputkan");
                    conn.Close();
                    btnLoadData.PerformClick();
                    txtNama.Text = cmbDept.Text = null;
                    r = null;
                }

                conn.Close();
            } catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
            

        }
    }
}
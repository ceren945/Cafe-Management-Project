using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace coffeshop
{
    public partial class LoadData : Form
    {
        private SqlConnection con;
        public LoadData()
        {
            InitializeComponent();
            //Veritabanına Baglan
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "DESKTOP-I8DTRQV\\SQLEXPRESS";
            builder.InitialCatalog = "user_info";
            builder.IntegratedSecurity = true;
            builder.Encrypt = true;
            builder.TrustServerCertificate = true; // Sertifika doğrulamasını devre dışı bırakma
            con = new SqlConnection(builder.ToString());
        }

        private void LoadData_Load(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand coman = new SqlCommand();
                coman.Connection = con;
                string query = "select * from item_tbl ";
                coman.CommandText = query;
                SqlDataAdapter da = new SqlDataAdapter(coman);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }
        }

        private void Print_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.PrintPreviewControl.Zoom = 1;
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bmp = new Bitmap(this.dataGridView1.Width, this.dataGridView1.Height);
            dataGridView1.DrawToBitmap(bmp, new Rectangle(0, 0, this.dataGridView1.Width, this.dataGridView1.Height));
            e.Graphics.DrawImage(bmp, 100, 150);
        }
    }
    }


    


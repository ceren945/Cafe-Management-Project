using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace coffeshop
{
    public partial class Form1 : Form
    {
        private SqlConnection con;
    
        public Form1()
        {
            InitializeComponent();
            //Veritabanýna Baglan
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "DESKTOP-I8DTRQV\\SQLEXPRESS";
            builder.InitialCatalog = "user_info";
            builder.IntegratedSecurity = true;
            builder.Encrypt = true;
            builder.TrustServerCertificate = true; // Sertifika doðrulamasýný devre dýþý býrakma
            con = new SqlConnection(builder.ToString());
        }
            private void Form1_Load(object sender, EventArgs e)
        {


            /*Baglantýyý test et
            try
             {
                 con.Open();
              MessageBox.Show("Baðlantýyý açtýk");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Baðlantý hatasý: " + ex.Message);
            }
            */
           

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
             radioButton1.ForeColor = System.Drawing.Color.BlueViolet;

            radioButton2.ForeColor = System.Drawing.Color.RosyBrown;

            comboBox1.Items.Clear();

            comboBox1.Items.Add("Coffee Item 1");

            comboBox1.Items.Add("Coffee Item 2");

            comboBox1.Items.Add("Coffee Item 3");
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            radioButton1.ForeColor = System.Drawing.Color.RosyBrown;

            radioButton2.ForeColor = System.Drawing.Color.BlueViolet;

            comboBox1.Items.Clear();

            comboBox1.Items.Add("Dessert Item 1");

            comboBox1.Items.Add("DessertItem 2");

            comboBox1.Items.Add("Dessert Item 3");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "Coffee Item 1")
            {
                textBox1.Text = "50";
            }
            else if (comboBox1.SelectedItem.ToString() == "Coffee Item 2")

            {
                textBox1.Text = "100";
            }
            else if (comboBox1.SelectedItem.ToString() == "Coffee Item 3")
            {
                textBox1.Text = "150";
            }

            else if (comboBox1.SelectedItem.ToString() == "Dessert Item 1")
            {
                textBox1.Text = "100";
            }
            else if (comboBox1.SelectedItem.ToString() == "DessertItem 2")
            {
                textBox1.Text = "150";
            }
            else if (comboBox1.SelectedItem.ToString() == "Dessert Item 3")
            {
                textBox1.Text = "250";
            }
            else
            {
                textBox1.Text = "0";
            }
            textBox3.Text = "";
            textBox2.Text = "";
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
          if (textBox2.Text.Length > 0)
            {
                textBox3.Text = (Convert.ToInt16(textBox1.Text) * Convert.ToInt16(textBox2.Text)).ToString();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add(comboBox1.Text, textBox1.Text, textBox2.Text, textBox3.Text, dateTimePicker1.Text);

            textBox4.Text = (Convert.ToInt16(textBox4.Text) + Convert.ToInt16(textBox3.Text)).ToString();

            //int n = dataGridView1.Rows.Add();

            //dataGridView1.Rows[n].Cells[0].Value = (n + 1).ToString();

            //  dataGridView1.Rows[n].Cells[1].Value = dateTimePicker1.Value.ToString();

            //comboBox1.Text = "";

            //Clear Data
            textBox1.Text = "";

            textBox2.Text = "";

            textBox3.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //index = dataGridView1.CurrentCell.RowIndex;

            //dataGridView1.Rows.RemoveAt(index);
            if (dataGridView1.SelectedRows.Count > 0)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Selected)
                    {
                        textBox4.Text = (Convert.ToInt16(textBox4.Text) - Convert.ToInt16(dataGridView1.Rows[i].Cells[3].Value)).ToString();
                        dataGridView1.Rows.RemoveAt(i);
                    }
                }
            }
            comboBox1.Text = "";

            textBox1.Text = "";

            textBox2.Text = "";

            textBox3.Text = "";
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (textBox5.Text.Length > 0)
            {
                textBox6.Text = (Convert.ToInt16(textBox4.Text) - Convert.ToInt16(textBox5.Text)).ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            try
            {
                con.Open();

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO item_tbl (ItemName, ItemPrice, Quantity, TotalPrice, Date) VALUES (@itemName, @itemPrice, @quantity, @totalPrice, @date)", con);
                    cmd.Parameters.AddWithValue("@itemName", dataGridView1.Rows[i].Cells[0].Value.ToString());
                    cmd.Parameters.AddWithValue("@itemPrice", dataGridView1.Rows[i].Cells[1].Value.ToString());
                    cmd.Parameters.AddWithValue("@quantity", dataGridView1.Rows[i].Cells[2].Value.ToString());
                    cmd.Parameters.AddWithValue("@totalPrice", dataGridView1.Rows[i].Cells[3].Value.ToString());
                    cmd.Parameters.AddWithValue("@date", dataGridView1.Rows[i].Cells[4].Value.ToString());

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Veriler baþarýyla kaydedildi.");

                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Veritabaný hatasý: " + ex.Message);
            }

            dataGridView1.Rows.Clear();

            textBox4.Text = "0";

            textBox5.Text = "";

            textBox6.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();

            textBox4.Text = "0";

            textBox5.Text = "";

            textBox6.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();

            LoadData ldform = new LoadData();

            ldform.ShowDialog();

            ldform = null;

            this.Show();
        }
    }


   

       
    }
    
    

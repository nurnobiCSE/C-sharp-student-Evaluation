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

namespace StudentEVL
{
    public partial class stdFaculty : Form
    {
        public stdFaculty()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-9G2BI4L\MSSQLSERVER01;Initial Catalog=studentEVL;Integrated Security=True");
        private void save_Click(object sender, EventArgs e)
        {
            if (studentID.Text == "" || fName.Text == "" || lName.Text == "" || address.Text == "" || gender.Text == "" || department.Text == "")
            {
                MessageBox.Show("Please check your all any missing information");
            }

            else
            {
                try
                {
                    con.Open();
                    string query = "insert into stdTable values('" + studentID.Text + "','" + fName.Text + "','" + lName.Text + "','" + address.Text + "','" + gender.Text + "','" + department.Text + "','NULL')";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data insert Successful!");

                    con.Close();
                    populate();
                    dataClear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void dataClear()
        {
            studentID.Text = "";
            fName.Text = "";
            lName.Text = "";
            address.Text = "";
            gender.Text = "";
            department.Text = "";
            operand.Text = "";
        }
        private void populate()
        {
            con.Open();
            string query = "select * from stdTable";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder();
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridViewInsert.DataSource = ds.Tables[0];
            con.Close();
        }
        private void populateSearch()
        {

            con.Open();
            string query = "select * from stdTable where studentID = '" + searchBar.Text + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder();
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridViewInsert.DataSource = ds.Tables[0];
            con.Close();
        }

        private void edit_Click(object sender, EventArgs e)
        {
            con.Open();
            string query = "update stdTable set studentID = '" + studentID.Text + "', first_name ='" + fName.Text + "' ,last_name ='" + lName.Text + "' , address ='" + address.Text + "' ,gender ='" + gender.Text + "' ,department = '" + department.Text + "' where studentID = '" + operand.Text + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("data has been Updated ");
            populate();
            dataClear();
        }

        private void delete_Click(object sender, EventArgs e)
        {
            con.Open();
            string query = "delete stdTable where studentID = '" + operand.Text + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Data has been deleted!");
            populate();
            dataClear();
        }

        private void reset_Click(object sender, EventArgs e)
        {
            dataClear();
        }

        private void search_Click(object sender, EventArgs e)
        {
            populateSearch();
        }

        private void refresh_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void stdFaculty_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void dataGridViewInsert_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            operand.Text = dataGridViewInsert.CurrentRow.Cells[0].Value.ToString();
            studentID.Text = dataGridViewInsert.CurrentRow.Cells[0].Value.ToString();
            fName.Text = dataGridViewInsert.CurrentRow.Cells[1].Value.ToString();
            lName.Text = dataGridViewInsert.CurrentRow.Cells[2].Value.ToString();
            address.Text = dataGridViewInsert.CurrentRow.Cells[3].Value.ToString();
            gender.Text = dataGridViewInsert.CurrentRow.Cells[4].Value.ToString();
            department.Text = dataGridViewInsert.CurrentRow.Cells[5].Value.ToString();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            new facultydashboard().Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            new LoginForm().Show();
            this.Hide();
        }
    }
}

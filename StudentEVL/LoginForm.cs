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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void login_Click(object sender, EventArgs e)
        {
            if(username.Text == "admin" && password.Text == "admin")
            {
                homeForm hom = new homeForm();
                hom.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password!");
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-9G2BI4L\MSSQLSERVER01;Initial Catalog=studentEVL;Integrated Security=True");
        private void facultyLog_Click(object sender, EventArgs e)
        {
            con.Open();
            string login = "select * from userTable where userName= '" + username.Text + "' and password = '" + password.Text + "'";
            SqlCommand cmd = new SqlCommand(login , con);
            SqlDataReader dr = cmd.ExecuteReader();
            

            if(dr.Read() == true)
            {
                MessageBox.Show("Logged as a faculty","Faculty Login",MessageBoxButtons.OK,MessageBoxIcon.Information);
                new facultydashboard().Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid user name or password!", "Login Failed" , MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            con.Close();
        }
    }
}

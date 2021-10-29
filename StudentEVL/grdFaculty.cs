﻿using iTextSharp.text;
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
 


namespace StudentEVL
{
    public partial class grdFaculty : Form
    {
        public grdFaculty()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-9G2BI4L\MSSQLSERVER01;Initial Catalog=studentEVL;Integrated Security=True");
        
        private void save_Click(object sender, EventArgs e)
        {
            if (stdID.Text == "" || grades.Text == "")
            {
                MessageBox.Show("Please select student from information list!");
            }
            else
            {
                try
                {
                    con.Open();
                    string updatequery = "update stdTable set grade='" + grades.Text + "' where studentId = '" + stdID.Text + "'";
                    SqlCommand updatecmd = new SqlCommand(updatequery, con);
                    updatecmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Data insert Successful!");
                    grades.Text = "";
                    stdID.Text = "";
                    populate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void populate()
        {
            con.Open();
            string query = "select studentId, first_name, last_name ,grade from stdTable";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder();
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridViewGrading.DataSource = ds.Tables[0];
            con.Close();
        }
         

        private void grdFaculty_Load(object sender, EventArgs e)
        {
            populate();
             
        }

        private void clear_Click(object sender, EventArgs e)
        {
            searchbar.Text = "";
            //grdID.Text = "";
            grades.Text = "";
        }

        private void search_Click(object sender, EventArgs e)
        {
            con.Open();
            string query = "select studentId, first_name, last_name ,grade from stdTable where studentId = '" + searchbar.Text + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder();
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridViewGrading.DataSource = ds.Tables[0];
            con.Close();
        }

        private void refresh_Click(object sender, EventArgs e)
        {
            populate();
            searchbar.Text = "";
            stdID.Text = "";
            grades.Text = "";
            //grdID.Text = "";
        }

        private void dataGridViewGrading_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            stdID.Text = dataGridViewGrading.CurrentRow.Cells[0].Value.ToString();
        }

        private void createdataGridViewGrading_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            stdID.Text = dataGridViewGrading.CurrentRow.Cells[0].Value.ToString();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            new LoginForm().Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            new facultydashboard().Show();
            this.Hide();
        }
 
    }
}

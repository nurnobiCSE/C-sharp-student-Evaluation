﻿using System;
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
    public partial class curriculumnForm : Form
    {
        public curriculumnForm()
        {
            InitializeComponent();
            fillCombobox2();
        }
        public void fillCombobox2()
        {
            con.Open();
            string show = "select department from dptTable";
            SqlCommand cmd = new SqlCommand(show, con);

            SqlDataReader rdr;
            try
            {

                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    string dlist = rdr.GetString(0);
                    department.Items.Add(dlist);

                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void semester_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void department_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            LoginForm log = new LoginForm();
            log.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            homeForm hom = new homeForm();
            hom.Show();
            this.Hide();
        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-9G2BI4L\MSSQLSERVER01;Initial Catalog=studentEVL;Integrated Security=True");
        private void save_Click(object sender, EventArgs e)
        {
            if (courseId.Text=="" || courseNo.Text=="" || CourseTitle.Text =="" || department.Text=="" || semester.Text == "" || credit.Text=="")
            {
                MessageBox.Show("Missing any information");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "insert into crcTable values('"+ courseId.Text + "','" + courseNo.Text + "','" + CourseTitle.Text + "','" + department.Text + "','" + semester.Text + "','" + credit.Text + "','none')";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Successfully \ndata inserted!");
                    populate();
                    dataClear();
                }catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void populate()
        {
            con.Open();
            string query = "select * from crcTable";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder();
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridViewCurrclmn.DataSource = ds.Tables[0];
            con.Close();
        }
        private void dataClear()
        {
            courseId.Text = "";
            courseNo.Text = "";
            CourseTitle.Text = "";
            department.Text = "";
            semester.Text = "";
            credit.Text = "";
             
        }

        private void curriculumnForm_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void clear_Click(object sender, EventArgs e)
        {
            dataClear();
        }

        private void delete_Click(object sender, EventArgs e)
        {
            if(courseId.Text == "")
            {
                MessageBox.Show("Please select data from list");
            }
            else
            {
                con.Open();
                string query = "delete crcTable where courseId = '" + courseId.Text +"'";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("data  has been deleted!");
                populate();
                dataClear();
            }
        }

        private void dataGridViewCurrclmn_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            courseId.Text = dataGridViewCurrclmn.CurrentRow.Cells[0].Value.ToString();
        }
    }
}

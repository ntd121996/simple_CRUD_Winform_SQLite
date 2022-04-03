using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace WindowsFormsApp1
{
    public partial class Register : Form
    {
        SQLiteConnection m_con;
        int m_ID;
        string path = ("Data Source=database.db;Version=3;New=True;Compress=True;");

        public Register()
        {
            InitializeComponent();
            SQLiteCreateTable();
            LoadDatatoGrid();


        }
        private void SQLiteCreateConnection()
        {
            m_con = new SQLiteConnection(path);
            try
            {
                m_con.Open();
            }
            catch (Exception ex)
            {

            }

        }
        private void SQLiteCreateTable()
        {
            
            string strCreateTable = "CREATE TABLE IF NOT EXISTS users (" +
                "ID INTEGER PRIMARY KEY," +
                "Name VARCHAR(50)," +
                "Age INT," +
                "Address VARCHAR(50))";

            string strAlterTable = "ALTER TABLE users ";

            SQLiteCreateConnection();
            try
            {
                SQLiteCommand cmd = new SQLiteCommand(strCreateTable, m_con);
                cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {
                MessageBox.Show("Create Table Failure");


            }

            SQLiteCloseConnection();
        }
        private void LoadDatatoGrid()
        {
            DataTable dt = new DataTable();
            SQLiteCreateConnection();
            SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT * FROM users", m_con);
            da.Fill(dt);
            SQLiteCloseConnection();
            dataGridView1.DataSource = dt;
        }
        private void SQLiteCloseConnection()
        {
            try
            {
                m_con.Close();
            }
            catch (Exception ex)
            {

                throw;
            }



        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string id = txtBoxID.Text;
            string name = txtBoxName.Text;
            string age = txtBoxAge.Text;
            string address = txtBoxAddress.Text;
            string strInsert = String.Format("INSERT INTO users (Name, Age, Address) VALUES ('{0}',{1},'{2}')", name, age, address);
            try
            {
                SQLiteCreateConnection();
                SQLiteCommand cmd = new SQLiteCommand(strInsert, m_con);
                cmd.ExecuteNonQuery();
                SQLiteCloseConnection();
                MessageBox.Show("Save Data Success");
                ClearGridView();
                LoadDatatoGrid();
            }
            catch (Exception)
            {
                MessageBox.Show("Error");
            }
            
            

        }
        private void ClearGridView()
        {
            txtBoxID.Text = "";
            txtBoxName.Text = "";
            txtBoxAge.Text = "";
            txtBoxAddress.Text = "";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string id = txtBoxID.Text;
            string name = txtBoxName.Text;
            string age = txtBoxAge.Text;
            string address = txtBoxAddress.Text;
            string strUpdate = String.Format("UPDATE users SET Name ='{0}', Age = {1}, Address = '{2}' WHERE ID = {3}", name, age, address, id);
            try
            {
                SQLiteCreateConnection();
                SQLiteCommand cmd = new SQLiteCommand(strUpdate, m_con);
                cmd.ExecuteNonQuery();
                SQLiteCloseConnection();
                MessageBox.Show("Update Data Success");
                LoadDatatoGrid();
            }
            catch (Exception)
            {

                MessageBox.Show("Error");

            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string strDelete = String.Format("DELETE FROM users WHERE ID={0}",m_ID);
            try
            {
                SQLiteCreateConnection();
                SQLiteCommand cmd = new SQLiteCommand(strDelete, m_con);
                cmd.ExecuteNonQuery();
                SQLiteCloseConnection();
                ClearGridView();
                LoadDatatoGrid();
            }
            catch (Exception)
            {
                MessageBox.Show("Error");
            }
            
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int a =1 ;
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtBoxID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtBoxName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtBoxAge.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtBoxAddress.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            if( txtBoxID.Text != "")
            {
                m_ID = int.Parse(txtBoxID.Text);
            }
            else
            {
                m_ID = -1;
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace AccessLoginApp
{
    public partial class Form1 : Form
    {
        //create a connection (this connection is private to form1, no other forms may use it)
        private OleDbConnection connection = new OleDbConnection();

        public Form1()
        {
            InitializeComponent();
            //use connection string to connect to db 
            connection.ConnectionString =
            @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\matt\Documents\MovieDB.accdb;
            Persist Security Info=False;";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                checkConnection.Text = "Connected!";
                connection.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error" + ex);
            }
          }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            connection.Open();
            //use this command for the connection
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            //query to validate credentials
            command.CommandText = "select * from tEmployees where Username='"+txt_Username.Text+"' and Password='"+txt_Password.Text+"'";
            //execute the query using the DB reader. We are reading the data
            OleDbDataReader reader = command.ExecuteReader();
            int count = 0;
            while (reader.Read())
            {
                count = count + 1;
                //or count++, same thing happening here
            }
            //check count value
            if (count == 1)
            { 
                MessageBox.Show("UserName and Password are correct");
            }
            
            else if (count > 1)
            {
                MessageBox.Show("Duplicate UserName and Password");
            }
            else 
            {
                MessageBox.Show("UserName and Password are not correct");
            }
            
            connection.Close();
        }
    }
}


using ADODB;
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

namespace Sign_up
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection connection = new SqlConnection("Data Source=localhost\\sqlexpress;Initial Catalog=LoginDb;Integrated Security=True");

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtfn.Text!="" && txtln.Text != "" && txtd.Text != "" && txtg.Text != "" && txta.Text != "" &&
                    txte.Text != "" && txtp.Text != "" && txtcp.Text != "" )
                {
                    if (txtp.Text==txtcp.Text)
                    {
                        int v = check(txte.Text);
                        if (v !=1)
                        {
                            connection.Open();
                            SqlCommand command = new SqlCommand("insert into RegistrationTbl values(@f_name,@l_name," +
                                "@b_date,@gender,@address,@email,@password)", connection);
                            command.Parameters.AddWithValue("@f_name", txtfn.Text);
                            command.Parameters.AddWithValue("@l_name", txtln.Text);
                            command.Parameters.AddWithValue("@b_date", Convert.ToDateTime(txtd.Text));
                            command.Parameters.AddWithValue("@gender", txtg.Text);
                            command.Parameters.AddWithValue("@address", txta.Text);
                            command.Parameters.AddWithValue("@email", txte.Text);
                            command.Parameters.AddWithValue("@password", txtp.Text);
                            command.ExecuteNonQuery();
                            connection.Close();
                            MessageBox.Show("Register Succesfully!");
                            txtfn.Text = "";
                            txtln.Text = "";
                            txtg.Text = "";
                            txta.Text = "";
                            txte.Text = "";
                            txtp.Text = "";
                            txtcp.Text = "";


                        }
                        else
                        {
                            MessageBox.Show("You are already registered"); 
                        }
                    }
                    else
                    {
                        MessageBox.Show("The password does not match");
                    }

                }
                else
                {
                    MessageBox.Show("Fill in the blinks!");
                }
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        int check(string email)
        {
            connection.Open();
            string query = "select count(*) from RegistrationTbl where email='" + email + "'";
            SqlCommand command = new SqlCommand(query, connection);
            int v=(int)command.ExecuteScalar();
            connection.Close();
            return v;

        }

        private void txtrm_CheckedChanged(object sender, EventArgs e)
        {
            if (txtrm.Checked)
            {
                txtp.UseSystemPasswordChar = false;
                txtcp.UseSystemPasswordChar = false;
                
            }
            else
            {
                txtp.UseSystemPasswordChar = true;
                txtcp.UseSystemPasswordChar = true;
            }
        }
    }
}

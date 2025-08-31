using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace WebApplication1
{
    public partial class addemployee : System.Web.UI.Page
    {
        SqlConnection con;// to establish connection
        SqlCommand cmd;// to execute command
        protected void Page_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString);// to get connection string from web.config file
            if (!IsPostBack)
            {
               LoadDept();// Load Departments in DropDownList

                TextBox2.Focus();
            }
        }
        private void LoadDept()// Load Departments in DropDownList
        {
            cmd = new SqlCommand("select did,dname from department order by did", con);// to select department id and name from department table
            if (con.State == ConnectionState.Closed)// to check connection state
            {
                con.Open();// to open connection
            }
            SqlDataReader dr = cmd.ExecuteReader();// to read data from database
            DropDownList1.DataSource = dr;// to bind data to dropdownlist
            DropDownList1.DataTextField = "dname";// to show department name
            DropDownList1.DataValueField = "did";// to get department id
            DropDownList1.DataBind();// to bind data to dropdownlist
            DropDownList1.Items.Insert(0, "Select Department");// to insert default item
            con.Close();
        }

        protected void Button1_Click(object sender, EventArgs e)// to add employee
        {
            if (DropDownList1.SelectedIndex > 0)
            {
                cmd = new SqlCommand("INSERT INTO employee (ename, job, salary, did) VALUES (@ename, @job, @salary, @did)", con);// to insert employee details into employee table
                cmd.Parameters.AddWithValue("@ename", TextBox2.Text);
                cmd.Parameters.AddWithValue("@job", TextBox3.Text);
                cmd.Parameters.AddWithValue("@salary", Convert.ToDecimal(TextBox4.Text));
                cmd.Parameters.AddWithValue("@did", DropDownList1.SelectedValue);

                con.Open();

                if (cmd.ExecuteNonQuery() > 0)// to check if the command executed successfully
                {
                    cmd.CommandText = "select max(eid) from employee";// to get the last inserted employee id
                    TextBox1.Text = cmd.ExecuteScalar().ToString();// to display the last inserted employee id
                }
                else
                {
                    Response.Write("<script>alert('Failed to Add Employee')</script>");
                }
                con.Close();
            }
            else
            {
                Response.Write("<script>alert('Select Department')</script>");
            }

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            TextBox1.Text = ""; 
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            DropDownList1.SelectedIndex = 0;
            TextBox2.Focus();
        }
    }
}
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
    public partial class updateDelete : System.Web.UI.Page
    {
        SqlConnection con;// to establish connection
        SqlCommand cmd;// to execute command
        SqlDataReader dr;// to read data from database
        protected void Page_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString);// to get connection string from web.config file
            cmd = new SqlCommand();// to execute command
            cmd.Connection = con;// to set connection

            if (!IsPostBack)
            {
                Loademp();
                Loaddep();
            }
        }

        private void Loaddep()
        {
            cmd = new SqlCommand("select did,dname from department order by did", con);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlDataReader dr = cmd.ExecuteReader();
            DropDownList2.DataSource = dr;
            DropDownList2.DataTextField = "dname";
            DropDownList2.DataValueField = "did";
            DropDownList2.DataBind();
            DropDownList2.Items.Insert(0, "Select Department");
            con.Close();
        }

        private void Loademp()
        {
            cmd.CommandText = "select eid from employee order by eid";
            if(con.State != ConnectionState.Open) 
            {
                con.Open();
            }
            dr = cmd.ExecuteReader();
            DropDownList1.DataSource = dr;
            DropDownList1.DataTextField = "eid";
            DropDownList1.DataValueField = "eid";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, "Select Employee");
            con.Close();
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList1.SelectedIndex > 0)
            {
                cmd.CommandText = "select ename,job,salary,did from employee where eid=" + DropDownList1.SelectedValue;// to select employee details based on selected employee id
                con.Open();
                dr = cmd.ExecuteReader();
                if(dr.Read())
                {
                    TextBox2.Text = dr["ename"].ToString();// to display employee name
                    TextBox3.Text = dr["job"].ToString();
                    TextBox4.Text = dr["salary"].ToString();
                    DropDownList2.SelectedValue = dr["did"].ToString();
                }
            }
            else// if no employee is selected
            {
                
                TextBox2.Text = "";
                TextBox3.Text = "";
                TextBox4.Text = "";
                DropDownList2.SelectedIndex = 0;
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (DropDownList1.SelectedIndex > 0)
            {
                cmd = new SqlCommand(" UPDATE employee SET ename=@ename, job=@job, salary=@salary, did=@did WHERE eid=@eid", con);

                cmd.Parameters.AddWithValue("@ename", TextBox2.Text);
                cmd.Parameters.AddWithValue("@job", TextBox3.Text);
                cmd.Parameters.AddWithValue("@salary", Convert.ToDecimal(TextBox4.Text));
                cmd.Parameters.AddWithValue("@did", DropDownList2.SelectedValue);
                cmd.Parameters.AddWithValue("@eid", Convert.ToInt32(DropDownList1.Text));

                con.Open();

                if (cmd.ExecuteNonQuery() > 0)
                {
                   Response.Write("<script>alert('Employee Updated Successfully')</script>");
                }
                else
                {
                    Response.Write("<script>alert('Failed to Update Employee')</script>");
                }
                con.Close();
            }
            else
            {
                Response.Write("<script>alert('Select Department to update')</script>");
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (DropDownList1.SelectedIndex > 0)
            {
                cmd.CommandText=" DELETE FROM employee where eid=" + DropDownList1.SelectedValue;

               

                con.Open();

                if (cmd.ExecuteNonQuery() > 0)
                {
                    Response.Write("<script>alert('Employee deleted Successfully')</script>");
                    DropDownList1.SelectedIndex = 0;
                    TextBox2.Text = "";
                    TextBox3.Text = "";
                    TextBox4.Text = "";
                    Loademp();

                }
                else
                {
                    Response.Write("<script>alert('Failed to Update Employee')</script>");
                }
                con.Close();
            }
            else
            {
                Response.Write("<script>alert('Select Department to update')</script>");
            }
        }
    }
}
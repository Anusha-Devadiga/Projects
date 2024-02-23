using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace cloth
{
    public partial class customerupdate : System.Web.UI.Page
    {
        connect c;
        DataSet ds;
        SqlDataAdapter adp = new SqlDataAdapter();
        protected void Page_Load(object sender, EventArgs e)
        {
            TextBox2.Focus();
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/customeradd.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                c = new connect();
                ds = new DataSet();
                if (TextBox2.Text != "" && TextBox3.Text != "" && TextBox4.Text != "" && TextBox6.Text != "")
                {

                    c.cmd.CommandText = "update customerdetails set name = @name where contact_no = '" + TextBox1.Text + "'";
                    c.cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = TextBox2.Text;
                    c.cmd.ExecuteNonQuery();
                    c.cmd.CommandText = "update customerdetails set address = @address where contact_no = '" + TextBox1.Text + "'";
                    c.cmd.Parameters.Add("@address", SqlDbType.NVarChar).Value = TextBox3.Text;
                    c.cmd.ExecuteNonQuery();
                    c.cmd.CommandText = "update customerdetails set contact_no = @contact_no where contact_no = '" + TextBox1.Text + "'";
                    c.cmd.Parameters.Add("@contact_no", SqlDbType.NVarChar).Value = TextBox4.Text;
                    c.cmd.ExecuteNonQuery();
                    c.cmd.CommandText = "update customerdetails set email = @email where contact_no = '" + TextBox1.Text + "'";
                    c.cmd.Parameters.Add("@email", SqlDbType.NVarChar).Value = TextBox6.Text;
                    c.cmd.ExecuteNonQuery();


                    Page.ClientScript.RegisterStartupScript(this.GetType(), "msgbox", "<script>alert('Record Updated')</script>");
                }
                else
                {

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "msgbox", "<script>alert('Enter all fields')</script>");
                }
                Label8.Text = "";
                TextBox1.Text = "";
                TextBox1.Enabled = true;
                TextBox2.Text = "";
                TextBox3.Text = "";
                TextBox4.Text = "";
                TextBox6.Text = "";
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                c.con.Close();
            }

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            c = new connect();
            ds = new DataSet();
            c.cmd.CommandText = "select * from customerdetails where contact_no = '" + TextBox1.Text + "'";
            adp.SelectCommand = c.cmd;
            adp.Fill(ds, "cust");
            if (TextBox1.Text != "")
            {
                if (ds.Tables["cust"].Rows.Count > 0)
                {
                    GridView1.DataSource = ds.Tables["cust"];
                    GridView1.DataBind();
                }
                else
                {

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "msgbox",
                    "<script>alert('Record Not Found')</script>");
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(),
               "msgbox", "<script>alert('Must Enter Contact_No')</script>");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                TextBox1.Enabled = false;
                c = new connect();
                ds = new DataSet();
                c.cmd.CommandText = "select * from customerdetails where contact_no = '" + TextBox1.Text + "'";
                if (TextBox1.Text != "")
                {
                    adp.SelectCommand = c.cmd;
                    adp.Fill(ds, "cust");
                    Label8.Text =Convert.ToString(ds.Tables["cust"].Rows[0].ItemArray[0]);
                    TextBox2.Text =
                   Convert.ToString(ds.Tables["cust"].Rows[0].ItemArray[1]);
                    TextBox2.Focus();
                    TextBox4.Text =
                   Convert.ToString(ds.Tables["cust"].Rows[0].ItemArray[3]);
                    TextBox3.Text =
                   Convert.ToString(ds.Tables["cust"].Rows[0].ItemArray[2]);
                    TextBox6.Text =
                   Convert.ToString(ds.Tables["cust"].Rows[0].ItemArray[4]);
                    c.cmd.ExecuteNonQuery();
                }
                else
                {

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "msgbox",
                    "<script>alert('Must Enter Contact_no')</script>");
                }
            }
            catch (Exception)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(),
               "msgbox", "<script>alert('No Record Found...Try again')</script>");
                TextBox1.Enabled = true;
            }
            finally
            {
                c.con.Close();
            }
        }

        protected void TextBox6_TextChanged(object sender, EventArgs e)
        {

        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}



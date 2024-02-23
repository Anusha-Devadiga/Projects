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
    public partial class supplier : System.Web.UI.Page
    {
        connect c;
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                c = new connect();
                int count;
                c.cmd.CommandText = "select count(*) from supplier";
                count = Convert.ToInt16(c.cmd.ExecuteScalar()) + 1;
                TextBox1.Text = "SU100" + count.ToString();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                c.con.Close();
            }
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                c = new connect();
                if (TextBox1.Text != "" && TextBox2.Text != "" && TextBox3.Text != "" && TextBox4.Text != "" && TextBox5.Text != "")
                {
                    c.cmd.CommandText = "insert into supplier values(@supid,@name,@address,@contact,@email,@status)";
                    c.cmd.Parameters.Clear();
                    c.cmd.Parameters.Add("@supid", SqlDbType.VarChar).Value = TextBox1.Text;
                    c.cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = TextBox2.Text;
                    c.cmd.Parameters.Add("@address", SqlDbType.VarChar).Value = TextBox3.Text;
                    c.cmd.Parameters.Add("@contact", SqlDbType.VarChar).Value = TextBox4.Text;
                    c.cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = TextBox5.Text;
                    c.cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = "active";
                    c.cmd.ExecuteNonQuery();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "msgbox", "<script> alert('Record saved')</script>");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "msgbox", "<script> alert('Enter all fields')</script>");
                }
                

                
            }
            
            catch (Exception)
            {
                throw;
            }
            finally
            {
                c.con.Close();
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox5.Text = "";
        }

        protected void TextBox4_TextChanged(object sender, EventArgs e)
        {
            try
            {
                c = new connect();
                c.cmd.CommandText = "select count(*) from supplier where contact='" + TextBox5.Text + "'";
                int p = Convert.ToInt16(c.cmd.ExecuteScalar());
                if (p > 0)
                {
                    MessageBox.Show("contact number already exists");
                    TextBox5.Text = "";
                    TextBox5.Focus();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                c.con.Close();
            }
        }

        protected void TextBox5_TextChanged(object sender, EventArgs e)
        {
            try
            {
                c = new connect();
                c.cmd.CommandText = "select count(*) from supplier where email='" + TextBox5.Text + "'";
                int p = Convert.ToInt16(c.cmd.ExecuteScalar());
                if (p > 0)
                {
                    MessageBox.Show("Email already exists");
                    TextBox5.Text = "";
                    TextBox5.Focus();
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                c.con.Close();
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/supplier.aspx");
        }
    }
}
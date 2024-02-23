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
    public partial class customeradd : System.Web.UI.Page
    {
        connect c;
        DataSet ds;
        SqlDataAdapter adp = new SqlDataAdapter();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                c = new connect();
                int count;
                c.cmd.CommandText = "select count(*) from customerdetails";
                count = Convert.ToInt16(c.cmd.ExecuteScalar()) + 1;
                TextBox1.Text = "CI100" + count.ToString();
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

        protected void Button8_Click(object sender, EventArgs e)
        {
            try
            {
                c = new connect();
                if (TextBox1.Text != "" && TextBox2.Text != "" &&
               TextBox3.Text != "" && TextBox5.Text != "" && TextBox6.Text != "")

                {
                    c.cmd.CommandText = "insert into customerdetails values(@cust_id, @name, @address, @contact_no, @email)";
                    c.cmd.Parameters.Clear();
                    c.cmd.Parameters.Add("@cust_id",SqlDbType.NVarChar).Value = TextBox1.Text;
                    c.cmd.Parameters.Add("@name",SqlDbType.NVarChar).Value = TextBox2.Text;
                    c.cmd.Parameters.Add("@address",SqlDbType.NVarChar).Value = TextBox3.Text;
                    c.cmd.Parameters.Add("@contact_no",SqlDbType.NVarChar).Value = TextBox5.Text;
                    c.cmd.Parameters.Add("@email",SqlDbType.NVarChar).Value = TextBox6.Text;
                    c.cmd.ExecuteNonQuery();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "msgbox","<script>alert('Record saved')</script>");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "msgbox","<script>alert('Enter all fields')</script>");
                }
                TextBox2.Text = "";
                TextBox3.Text = "";
                TextBox5.Text = "";
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

        protected void Button9_Click(object sender, EventArgs e)
        {
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox6.Text = "";
            TextBox5.Text = "";
        }

        protected void TextBox5_TextChanged(object sender, EventArgs e)
        {
            try
            {
                c = new connect();
                c.cmd.CommandText = "select count(*) from customerdetails where contact_no = '" + TextBox5.Text + "'";
                int p = Convert.ToInt16(c.cmd.ExecuteScalar());
                if (p > 0)

                {
                    MessageBox.Show("Conatct No already exist");
                    TextBox5.Text = "";
                    TextBox5.Focus();
                }
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

        protected void TextBox6_TextChanged(object sender, EventArgs e)
        {
            try
            {
                c = new connect();
                c.cmd.CommandText = "select count(*) from customerdetails where email = '" + TextBox6.Text + "'";
                int p = Convert.ToInt16(c.cmd.ExecuteScalar());
                if (p > 0)
                {
                    MessageBox.Show("Email already exist");
                    TextBox6.Text = "";
                    TextBox6.Focus();
                }
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

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/customerupdate.aspx");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/customersearch.aspx");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/customerdisplay.aspx");
        }

        protected void Button10_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/home.aspx");
        }
    }
}


    

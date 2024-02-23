using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Runtime.Remoting.Contexts;


namespace cloth
{
    public partial class forgot : System.Web.UI.Page
    {
        connect c;
        DataSet ds;
        SqlDataAdapter adp = new SqlDataAdapter();

        protected void Page_Load(object sender, EventArgs e)
        {
            Panel2.Visible = false;
            if (IsPostBack)
            {
                String password = TextBox1.Text;
                TextBox1.Attributes.Add("value", password);
                String password1 = TextBox2.Text;
                TextBox2.Attributes.Add("value", password1);
            }


        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            string conStr = WebConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection con = new SqlConnection(conStr);
            try
            {

                con.Open();
                c = new connect();
                SqlCommand cmd = new SqlCommand("select count(*) from asp where securityqn ='" + TextBox1.Text + "'and ans ='" + TextBox2.Text + "'",con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                cmd.ExecuteNonQuery();
                if (dt.Rows[0][0].ToString() == "1")
                {
                    Label3.Text = "correct answer";
                    Panel1.Visible = false;
                    Panel2.Visible = true;
                }
                else
                {
                    Label3.Text = "wrong  answer";

                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {

                c = new connect();
                ds = new DataSet();
                if (TextBox3.Text != "")
                {

                    if (TextBox3.Text.Length >= 6)
                    {
                        if (TextBox4.Text != "")
                        {
                            c.cmd.CommandText = "update asp set password ='" + TextBox4.Text + "'";
                            adp.SelectCommand = c.cmd;
                            adp.Fill(ds, "adreg");
                            Response.Redirect("~/home.aspx");
                        }
                    }


                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "msgbox", "<script>alert('Must be atleast 6 Characters')</script>");
                        Panel2.Visible = true;
                        TextBox3.Text = "";
                        TextBox4.Text = "";
                    }
                }

                else
                {

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "msgbox","<script>alert('Enter all Fields') </script>");
                    Panel2.Visible = true;
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
    }
}


        
    


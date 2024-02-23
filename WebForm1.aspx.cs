using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace cloth
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Unnamed2_Click(object sender, EventArgs e)
        {
            string conStr = WebConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection con = new SqlConnection(conStr);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select count(*) from asp where username='" + txtuser.Text + "' and password='" + txtpass.Text + "'",con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                cmd.ExecuteNonQuery();
                if (dt.Rows[0][0].ToString() == "1")
                {

                    Response.Redirect("~/home.aspx");
                }
                else
                {
                    Response.Write("<Script>alert('invalid username or password')</script");
                    lnkforgot.Visible = true;

                }
            }


            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
           
            

        }

        protected void lnkforgot_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/forgot.aspx");
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox1.Checked == false)
            {
                txtpass.TextMode = TextBoxMode.Password;

            }
            if (CheckBox1.Checked == true)
            {
                txtpass.TextMode = TextBoxMode.SingleLine;
            }
        }
    }
}
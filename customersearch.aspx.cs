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
    public partial class customersearch : System.Web.UI.Page
    {
        connect c;
        DataSet ds;
        SqlDataAdapter adp = new SqlDataAdapter();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
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
                Page.ClientScript.RegisterStartupScript(this.GetType(), "msgbox", "<script>alert('Must Enter the Conatact_No')</script>");

            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/customeradd.aspx");
        }
    }
}
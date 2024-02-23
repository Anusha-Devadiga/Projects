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
    public partial class customerdisplay : System.Web.UI.Page
    {
        connect c;
        DataSet ds;
        SqlDataAdapter adp = new SqlDataAdapter();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                c = new connect();
                c.cmd.CommandText = "select * from customerdetails";
                ds = new DataSet();
                adp.SelectCommand = c.cmd;
                adp.Fill(ds, "cust");
                if (ds.Tables["cust"].Rows.Count > 0)
                {
                    GridView1.DataSource = ds.Tables["cust"];
                    GridView1.DataBind();
                }
                else
                {

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "msgbox","<script>alert('No Records')</script>");
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/customeradd.aspx");
        }
    }
}
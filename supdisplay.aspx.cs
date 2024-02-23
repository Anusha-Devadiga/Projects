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
    public partial class supdisplay : System.Web.UI.Page
    {
        connect c;
        DataSet ds, ds1;
        SqlDataAdapter adp = new SqlDataAdapter();

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/supplier.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                c = new connect();
                c.cmd.CommandText = "select * from supplier where status='active'";
                ds = new DataSet();
                adp.SelectCommand = c.cmd;
                adp.Fill(ds, "sup");
                if (ds.Tables["sup"].Rows.Count > 0)
                {
                    GridView1.DataSource = ds.Tables["sup"];
                    GridView1.DataBind();

                }

                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "msgbox", ",<script>alert('No Records')</script>");

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
            try
            {
                c = new connect();
                ds1 = new DataSet();
                c.cmd.CommandText = "select * from supplier where status='inactive'";
                adp.SelectCommand = c.cmd;
                adp.Fill(ds1, "sup");
                if (ds1.Tables["sup"].Rows.Count > 0)
                {
                    GridView2.DataSource = ds1.Tables["sup"];
                    GridView2.DataBind();
                    
                }
                else
                {
                    Panel2.Visible = false;

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


    }
}
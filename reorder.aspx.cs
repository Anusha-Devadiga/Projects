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
    public partial class reorder : System.Web.UI.Page
    {
        connect c;
        DataSet ds;
        SqlDataAdapter adp = new SqlDataAdapter();
        protected void Page_Load(object sender, EventArgs e)
        {
            c = new connect();
            ds = new DataSet();
            Panel1.Visible = false;
            Label1.Visible = false;
            c.cmd.CommandText = "select * from stock where qty<reorder";
            adp.SelectCommand = c.cmd;
            adp.Fill(ds, "stk");
            if (ds.Tables["stk"].Rows.Count > 0)
            {
                Label1.Visible = false;
                Panel1.Visible = true;
                GridView1.DataSource = ds.Tables["stk"];
                GridView1.DataBind();
            }
            else
            {
                Panel1.Visible = false;
                Label1.Visible = true;
                Label1.Text = "No Item To Reorder";
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Session["reorder"] = "yes";
            Response.Redirect("~/neworder.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Session["reorder"] = "no";
            Response.Redirect("~/stock.aspx");
        }
    }
}
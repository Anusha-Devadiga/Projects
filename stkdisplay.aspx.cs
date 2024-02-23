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
    public partial class stkdisplay : System.Web.UI.Page
    {
        connect c;
        DataSet ds;
        SqlDataAdapter adp = new SqlDataAdapter();
        protected void Page_Load(object sender, EventArgs e)
        {
            c = new connect();
            ds = new DataSet();
            c.cmd.CommandText = "select * from stock where qty<reorder";
            adp.SelectCommand = c.cmd;
            adp.Fill(ds, "stk");
            if (ds.Tables["stk"].Rows.Count > 0)
            {
                GridView1.DataSource = ds.Tables["stk"];
                GridView1.DataBind();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "msgbox", "<script>alert('no records')</script>");
            }
        }


        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/stock.aspx");
        }
    }
}
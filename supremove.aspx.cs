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
    public partial class supremove : System.Web.UI.Page
    {
        connect c;
        DataSet ds;
        SqlDataAdapter adp = new SqlDataAdapter();
        protected void Page_Load(object sender, EventArgs e)
        {
            c = new connect();
            ds = new DataSet();
            if (DropDownList1.Items.Count == 0)
            {
                c.cmd.CommandText = "select distinct(name) from supplier where status='active'";
                adp.SelectCommand = c.cmd;
                adp.Fill(ds, "sup");
                if (ds.Tables["sup"].Rows.Count > 0)
                {
                    DropDownList1.Items.Add("select");
                    int i;
                    for (i = 0; i < ds.Tables["sup"].Rows.Count; i++)
                    {
                        DropDownList1.Items.Add(ds.Tables["sup"].Rows[i].ItemArray[0].ToString());
                    }
                }
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string r = "inactive";
                c = new connect();
                ds = new DataSet();
                c.cmd.CommandText = "select * from supplier where name='" + DropDownList1.SelectedItem.ToString() + "'";
                adp.SelectCommand = c.cmd;
                adp.Fill(ds, "sup");
                if ((DropDownList1.SelectedIndex) != 0)
                {
                    if (ds.Tables["sup"].Rows.Count > 0)
                    {
                        c.cmd.CommandText = "delete from supplier where name='" + DropDownList1.SelectedItem.ToString() + "'";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "msgbox", "<script>alert('record removed')</script>");
                        c.cmd.CommandText = "update supplier set status=@status where name='" + DropDownList1.SelectedItem.ToString() + "'";
                        c.cmd.Parameters.Clear();
                        c.cmd.Parameters.Add("@status", SqlDbType.NVarChar).Value = Convert.ToString(r);
                        c.cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "msgbox", "<script>alert('record does not exist')</script>");
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "msgbox", "<script>alert('enter valid supplier id')</script>");
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
            try
            {
                c = new connect();
                ds = new DataSet();
                c.cmd.CommandText = "select * from supplier";
                if (DropDownList1.SelectedItem.ToString() != "")
                {
                    adp.SelectCommand = c.cmd;
                    adp.Fill(ds, "sup");
                    if (ds.Tables["sup"].Rows.Count > 0)
                    {
                        GridView1.DataSource = ds.Tables["sup"];
                        GridView1.DataBind();
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "msgbox", "<script>alert(' no record ')</script>");
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "msgbox", "<script>alert('must enter supid')</script>");
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

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/supplier.aspx");
        }
    }
}
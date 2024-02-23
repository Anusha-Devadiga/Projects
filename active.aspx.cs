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
    public partial class active : System.Web.UI.Page
    {
        connect c;
        DataSet ds;
        SqlDataAdapter adp = new SqlDataAdapter();
        string lockstatus;

        protected void Page_Load(object sender, EventArgs e)
        {
            c = new connect();
            ds = new DataSet();
            if (DropDownList1.Items.Count == 0)
            {
                c.cmd.CommandText = "select distinct(supid) from supplier where status='inactive'";
                adp.SelectCommand = c.cmd;
                adp.Fill(ds, "sup");
                if(ds.Tables["sup"].Rows.Count>0)
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

        protected void Button5_Click(object sender, EventArgs e)
        {
            try
            {
                c = new connect();
                ds = new DataSet();
                if ((DropDownList1.SelectedIndex) != 0)
                {
                    c.cmd.CommandText = "Select* from supplier where supid='" + DropDownList1.SelectedItem.Text + "'";
                    adp.SelectCommand = c.cmd;
                    adp.Fill(ds, "sup");
                    if (ds.Tables["sup"].Rows.Count > 0)
                    {
                        lockstatus = "active";
                        c.cmd.CommandText = "update supplier set status=@status where supid='" + DropDownList1.SelectedItem.Text + "'";
                        c.cmd.Parameters.Add("@status", SqlDbType.NVarChar).Value = Convert.ToString(lockstatus);
                        c.cmd.ExecuteNonQuery();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "msgbox", "<script>alert('supplier is activated')</script>");

                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "msgbox", "<script>alert('supplier is not found')</script>");

                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "msgbox", "<script>alert('Enter supid')</script>");
                    DropDownList1.SelectedIndex = 0;

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

        protected void Button6_Click(object sender, EventArgs e)
        {
            try
            {
                c = new connect();
                ds = new DataSet();
                c.cmd.CommandText = "select * from supplier where status='active'";
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
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "msgbox", "<script>alert('no records')</script>");

                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "msgbox", "<script>alert('must enter supid')</script>");

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

        protected void Button7_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/supplier.aspx");
        }
    }
}
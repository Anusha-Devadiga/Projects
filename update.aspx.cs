using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace cloth
{
    public partial class supadd : System.Web.UI.Page
    {
        connect c;
        DataSet ds;
        SqlDataAdapter adp = new SqlDataAdapter();

        protected void Page_Load(object sender, EventArgs e)
        {
            
            
                c = new connect();
                ds = new DataSet();
                TextBox4.Focus();
            if (DropDownList1.Items.Count == 0)
            {

                c.cmd.CommandText = "select supid from supplier where status='active'";
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
    
        protected void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                c = new connect();
                ds = new DataSet();
                c.cmd.CommandText = "update supplier set address=@address where supid='" + DropDownList1.SelectedItem.ToString() + "'";
                c.cmd.Parameters.Add("@address", SqlDbType.NVarChar).Value = TextBox3.Text;
                c.cmd.ExecuteNonQuery();
                c.cmd.CommandText = "Update supplier set contact=@contact where supid='" + DropDownList1.SelectedItem.ToString() + "'";
                c.cmd.Parameters.Add("@contact", SqlDbType.NVarChar).Value = TextBox4.Text;
                c.cmd.ExecuteNonQuery();
                c.cmd.CommandText = "Update supplier set email=@email where supid='" + DropDownList1.SelectedItem.ToString() + "'";
                c.cmd.Parameters.Add("@email", SqlDbType.NVarChar).Value = TextBox5.Text;
                if (TextBox3.Text != "" && TextBox4.Text != "" && TextBox5.Text != "" )
                {
                    c.cmd.ExecuteNonQuery();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "msgbox", "<script>alert('Record updated')</script>");
                   
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "msgbox", "<script>alert('Enter all fields')</script>");

                }
                TextBox2.Enabled = false;
                TextBox3.Enabled = false;
                TextBox4.Enabled = false;
                TextBox5.Enabled = false;
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

        protected void Button4_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/supplier.aspx");
        }

       protected void Button1_Click1(object sender, EventArgs e)
        {
           /* try
            {
                c = new connect();
                ds = new DataSet();
                c.cmd.CommandText = "select * from supplier where supid='" + DropDownList1.SelectedItem + "'";
                DropDownList1.Enabled = false;
                if (DropDownList1.SelectedItem.ToString() != "")
                {
                    adp.SelectCommand = c.cmd;
                    adp.Fill(ds, "sup");
                    if (Convert.ToString(ds.Tables["sup"].Rows[0].ItemArray[5]) != "InActive")
                    {
                        TextBox2.Text = Convert.ToString(ds.Tables["sup"].Rows[0].ItemArray[1]);
                        TextBox2.Focus();
                        TextBox3.Text = Convert.ToString(ds.Tables["sup"].Rows[0].ItemArray[2]);
                        TextBox4.Text = Convert.ToString(ds.Tables["sup"].Rows[0].ItemArray[3]);
                        TextBox5.Text = Convert.ToString(ds.Tables["sup"].Rows[0].ItemArray[4]);
                        Label7.Text = Convert.ToString(ds.Tables["sup"].Rows[0].ItemArray[5]);
                        c.cmd.ExecuteNonQuery();

                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "msgbox", "<script>alert('supplier is inactive')</script>");

                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "msgbox", "<script>alert('must enter supplier id')</script>");

                }
            }
            catch (Exception)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "msgbox", "<script>alert('supplier Record not found....try again!!!!')</script>");


            }
            finally
            {
                c.con.Close();

            }*/
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                c = new connect();
                ds = new DataSet();
                c.cmd.CommandText = "select * from supplier where supid='" + DropDownList1.SelectedItem + "'";
                DropDownList1.Enabled = false;
                if (DropDownList1.SelectedItem.ToString() != "")
                {
                    adp.SelectCommand = c.cmd;
                    adp.Fill(ds, "sup");
                    if (Convert.ToString(ds.Tables["sup"].Rows[0].ItemArray[5]) != "InActive")
                    {
                        TextBox2.Text = Convert.ToString(ds.Tables["sup"].Rows[0].ItemArray[1]);
                        TextBox2.Focus();
                        TextBox3.Text = Convert.ToString(ds.Tables["sup"].Rows[0].ItemArray[2]);
                        TextBox4.Text = Convert.ToString(ds.Tables["sup"].Rows[0].ItemArray[3]);
                        TextBox5.Text = Convert.ToString(ds.Tables["sup"].Rows[0].ItemArray[4]);
                        Label7.Text = Convert.ToString(ds.Tables["sup"].Rows[0].ItemArray[5]);
                        c.cmd.ExecuteNonQuery();

                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "msgbox", "<script>alert('supplier is inactive')</script>");

                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "msgbox", "<script>alert('must enter supplier id')</script>");

                }
            }
            catch (Exception)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "msgbox", "<script>alert('supplier Record not found....try again!!!!')</script>");


            }
            finally
            {
                c.con.Close();

            }
        }
    }
}
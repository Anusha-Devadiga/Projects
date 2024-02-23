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
    public partial class purchasereturn : System.Web.UI.Page
    {
        connect c;
        DataSet ds, ds1;
        SqlDataAdapter adp = new SqlDataAdapter();
        int qty1, qty2;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                c = new connect();
                ds = new DataSet();
                Label1.Text = DateTime.Now.ToShortDateString();
                int count, count1;
                c.cmd.CommandText = "select count(*) from purchasereturn";
                count1 = Convert.ToInt16(c.cmd.ExecuteScalar()) + 1;
                Label2.Text = count1.ToString();
                c.cmd.CommandText = "select count(*) from purchasereturn";
                count = Convert.ToInt16(c.cmd.ExecuteScalar()) + 1;
                TextBox1.Text = "R100" + count.ToString();
                TextBox9.Focus();
                if ((String)Session["nextpur"] == "yes")
                {
                    TextBox1.Text = (String)Session["rid"];
                    DropDownList5.SelectedIndex = (int)Session["bno"];
                    TextBox2.Text = (String)Session["supname"];
                    TextBox12.Text = (String)Session["supid"];
                }
                if (DropDownList5.Items.Count == 0)
                {
                    c.cmd.CommandText = "select distinct(bill_no) from purbill";
                    adp.SelectCommand = c.cmd;
                    adp.Fill(ds, "pbill");
                    if (ds.Tables["pbill"].Rows.Count > 0)
                    {
                        DropDownList5.Items.Add("--Select--");
                        for (int i = 0; i < ds.Tables["pbill"].Rows.Count; i++)
                        {
                            DropDownList5.Items.Add(ds.Tables["pbill"].Rows[i].ItemArray[0].ToString());
                        }
                    }
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
            try
            {
                c = new connect();
                ds = new DataSet();
                c.cmd.CommandText = "insert into purchasereturn values(@return_no, @return_id, @bill_no, @name, @supid, @item_id, @item_name, @category, @cat_id, @brand, @color, @size, @quantity, @p_date, @pr_date)";
                c.cmd.Parameters.Clear();
                if (TextBox1.Text != "" && TextBox2.Text != "" &&
                TextBox5.Text != "" && TextBox7.Text != "" && TextBox9.Text != "" &&TextBox11.Text != "")
                {
                    c.cmd.Parameters.Add("@return_no",SqlDbType.NVarChar).Value = Label2.Text;
                    c.cmd.Parameters.Add("@return_id",SqlDbType.NVarChar).Value = TextBox1.Text;
                    c.cmd.Parameters.Add("@bill_no",SqlDbType.NVarChar).Value = DropDownList5.SelectedItem.ToString();
                    c.cmd.Parameters.Add("@name",SqlDbType.NVarChar).Value = TextBox2.Text;
                    c.cmd.Parameters.Add("@supid",SqlDbType.NVarChar).Value = TextBox12.Text;
                    c.cmd.Parameters.Add("@item_id",SqlDbType.NVarChar).Value = DropDownList4.SelectedItem.ToString();
                    c.cmd.Parameters.Add("@item_name",SqlDbType.NVarChar).Value = TextBox5.Text;
                    c.cmd.Parameters.Add("@category",SqlDbType.NVarChar).Value = TextBox4.Text;
                    c.cmd.Parameters.Add("@cat_id",SqlDbType.NVarChar).Value = TextBox7.Text;
                    c.cmd.Parameters.Add("@brand",SqlDbType.NVarChar).Value = TextBox6.Text;
                    c.cmd.Parameters.Add("@size",SqlDbType.NVarChar).Value = TextBox3.Text;
                    c.cmd.Parameters.Add("@color",SqlDbType.NVarChar).Value = TextBox8.Text;
                    c.cmd.Parameters.Add("@quantity",SqlDbType.SmallInt).Value = Convert.ToInt16(TextBox9.Text);
                    c.cmd.Parameters.Add("@p_date",SqlDbType.DateTime).Value = Convert.ToDateTime(TextBox11.Text);
                    c.cmd.Parameters.Add("@pr_date",SqlDbType.DateTime).Value = Label1.Text;
                    c.cmd.ExecuteNonQuery();
                    Session["rid"] = TextBox1.Text;
                    Session["bno"] = DropDownList5.SelectedIndex;
                    Session["supname"] = TextBox2.Text;
                    Session["supid"] = TextBox12.Text;
                    Session["itemid"] = DropDownList4.SelectedItem.Text;
                    Session["itemname"] = TextBox5.Text;
                    Session["categ"] = TextBox4.Text;
                    Session["catid"] = TextBox7.Text;
                    Session["brand"] = TextBox6.Text;
                    Session["qnty"] = TextBox9.Text;
                    Session["colour"] = TextBox8.Text;
                    Session["pdate"] = TextBox11.Text;
                    Session["prd"] = Label1.Text;
                    Session["nextpur"] = "no";
                    c.cmd.CommandText = "select qty from stock where item_id = '" + DropDownList4.SelectedItem.Text + "'";
                    adp.SelectCommand = c.cmd;
                    adp.Fill(ds, "stk");
                    if (ds.Tables["stk"].Rows.Count > 0)
                    {
                        qty1 = Convert.ToInt16(ds.Tables["stk"].Rows[0].ItemArray[0]);
                        qty2 = qty1 - Convert.ToInt16(TextBox9.Text);
                        c.cmd.CommandText = "Update stock set qty=@qty where item_id = '" + DropDownList4.SelectedItem.Text + "'";
                        c.cmd.Parameters.Add("@qty",SqlDbType.SmallInt).Value = qty2;
                        c.cmd.ExecuteNonQuery();
                    }
                    String title = "dress point";
                    MessageBoxButtons button = MessageBoxButtons.YesNo;
                    String message = "do you want to return one more items";
                    DialogResult result = MessageBox.Show(message,title, button);
                    if (result == DialogResult.Yes)
                    {
                        Session["nextpur"] = "yes";
                        Response.Redirect("~/purchasereturn.aspx");
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "msgbox","<script>alert('Submitted')</script>");
                        c.cmd.CommandText = "select * from purchasereturn where return_id = '" + (String)Session["rid"] + "'";
                        adp.SelectCommand = c.cmd;
                        adp.Fill(ds, "ret");
                        Session["ds"] = ds;
                        
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "msgbox", "<script>alert('Enter all fields')</script>");
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

        protected void Button2_Click(object sender, EventArgs e)
        {
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox12.Text = "";
            TextBox6.Text = "";
            TextBox5.Text = "";
            TextBox7.Text = "";
            TextBox9.Text = "";
            TextBox11.Text = "";
            TextBox8.Text = "";
           // DropDownList4.Items.Clear();
            //DropDownList5.Items.Clear();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                c = new connect();
                c.cmd.CommandText = "select * from purchasereturn";
                ds = new DataSet();
                adp.SelectCommand = c.cmd;
                adp.Fill(ds, "ret");
                if (ds.Tables["ret"].Rows.Count > 0)
                {
                    GridView1.DataSource = ds.Tables["ret"];
                    GridView1.DataBind();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(),"msgbox", "<script>alert('No Records')</script>");
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

        protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
        {
            c = new connect();
            ds = new DataSet();
            c.cmd.CommandText = "select * from purbill where item_id='" + DropDownList4.SelectedItem.Text + "'";
            adp.SelectCommand = c.cmd;
            adp.Fill(ds, "pbill");
            if (ds.Tables["pbill"].Rows.Count > 0)
            {
                TextBox3.Text = Convert.ToString(ds.Tables["pbill"].Rows[0].ItemArray[12]);
                TextBox4.Text = Convert.ToString(ds.Tables["pbill"].Rows[0].ItemArray[8]);
                TextBox8.Text = Convert.ToString(ds.Tables["pbill"].Rows[0].ItemArray[11]);
                TextBox5.Text = Convert.ToString(ds.Tables["pbill"].Rows[0].ItemArray[7]);
                TextBox7.Text = Convert.ToString(ds.Tables["pbill"].Rows[0].ItemArray[9]);
                TextBox6.Text = Convert.ToString(ds.Tables["pbill"].Rows[0].ItemArray[10]);
                TextBox11.Text = Convert.ToString(ds.Tables["pbill"].Rows[0].ItemArray[1]);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(),"msgbox", "<script>alert('no records')</script>");
            }

        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/purchase.aspx");
        }

        protected void DropDownList5_SelectedIndexChanged(object sender, EventArgs e)
        {
            c = new connect();
            ds = new DataSet();
            ds1 = new DataSet();
            if (DropDownList4.Items.Count == 0)
            {
                c.cmd.CommandText = "select distinct(item_id) from purbill where bill_no = '" + DropDownList5.SelectedItem.Text + "'";
                adp.SelectCommand = c.cmd;
                adp.Fill(ds, "pbill");
                if (ds.Tables["pbill"].Rows.Count > 0)
                {
                    DropDownList4.Items.Add("--Select--");
                    for (int i = 0; i < ds.Tables["pbill"].Rows.Count;
                    i++)
                    {
                        DropDownList4.Items.Add(ds.Tables["pbill"].Rows[i].ItemArray[0].ToString());
                    }
                }
            }
            else
            {
                DropDownList4.Items.Clear();
                if (DropDownList4.Items.Count == 0)
                {
                    c.cmd.CommandText = "select distinct(item_id) from purbill where bill_no = '" + DropDownList5.SelectedItem.Text + "'";
                    adp.SelectCommand = c.cmd;
                    adp.Fill(ds, "pbill");
                    if (ds.Tables["pbill"].Rows.Count > 0)
                    {
                        DropDownList4.Items.Add("--Select--");
                        for (int i = 0; i < ds.Tables["pbill"].Rows.Count; i++)
                        {
                            DropDownList4.Items.Add(ds.Tables["pbill"].Rows[i].ItemArray[0].ToString());
                        }
                    }
                }
            }
            c.cmd.CommandText = "select * from purbill where bill_no='" + DropDownList5.SelectedItem.Text + "'";
            adp.SelectCommand = c.cmd;
            adp.Fill(ds1, "pbill");
            if (ds1.Tables["pbill"].Rows.Count > 0)
            {
                TextBox2.Text = Convert.ToString(ds1.Tables["pbill"].Rows[0].ItemArray[5]);
                TextBox12.Text = Convert.ToString(ds1.Tables["pbill"].Rows[0].ItemArray[4]);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(),"msgbox", "<script>alert('no records')</script>");
            }

        }
        protected void TextBox9_TextChanged(object sender, EventArgs e)
        {
            c = new connect();
            ds = new DataSet();
            int n, m;
            c.cmd.CommandText = "select quantity from purbill where item_id = '" + DropDownList4.SelectedItem.Text + "'";
            adp.SelectCommand = c.cmd;
            adp.Fill(ds, "pbill");
            if (ds.Tables["pbill"].Rows.Count > 0)
            {
                n = Convert.ToInt16(ds.Tables["pbill"].Rows[0].ItemArray[0]);
                m = Convert.ToInt16(TextBox9.Text);
                if (m > n)
                {
                    MessageBox.Show("Quantity less than received" + n);
                }
            }
        }
    }

}
    

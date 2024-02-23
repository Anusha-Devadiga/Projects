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
    public partial class Salesdetails : System.Web.UI.Page
    {
        connect c;
        DataSet ds, ds1, ds2;
        SqlDataAdapter adp = new SqlDataAdapter();
        string bno;
        protected void Page_Load(object sender, EventArgs e)
        {
            c = new connect();
            ds = new DataSet();
            try
            {
                if (!IsPostBack)
                {
                    Calendar1.Visible = false;
                }

                int count;
                TextBox12.Focus();
                TextBox12.Text = DateTime.Now.ToShortDateString();
                c.cmd.CommandText = "select count(*) from salesdetails";
                count = Convert.ToInt16(c.cmd.ExecuteScalar()) + 1;
                Label19.Text = count.ToString();
                c.cmd.CommandText = "select max(bill_id) from salesdetails";
                adp.SelectCommand = c.cmd;
                adp.Fill(ds, "sale");
                if (Convert.ToString(ds.Tables["sale"].Rows[0].ItemArray[0]) == "")
                {
                    bno = "B100";
                }
                else
                {
                    if (ds.Tables["sale"].Rows.Count > 0)
                    {
                        String s1 = Convert.ToString(ds.Tables["sale"].Rows[0].ItemArray[0]);
                        int n = Convert.ToInt16(s1.Length.ToString());
                        s1 = s1.Substring(1, n - 1);
                        int m = 1 + Convert.ToInt16(s1);
                        bno = "B" + m.ToString();
                    }

                }
                if ((String)Session["nextpur"] == "yes")
                {
                    Button3.Visible = false;
                    bno = (String)Session["bid"];
                    TextBox2.Text = (String)Session["cid"];
                    TextBox3.Text = (String)Session["cname"];
                    TextBox1.Text = (String)Session["contact"];
                    TextBox4.Text = (String)Session["address"];
                    TextBox5.Text = (String)Session["email"];
                    TextBox12.Text = (String)Session["date"];
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                c = new connect();
                ds = new DataSet();
                int qty1, qty2;
                c.cmd.CommandText = "insert into salesdetails values(@s_no, @cust_id, @item_id, @it_name, @category, @cat_id, @brand, @quantity, @price, @color, @size, @deli_date, @bill_id, @amt)";
                c.cmd.Parameters.Clear();
                if (TextBox2.Text != "" && TextBox3.Text != "" && TextBox4.Text != "" && TextBox5.Text != "" && TextBox6.Text != "" &&
               TextBox7.Text != "" && TextBox8.Text != "" && TextBox9.Text != "" && TextBox12.Text != "")
                {
                    c.cmd.Parameters.Add("@s_no",SqlDbType.NVarChar).Value = Label1.Text;
                    c.cmd.Parameters.Add("@cust_id",SqlDbType.NVarChar).Value = TextBox2.Text;
                    c.cmd.Parameters.Add("@item_id",SqlDbType.NVarChar).Value = TextBox6.Text;
                    c.cmd.Parameters.Add("@it_name",SqlDbType.NVarChar).Value = TextBox9.Text;
                    c.cmd.Parameters.Add("@category",SqlDbType.NVarChar).Value = DropDownList1.SelectedItem.ToString();
                    c.cmd.Parameters.Add("@cat_id",SqlDbType.NVarChar).Value = TextBox7.Text;
                    c.cmd.Parameters.Add("@brand",SqlDbType.NVarChar).Value = DropDownList2.SelectedItem.ToString();
                    c.cmd.Parameters.Add("@quantity", SqlDbType.SmallInt).Value = Convert.ToInt16(TextBox8.Text);
                    c.cmd.Parameters.Add("@price",SqlDbType.BigInt).Value =Convert.ToInt64(DropDownList5.SelectedItem.Text);
                    c.cmd.Parameters.Add("@color",SqlDbType.NVarChar).Value = DropDownList3.SelectedItem.ToString();
                    c.cmd.Parameters.Add("size",SqlDbType.NVarChar).Value = DropDownList4.SelectedItem.ToString();
                    c.cmd.Parameters.Add("@deli_date",SqlDbType.DateTime).Value = Convert.ToDateTime(TextBox12.Text);
                    c.cmd.Parameters.Add("@bill_id",SqlDbType.NVarChar).Value = Convert.ToString(bno);
                    c.cmd.Parameters.Add("@amt", SqlDbType.BigInt).Value= Convert.ToInt64(TextBox10.Text);
                    c.cmd.ExecuteNonQuery();
                    c.cmd.CommandText = "insert into sstock values(@bill_no, @it_id, @qnty)";
                    c.cmd.Parameters.Add("@bill_no", SqlDbType.NVarChar).Value = Convert.ToString(bno);
                    c.cmd.Parameters.Add("@it_id", SqlDbType.NVarChar).Value = TextBox6.Text;
                    c.cmd.Parameters.Add("@qnty", SqlDbType.SmallInt).Value = Convert.ToInt16(TextBox8.Text);
                    c.cmd.ExecuteNonQuery();
                    Session["cid"] = TextBox2.Text;
                    Session["cname"] = TextBox3.Text;
                    Session["contact"] = TextBox1.Text;
                    Session["address"] = TextBox4.Text;
                    Session["email"] = TextBox5.Text;
                    Session["iid"] = TextBox6.Text;
                    Session["iname"] = TextBox9.Text;
                    Session["cat"] = DropDownList1.SelectedItem;
                    Session["catid"] = TextBox7.Text;
                    Session["brand"] = DropDownList2.SelectedItem;
                    Session["qty"] = TextBox8.Text;
                    Session["amt"] = DropDownList5.SelectedItem.Text;
                    Session["col"] = DropDownList3.SelectedItem;
                    Session["date"] = TextBox12.Text;
                    Session["bid"] = Convert.ToString(bno);
                    Session["amt"] = TextBox10.Text;
                    Session["nextpur"] = "no";
                    c.cmd.CommandText = "select qty from stock where item_id = '" + TextBox6.Text + "'";
                    adp.SelectCommand = c.cmd;
                    adp.Fill(ds, "stk");
                    if (ds.Tables["stk"].Rows.Count > 0)
                    {
                        qty1 = Convert.ToInt16(ds.Tables["stk"].Rows[0].ItemArray[0]);
                        qty2 = qty1 - Convert.ToInt16(TextBox8.Text);
                        c.cmd.CommandText = "Update stock set qty=@qty where item_id = '" + TextBox6.Text + "'";
                        c.cmd.Parameters.Add("@qty", SqlDbType.SmallInt).Value = qty2;
                        c.cmd.ExecuteNonQuery();
                        String title = "dress point";
                        MessageBoxButtons button = MessageBoxButtons.YesNo;
                        String message = "do you want to add more items";
                        DialogResult result = MessageBox.Show(message,title, button);
                        if (result == DialogResult.Yes)
                        {
                            Session["nextpur"] = "yes";
                            Response.Redirect("~/Salesdetails.aspx");
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "msgbox","<script>alert('bill is ready')</script>");
                            c.cmd.CommandText = "select * from salesdetails where bill_id = '" + (String)Session["bid"] + "'";
                            adp.SelectCommand = c.cmd;
                            adp.Fill(ds, "sales");
                            Session["ds"] = ds;
                           // Response.Redirect("~/salebill.aspx");
                        }
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "msgbox", "<script>alert('Enter all fields')</script>");
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
        protected void Button4_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Salesdetails.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {

                c = new connect();
                c.cmd.CommandText = "select * from salesdetails";
                ds = new DataSet();
                adp.SelectCommand = c.cmd;
                adp.Fill(ds, "sales");
                if (ds.Tables["sales"].Rows.Count > 0)
                {
                    GridView1.DataSource = ds.Tables["sales"];
                    GridView1.DataBind();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "msgbox", "<script>alert('no records')</script>");
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
            c = new connect();
            try
            {
                if (TextBox1.Text != "" && TextBox2.Text != "" &&
               TextBox3.Text != "" && TextBox4.Text != "" && TextBox5.Text != "")
                {
                    c.cmd.CommandText = "insert into customerdetails  values(@cust_id, @name, @address,@contact_no, @email)";
                    c.cmd.Parameters.Add("@cust_id", SqlDbType.NVarChar).Value = TextBox2.Text;
                    c.cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = TextBox3.Text;
                    c.cmd.Parameters.Add("@contact_no", SqlDbType.NVarChar).Value = TextBox1.Text;
                    c.cmd.Parameters.Add("@address", SqlDbType.NVarChar).Value = TextBox4.Text;
                    c.cmd.Parameters.Add("@email", SqlDbType.NVarChar).Value = TextBox5.Text;
                    c.cmd.ExecuteNonQuery();
                    Button3.Visible = false;
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "msgbox", "<script>alert('Must enter customer details')</script>");
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
        
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList2.Items.Clear();
            DropDownList3.Items.Clear();
            DropDownList5.Items.Clear();
            c = new connect();
            ds = new DataSet();
            c.cmd.CommandText = "select * from stock where category='" + DropDownList1.SelectedItem.Text + "'";
            adp.SelectCommand = c.cmd;
            adp.Fill(ds, "stk");
            if (ds.Tables["stk"].Rows.Count > 0)
            {
                TextBox7.Text = Convert.ToString(ds.Tables["stk"].Rows[0].ItemArray[3]);
                TextBox9.Text = Convert.ToString(ds.Tables["stk"].Rows[0].ItemArray[1]);
            }
            ds1 = new DataSet();
            ds2 = new DataSet();
            if (DropDownList2.Items.Count == 0)
            {
                c.cmd.CommandText = "select distinct(brand) from stock where category = '" + DropDownList1.SelectedItem.Text + "'";
                adp.SelectCommand = c.cmd;
                adp.Fill(ds1, "stk");
                if (ds1.Tables["stk"].Rows.Count > 0)
                {
                    DropDownList2.Items.Add(" ");
                    for (int i = 0; i < ds1.Tables["stk"].Rows.Count;i++)
                    {
                        DropDownList2.Items.Add(ds1.Tables["stk"].Rows[i].ItemArray[0].ToString());
                    }
                }
            }

        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            c = new connect();
            ds1 = new DataSet();
            if (DropDownList3.Items.Count == 0)
            {
                c.cmd.CommandText = "select distinct(color) from stock where brand = '" + DropDownList2.SelectedItem.Text + "' and category = '" + DropDownList1.SelectedItem.Text + "'";
                adp.SelectCommand = c.cmd;
                adp.Fill(ds1, "stk");
                if (ds1.Tables["stk"].Rows.Count > 0)
                {
                    DropDownList3.Items.Add(" ");
                    for (int i = 0; i < ds1.Tables["stk"].Rows.Count; i++)
                    {
                        DropDownList3.Items.Add(ds1.Tables["stk"].Rows[i].ItemArray[0].ToString());
                    }
                }
            }
        }

        protected void TextBox8_TextChanged(object sender, EventArgs e)
        {
            c = new connect();
            ds = new DataSet();
            decimal p, q, tp;
            int n, m;
            c.cmd.CommandText = "select qty from stock where item_id='" + TextBox6.Text + "'";
            adp.SelectCommand = c.cmd;
            adp.Fill(ds, "stk");
            if (ds.Tables["stk"].Rows.Count > 0)
            {
                n =Convert.ToInt16(ds.Tables["stk"].Rows[0].ItemArray[0]);
                m = Convert.ToInt16(TextBox8.Text);
                if (m < n)
                {
                    p =Convert.ToDecimal(DropDownList5.SelectedItem.Text);
                    q = Convert.ToInt16(TextBox8.Text);
                    tp = Convert.ToDecimal(p * q);
                    TextBox10.Text = Convert.ToString(tp);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "msgbox", "<script>alert('Out of stock')</script>");
                }
            }
        }
        
        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            c = new connect();
            ds1 = new DataSet();
            if (DropDownList5.Items.Count == 0)
            {
                c.cmd.CommandText = "select amt from stock where color='" + DropDownList3.SelectedItem.Text + "' and brand='" + DropDownList2.SelectedItem.Text + "'and category='" + DropDownList1.SelectedItem.Text + "'";
                adp.SelectCommand = c.cmd;
                adp.Fill(ds1, "stk");
                if (ds1.Tables["stk"].Rows.Count > 0)
                {
                    DropDownList5.Items.Add(" ");
                    for (int i = 0; i < ds1.Tables["stk"].Rows.Count;i++)
                    {
                        DropDownList5.Items.Add(ds1.Tables["stk"].Rows[i].ItemArray[0].ToString());
                    }
                }
            }

        }
        
        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            int count;
            try
            {
                c = new connect();
                ds = new DataSet();
                c.cmd.CommandText = "select * from customerdetails where contact_no = '" + TextBox1.Text + "'";
                adp.SelectCommand = c.cmd;
                adp.Fill(ds, "cust");
                if (ds.Tables["cust"].Rows.Count > 0)
                {
                    TextBox3.ReadOnly = true;
                    TextBox4.ReadOnly = true;
                    TextBox5.ReadOnly = true;
                    TextBox2.Text = Convert.ToString(ds.Tables["cust"].Rows[0].ItemArray[0]);
                    TextBox3.Text = Convert.ToString(ds.Tables["cust"].Rows[0].ItemArray[1]);
                    TextBox4.Text = Convert.ToString(ds.Tables["cust"].Rows[0].ItemArray[2]);
                    TextBox5.Text = Convert.ToString(ds.Tables["cust"].Rows[0].ItemArray[4]);
                    c.cmd.ExecuteNonQuery();
                    Button3.Visible = false;
                }
                else
                {
                    c.cmd.CommandText = "select count(*) from customerdetails";
                    count = Convert.ToInt16(c.cmd.ExecuteScalar()) + 1;
                    TextBox2.Text = "CI100" + count.ToString();
                    TextBox3.Text = "";
                    TextBox4.Text = "";
                    TextBox5.Text = "";
                    Button3.Visible = true;
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

        protected void TextBox5_TextChanged(object sender, EventArgs e)
        {
            try
            {
                c = new connect();
                c.cmd.CommandText = "select count(*) from customerdetails where email = '" + TextBox5.Text + "'";
                int p = Convert.ToInt16(c.cmd.ExecuteScalar());
                if (p > 0)
                {
                    MessageBox.Show("Email id already exist");
                    TextBox5.Text = "";
                    TextBox5.Focus();
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
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Calendar1.Visible = true;
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/home.aspx");
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            TextBox12.Text = Calendar1.SelectedDate.ToShortDateString();
            if (TextBox12.Text == "")
            {
                Calendar1.Visible = true;
            }
            else
            {
                Calendar1.Visible = false;
            }
        }

        protected void DropDownList5_SelectedIndexChanged(object sender, EventArgs e)
        {
            c = new connect();
            ds = new DataSet();
            c.cmd.CommandText = "select item_id from stock where amt='" + DropDownList5.SelectedItem.Text + "'and color='" + DropDownList3.SelectedItem.Text + "'and brand='" + DropDownList2.SelectedItem.Text + "' and category='" + DropDownList1.SelectedItem.Text + "'";
            adp.SelectCommand = c.cmd;
            adp.Fill(ds, "stk");
            if (ds.Tables["stk"].Rows.Count > 0)
            {
                TextBox6.Text = Convert.ToString(ds.Tables["stk"].Rows[0].ItemArray[0]);
            }
        }      
    }
}

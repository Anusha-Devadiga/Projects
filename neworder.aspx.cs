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
    public partial class neworder : System.Web.UI.Page
    {
        connect c;
        DataSet ds;
        SqlDataAdapter adp = new SqlDataAdapter();
        protected void Page_Load(object sender, EventArgs e)
        {
            c = new connect();
            ds = new DataSet();
            c.cmd.CommandText = "select max(puror_no) from purchaseorder";
            adp.SelectCommand = c.cmd;
            adp.Fill(ds, "pur");
            if (Convert.ToString(ds.Tables["pur"].Rows[0].ItemArray[0]) == "")
            {
                TextBox1.Text = "P1000";
            }
            else
            {
                if (ds.Tables["pur"].Rows.Count > 0)
                {
                    string s1 = Convert.ToString(ds.Tables["pur"].Rows[0].ItemArray[0]);
                    int n = Convert.ToInt16(s1.Length.ToString());
                    s1 = s1.Substring(1, n - 1);
                    int m = 1 + Convert.ToInt16(s1);
                    TextBox1.Text = "P" + m.ToString();
                }
            }

            TextBox4.Focus();
            try
            {
                if (DropDownList1.Items.Count == 0)
                {
                    c.cmd.CommandText = "select (name) from supplier where status='active'";
                    adp.SelectCommand = c.cmd;
                    adp.Fill(ds, "sup");
                    if (ds.Tables["sup"].Rows.Count > 0)
                    {
                        DropDownList1.Items.Add("--Select--");
                        for (int i = 0; i < ds.Tables["sup"].Rows.Count; i++)
                        {
                            DropDownList1.Items.Add(ds.Tables["sup"].Rows[i].ItemArray[0].ToString());
                        }
                    }
                }
                if (!IsPostBack)
                {
                    Calendar1.Visible = false;
                }
                if ((String)Session["reorder"] == "yes")
                {
                    c.cmd.CommandText = "select *from stock where qty < reorder";
                    adp.SelectCommand = c.cmd;
                    adp.Fill(ds, "stk");
                    if (ds.Tables["stk"].Rows.Count > 0)
                    {
                        GridView2.DataSource = ds.Tables["stk"];
                        GridView2.DataBind();
                    }
                }
                Label1.Text = DateTime.Now.ToShortDateString();
                if ((String)Session["nextpur"] == "yes")
                {
                    Button4.Visible = false;
                    Button1.Visible = true;
                    TextBox1.Text = (String)Session["purno"];
                }
                int count1;
                c.cmd.CommandText = "select count(*) from purchaseorder";
                count1 = Convert.ToInt16(c.cmd.ExecuteScalar()) + 1;
                Label2.Text = count1.ToString();
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
                c.cmd.CommandText = "insert into purchaseorder values(@p_id, @puror_no, @name, @supid, @item_id, @it_name, @category, @cat_id, @brand, @quantity, @color, @size, @or_date, @due_date, @Status, @price)";
                c.cmd.Parameters.Clear();
                if (TextBox1.Text != "" && TextBox2.Text != "" && TextBox3.Text != "" && TextBox4.Text !="" && TextBox5.Text != "" && TextBox7.Text != "" && TextBox8.Text != "" && TextBox6.Text != "")
                {
                    c.cmd.Parameters.Add("@p_id", SqlDbType.NVarChar).Value = Label2.Text;
                    c.cmd.Parameters.Add("@puror_no", SqlDbType.NVarChar).Value = TextBox1.Text;
                    c.cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = DropDownList1.SelectedItem.ToString();
                    c.cmd.Parameters.Add("@supid", SqlDbType.NVarChar).Value = TextBox2.Text;
                    c.cmd.Parameters.Add("@item_id", SqlDbType.NVarChar).Value = TextBox3.Text;
                    c.cmd.Parameters.Add("@it_name", SqlDbType.NVarChar).Value = TextBox5.Text;
                    c.cmd.Parameters.Add("@category", SqlDbType.NVarChar).Value = DropDownList2.SelectedItem.ToString();
                    c.cmd.Parameters.Add("@cat_id", SqlDbType.NVarChar).Value = TextBox8.Text;
                    c.cmd.Parameters.Add("@brand", SqlDbType.NVarChar).Value = TextBox9.Text;
                    c.cmd.Parameters.Add("@quantity", SqlDbType.SmallInt).Value = Convert.ToInt16(TextBox4.Text);
                    c.cmd.Parameters.Add("@color", SqlDbType.NVarChar).Value = TextBox10.Text;
                    c.cmd.Parameters.Add("@size", SqlDbType.NVarChar).Value = DropDownList3.SelectedItem.ToString();
                    c.cmd.Parameters.Add("@or_date", SqlDbType.DateTime).Value = Label1.Text;
                    c.cmd.Parameters.Add("@due_date", SqlDbType.DateTime).Value = TextBox7.Text;
                    c.cmd.Parameters.Add("@Status", SqlDbType.NVarChar).Value = "incomplete";
                    c.cmd.Parameters.Add("@price", SqlDbType.Decimal).Value = Convert.ToDecimal(TextBox6.Text);
                    c.cmd.ExecuteNonQuery();
                    Session["purno"] = TextBox1.Text;
                    Session["sname"] = DropDownList1.SelectedItem.Text;
                    Session["sid"] = TextBox2.Text;
                    Session["itid"] = TextBox3.Text;
                    Session["itname"] = TextBox5.Text;
                    Session["cate"] = DropDownList2.SelectedItem.Text;
                    Session["cat_id"] = TextBox8.Text;
                    Session["brand"] = TextBox9.Text;
                    Session["qty"] = TextBox4.Text;
                    Session["clr"] = TextBox10.Text;
                    Session["odate"] = Label1.Text;
                    Session["dudate"] = TextBox7.Text;
                    Session["price"] = TextBox6.Text;
                    Session["nextpur"] = "no";
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "msgbox", "<script>alert('Enter all Feilds')</script>");
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
            try
            {
                c = new connect();
                c.cmd.CommandText = "insert into pstock values(@po_no,@item_id,@qty)";
                c.cmd.Parameters.Add("@po_no", SqlDbType.NVarChar).Value = TextBox1.Text;
                c.cmd.Parameters.Add("@item_id", SqlDbType.NVarChar).Value = TextBox3.Text;
                c.cmd.Parameters.Add("@qty", SqlDbType.SmallInt).Value = Convert.ToInt16(TextBox4.Text);
                c.cmd.ExecuteNonQuery();
                String title = "dress point";
                MessageBoxButtons button = MessageBoxButtons.YesNo;
                String message = "add items";
                DialogResult result = MessageBox.Show(message, title, button);
                if (result == DialogResult.Yes)
                {
                    Session["nextpur"] = "yes";

                    // Response.Redirect("~/purchase.aspx");
                    TextBox5.Text = "";
                    TextBox8.Text = "";
                    TextBox9.Text = "";
                    TextBox6.Text = "";
                    TextBox10.Text = "";
                    TextBox4.Text = "";
                    TextBox3.Text = "";
                   // DropDownList2.Items.Clear();
                   // DropDownList3.Items.Clear();
                    Button4.Visible = false;
                    DropDownList1.Enabled = false;
                    MessageBox.Show("Submitted");
                }
                else
                {

                    Button1.Visible = false;
                    Button4.Visible = true;
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
            try
            {
                GridView2.Visible = false;
                c = new connect();
                c.cmd.CommandText = "select * from purchaseorder";
                ds = new DataSet();
                adp.SelectCommand = c.cmd;
                adp.Fill(ds, "pur");
                if (ds.Tables["pur"].Rows.Count > 0)
                {
                    GridView1.DataSource = ds.Tables["pur"];
                    GridView1.DataBind();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "msgbox", "<script>alert('No record found')</script>");
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
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox5.Text = "";
            TextBox7.Text = "";
            TextBox8.Text = "";
            TextBox6.Text = "";
            TextBox9.Text = "";
            TextBox10.Text = "";
            DropDownList3.SelectedIndex = 0;
            DropDownList1.SelectedIndex = 0;
            DropDownList2.SelectedIndex = 0;
            Button4.Visible = true;

        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                c = new connect();
                ds = new DataSet();
                c.cmd.CommandText = "select * from supplier where name='" +DropDownList1.SelectedItem + "'";
                adp.SelectCommand = c.cmd;
                adp.Fill(ds, "sup");
                if ((DropDownList1.SelectedIndex) != 0)
                {
                    TextBox2.Text = Convert.ToString(ds.Tables["sup"].Rows[0].ItemArray[0]);
                    c.cmd.ExecuteNonQuery();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "msgbox", "<script>alert('select supplier name')</script>");
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

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToString(DropDownList2.SelectedIndex) == "0")
            {
                TextBox3.Text = "";
                TextBox5.Text = "";
                TextBox8.Text = "";
                TextBox9.Text = "";
                TextBox10.Text = "";
            }
            if (Convert.ToString(DropDownList2.SelectedIndex) == "1")
            {
                //TextBox5.Text =  "saree";
                TextBox8.Text = "SR101";
            }
            if (Convert.ToString(DropDownList2.SelectedIndex) == "2")
            {
                //TextBox5.Text = "kurthis";
                TextBox8.Text = "KU101";
            }
            if (Convert.ToString(DropDownList2.SelectedIndex) == "3")
            {
                //TextBox5.Text = "shirt";
                TextBox8.Text = "SH101";
            }
            if (Convert.ToString(DropDownList2.SelectedIndex) == "4")
            {
                //TextBox5.Text = "jeans";
                TextBox8.Text = "JE101";
            }
            if (Convert.ToString(DropDownList2.SelectedIndex) == "5")
            {
                //TextBox5.Text = "trouser";
                TextBox8.Text = "TR101";
            }
            if (Convert.ToString(DropDownList2.SelectedIndex) == "6")
            {
                //TextBox5.Text = "baby dress";
                TextBox8.Text = "BA101";
            }
            if (Convert.ToString(DropDownList2.SelectedIndex) == "7")
            {
                //TextBox5.Text = "girl kiddress";
                TextBox8.Text = "GI101";
            }
            if (Convert.ToString(DropDownList2.SelectedIndex) == "8")
            {
                //TextBox5.Text = "boy kiddress";
                TextBox8.Text = "BO101";
            }
            if (Convert.ToString(DropDownList2.SelectedIndex) == "9")
            {
                //TextBox5.Text = "t-shirt";
                TextBox8.Text = "TS101";
            }

        }

      /* protected void Button4_Click(object sender, EventArgs e)
        {
            Button1.Visible = true;
            Button4.Visible = false;
            c = new connect();
            ds = new DataSet();
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox5.Text = "";
            TextBox6.Text = "";
            TextBox7.Text = "";
            TextBox8.Text = "";
            c.cmd.CommandText = "select max(puror_no) from purchaseorder";
            adp.SelectCommand = c.cmd;
            adp.Fill(ds, "pur");
            if (Convert.ToString(ds.Tables["pur"].Rows[0].ItemArray[0]) == "")
            {
                TextBox1.Text = "P1000";
            }
            else
            {
                if (ds.Tables["pur"].Rows.Count > 0)
                {
                    string s1 = Convert.ToString(ds.Tables["pur"].Rows[0].ItemArray[0]);
                    int n = Convert.ToInt16(s1.Length.ToString());
                    s1 = s1.Substring(1, n - 1);
                    int m = 1 + Convert.ToInt16(s1);
                    TextBox1.Text = "P" + m.ToString();
                }
            }

        }*/

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            DateTime dt1 = DateTime.Now;
            DateTime dt2 = Convert.ToDateTime(Calendar1.SelectedDate.ToString());
            DateTime dt3 = DateTime.Now.AddDays(15);
            if (dt2 <= dt1)
            {
                MessageBox.Show("we can't select date");
            }
            else if (dt2 > dt3)
            {
                MessageBox.Show("due date must be within 15 days");
            }
            else
            {
                TextBox7.Text = Calendar1.SelectedDate.ToShortDateString();
            }
            if (TextBox7.Text == "")
            {
                Calendar1.Visible = true;
            }
            else
            {
                Calendar1.Visible = false;
            }

        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Calendar1.Visible = true;
        }

        protected void TextBox6_TextChanged(object sender, EventArgs e)
        {
            c = new connect();
            ds = new DataSet();
            if ((DropDownList2.SelectedIndex) == 1)
            {
                c.cmd.CommandText = "select item_id from stock where category='" +DropDownList2.SelectedItem.Text + "'and brand='" + TextBox9.Text + "'and color='" +TextBox10.Text + "'and amt='" + TextBox6.Text + "'";
                adp.SelectCommand = c.cmd;
                adp.Fill(ds, "stk");
                if (ds.Tables["stk"].Rows.Count > 0)
                {
                    TextBox3.Text = Convert.ToString(ds.Tables["stk"].Rows[0].ItemArray[0]);
                }
                else
                {
                    c.cmd.CommandText = "select count(item_id) from stock where category='" +DropDownList2.SelectedItem.Text + "'";
                    int count = Convert.ToInt16(c.cmd.ExecuteScalar()) + 1;
                    TextBox3.Text = "SR11" + count.ToString();
                }
            }
            else if ((DropDownList2.SelectedIndex) == 2)
            {
                c.cmd.CommandText = "select item_id from stock where category='" +DropDownList2.SelectedItem.Text + "'and brand='" + TextBox9.Text + "'and color='" +TextBox10.Text + "'and amt='" + TextBox6.Text + "'";
                adp.SelectCommand = c.cmd;
                adp.Fill(ds, "stk");
                if (ds.Tables["stk"].Rows.Count > 0)
                {
                    TextBox3.Text = Convert.ToString(ds.Tables["stk"].Rows[0].ItemArray[0]);
                }
                else
                {
                    c.cmd.CommandText = "select count(item_id) from stock where category='" +DropDownList2.SelectedItem.Text + "'";
                    int count = Convert.ToInt16(c.cmd.ExecuteScalar()) + 1;
                    TextBox3.Text = "KU30" + count.ToString();
                }
            }
            else if ((DropDownList2.SelectedIndex) == 3)
            {
                c.cmd.CommandText = "select item_id from stock where category='" + DropDownList2.SelectedItem.Text + "'and brand='" + TextBox9.Text + "'and color='" + TextBox10.Text + "'and amt='" + TextBox6.Text + "'";
                adp.SelectCommand = c.cmd;
                adp.Fill(ds, "stk");
                if (ds.Tables["stk"].Rows.Count > 0)
                {
                    TextBox3.Text = Convert.ToString(ds.Tables["stk"].Rows[0].ItemArray[0]);
                }
                else
                {
                    c.cmd.CommandText = "select count(item_id) from stock where category='" +DropDownList2.SelectedItem.Text + "'";
                    int count = Convert.ToInt16(c.cmd.ExecuteScalar()) + 1;
                    TextBox3.Text = "SH50" + count.ToString();
                }
            }
            else if ((DropDownList2.SelectedIndex) == 4)
            {
                c.cmd.CommandText = "select item_id from stock where category='" +DropDownList2.SelectedItem.Text + "'and brand='" + TextBox9.Text + "'and color='" +TextBox10.Text + "'and amt='" + TextBox6.Text + "'";
                adp.SelectCommand = c.cmd;
                adp.Fill(ds, "stk");
                if (ds.Tables["stk"].Rows.Count > 0)
                {
                    TextBox3.Text = Convert.ToString(ds.Tables["stk"].Rows[0].ItemArray[0]);
                }
                else
                {
                    c.cmd.CommandText = "select count(item_id) from stock where category='" +DropDownList2.SelectedItem.Text + "'";
                    int count = Convert.ToInt16(c.cmd.ExecuteScalar()) + 1;
                    TextBox3.Text = "JE70" + count.ToString();
                }
            }
            else if ((DropDownList2.SelectedIndex) == 5)
            {
                c.cmd.CommandText = "select item_id from stock where category='" +DropDownList2.SelectedItem.Text + "'and brand='" + TextBox9.Text + "'and color='" +TextBox10.Text + "'and amt='" + TextBox6.Text + "'";
                adp.SelectCommand = c.cmd;
                adp.Fill(ds, "stk");
                if (ds.Tables["stk"].Rows.Count > 0)
                {
                    TextBox3.Text = Convert.ToString(ds.Tables["stk"].Rows[0].ItemArray[0]);
                }
                else
                {
                    c.cmd.CommandText = "select count(item_id) from stock where category='" +DropDownList2.SelectedItem.Text + "'";
                    int count = Convert.ToInt16(c.cmd.ExecuteScalar()) + 1;
                    TextBox3.Text = "TR80" + count.ToString();
                }
            }
            else if ((DropDownList2.SelectedIndex) == 6)
            {
                c.cmd.CommandText = "select item_id from stock where category='" + DropDownList2.SelectedItem.Text + "'and brand='" + TextBox9.Text + "'and color='" +TextBox10.Text + "'and amt='" + TextBox6.Text + "'";
                adp.SelectCommand = c.cmd;
                adp.Fill(ds, "stk");
                if (ds.Tables["stk"].Rows.Count > 0)
                {
                    TextBox3.Text = Convert.ToString(ds.Tables["stk"].Rows[0].ItemArray[0]);
                }
                else
                {
                    c.cmd.CommandText = "select count(item_id) from stock where category='" + DropDownList2.SelectedItem.Text + "'";
                    int count = Convert.ToInt16(c.cmd.ExecuteScalar()) + 1;
                    TextBox3.Text = "BA90" + count.ToString();
                }
            }
            else if ((DropDownList2.SelectedIndex) == 7)
            {
                c.cmd.CommandText = "select item_id from stock where category='" + DropDownList2.SelectedItem.Text + "'and brand='" + TextBox9.Text + "'and color='" +TextBox10.Text + "'and amt='" + TextBox6.Text + "'";
                adp.SelectCommand = c.cmd;
                adp.Fill(ds, "stk");
                if (ds.Tables["stk"].Rows.Count > 0)
                {
                    TextBox3.Text = Convert.ToString(ds.Tables["stk"].Rows[0].ItemArray[0]);
                }
                else
                {
                    c.cmd.CommandText = "select count(item_id) from stock where category='" + DropDownList2.SelectedItem.Text + "'";
                    int count = Convert.ToInt16(c.cmd.ExecuteScalar()) + 1;
                    TextBox3.Text = "GI100" + count.ToString();
                }
            }
            else if ((DropDownList2.SelectedIndex) == 8)
            {
                c.cmd.CommandText = "select item_id from stock where category='" +DropDownList2.SelectedItem.Text + "'and brand='" + TextBox9.Text + "'and color='" +TextBox10.Text + "'and amt='" + TextBox6.Text + "'";
                adp.SelectCommand = c.cmd;
                adp.Fill(ds, "stk");
                if (ds.Tables["stk"].Rows.Count > 0)
                {
                    TextBox3.Text = Convert.ToString(ds.Tables["stk"].Rows[0].ItemArray[0]);
                }
                else
                {
                    c.cmd.CommandText = "select count(item_id) from stock where category='" +DropDownList2.SelectedItem.Text + "'";
                    int count = Convert.ToInt16(c.cmd.ExecuteScalar()) + 1;
                    TextBox3.Text = "BO110" + count.ToString();
                }
            }
            else if ((DropDownList2.SelectedIndex) == 9)
            {
                c.cmd.CommandText = "select item_id from stock where category='" +DropDownList2.SelectedItem.Text + "'and brand='" + TextBox9.Text + "'and color='" +TextBox10.Text + "'and amt='" + TextBox6.Text + "'";
                adp.SelectCommand = c.cmd;
                adp.Fill(ds, "stk");
                if (ds.Tables["stk"].Rows.Count > 0)
                {
                    TextBox3.Text = Convert.ToString(ds.Tables["stk"].Rows[0].ItemArray[0]);
                }
                else
                {
                    c.cmd.CommandText = "select count(item_id) from stock where category='" +DropDownList2.SelectedItem.Text + "'";
                    int count = Convert.ToInt16(c.cmd.ExecuteScalar()) + 1;
                    TextBox3.Text = "TS130" + count.ToString();
                }
            }

        }

        protected void TextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/pbill.aspx");
        }

        protected void Button4_Click1(object sender, EventArgs e)
        {
            Button1.Visible = true;
            Button4.Visible = false;
            c = new connect();
            ds = new DataSet();
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox5.Text = "";
            TextBox6.Text = "";
            TextBox7.Text = "";
            TextBox8.Text = "";
            c.cmd.CommandText = "select max(puror_no) from purchaseorder";
            adp.SelectCommand = c.cmd;
            adp.Fill(ds, "pur");
            if (Convert.ToString(ds.Tables["pur"].Rows[0].ItemArray[0]) == "")
            {
                TextBox1.Text = "P1000";
            }
            else
            {
                if (ds.Tables["pur"].Rows.Count > 0)
                {
                    string s1 = Convert.ToString(ds.Tables["pur"].Rows[0].ItemArray[0]);
                    int n = Convert.ToInt16(s1.Length.ToString());
                    s1 = s1.Substring(1, n - 1);
                    int m = 1 + Convert.ToInt16(s1);
                    TextBox1.Text = "P" + m.ToString();
                }
            }

        }
    }
}
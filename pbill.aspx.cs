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
    public partial class pbill : System.Web.UI.Page
    {
        connect c;
        DataSet ds, ds1;
        SqlDataAdapter adp = new SqlDataAdapter();
        decimal p, tp, gt;
        int q;
        decimal d, e, f;
        
        protected void Page_Load(object sender, EventArgs e)
        {
           // Button1.Visible = true;
            //Button4.Visible = false;
            c = new connect();
            ds = new DataSet();
            //TextBox2.Text = "";
           // TextBox3.Text = "";
           // TextBox4.Text = "";
            //TextBox5.Text = "";
            //TextBox6.Text = "";
            //TextBox7.Text = "";
            //TextBox8.Text = "";
            c.cmd.CommandText = "select max(bill_no) from purbill";
            adp.SelectCommand = c.cmd;
            adp.Fill(ds, "pur");
            if (Convert.ToString(ds.Tables["pur"].Rows[0].ItemArray[0]) == "")
            {
                TextBox1.Text = "B100";
            }
            else
            {
                if (ds.Tables["pur"].Rows.Count > 0)
                {
                    string s1 = Convert.ToString(ds.Tables["pur"].Rows[0].ItemArray[0]);
                    int n = Convert.ToInt16(s1.Length.ToString());
                    s1 = s1.Substring(1, n - 1);
                    int m = 1 + Convert.ToInt16(s1);
                    TextBox1.Text = "B" + m.ToString();
                }
            }

            TextBox10.Enabled = false;
            TextBox11.Enabled = false;
            TextBox12.Enabled = false;
            TextBox13.Enabled = false;
            TextBox14.Enabled = false;
            TextBox15.Enabled = false;
            TextBox2.Enabled = false;
            TextBox3.Enabled = false;
            TextBox4.Enabled = false;
            TextBox5.Enabled = false;
            TextBox6.Enabled = false;
            TextBox7.Enabled = false;
            TextBox8.Enabled = false;
            TextBox9.Enabled = false;
            c = new connect();
            ds = new DataSet();
            Button1.Visible = true;
            TextBox2.Text = DateTime.Now.ToShortDateString();
            TextBox1.Focus();
            try
            {
                if (DropDownList2.Items.Count == 0)
                {
                    c.cmd.CommandText = "select distinct(puror_no) from  purchaseorder where Status = 'incomplete'";
                    adp.SelectCommand = c.cmd;
                    adp.Fill(ds, "pur");
                    if (ds.Tables["pur"].Rows.Count > 0)
                    {
                        DropDownList2.Items.Add("--Select--");
                        for (int i = 0; i < ds.Tables["pur"].Rows.Count; i++)
                        {
                            DropDownList2.Items.Add(ds.Tables["pur"].Rows[i].ItemArray[0].ToString());
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
        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            c = new connect();
            ds = new DataSet();
            ds1 = new DataSet();
            if (DropDownList1.Items.Count == 0)
            {
                c.cmd.CommandText = "select item_id from purchaseorder where puror_no = '" + DropDownList2.SelectedItem.Text + "'and Status = 'incomplete'";
                adp.SelectCommand = c.cmd;
                adp.Fill(ds1, "pur");
                if (ds1.Tables["pur"].Rows.Count > 0)
                {
                    DropDownList1.Items.Add("--Select--");
                    for (int i = 0; i < ds1.Tables["pur"].Rows.Count;i++)
                    {

                        DropDownList1.Items.Add(ds1.Tables["pur"].Rows[i].ItemArray[0].ToString());
                    }
                }
            }
            else
            {

                DropDownList1.Items.Clear();
                TextBox5.Text = "";
                TextBox8.Text = "";
                TextBox9.Text = "";
                TextBox10.Text = "";
                TextBox11.Text = "";
                TextBox12.Text = "";
                TextBox3.Text = "";
                if (DropDownList1.Items.Count == 0)

                {
                    c.cmd.CommandText = "select item_id from purchaseorder where puror_no = '" + DropDownList2.SelectedItem.Text + "'and Status='incomplete'";
                    adp.SelectCommand = c.cmd;
                    adp.Fill(ds1, "pur");
                    if (ds1.Tables["pur"].Rows.Count > 0)
                    {
                        DropDownList1.Items.Add("--Select--");
                        for (int i = 0; i < ds1.Tables["pur"].Rows.Count;i++)
                        {

                            DropDownList1.Items.Add(ds1.Tables["pur"].Rows[i].ItemArray[0].ToString());
                        }
                    }
                }
            }
            c.cmd.CommandText = "select * from purchaseorder where puror_no = '" + DropDownList2.SelectedItem.ToString() + "'";
            adp.SelectCommand = c.cmd;
            adp.Fill(ds, "pur");
            if (ds.Tables["pur"].Rows.Count > 0)
            {
                TextBox4.Text =Convert.ToString(ds.Tables["pur"].Rows[0].ItemArray[12]);
                TextBox6.Text =Convert.ToString(ds.Tables["pur"].Rows[0].ItemArray[3]);
                TextBox7.Text =Convert.ToString(ds.Tables["pur"].Rows[0].ItemArray[2]);
            }
            else
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "msgbox","<script>alert('no records')</script>");
            }
        }

       
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            c = new connect();
            ds = new DataSet();
            c.cmd.CommandText = "select * from purchaseorder where puror_no = '" + DropDownList2.SelectedItem.ToString() + "'and item_id = '" + DropDownList1.SelectedItem.ToString() + "'";
            adp.SelectCommand = c.cmd;
            adp.Fill(ds, "pur");
            if (ds.Tables["pur"].Rows.Count > 0)
            {
                TextBox3.Text = Convert.ToString(ds.Tables["pur"].Rows[0].ItemArray[11]);
                TextBox5.Text = Convert.ToString(ds.Tables["pur"].Rows[0].ItemArray[5]);
                TextBox8.Text =Convert.ToString(ds.Tables["pur"].Rows[0].ItemArray[6]);
                TextBox9.Text =Convert.ToString(ds.Tables["pur"].Rows[0].ItemArray[7]);
                TextBox10.Text =Convert.ToString(ds.Tables["pur"].Rows[0].ItemArray[8]);
                TextBox11.Text =Convert.ToString(ds.Tables["pur"].Rows[0].ItemArray[10]);
                TextBox12.Text =Convert.ToString(ds.Tables["pur"].Rows[0].ItemArray[9]);
                TextBox13.Text =Convert.ToString(ds.Tables["pur"].Rows[0].ItemArray[15]);
            }
            p = Convert.ToDecimal(TextBox13.Text);
            q = Convert.ToInt16(TextBox12.Text);
            tp = Convert.ToDecimal(p * q);
            TextBox14.Text = Convert.ToString(tp);
            d = d+tp;

            
            //f = Convert.ToInt32(d + e);
            gt = Convert.ToDecimal(TextBox15.Text+TextBox14.Text);
            TextBox15.Text = Convert.ToString(d);
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            d = tp;
            c = new connect();
            ds = new DataSet();
            int qty3, qty4;
            c.cmd.CommandText = "insert into purbill values(@bill_no, @bill_date, @puror_no, @or_date, @supid, @name, @item_id,@item_name, @category, @cat_id, @brand, @color, @size, @quantity, @price, @ttl, @gttl)";
            c.cmd.Parameters.Clear();
            if (TextBox1.Text != "" && TextBox2.Text != "" &&
           TextBox4.Text != "" && TextBox5.Text != "" && TextBox7.Text != "" &&
           TextBox8.Text != "" && TextBox9.Text != "" && TextBox12.Text != "" &&
           TextBox13.Text != "" && TextBox14.Text != "" && TextBox3.Text != "")
            {
                c.cmd.Parameters.Add("@bill_no", SqlDbType.NVarChar).Value = TextBox1.Text;
                c.cmd.Parameters.Add("@bill_date", SqlDbType.DateTime).Value = TextBox2.Text;
                c.cmd.Parameters.Add("@puror_no", SqlDbType.NVarChar).Value = DropDownList2.SelectedItem.ToString();
                c.cmd.Parameters.Add("@or_date", SqlDbType.DateTime).Value = TextBox4.Text;
                c.cmd.Parameters.Add("@supid", SqlDbType.NVarChar).Value = TextBox6.Text;
                c.cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = TextBox7.Text;
                c.cmd.Parameters.Add("@item_id",SqlDbType.NVarChar).Value = DropDownList1.SelectedItem.ToString();
                c.cmd.Parameters.Add("@item_name", SqlDbType.NVarChar).Value = TextBox5.Text;
                c.cmd.Parameters.Add("@category",SqlDbType.NVarChar).Value = TextBox8.Text;
                c.cmd.Parameters.Add("@cat_id", SqlDbType.NVarChar).Value = TextBox9.Text;
                c.cmd.Parameters.Add("@brand", SqlDbType.NVarChar).Value = TextBox10.Text;
                c.cmd.Parameters.Add("@size", SqlDbType.NVarChar).Value = TextBox3.Text;
                c.cmd.Parameters.Add("@color", SqlDbType.NVarChar).Value = TextBox11.Text;
                c.cmd.Parameters.Add("@quantity", SqlDbType.SmallInt).Value = Convert.ToInt16(TextBox12.Text);
                c.cmd.Parameters.Add("@price", SqlDbType.Decimal).Value = Convert.ToDecimal(TextBox13.Text);
                c.cmd.Parameters.Add("@ttl", SqlDbType.Decimal).Value =Convert.ToString(tp);
                c.cmd.Parameters.Add("@gttl", SqlDbType.Decimal).Value =Convert.ToDecimal(TextBox15.Text);
                c.cmd.ExecuteNonQuery();
                c.cmd.CommandText = "select qty from stock where item_id = '" + DropDownList1.SelectedItem.ToString() + "' and amt = '" + TextBox13.Text + "'";
                adp.SelectCommand = c.cmd;
                adp.Fill(ds, "stk");
                if (ds.Tables["stk"].Rows.Count > 0)
                {
                    qty3 =Convert.ToInt16(ds.Tables["stk"].Rows[0].ItemArray[0]);
                    qty4 = qty3 + Convert.ToInt16(TextBox12.Text);
                    c.cmd.CommandText = "Update stock set qty= @qty where item_id = '" + DropDownList1.SelectedItem.ToString() + "'";
                    c.cmd.Parameters.Add("@qty", SqlDbType.SmallInt).Value = qty4;
                    c.cmd.ExecuteNonQuery();
                }
                else
                {
                    c.cmd.CommandText = "insert into stock values(@item_id, @it_name, @category, @cat_id, @brand, @color, @qty, @amt, @reorder)";
                    c.cmd.Parameters.Clear();
                    if (TextBox1.Text != "" && TextBox2.Text != "" &&
                   TextBox4.Text != "" && TextBox5.Text != "" && TextBox7.Text != "" &&
                   TextBox8.Text != "" && TextBox9.Text != "" && TextBox12.Text != "" &&
                   TextBox13.Text != "" && TextBox14.Text != "" && TextBox3.Text != "")
                    {
                        c.cmd.Parameters.Add("@item_id",SqlDbType.NVarChar).Value = DropDownList1.SelectedItem.ToString();
                        c.cmd.Parameters.Add("@it_name",SqlDbType.NVarChar).Value = TextBox5.Text;
                        c.cmd.Parameters.Add("@category",SqlDbType.NVarChar).Value = TextBox8.Text;
                        c.cmd.Parameters.Add("@cat_id",SqlDbType.NVarChar).Value = TextBox9.Text;
                        c.cmd.Parameters.Add("@brand",SqlDbType.NVarChar).Value = TextBox10.Text;
                        c.cmd.Parameters.Add("@color",SqlDbType.NVarChar).Value = TextBox11.Text;
                        c.cmd.Parameters.Add("@qty",SqlDbType.SmallInt).Value = Convert.ToInt16(TextBox12.Text);
                        c.cmd.Parameters.Add("@amt",SqlDbType.SmallInt).Value = Convert.ToInt16(TextBox13.Text);
                        c.cmd.Parameters.Add("@reorder",SqlDbType.BigInt).Value = "10";
                        c.cmd.ExecuteNonQuery();
                    }
                }
                c.cmd.CommandText = "select Status from purchaseorder  where item_id = '" + DropDownList1.SelectedItem.ToString() + "'";
                adp.SelectCommand = c.cmd;
                adp.Fill(ds, "pur");
                if (ds.Tables["pur"].Rows.Count > 0)
                {
                    c.cmd.CommandText = "Update purchaseorder set Status = @Status where item_id = '" +DropDownList1.SelectedItem.ToString() + "'";
                    c.cmd.Parameters.Add("@Status",SqlDbType.NVarChar).Value = "completed";
                    c.cmd.ExecuteNonQuery();
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(),"msgbox", "<script>alert('Enter all fields')</script>");
            }
            if ((DropDownList2.SelectedIndex) <= 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(),"msgbox", "<script>alert('Enter all fields')</script>");
            }
            else
            {
                c = new connect();
                ds = new DataSet();
                c.cmd.CommandText = "select item_id from purchaseorder where puror_no = '" + DropDownList2.SelectedItem.ToString() + "' and Status = 'incomplete'";
                adp.SelectCommand = c.cmd;
                adp.Fill(ds, "pur");
                if (ds.Tables["pur"].Rows.Count > 0)
                {
                    TextBox3.Text = "";

                    TextBox5.Text = "";
                    TextBox8.Text = "";
                    TextBox9.Text = "";
                    TextBox10.Text = "";
                    TextBox11.Text = "";
                    TextBox12.Text = "";
                    TextBox13.Text = "";
                    TextBox14.Text = "";
                    DropDownList1.SelectedItem.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Bill is done");
                    Response.Redirect("~/pbill.aspx");
                }
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/home.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/purchase.aspx");
        }

        protected void TextBox13_TextChanged(object sender, EventArgs e)
        {
          /*  p = Convert.ToDecimal(TextBox13.Text);
            q = Convert.ToInt16(TextBox12.Text);
            tp = Convert.ToDecimal(p * q);
            TextBox14.Text = Convert.ToString(tp);
            gt = Convert.ToInt32(TextBox14.Text) + Convert.ToInt32(TextBox15.Text); */
               


           
            

        }
    }
}





/*p = Convert.ToDecimal(TextBox13.Text);
        q = Convert.ToInt16(TextBox12.Text);
        tp = Convert.ToDecimal(p * q);
        TextBox14.Text = Convert.ToString(tp);
        gt = Convert.ToDecimal(TextBox15.Text + TextBox14.Text);
        TextBox15.Text = Convert.ToString(gt);*/

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
    public partial class ladies : System.Web.UI.Page
    {
        connect c;
        DataSet ds;
        SqlDataAdapter adp = new SqlDataAdapter();
        protected void Page_Load(object sender, EventArgs e)
        {
            TextBox3.Focus();
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            c = new connect();
            if (Convert.ToString(DropDownList1.SelectedIndex) == "0")
            {
                TextBox1.Text = "";
                TextBox4.Text = "";
                TextBox2.Text = "";
            }
            else if (Convert.ToString(DropDownList1.SelectedIndex) == "1")
            {
                c.cmd.CommandText = "select count(item_id) from stock where category = '" + DropDownList1.SelectedItem.Text + "'";
            int count = Convert.ToInt16(c.cmd.ExecuteScalar()) + 1;
                TextBox1.Text = "SR11" + count.ToString();
                
                   // TextBox4.Text = "Saree";
                    TextBox2.Text = "SR101";
               
            }
            else if (Convert.ToString(DropDownList1.SelectedIndex) == "2")
            {
                c.cmd.CommandText = "select count(item_id) from stock where category = '" + DropDownList1.SelectedItem.Text + "'";
                int count = Convert.ToInt16(c.cmd.ExecuteScalar()) + 1;
                TextBox1.Text = "KU111" + count.ToString();
               // TextBox4.Text = "kurthis";
                TextBox2.Text = "KU101";
            }
            else if (Convert.ToString(DropDownList1.SelectedIndex) == "3")
            {
                c.cmd.CommandText = "select count(item_id) from stock where category = '" + DropDownList1.SelectedItem.Text + "'";
                int count = Convert.ToInt16(c.cmd.ExecuteScalar()) + 1;
                TextBox1.Text = "JE311" + count.ToString();
               // TextBox4.Text = "jeans";
                TextBox2.Text = "JE101";
            }
            else if (Convert.ToString(DropDownList1.SelectedIndex) == "4")
            {
                c.cmd.CommandText = "select count(item_id) from stock where category = '" + DropDownList1.SelectedItem.Text + "'";
                int count = Convert.ToInt16(c.cmd.ExecuteScalar()) + 1;
                TextBox1.Text = "TS811" + count.ToString();
               // TextBox4.Text = "t-shirt";
                TextBox2.Text = "TS101";
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                c = new connect();
                ds = new DataSet();
                c.cmd.CommandText = "insert into stock values(@item_id, @it_name, @category, @cat_id, @brand, @color, @qty, @amt, @reorder)";
                c.cmd.Parameters.Clear();
                if (TextBox1.Text != "" && TextBox2.Text != "" &&
               TextBox3.Text != "" && TextBox4.Text != "" && TextBox6.Text != "" &&
               TextBox5.Text != "")
                {
                    c.cmd.Parameters.Add("@item_id",
                   SqlDbType.NVarChar).Value = TextBox1.Text;
                    c.cmd.Parameters.Add("@it_name",
                   SqlDbType.NVarChar).Value = TextBox4.Text;
                    c.cmd.Parameters.Add("@category",
                   SqlDbType.NVarChar).Value = DropDownList1.SelectedItem.ToString();
                    c.cmd.Parameters.Add("@cat_id",
                   SqlDbType.NVarChar).Value = TextBox2.Text;
                    c.cmd.Parameters.Add("@brand",
                   SqlDbType.NVarChar).Value = DropDownList3.SelectedItem.ToString();
                    c.cmd.Parameters.Add("@color",
                   SqlDbType.NVarChar).Value = DropDownList2.SelectedItem.ToString();
                    c.cmd.Parameters.Add("@qty",
                   SqlDbType.SmallInt).Value = Convert.ToInt16(TextBox3.Text);
                    c.cmd.Parameters.Add("@amt", SqlDbType.BigInt).Value= Convert.ToInt32(TextBox6.Text);
                    c.cmd.Parameters.Add("@reorder",
                   SqlDbType.BigInt).Value = Convert.ToInt32(TextBox5.Text);
                    c.cmd.ExecuteNonQuery();

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "msgbox", "<script>alert('Record saved')</script>");
                }
                else
                {

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "msgbox","<script>alert('Enter all fields')</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
            }

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox6.Text = "";
            DropDownList1.SelectedIndex = 0;
            DropDownList2.SelectedIndex = 0;
            DropDownList3.SelectedIndex = 0;
        }

       /* protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBox6.Text = DropDownList3.SelectedValue.ToString();
        }*/

        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/stock.aspx");
        }
    }
}
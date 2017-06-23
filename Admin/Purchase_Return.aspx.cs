#region " Using "
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.Security;
using Microsoft.Reporting.WebForms;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
#endregion


public partial class Admin_Purchase_Return : System.Web.UI.Page
{
    public static int company_id = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneInfo);
            DateTime date = now;

            TextBox3.Attributes.Add("onkeypress", "return controlEnter('" + TextBox2.ClientID + "', event)");
            TextBox2.Attributes.Add("onkeypress", "return controlEnter('" + TextBox4.ClientID + "', event)");
            TextBox4.Attributes.Add("onkeypress", "return controlEnter('" + TextBox11.ClientID + "', event)");
            TextBox11.Attributes.Add("onkeypress", "return controlEnter('" + TextBox12.ClientID + "', event)");
            TextBox12.Attributes.Add("onkeypress", "return controlEnter('" + TextBox13.ClientID + "', event)");
        
            getinvoiceno();
            getService_no();
            getUnit_no();
            //show_category();
            showrating();
            BindData();
            show_product();
            show_MaterialName();
            show_SupplierName();
            show_Unit();
            active();
            created();
            Button12.Visible = false;
            Button1.Visible = true;
            if (User.Identity.IsAuthenticated)
            {
                SqlConnection con1000 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd1000 = new SqlCommand("select * from user_details where company_name='" + User.Identity.Name + "'", con1000);
                SqlDataReader dr1000;
                con1000.Open();
                dr1000 = cmd1000.ExecuteReader();
                if (dr1000.Read())
                {
                    company_id = Convert.ToInt32(dr1000["com_id"].ToString());

                }
                con1000.Close();
            }

        }

    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Button12.Visible = true;
        Button1.Visible = false;
        ImageButton IMG = (ImageButton)sender;
        GridViewRow ROW = (GridViewRow)IMG.NamingContainer;
        
        if (User.Identity.IsAuthenticated)
        {
            SqlConnection con1000 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd1000 = new SqlCommand("select * from user_details where company_name='" + User.Identity.Name + "'", con1000);
            SqlDataReader dr1000;
            con1000.Open();
            dr1000 = cmd1000.ExecuteReader();
            if (dr1000.Read())
            {
                company_id = Convert.ToInt32(dr1000["com_id"].ToString());

            }
            con1000.Close();
        }


        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("select * from Purchase_Return where Pur_Return_id='" + ROW.Cells[0].Text + "' and Com_Id='" + company_id + "'", con);
        SqlDataReader dr;
        con.Open();
        dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            Label1.Text = dr["Pur_Return_id"].ToString();
            TextBox3.Text = dr["Material_Name"].ToString();
            TextBox19.Text = dr["Item_Code"].ToString();
            DropDownList1.SelectedItem.Text = dr["Supplier_Name"].ToString();
            TextBox2.Text = dr["Supplier_Address"].ToString();
            TextBox4.Text = dr["Mobile_no"].ToString();
            TextBox12.Text = dr["Order_Qty"].ToString();
            DropDownList4.SelectedItem.Text = dr["units"].ToString();
            TextBox18.Text = dr["Rolls"].ToString();
            TextBox20.Text = dr["Boxes"].ToString();
            TextBox21.Text = dr["Shipment"].ToString();
            TextBox22.Text = dr["Indent_no"].ToString();
            TextBox11.Text = Convert.ToDateTime(dr["Req_Date"]).ToString("dd-MM-yyyy");
            TextBox13.Text = dr["Payment_mode"].ToString();
            TextBox3.Focus();
        }
        con.Close();

    }
    protected void Button12_Click(object sender, EventArgs e)
    {
        if (User.Identity.IsAuthenticated)
        {
            SqlConnection con1000 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd1000 = new SqlCommand("select * from user_details where company_name='" + User.Identity.Name + "'", con1000);
            SqlDataReader dr1000;
            con1000.Open();
            dr1000 = cmd1000.ExecuteReader();
            if (dr1000.Read())
            {
                company_id = Convert.ToInt32(dr1000["com_id"].ToString());

                SqlConnection CON = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd = new SqlCommand("update Purchase_Return set Material_Name='" + HttpUtility.HtmlDecode(TextBox3.Text) +
                    "', Item_Code='" + HttpUtility.HtmlDecode(TextBox19.Text) +
                    "',Supplier_Name='" + HttpUtility.HtmlDecode(DropDownList1.SelectedItem.Text) +
                    "',Supplier_Address='" + HttpUtility.HtmlDecode(TextBox2.Text) +
                    "',Mobile_no='" + HttpUtility.HtmlDecode(TextBox4.Text) +
                    "',Order_Qty='" + HttpUtility.HtmlDecode(TextBox12.Text) +
                    "',units='" + HttpUtility.HtmlDecode(DropDownList4.SelectedItem.Text) +
                    "',Rolls='" + HttpUtility.HtmlDecode(TextBox18.Text) +
                       "',Boxes='" + HttpUtility.HtmlDecode(TextBox20.Text) +
                       "',Shipment='" + HttpUtility.HtmlDecode(TextBox21.Text) +
                       "',Indent_no='" + HttpUtility.HtmlDecode(TextBox22.Text) +
                         "',Req_Date='" + HttpUtility.HtmlDecode(Convert.ToDateTime(TextBox11.Text).ToString("MM-dd-yyyy")) +
                         "',Payment_mode='" + HttpUtility.HtmlDecode(TextBox13.Text) +
                    "' where Pur_Return_id='" + Label1.Text + "'  and Com_Id='" + company_id + "' ", CON);

                CON.Open();
                cmd.ExecuteNonQuery();
                CON.Close();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Purchase Return Updated successfully')", true);
                TextBox3.Text = "";
                TextBox2.Text = "";
                TextBox4.Text = "";
                TextBox11.Text = "";
                TextBox12.Text = "";
                TextBox13.Text = "";
                TextBox19.Text = "";
                TextBox18.Text = "";
                TextBox20.Text = "";
                TextBox21.Text = "";
                TextBox22.Text = "";
                BindData();
                getinvoiceno();
                show_product();
                show_MaterialName();
                show_Unit();
                show_SupplierName();
                Button12.Visible = false;
                Button1.Visible = true;
            }
            con1000.Close();
        }
    }

    protected void Button14_Click(object sender, EventArgs e)
    {
        if (User.Identity.IsAuthenticated)
        {
            SqlConnection con1000 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd1000 = new SqlCommand("select * from user_details where company_name='" + User.Identity.Name + "'", con1000);
            SqlDataReader dr1000;
            con1000.Open();
            dr1000 = cmd1000.ExecuteReader();
            if (dr1000.Read())
            {
                company_id = Convert.ToInt32(dr1000["com_id"].ToString());

                foreach (GridViewRow gvrow in GridView1.Rows)
                {
                    //Finiding checkbox control in gridview for particular row
                    CheckBox chkdelete = (CheckBox)gvrow.FindControl("CheckBox3");
                    //Condition to check checkbox selected or not
                    if (chkdelete.Checked)
                    {
                        //Getting UserId of particular row using datakey value
                        int usrid = Convert.ToInt32(gvrow.Cells[1].Text);
                        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);

                        con.Open();
                        SqlCommand cmd = new SqlCommand("delete from Purchase_Return where Pur_Return_id='" + usrid + "' and Com_Id='" + company_id + "'", con);
                        cmd.ExecuteNonQuery();
                        con.Close();

                    }
                }
                BindData();
                getinvoiceno();
            }
            con1000.Close();
        }
    }
    private void show_product()
    {
        if (User.Identity.IsAuthenticated)
        {
            SqlConnection con1000 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd1000 = new SqlCommand("select * from user_details where company_name='" + User.Identity.Name + "'", con1000);
            SqlDataReader dr1000;
            con1000.Open();
            dr1000 = cmd1000.ExecuteReader();
            if (dr1000.Read())
            {
                company_id = Convert.ToInt32(dr1000["com_id"].ToString());

                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd = new SqlCommand("Select * from Vendor where Com_Id='" + company_id + "' ORDER BY Vendor_Code asc", con);
                con.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);


                DropDownList1.DataSource = ds;
                DropDownList1.DataTextField = "Vendor_Name";
                DropDownList1.DataValueField = "Vendor_Code";
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0, new ListItem("All", "0"));

                con.Close();
            }
            con1000.Close();
        }
    }
    private void show_MaterialName()
    {
        if (User.Identity.IsAuthenticated)
        {
            SqlConnection con1000 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd1000 = new SqlCommand("select * from user_details where company_name='" + User.Identity.Name + "'", con1000);
            SqlDataReader dr1000;
            con1000.Open();
            dr1000 = cmd1000.ExecuteReader();
            if (dr1000.Read())
            {
                company_id = Convert.ToInt32(dr1000["com_id"].ToString());

                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd = new SqlCommand("Select * from ADD_Material_Name where Com_Id='" + company_id + "' ORDER BY Mat_Name_id asc", con);
                con.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                DropDownList5.DataSource = ds;
                DropDownList5.DataTextField = "Material_Name";
                DropDownList5.DataValueField = "Mat_Name_id";
                DropDownList5.DataBind();
                DropDownList5.Items.Insert(0, new ListItem("All", "0"));


                con.Close();
            }
            con1000.Close();
        }
    }
    private void show_Unit()
    {
        if (User.Identity.IsAuthenticated)
        {
            SqlConnection con1000 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd1000 = new SqlCommand("select * from user_details where company_name='" + User.Identity.Name + "'", con1000);
            SqlDataReader dr1000;
            con1000.Open();
            dr1000 = cmd1000.ExecuteReader();
            if (dr1000.Read())
            {
                company_id = Convert.ToInt32(dr1000["com_id"].ToString());

                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd = new SqlCommand("Select * from ADD_Unit where Com_Id='" + company_id + "' ORDER BY Unit_id asc", con);
                con.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                DropDownList4.DataSource = ds;
                DropDownList4.DataTextField = "Unit_Name";
                DropDownList4.DataValueField = "Unit_id";
                DropDownList4.DataBind();
                DropDownList4.Items.Insert(0, new ListItem("All", "0"));


                con.Close();
            }
            con1000.Close();
        }
    }
    private void show_SupplierName()
    {
        if (User.Identity.IsAuthenticated)
        {
            SqlConnection con1000 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd1000 = new SqlCommand("select * from user_details where company_name='" + User.Identity.Name + "'", con1000);
            SqlDataReader dr1000;
            con1000.Open();
            dr1000 = cmd1000.ExecuteReader();
            if (dr1000.Read())
            {
                company_id = Convert.ToInt32(dr1000["com_id"].ToString());

                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd = new SqlCommand("Select * from Purchase_Return where Com_Id='" + company_id + "' ORDER BY Pur_Return_id asc", con);
                con.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);


                DropDownList2.DataSource = ds;
                DropDownList2.DataTextField = "Material_Name";
                DropDownList2.DataValueField = "Pur_Return_id";
                DropDownList2.DataBind();
                DropDownList2.Items.Insert(0, new ListItem("All", "0"));

                con.Close();
            }
            con1000.Close();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (TextBox3.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please Enter Material name')", true);
        }
        else if (TextBox19.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please Enter Item Code')", true);
        }
        else if (TextBox4.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please Enter Mobile No')", true);
        }
        else
        {
            if (User.Identity.IsAuthenticated)
            {
                SqlConnection con1000 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd1000 = new SqlCommand("select * from user_details where company_name='" + User.Identity.Name + "'", con1000);
                SqlDataReader dr1000;
                con1000.Open();
                dr1000 = cmd1000.ExecuteReader();
                if (dr1000.Read())
                {
                    company_id = Convert.ToInt32(dr1000["com_id"].ToString());

                    SqlConnection CON = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd = new SqlCommand("insert into Purchase_Return values(@Pur_Return_id,@Material_Name,@Item_Code,@Supplier_Name,@Supplier_Address,@Mobile_no,@Order_Qty,@units,@Rolls,@Boxes,@Shipment,@Indent_no,@Req_Date,@Payment_mode,@Com_Id)", CON);
                    cmd.Parameters.AddWithValue("@Pur_Return_id", Label1.Text);
                    cmd.Parameters.AddWithValue("@Material_Name", HttpUtility.HtmlDecode(TextBox3.Text));
                    cmd.Parameters.AddWithValue("@Item_Code", HttpUtility.HtmlDecode(TextBox19.Text));
                    cmd.Parameters.AddWithValue("@Supplier_Name", HttpUtility.HtmlDecode(DropDownList1.SelectedItem.Text));
                    cmd.Parameters.AddWithValue("@Supplier_Address", HttpUtility.HtmlDecode(TextBox2.Text));
                    cmd.Parameters.AddWithValue("@Mobile_no", HttpUtility.HtmlDecode(TextBox4.Text));
                    cmd.Parameters.AddWithValue("@Order_Qty", HttpUtility.HtmlDecode(TextBox12.Text));
                    cmd.Parameters.AddWithValue("@units", HttpUtility.HtmlDecode(DropDownList4.SelectedItem.Text));
                    cmd.Parameters.AddWithValue("@Rolls", HttpUtility.HtmlDecode(TextBox18.Text));
                    cmd.Parameters.AddWithValue("@Boxes", HttpUtility.HtmlDecode(TextBox20.Text));
                    cmd.Parameters.AddWithValue("@Shipment", HttpUtility.HtmlDecode(TextBox21.Text));
                    cmd.Parameters.AddWithValue("@Indent_no", HttpUtility.HtmlDecode(TextBox22.Text));
                    cmd.Parameters.AddWithValue("@Req_Date", HttpUtility.HtmlDecode(Convert.ToDateTime(TextBox11.Text).ToString("MM-dd-yyyy")));
                    cmd.Parameters.AddWithValue("@Payment_mode", HttpUtility.HtmlDecode(TextBox13.Text));
                    cmd.Parameters.AddWithValue("@Com_Id", company_id);

                    CON.Open();
                    cmd.ExecuteNonQuery();
                    CON.Close();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Purchase Return Saved successfully')", true);
                    BindData();
                    show_category();
                    getinvoiceno();
                    TextBox3.Text = "";
                    TextBox2.Text = "";
                    TextBox4.Text = "";
                    TextBox11.Text = "";
                    TextBox12.Text = "";
                    TextBox13.Text = "";
                    TextBox19.Text = "";
                    TextBox18.Text = "";
                    TextBox20.Text = "";
                    TextBox21.Text = "";
                    TextBox22.Text = "";
                    show_product();
                    show_SupplierName();
                    show_Unit();
                }
                con1000.Close();
            }
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        
        TextBox3.Text = "";
        TextBox2.Text = "";
        TextBox4.Text = "";
        TextBox11.Text = "";
        TextBox12.Text = "";
        TextBox13.Text = "";
        TextBox19.Text = "";
        TextBox18.Text = "";
        TextBox20.Text = "";
        TextBox21.Text = "";
        TextBox22.Text = "";
        Button12.Visible = false;
        Button1.Visible = true;
        getinvoiceno();
        BindData();
        show_category();
        show_product();
        show_SupplierName();
        show_Unit();
        TextBox3.Focus();

    }
    private void active()
    {

    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]

    public static List<string> SearchCustomers1(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.AppSettings["connection"];

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select distinct Mobile_no from Purchase_Return where Com_Id=@Com_Id and " +
                "Mobile_no like @Mobile_no + '%'";
                cmd.Parameters.AddWithValue("@Mobile_no", prefixText);
                cmd.Parameters.AddWithValue("@Com_Id",company_id);
                cmd.Connection = conn;
                conn.Open();
                List<string> customers = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(sdr["Mobile_no"].ToString());
                    }
                }
                conn.Close();
                return customers;
            }
        }
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]

    public static List<string> SearchMaterial_Name(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.AppSettings["connection"];

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select distinct Material_name from Material_Entry where Com_Id=@Com_Id and " +
                "Material_name like @Material_name + '%'";
                cmd.Parameters.AddWithValue("@Material_name", prefixText);
                cmd.Parameters.AddWithValue("@Com_Id", company_id);
                cmd.Connection = conn;
                conn.Open();
                List<string> customers = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(sdr["Material_name"].ToString());
                    }
                }
                conn.Close();
                return customers;
            }
        }
    }

    protected void TextBox15_TextChanged(object sender, EventArgs e)
    {

        if (User.Identity.IsAuthenticated)
        {
            SqlConnection con1000 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd1000 = new SqlCommand("select * from user_details where company_name='" + User.Identity.Name + "'", con1000);
            SqlDataReader dr1000;
            con1000.Open();
            dr1000 = cmd1000.ExecuteReader();
            if (dr1000.Read())
            {
                company_id = Convert.ToInt32(dr1000["com_id"].ToString());
                SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand CMD = new SqlCommand("select * from Purchase_Return where Mobile_no='" + TextBox15.Text + "' and Com_Id='" + company_id + "' ", con1);
                DataTable dt1 = new DataTable();
                con1.Open();
                SqlDataAdapter da1 = new SqlDataAdapter(CMD);
                da1.Fill(dt1);
                GridView1.DataSource = dt1;
                GridView1.DataBind();
            }
            con1000.Close();
        }
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (User.Identity.IsAuthenticated)
        {
            SqlConnection con1000 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd1000 = new SqlCommand("select * from user_details where company_name='" + User.Identity.Name + "'", con1000);
            SqlDataReader dr1000;
            con1000.Open();
            dr1000 = cmd1000.ExecuteReader();
            if (dr1000.Read())
            {
                company_id = Convert.ToInt32(dr1000["com_id"].ToString());

                SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand CMD = new SqlCommand("select * from Purchase_Return where Material_Name='" + DropDownList2.SelectedItem.Text + "' and  Com_Id='" + company_id + "' ORDER BY Pur_Return_id asc", con1);
                DataTable dt1 = new DataTable();
                con1.Open();
                SqlDataAdapter da1 = new SqlDataAdapter(CMD);
                da1.Fill(dt1);
                GridView1.DataSource = dt1;
                GridView1.DataBind();
            }
            con1000.Close();
        }
    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
        GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;


        LinkButton Lnk = (LinkButton)sender;
        string name = Lnk.Text;
        Session["name"] = name;
        Response.Redirect("Account_show.aspx");


    }

    private void created()
    {

    }
    protected void BindData()
    {
        if (User.Identity.IsAuthenticated)
        {
            SqlConnection con1000 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd1000 = new SqlCommand("select * from user_details where company_name='" + User.Identity.Name + "'", con1000);
            SqlDataReader dr1000;
            con1000.Open();
            dr1000 = cmd1000.ExecuteReader();
            if (dr1000.Read())
            {
                company_id = Convert.ToInt32(dr1000["com_id"].ToString());
              
                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand CMD = new SqlCommand("select * from Purchase_Return where Com_Id='" + company_id + "' ORDER BY Pur_Return_id asc", con);
                DataTable dt1 = new DataTable();
                SqlDataAdapter da1 = new SqlDataAdapter(CMD);
                da1.Fill(dt1);
                GridView1.DataSource = dt1;
                GridView1.DataBind();
            }
            con1000.Close();
        }
    }
    protected void ImageButton9_Click(object sender, ImageClickEventArgs e)
    {
        if (User.Identity.IsAuthenticated)
        {
            SqlConnection con1000 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd1000 = new SqlCommand("select * from user_details where company_name='" + User.Identity.Name + "'", con1000);
            SqlDataReader dr1000;
            con1000.Open();
            dr1000 = cmd1000.ExecuteReader();
            if (dr1000.Read())
            {
                company_id = Convert.ToInt32(dr1000["com_id"].ToString());

                ImageButton img = (ImageButton)sender;
                GridViewRow row = (GridViewRow)img.NamingContainer;
                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);

                con.Open();
                SqlCommand cmd = new SqlCommand("delete from Purchase_Return where Pur_Return_id='" + row.Cells[0].Text + "' and Com_Id='" + company_id + "' ", con);
                cmd.ExecuteNonQuery();
                con.Close();

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Purchase Return Deleted Successfully')", true);

                BindData();
                show_category();
                getinvoiceno();
            }
            con1000.Close();
        }
    }

    private void getinvoiceno()
    {
        if (User.Identity.IsAuthenticated)
        {
            SqlConnection con1000 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd1000 = new SqlCommand("select * from user_details where company_name='" + User.Identity.Name + "'", con1000);
            SqlDataReader dr1000;
            con1000.Open();
            dr1000 = cmd1000.ExecuteReader();
            if (dr1000.Read())
            {
                company_id = Convert.ToInt32(dr1000["com_id"].ToString());

                int a;

                SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                con1.Open();
                string query = "Select max(Pur_Return_id) from Purchase_Return where Com_Id='" + company_id + "'";
                SqlCommand cmd1 = new SqlCommand(query, con1);
                SqlDataReader dr = cmd1.ExecuteReader();
                if (dr.Read())
                {
                    string val = dr[0].ToString();
                    if (val == "")
                    {
                        Label1.Text = "1";
                    }
                    else
                    {
                        a = Convert.ToInt32(dr[0].ToString());
                        a = a + 1;
                        Label1.Text = a.ToString();
                    }
                }
            }
            con1000.Close();
        }

    }
    private void getService_no()
    {
        if (User.Identity.IsAuthenticated)
        {
            SqlConnection con1000 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd1000 = new SqlCommand("select * from user_details where company_name='" + User.Identity.Name + "'", con1000);
            SqlDataReader dr1000;
            con1000.Open();
            dr1000 = cmd1000.ExecuteReader();
            if (dr1000.Read())
            {
                company_id = Convert.ToInt32(dr1000["com_id"].ToString());

                int a;

                SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                con1.Open();
                string query = "Select max(Vendor_Code) from Vendor where Com_Id='" + company_id + "'";
                SqlCommand cmd1 = new SqlCommand(query, con1);
                SqlDataReader dr = cmd1.ExecuteReader();
                if (dr.Read())
                {
                    string val = dr[0].ToString();
                    if (val == "")
                    {
                        Label10.Text = "1";
                    }
                    else
                    {
                        a = Convert.ToInt32(dr[0].ToString());
                        a = a + 1;
                        Label10.Text = a.ToString();
                    }
                }
            }
            con1000.Close();
        }

    }

    private void getUnit_no()
    {
        if (User.Identity.IsAuthenticated)
        {
            SqlConnection con1000 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd1000 = new SqlCommand("select * from user_details where company_name='" + User.Identity.Name + "'", con1000);
            SqlDataReader dr1000;
            con1000.Open();
            dr1000 = cmd1000.ExecuteReader();
            if (dr1000.Read())
            {
                company_id = Convert.ToInt32(dr1000["com_id"].ToString());

                int a;

                SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                con1.Open();
                string query = "Select max(Unit_id) from ADD_Unit where Com_Id='" + company_id + "'";
                SqlCommand cmd1 = new SqlCommand(query, con1);
                SqlDataReader dr = cmd1.ExecuteReader();
                if (dr.Read())
                {
                    string val = dr[0].ToString();
                    if (val == "")
                    {
                        Label21.Text = "1";
                    }
                    else
                    {
                        a = Convert.ToInt32(dr[0].ToString());
                        a = a + 1;
                        Label21.Text = a.ToString();
                    }
                }
            }
            con1000.Close();
        }

    }
    
    private void show_category()
    {
        if (User.Identity.IsAuthenticated)
        {
            SqlConnection con1000 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd1000 = new SqlCommand("select * from user_details where company_name='" + User.Identity.Name + "'", con1000);
            SqlDataReader dr1000;
            con1000.Open();
            dr1000 = cmd1000.ExecuteReader();
            if (dr1000.Read())
            {
                company_id = Convert.ToInt32(dr1000["com_id"].ToString());

                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd = new SqlCommand("Select * from Job_Work_Vendor where Com_Id='" + company_id + "' ORDER BY Vendor_Code asc", con);
                con.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);


                DropDownList2.DataSource = ds;
                DropDownList2.DataTextField = "Vendor_Name";
                DropDownList2.DataValueField = "Vendor_Code";
                DropDownList2.DataBind();
                DropDownList2.Items.Insert(0, new ListItem("All", "0"));

                con.Close();
            }
            con1000.Close();
        }

    }
    protected void LoginLink_OnClick(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        Response.Redirect("~/login.aspx");

    }

    protected void btnRandom_Click(object sender, EventArgs e)
    {
        Session["name1"] = "";
        Response.Redirect("~/Admin/Category_Add.aspx");
    }

    private void showcustomertype()
    {

    }
    private void showrating()
    {

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        BindData();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "Page " + (GridView1.PageIndex + 1) + " of " + GridView1.PageCount;
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {

    }
    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }


    protected void TextBox3_TextChanged(object sender, EventArgs e)
    {
        if (!System.Text.RegularExpressions.Regex.IsMatch(TextBox3.Text, "^[a-zA-Z]"))
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please Provide valide Supplier Name')", true);

            TextBox3.Text.Remove(TextBox3.Text.Length - 1);
            TextBox3.Text = "";
        }
        else
        {
            if (User.Identity.IsAuthenticated)
            {
                SqlConnection con1000 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd1000 = new SqlCommand("select * from user_details where company_name='" + User.Identity.Name + "'", con1000);
                SqlDataReader dr1000;
                con1000.Open();
                dr1000 = cmd1000.ExecuteReader();
                if (dr1000.Read())
                {
                    company_id = Convert.ToInt32(dr1000["com_id"].ToString());

                }
                con1000.Close();
            }


            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd = new SqlCommand("select * from Material_Entry where Material_name='" + TextBox3.Text + "' and Com_Id='" + company_id + "'", con);
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                TextBox19.Text = dr["Material_Code"].ToString();
            }
            con.Close();

            SqlConnection con22 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd22 = new SqlCommand("Select * from Vendor where Product='" + TextBox3.Text + "' AND Com_Id='" + company_id + "' ORDER BY Vendor_Code asc", con22);
            con22.Open();
            DataSet ds22 = new DataSet();
            SqlDataAdapter da22 = new SqlDataAdapter(cmd22);
            da22.Fill(ds22);


            DropDownList1.DataSource = ds22;
            DropDownList1.DataTextField = "Vendor_Name";
            DropDownList1.DataValueField = "Vendor_Code";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, new ListItem("All", "0"));

            con22.Close();

        }
    }
    protected void TextBox4_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Int64 temp = Convert.ToInt64(TextBox4.Text);
        }
        catch (Exception h)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please provide Number only')", true);

            TextBox4.Text = "";
        }
    }
    //protected void TextBox6_TextChanged(object sender, EventArgs e)
    //{
    //    if (!System.Text.RegularExpressions.Regex.IsMatch(TextBox6.Text, "^[a-zA-Z]"))
    //    {
    //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please Provide valide Supplier Name')", true);

    //        TextBox6.Text.Remove(TextBox6.Text.Length - 1);
    //        TextBox6.Text = "";
    //    }
    //    this.ModalPopupExtender3.Show();
    //}

    //protected void TextBox5_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        Int64 temp = Convert.ToInt64(TextBox5.Text);
    //    }
    //    catch (Exception h)
    //    {
    //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please provide Number only')", true);

    //        TextBox5.Text = "";
    //    }
    //    this.ModalPopupExtender3.Show();
    //}
    protected void TextBox1_TextChanged1(object sender, EventArgs e)
    {
        if (TextBox1.Text != "")
        {
            if (User.Identity.IsAuthenticated)
            {
                SqlConnection con1000 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd1000 = new SqlCommand("select * from user_details where company_name='" + User.Identity.Name + "'", con1000);
                SqlDataReader dr1000;
                con1000.Open();
                dr1000 = cmd1000.ExecuteReader();
                if (dr1000.Read())
                {
                    company_id = Convert.ToInt32(dr1000["com_id"].ToString());
                    SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand CMD = new SqlCommand("select * from Job_Work_Vendor where Vendor_Name='" + TextBox1.Text + "' and Com_Id='" + company_id + "' ", con1);
                    DataTable dt1 = new DataTable();
                    con1.Open();
                    SqlDataAdapter da1 = new SqlDataAdapter(CMD);
                    da1.Fill(dt1);
                    GridView1.DataSource = dt1;
                    GridView1.DataBind();
                }
                con1000.Close();
            }
        }
        else
        {
            BindData();
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.AddHeader("content-disposition", "attachment; filename=gvtoexcel.xls");
        Response.ContentType = "application/excel";
        System.IO.StringWriter sw = new System.IO.StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        GridView1.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }
    protected void Button8_Click(object sender, EventArgs e)
    {
        this.ModalPopupExtender1.Show();
    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        if (User.Identity.IsAuthenticated)
        {
            SqlConnection con1000 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd1000 = new SqlCommand("select * from user_details where company_name='" + User.Identity.Name + "'", con1000);
            SqlDataReader dr1000;
            con1000.Open();
            dr1000 = cmd1000.ExecuteReader();
            if (dr1000.Read())
            {
                company_id = Convert.ToInt32(dr1000["com_id"].ToString());
                if (TextBox14.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please Enter Supplier Name')", true);
                }
                else
                {
                    SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd1 = new SqlCommand("select * from Vendor where Vendor_Name='" + TextBox14.Text + "' AND Com_Id='" + company_id + "'  ", con1);
                    con1.Open();
                    SqlDataReader dr1;
                    dr1 = cmd1.ExecuteReader();
                    if (dr1.HasRows)
                    {

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Supplier Name already exist')", true);
                        TextBox17.Text = "";
                    }
                    else
                    {



                        SqlConnection CON = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                        SqlCommand cmd = new SqlCommand("insert into Vendor values(@Vendor_Code,@Vendor_Name,@Vendor_Address,@Mobile_no,@Bank_name,@Account_no,@Account_Name,@IFSC_code,@Product,@Com_Id)", CON);
                        cmd.Parameters.AddWithValue("@Vendor_Code", Label10.Text);
                        cmd.Parameters.AddWithValue("@Vendor_Name", HttpUtility.HtmlDecode(TextBox14.Text));
                        cmd.Parameters.AddWithValue("@Vendor_Address", HttpUtility.HtmlDecode(TextBox27.Text));
                        cmd.Parameters.AddWithValue("@Mobile_no", HttpUtility.HtmlDecode(TextBox17.Text));
                        cmd.Parameters.AddWithValue("@Bank_name", HttpUtility.HtmlDecode(TextBox23.Text));
                        cmd.Parameters.AddWithValue("@Account_no", HttpUtility.HtmlDecode(TextBox24.Text));
                        cmd.Parameters.AddWithValue("@Account_Name", HttpUtility.HtmlDecode(TextBox25.Text));
                        cmd.Parameters.AddWithValue("@IFSC_code", HttpUtility.HtmlDecode(TextBox26.Text));
                        cmd.Parameters.AddWithValue("@Product", HttpUtility.HtmlDecode(DropDownList5.SelectedItem.Text));
                        cmd.Parameters.AddWithValue("@Com_Id", company_id);
                        CON.Open();
                        cmd.ExecuteNonQuery();
                        CON.Close();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Supplier Entry saved Successfully')", true);
                        getService_no();
                        BindData();
                        show_MaterialName();
                        TextBox17.Text = "";
                        con1.Close();

                    }
                }
            }
            con1000.Close();
        }
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (User.Identity.IsAuthenticated)
        {
            SqlConnection con1000 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd1000 = new SqlCommand("select * from user_details where company_name='" + User.Identity.Name + "'", con1000);
            SqlDataReader dr1000;
            con1000.Open();
            dr1000 = cmd1000.ExecuteReader();
            if (dr1000.Read())
            {
                company_id = Convert.ToInt32(dr1000["com_id"].ToString());

            }
            con1000.Close();
        }


        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("select * from Vendor where Vendor_Name='" + DropDownList1.SelectedItem.Text + "' and Com_Id='" + company_id + "'", con);
        SqlDataReader dr;
        con.Open();
        dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            TextBox2.Text = dr["Vendor_Address"].ToString();
            TextBox4.Text = dr["Mobile_no"].ToString();
        }
        con.Close();
    }
    protected void Button7_Click(object sender, EventArgs e)
    {
        this.ModalPopupExtender2.Show();
        
    }
    protected void Button10_Click(object sender, EventArgs e)
    {
        if (User.Identity.IsAuthenticated)
        {
            SqlConnection con1000 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd1000 = new SqlCommand("select * from user_details where company_name='" + User.Identity.Name + "'", con1000);
            SqlDataReader dr1000;
            con1000.Open();
            dr1000 = cmd1000.ExecuteReader();
            if (dr1000.Read())
            {
                company_id = Convert.ToInt32(dr1000["com_id"].ToString());
                if (TextBox28.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please Enter Unit Name')", true);
                }
                else
                {
                    SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd1 = new SqlCommand("select * from ADD_Unit where Unit_Name='" + TextBox28.Text + "' AND Com_Id='" + company_id + "'  ", con1);
                    con1.Open();
                    SqlDataReader dr1;
                    dr1 = cmd1.ExecuteReader();
                    if (dr1.HasRows)
                    {

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Unit Name already exist')", true);
                        TextBox28.Text = "";
                    }
                    else
                    {



                        SqlConnection CON = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                        SqlCommand cmd = new SqlCommand("insert into ADD_Unit values(@Unit_id,@Unit_Name,@Com_Id)", CON);
                        cmd.Parameters.AddWithValue("@Unit_id", Label21.Text);
                        cmd.Parameters.AddWithValue("@Unit_Name", HttpUtility.HtmlDecode(TextBox28.Text));           
                        cmd.Parameters.AddWithValue("@Com_Id", company_id);
                        CON.Open();
                        cmd.ExecuteNonQuery();
                        CON.Close();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Unit Entry saved Successfully')", true);
                        getUnit_no();
                        BindData();
                        show_Unit();
                        TextBox28.Text = "";
                        con1.Close();

                    }
                }
            }
            con1000.Close();
        }
    }
    protected void TextBox11_TextChanged(object sender, EventArgs e)
    {

    }
    protected void Button15_Click(object sender, EventArgs e)
    {
        show_SupplierName();
        TextBox15.Text = "";
        BindData();
    }
}
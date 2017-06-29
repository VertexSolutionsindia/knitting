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


public partial class Admin_MachineWiseProduction : System.Web.UI.Page
{
    public static int company_id = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneInfo);
            DateTime date = now;


            TextBox2.Attributes.Add("onkeypress", "return controlEnter('" + TextBox4.ClientID + "', event)");


            getinvoiceno();
            getAccosories_no();
            getKnitting_no();
            BindData();
            show_MachineName();
            show_Unit();
            show_Accossories();
            show_Knitting();
            show_ProductName();
            active();
            created();
            Button12.Visible = false;
            Button1.Visible = true;
            DropDownList5.Items.Clear();
            DropDownList5.Items.Insert(0, new ListItem("All", "0"));
            DropDownList5.Items.Insert(1, new ListItem("Yes", "1"));
            DropDownList5.Items.Insert(2, new ListItem("No", "2"));
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

    //public override void VerifyRenderingInServerForm(Control control)
    //{
    //    /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
    //       server control at run time. */
    //}

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
        SqlCommand cmd = new SqlCommand("select * from Machine_Wise_Production where Mac_Pro_id='" + ROW.Cells[1].Text + "' and Com_Id='" + company_id + "'", con);
        SqlDataReader dr;
        con.Open();
        dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            Label1.Text = dr["Mac_Pro_id"].ToString();
            DropDownList3.SelectedItem.Text = dr["Machine_Details"].ToString();
            TextBox3.Text = dr["Yarn_Input"].ToString();
            DropDownList6.SelectedItem.Text = dr["Yarn_Input_unit"].ToString();
            DropDownList7.SelectedItem.Text = dr["Accessories_Used"].ToString();
            TextBox7.Text = dr["Accessories_Used_Dtl"].ToString();
            DropDownList10.SelectedItem.Text = dr["Colors"].ToString();
            DropDownList8.SelectedItem.Text = dr["Shades"].ToString();
            DropDownList1.SelectedItem.Text = dr["Knitting_Machcine"].ToString();
            TextBox2.Text = dr["Fabric_Width"].ToString();
            TextBox19.Text = dr["Fabric_Length"].ToString();
            TextBox30.Text = dr["GSM"].ToString();
            DropDownList9.SelectedItem.Text = dr["GSM_Unit"].ToString();
            TextBox18.Text = dr["Yarn_Count"].ToString();
            TextBox5.Text = dr["Stitch_Length"].ToString();
            TextBox8.Text = dr["Cylinder_Dia"].ToString();
            TextBox16.Text = dr["No_OfFeed"].ToString();
            TextBox4.Text = dr["RPM"].ToString();
            TextBox12.Text = dr["Time_Consume"].ToString();
            DropDownList4.SelectedItem.Text = dr["Times_Dtl"].ToString();
            TextBox10.Text = dr["Product_Name"].ToString();
            DropDownList5.SelectedItem.Text = dr["Quality_Check"].ToString();

            DropDownList4.Focus();
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
                SqlCommand cmd = new SqlCommand("update Machine_Wise_Production set Machine_Details='" + HttpUtility.HtmlDecode(DropDownList3.SelectedItem.Text) +
                    "', Yarn_Input='" + HttpUtility.HtmlDecode(TextBox3.Text) +
                    "',Yarn_Input_unit='" + HttpUtility.HtmlDecode(DropDownList6.SelectedItem.Text) +
                    "',Accessories_Used='" + HttpUtility.HtmlDecode(DropDownList1.SelectedItem.Text) +
                    "',Accessories_Used_Dtl='" + HttpUtility.HtmlDecode(TextBox7.Text) +
                    "',Colors='" + HttpUtility.HtmlDecode(DropDownList10.SelectedItem.Text) +
                    "',Shades='" + HttpUtility.HtmlDecode(DropDownList8.SelectedItem.Text) +
                    "',Knitting_Machcine='" + HttpUtility.HtmlDecode(DropDownList1.SelectedItem.Text) +
                       "',Fabric_Width='" + HttpUtility.HtmlDecode(TextBox2.Text) +
                       "',Fabric_Length='" + HttpUtility.HtmlDecode(TextBox19.Text) +
                       "',GSM='" + HttpUtility.HtmlDecode(TextBox30.Text) +
                         "',GSM_Unit='" + HttpUtility.HtmlDecode(DropDownList9.SelectedItem.Text) +
                          "',Yarn_Count='" + HttpUtility.HtmlDecode(TextBox18.Text) +
                          "',Stitch_Length='" + HttpUtility.HtmlDecode(TextBox5.Text) +
                          "',Cylinder_Dia='" + HttpUtility.HtmlDecode(TextBox8.Text) +
                          "',No_OfFeed='" + HttpUtility.HtmlDecode(TextBox16.Text) +
                          "',RPM='" + HttpUtility.HtmlDecode(TextBox4.Text) +
                          "',Time_Consume='" + HttpUtility.HtmlDecode(TextBox12.Text) +
                          "',Times_Dtl='" + HttpUtility.HtmlDecode(DropDownList4.SelectedItem.Text) +
                           "',Product_Name='" + HttpUtility.HtmlDecode(TextBox10.Text) +
                         "',Quality_Check='" + HttpUtility.HtmlDecode(DropDownList5.SelectedItem.Text) +
                    "' where Mac_Pro_id='" + Label1.Text + "'  and Com_Id='" + company_id + "' ", CON);

                CON.Open();
                cmd.ExecuteNonQuery();
                CON.Close();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Machine Wise Production Updated successfully')", true);
                TextBox3.Text = "";
                TextBox2.Text = "";
                TextBox4.Text = "";
                TextBox7.Text = "";
                TextBox12.Text = "";
                TextBox19.Text = "";
                TextBox30.Text = "";
                TextBox18.Text = "";
                TextBox5.Text = "";
                TextBox8.Text = "";
                TextBox16.Text = "";
                TextBox10.Text = "";
                getinvoiceno();
                getAccosories_no();
                getKnitting_no();
                BindData();
                show_MachineName();
                show_Unit();
                show_Accossories();
                show_Knitting();
                show_ProductName();
                Button12.Visible = false;
                Button1.Visible = true;
                DropDownList5.Items.Clear();
                DropDownList5.Items.Insert(0, new ListItem("All", "0"));
                DropDownList5.Items.Insert(1, new ListItem("Yes", "1"));
                DropDownList5.Items.Insert(2, new ListItem("No", "2"));
            }
            con1000.Close();
        }
    }

    protected void Button7_Click(object sender, EventArgs e)
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
                        SqlCommand cmd = new SqlCommand("delete from Machine_Wise_Production where Mac_Pro_id='" + usrid + "' and Com_Id='" + company_id + "'", con);
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
   
    private void show_MachineName()
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
                SqlCommand cmd = new SqlCommand("Select * from Material_Entry where Com_Id='" + company_id + "' ORDER BY Material_id asc", con);
                con.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                DropDownList3.DataSource = ds;
                DropDownList3.DataTextField = "Machine_details";
                DropDownList3.DataValueField = "Material_id";
                DropDownList3.DataBind();
                DropDownList3.Items.Insert(0, new ListItem("All", "0"));

                DropDownList10.DataSource = ds;
                DropDownList10.DataTextField = "Colors";
                DropDownList10.DataValueField = "Material_id";
                DropDownList10.DataBind();
                DropDownList10.Items.Insert(0, new ListItem("All", "0"));

                DropDownList8.DataSource = ds;
                DropDownList8.DataTextField = "Shades";
                DropDownList8.DataValueField = "Material_id";
                DropDownList8.DataBind();
                DropDownList8.Items.Insert(0, new ListItem("All", "0"));

                con.Close();
            }
            con1000.Close();
        }
    }
    private void show_ProductName()
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
                SqlCommand cmd = new SqlCommand("Select * from Machine_Wise_Production where Com_Id='" + company_id + "' ORDER BY Mac_Pro_id asc", con);
                con.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                DropDownList2.DataSource = ds;
                DropDownList2.DataTextField = "Product_Name";
                DropDownList2.DataValueField = "Mac_Pro_id";
                DropDownList2.DataBind();
                DropDownList2.Items.Insert(0, new ListItem("All", "0"));



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

                DropDownList6.DataSource = ds;
                DropDownList6.DataTextField = "Unit_Name";
                DropDownList6.DataValueField = "Unit_id";
                DropDownList6.DataBind();
                DropDownList6.Items.Insert(0, new ListItem("All", "0"));

                DropDownList9.DataSource = ds;
                DropDownList9.DataTextField = "Unit_Name";
                DropDownList9.DataValueField = "Unit_id";
                DropDownList9.DataBind();
                DropDownList9.Items.Insert(0, new ListItem("All", "0"));


                con.Close();
            }
            con1000.Close();
        }
    }
    private void show_Accossories()
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
                SqlCommand cmd = new SqlCommand("Select * from ADD_Accessories where Com_Id='" + company_id + "' ORDER BY Accessories_id asc", con);
                con.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);


                DropDownList7.DataSource = ds;
                DropDownList7.DataTextField = "Accessories_Name";
                DropDownList7.DataValueField = "Accessories_id";
                DropDownList7.DataBind();
                DropDownList7.Items.Insert(0, new ListItem("All", "0"));

                con.Close();
            }
            con1000.Close();
        }
    }
    private void show_Knitting()
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
                SqlCommand cmd = new SqlCommand("Select * from ADD_Knitting where Com_Id='" + company_id + "' ORDER BY Knitting_id asc", con);
                con.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);


                DropDownList1.DataSource = ds;
                DropDownList1.DataTextField = "Knitting_Name";
                DropDownList1.DataValueField = "Knitting_id";
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0, new ListItem("All", "0"));

                con.Close();
            }
            con1000.Close();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (DropDownList3.SelectedItem.Text == "All")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please Select Machine Details')", true);
        }
        else if (DropDownList1.SelectedItem.Text == "All")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please Select Knitting Machine')", true);
        }
        else if (DropDownList7.SelectedItem.Text == "All")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please Select Accessories Details')", true);
        }
        else if (DropDownList5.SelectedItem.Text == "All")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please Select Quality Check')", true);
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
                    SqlCommand cmd = new SqlCommand("insert into Machine_Wise_Production values(@Mac_Pro_id,@Machine_Details,@Yarn_Input,@Yarn_Input_unit,@Accessories_Used,@Accessories_Used_Dtl,@Colors,@Shades,@Knitting_Machcine,@Fabric_Width,@Fabric_Length,@GSM,@GSM_Unit,@Yarn_Count,@Stitch_Length,@Cylinder_Dia,@No_OfFeed,@RPM,@Time_Consume,@Times_Dtl,@Com_Id,@Product_Name,@Quality_Check)", CON);
                    cmd.Parameters.AddWithValue("@Mac_Pro_id", Label1.Text);
                    cmd.Parameters.AddWithValue("@Machine_Details", HttpUtility.HtmlDecode(DropDownList3.SelectedItem.Text));
                    cmd.Parameters.AddWithValue("@Yarn_Input", HttpUtility.HtmlDecode(TextBox3.Text));
                    cmd.Parameters.AddWithValue("@Yarn_Input_unit", HttpUtility.HtmlDecode(DropDownList6.SelectedItem.Text));
                    cmd.Parameters.AddWithValue("@Accessories_Used", HttpUtility.HtmlDecode(DropDownList7.SelectedItem.Text));
                    cmd.Parameters.AddWithValue("@Accessories_Used_Dtl", HttpUtility.HtmlDecode(TextBox7.Text));
                    cmd.Parameters.AddWithValue("@Colors", HttpUtility.HtmlDecode(DropDownList10.SelectedItem.Text));
                    cmd.Parameters.AddWithValue("@Shades", HttpUtility.HtmlDecode(DropDownList8.SelectedItem.Text));
                    cmd.Parameters.AddWithValue("@Knitting_Machcine", HttpUtility.HtmlDecode(DropDownList1.SelectedItem.Text));
                    cmd.Parameters.AddWithValue("@Fabric_Width", HttpUtility.HtmlDecode(TextBox2.Text));
                    cmd.Parameters.AddWithValue("@Fabric_Length", HttpUtility.HtmlDecode(TextBox19.Text));
                    cmd.Parameters.AddWithValue("@GSM", HttpUtility.HtmlDecode(TextBox30.Text));
                    cmd.Parameters.AddWithValue("@GSM_Unit", HttpUtility.HtmlDecode(DropDownList9.SelectedItem.Text));
                    cmd.Parameters.AddWithValue("@Yarn_Count", HttpUtility.HtmlDecode(TextBox18.Text));
                    cmd.Parameters.AddWithValue("@Stitch_Length", HttpUtility.HtmlDecode(TextBox5.Text));
                    cmd.Parameters.AddWithValue("@Cylinder_Dia", HttpUtility.HtmlDecode(TextBox8.Text));
                    cmd.Parameters.AddWithValue("@No_OfFeed", HttpUtility.HtmlDecode(TextBox16.Text));
                    cmd.Parameters.AddWithValue("@RPM", HttpUtility.HtmlDecode(TextBox4.Text));
                    cmd.Parameters.AddWithValue("@Time_Consume", HttpUtility.HtmlDecode(TextBox12.Text));
                    cmd.Parameters.AddWithValue("@Times_Dtl", HttpUtility.HtmlDecode(DropDownList4.SelectedItem.Text));
                    cmd.Parameters.AddWithValue("@Com_Id", company_id);
                    cmd.Parameters.AddWithValue("@Product_Name", HttpUtility.HtmlDecode(TextBox10.Text));
                    cmd.Parameters.AddWithValue("@Quality_Check", HttpUtility.HtmlDecode(DropDownList5.SelectedItem.Text));

                    CON.Open();
                    cmd.ExecuteNonQuery();
                    CON.Close();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Machine Wise Production Saved successfully')", true);
                    TextBox3.Text = "";
                    TextBox2.Text = "";
                    TextBox4.Text = "";
                    TextBox7.Text = "";
                    TextBox12.Text = "";
                    TextBox19.Text = "";
                    TextBox30.Text = "";
                    TextBox18.Text = "";
                    TextBox5.Text = "";
                    TextBox8.Text = "";
                    TextBox16.Text = "";
                    TextBox10.Text = "";
                    getinvoiceno();
                    getAccosories_no();
                    getKnitting_no();
                    BindData();
                    show_MachineName();
                    show_Unit();
                    show_Accossories();
                    show_ProductName();
                    show_Knitting();
                    DropDownList5.Items.Clear();
                    DropDownList5.Items.Insert(0, new ListItem("All", "0"));
                    DropDownList5.Items.Insert(1, new ListItem("Yes", "1"));
                    DropDownList5.Items.Insert(2, new ListItem("No", "2"));
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
        TextBox7.Text = "";
        TextBox12.Text = "";
        TextBox19.Text = "";
        TextBox30.Text = "";
        TextBox18.Text = "";
        TextBox5.Text = "";
        TextBox8.Text = "";
        TextBox16.Text = "";
        TextBox10.Text = "";

        getinvoiceno();
        getAccosories_no();
        getKnitting_no();
        BindData();
        show_MachineName();
        show_Unit();
        show_Accossories();
        show_Knitting();
        DropDownList4.Focus();
        Button12.Visible = false;
        Button1.Visible = true;
        DropDownList5.Items.Clear();
        DropDownList5.Items.Insert(0, new ListItem("All", "0"));
        DropDownList5.Items.Insert(1, new ListItem("Yes", "1"));
        DropDownList5.Items.Insert(2, new ListItem("No", "2"));
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
                cmd.CommandText = "select distinct Colors from Material_Entry where Com_Id=@Com_Id and " +
                "Colors like @Colors + '%'";
                cmd.Parameters.AddWithValue("@Colors", prefixText);
                cmd.Parameters.AddWithValue("@Com_Id", company_id);
                cmd.Connection = conn;
                conn.Open();
                List<string> customers = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(sdr["Colors"].ToString());
                    }
                }
                conn.Close();
                return customers;
            }
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
                SqlCommand CMD = new SqlCommand("select * from Machine_Wise_Production where Com_Id='" + company_id + "' ORDER BY Mac_Pro_id asc", con);
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
                SqlCommand cmd = new SqlCommand("delete from Machine_Wise_Production where Mac_Pro_id='" + row.Cells[1].Text + "' and Com_Id='" + company_id + "' ", con);
                cmd.ExecuteNonQuery();
                con.Close();

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Machine Wise Production Deleted Successfully')", true);

                BindData();  
                getinvoiceno();
                show_ProductName();
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
                string query = "Select max(Mac_Pro_id) from Machine_Wise_Production where Com_Id='" + company_id + "'";
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


    private void getAccosories_no()
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
                string query = "Select max(Accessories_id) from ADD_Accessories where Com_Id='" + company_id + "'";
                SqlCommand cmd1 = new SqlCommand(query, con1);
                SqlDataReader dr = cmd1.ExecuteReader();
                if (dr.Read())
                {
                    string val = dr[0].ToString();
                    if (val == "")
                    {
                        Label7.Text = "1";
                    }
                    else
                    {
                        a = Convert.ToInt32(dr[0].ToString());
                        a = a + 1;
                        Label7.Text = a.ToString();
                    }
                }
            }
            con1000.Close();
        }

    }
    private void getKnitting_no()
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
                string query = "Select max(Knitting_id) from ADD_Knitting where Com_Id='" + company_id + "'";
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
    
   
    protected void LoginLink_OnClick(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        Response.Redirect("~/login.aspx");

    }

    //protected void btnRandom_Click(object sender, EventArgs e)
    //{
    //    Session["name1"] = "";
    //    Response.Redirect("~/Admin/Category_Add.aspx");
    //}

    //private void showcustomertype()
    //{

    //}
    //private void showrating()
    //{

    //}
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
    //protected void TextBox1_TextChanged(object sender, EventArgs e)
    //{

    //}
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }




    protected void Button15_Click(object sender, EventArgs e)
    {
        show_ProductName();
        TextBox15.Text = "";
        BindData();
    }
    protected void Button13_Click(object sender, EventArgs e)
    {
        this.ModalPopupExtender4.Show();
    }
    protected void Button20_Click(object sender, EventArgs e)
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
                if (TextBox9.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please Enter Accessories Name')", true);
                }
                else
                {
                    SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd1 = new SqlCommand("select * from ADD_Accessories where Accessories_Name='" + TextBox9.Text + "' AND Com_Id='" + company_id + "'  ", con1);
                    con1.Open();
                    SqlDataReader dr1;
                    dr1 = cmd1.ExecuteReader();
                    if (dr1.HasRows)
                    {

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Accessories Name already exist')", true);
                        TextBox9.Text = "";
                    }
                    else
                    {



                        SqlConnection CON = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                        SqlCommand cmd = new SqlCommand("insert into ADD_Accessories values(@Accessories_id,@Accessories_Name,@Com_Id)", CON);
                        cmd.Parameters.AddWithValue("@Accessories_id", Label7.Text);
                        cmd.Parameters.AddWithValue("@Accessories_Name", HttpUtility.HtmlDecode(TextBox9.Text));
                        cmd.Parameters.AddWithValue("@Com_Id", company_id);
                        CON.Open();
                        cmd.ExecuteNonQuery();
                        CON.Close();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Accessories Entry saved Successfully')", true);
                        getAccosories_no();
                        BindData();
                        show_Accossories();
                        TextBox9.Text = "";
                        con1.Close();

                    }
                }
            }
            con1000.Close();
        }
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
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please Enter Knitting Name')", true);
                }
                else
                {
                    SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd1 = new SqlCommand("select * from ADD_Knitting where Knitting_Name='" + TextBox14.Text + "' AND Com_Id='" + company_id + "'  ", con1);
                    con1.Open();
                    SqlDataReader dr1;
                    dr1 = cmd1.ExecuteReader();
                    if (dr1.HasRows)
                    {

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Knitting Name already exist')", true);
                        TextBox14.Text = "";
                    }
                    else
                    {



                        SqlConnection CON = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                        SqlCommand cmd = new SqlCommand("insert into ADD_Knitting values(@Knitting_id,@Knitting_Name,@Com_Id)", CON);
                        cmd.Parameters.AddWithValue("@Knitting_id", Label10.Text);
                        cmd.Parameters.AddWithValue("@Knitting_Name", HttpUtility.HtmlDecode(TextBox14.Text));
                        cmd.Parameters.AddWithValue("@Com_Id", company_id);
                        CON.Open();
                        cmd.ExecuteNonQuery();
                        CON.Close();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Knitting Entry saved Successfully')", true);
                        getKnitting_no();
                        BindData();
                        show_Knitting();
                        TextBox14.Text = "";
                        con1.Close();

                    }
                }
            }
            con1000.Close();
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
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
    protected void DropDownList2_SelectedIndexChanged1(object sender, EventArgs e)
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
                SqlCommand CMD = new SqlCommand("select * from Machine_Wise_Production where Product_Name='" + DropDownList2.SelectedItem.Text + "' and  Com_Id='" + company_id + "' ORDER BY Mac_Pro_id asc", con1);
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
                SqlCommand CMD = new SqlCommand("select * from Machine_Wise_Production where Colors='" + TextBox15.Text + "' and Com_Id='" + company_id + "' ", con1);
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
}
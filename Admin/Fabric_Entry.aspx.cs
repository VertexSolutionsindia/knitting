﻿#region " Using "
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


public partial class Admin_Fabric_Entry : System.Web.UI.Page
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
            getComposition_no();
            getQualifier_no();
            BindData();
            show_Division();
            show_Descrition();
            show_Colors();
            show_CompositionName();
            show_QualifierName();
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
        SqlCommand cmd = new SqlCommand("select * from Fabric_Entry where Fab_Pro_id='" + ROW.Cells[1].Text + "' and Com_Id='" + company_id + "'", con);
        SqlDataReader dr;
        con.Open();
        dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            Label1.Text = dr["Fab_Pro_id"].ToString();
            DropDownList3.SelectedItem.Text = dr["Division"].ToString();
            DropDownList6.SelectedItem.Text = dr["Description"].ToString();
            DropDownList7.SelectedItem.Text = dr["Composition"].ToString();
            TextBox7.Text = dr["Composition_Per"].ToString();
            DropDownList1.SelectedItem.Text = dr["Qualifier"].ToString();
            TextBox2.Text = dr["Weight"].ToString();
            TextBox11.Text = dr["Yarn_Count_1"].ToString();
            TextBox5.Text = dr["Yarn_Count_2"].ToString();
            TextBox3.Text = dr["Yarn_Count_3"].ToString();
            DropDownList11.SelectedItem.Text = dr["Color_1"].ToString();
            DropDownList8.SelectedItem.Text = dr["Color_2"].ToString();
            DropDownList12.SelectedItem.Text = dr["Color_3"].ToString();
            DropDownList9.SelectedItem.Text = dr["Color_4"].ToString();
            TextBox8.Text = dr["Fabric_Finis_1"].ToString();
            TextBox4.Text = dr["Fabric_Finish_2"].ToString();
            TextBox12.Text = dr["Time_Consume"].ToString();
            DropDownList4.SelectedItem.Text = dr["Times_Dtl"].ToString();
            TextBox10.Text = dr["Product_Name"].ToString();
            DropDownList5.SelectedItem.Text = dr["Quality_Check"].ToString();
            DropDownList3.Focus();
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
                SqlCommand cmd = new SqlCommand("update Fabric_Entry set Division='" + HttpUtility.HtmlDecode(DropDownList3.SelectedItem.Text) +
                    "', Description='" + HttpUtility.HtmlDecode(DropDownList6.SelectedItem.Text) +
                    "',Composition='" + HttpUtility.HtmlDecode(DropDownList7.SelectedItem.Text) +
                    "',Composition_Per='" + HttpUtility.HtmlDecode(TextBox7.Text) +
                    "',Qualifier='" + HttpUtility.HtmlDecode(DropDownList1.SelectedItem.Text) +
                    "',Weight='" + HttpUtility.HtmlDecode(TextBox2.Text) +
                    "',Yarn_Count_1='" + HttpUtility.HtmlDecode(TextBox11.Text) +
                    "',Yarn_Count_2='" + HttpUtility.HtmlDecode(TextBox5.Text) +
                       "',Yarn_Count_3='" + HttpUtility.HtmlDecode(TextBox3.Text) +
                       "',Color_1='" + HttpUtility.HtmlDecode(DropDownList11.SelectedItem.Text) +
                       "',Color_2='" + HttpUtility.HtmlDecode(DropDownList8.SelectedItem.Text) +
                         "',Color_3='" + HttpUtility.HtmlDecode(DropDownList12.SelectedItem.Text) +
                          "',Color_4='" + HttpUtility.HtmlDecode(DropDownList9.SelectedItem.Text) +
                          "',Fabric_Finis_1='" + HttpUtility.HtmlDecode(TextBox8.Text) +
                          "',Fabric_Finish_2='" + HttpUtility.HtmlDecode(TextBox4.Text) +
                          "',Time_Consume='" + HttpUtility.HtmlDecode(TextBox12.Text) +
                          "',Times_Dtl='" + HttpUtility.HtmlDecode(DropDownList4.SelectedItem.Text) +
                           "',Product_Name='" + HttpUtility.HtmlDecode(TextBox10.Text) +
                         "',Quality_Check='" + HttpUtility.HtmlDecode(DropDownList5.SelectedItem.Text) +
                    "' where Fab_Pro_id='" + Label1.Text + "'  and Com_Id='" + company_id + "' ", CON);

                CON.Open();
                cmd.ExecuteNonQuery();
                CON.Close();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Fabric Entry Updated successfully')", true);
                TextBox3.Text = "";
                TextBox2.Text = "";
                TextBox4.Text = "";
                TextBox7.Text = "";
                TextBox12.Text = "";
                TextBox11.Text = "";
                TextBox5.Text = "";
                TextBox8.Text = "";
                TextBox10.Text = "";
                getinvoiceno();
                getComposition_no();
                getQualifier_no();
                BindData();
                show_Division();
                show_Descrition();
                show_Colors();
                show_CompositionName();
                show_QualifierName();
                show_ProductName();
                DropDownList5.Items.Clear();
                DropDownList5.Items.Insert(0, new ListItem("All", "0"));
                DropDownList5.Items.Insert(1, new ListItem("Yes", "1"));
                DropDownList5.Items.Insert(2, new ListItem("No", "2"));
                Button12.Visible = false;
                Button1.Visible = true;
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
                        SqlCommand cmd = new SqlCommand("delete from Fabric_Entry where Fab_Pro_id='" + usrid + "' and Com_Id='" + company_id + "'", con);
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

    private void show_Division()
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
                SqlCommand cmd = new SqlCommand("Select * from ADD_Department where Com_Id='" + company_id + "' ORDER BY Dept_id asc", con);
                con.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                DropDownList3.DataSource = ds;
                DropDownList3.DataTextField = "Department_Name";
                DropDownList3.DataValueField = "Dept_id";
                DropDownList3.DataBind();
                DropDownList3.Items.Insert(0, new ListItem("All", "0"));


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
                SqlCommand cmd = new SqlCommand("Select * from Fabric_Entry where Com_Id='" + company_id + "' ORDER BY Fab_Pro_id asc", con);
                con.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                DropDownList2.DataSource = ds;
                DropDownList2.DataTextField = "Product_Name";
                DropDownList2.DataValueField = "Fab_Pro_id";
                DropDownList2.DataBind();
                DropDownList2.Items.Insert(0, new ListItem("All", "0"));



                con.Close();
            }
            con1000.Close();
        }
    }
    private void show_Descrition()
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
                SqlCommand cmd = new SqlCommand("Select * from ADD_Description where Com_Id='" + company_id + "' ORDER BY Desc_id asc", con);
                con.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                DropDownList6.DataSource = ds;
                DropDownList6.DataTextField = "Description";
                DropDownList6.DataValueField = "Desc_id";
                DropDownList6.DataBind();
                DropDownList6.Items.Insert(0, new ListItem("All", "0"));



                con.Close();
            }
            con1000.Close();
        }
    }
    private void show_Colors()
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


                DropDownList11.DataSource = ds;
                DropDownList11.DataTextField = "Colors";
                DropDownList11.DataValueField = "Material_id";
                DropDownList11.DataBind();
                DropDownList11.Items.Insert(0, new ListItem("All", "0"));

                DropDownList8.DataSource = ds;
                DropDownList8.DataTextField = "Colors";
                DropDownList8.DataValueField = "Material_id";
                DropDownList8.DataBind();
                DropDownList8.Items.Insert(0, new ListItem("All", "0"));

                DropDownList12.DataSource = ds;
                DropDownList12.DataTextField = "Colors";
                DropDownList12.DataValueField = "Material_id";
                DropDownList12.DataBind();
                DropDownList12.Items.Insert(0, new ListItem("All", "0"));

                DropDownList9.DataSource = ds;
                DropDownList9.DataTextField = "Colors";
                DropDownList9.DataValueField = "Material_id";
                DropDownList9.DataBind();
                DropDownList9.Items.Insert(0, new ListItem("All", "0"));

                con.Close();
            }
            con1000.Close();
        }
    }
    private void show_CompositionName()
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
                SqlCommand cmd = new SqlCommand("Select * from ADD_Composition where Com_Id='" + company_id + "' ORDER BY Composition_id asc", con);
                con.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);


                DropDownList7.DataSource = ds;
                DropDownList7.DataTextField = "Composition_Name";
                DropDownList7.DataValueField = "Composition_id";
                DropDownList7.DataBind();
                DropDownList7.Items.Insert(0, new ListItem("All", "0"));

                con.Close();
            }
            con1000.Close();
        }
    }
    private void show_QualifierName()
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
                SqlCommand cmd = new SqlCommand("Select * from ADD_Qualifier where Com_Id='" + company_id + "' ORDER BY Quali_Comp_id asc", con);
                con.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);


                DropDownList1.DataSource = ds;
                DropDownList1.DataTextField = "Quali_Company_Name";
                DropDownList1.DataValueField = "Quali_Comp_id";
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
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please Select Division')", true);
        }
        else if (DropDownList6.SelectedItem.Text == "All")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please Select Description')", true);
        }
        else if (DropDownList1.SelectedItem.Text == "All")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please Select Qualifier')", true);
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
                    SqlCommand cmd = new SqlCommand("insert into Fabric_Entry values(@Fab_Pro_id,@Division,@Description,@Composition,@Composition_Per,@Qualifier,@Weight,@Yarn_Count_1,@Yarn_Count_2,@Yarn_Count_3,@Color_1,@Color_2,@Color_3,@Color_4,@Fabric_Finis_1,@Fabric_Finish_2,@Time_Consume,@Times_Dtl,@Com_Id,@Product_Name,@Quality_Check)", CON);
                    cmd.Parameters.AddWithValue("@Fab_Pro_id", Label1.Text);
                    cmd.Parameters.AddWithValue("@Division", HttpUtility.HtmlDecode(DropDownList3.SelectedItem.Text));
                    cmd.Parameters.AddWithValue("@Description", HttpUtility.HtmlDecode(DropDownList6.SelectedItem.Text));
                    cmd.Parameters.AddWithValue("@Composition", HttpUtility.HtmlDecode(DropDownList7.SelectedItem.Text));
                    cmd.Parameters.AddWithValue("@Composition_Per", HttpUtility.HtmlDecode(TextBox7.Text));
                    cmd.Parameters.AddWithValue("@Qualifier", HttpUtility.HtmlDecode(DropDownList1.SelectedItem.Text));
                    cmd.Parameters.AddWithValue("@Weight", HttpUtility.HtmlDecode(TextBox2.Text));
                    cmd.Parameters.AddWithValue("@Yarn_Count_1", HttpUtility.HtmlDecode(TextBox11.Text));
                    cmd.Parameters.AddWithValue("@Yarn_Count_2", HttpUtility.HtmlDecode(TextBox5.Text));
                    cmd.Parameters.AddWithValue("@Yarn_Count_3", HttpUtility.HtmlDecode(TextBox3.Text));
                    cmd.Parameters.AddWithValue("@Color_1", HttpUtility.HtmlDecode(DropDownList11.SelectedItem.Text));
                    cmd.Parameters.AddWithValue("@Color_2", HttpUtility.HtmlDecode(DropDownList8.SelectedItem.Text));
                    cmd.Parameters.AddWithValue("@Color_3", HttpUtility.HtmlDecode(DropDownList12.SelectedItem.Text));
                    cmd.Parameters.AddWithValue("@Color_4", HttpUtility.HtmlDecode(DropDownList9.SelectedItem.Text));
                    cmd.Parameters.AddWithValue("@Fabric_Finis_1", HttpUtility.HtmlDecode(TextBox8.Text));
                    cmd.Parameters.AddWithValue("@Fabric_Finish_2", HttpUtility.HtmlDecode(TextBox4.Text));
                    cmd.Parameters.AddWithValue("@Time_Consume", HttpUtility.HtmlDecode(TextBox12.Text));
                    cmd.Parameters.AddWithValue("@Times_Dtl", HttpUtility.HtmlDecode(DropDownList4.SelectedItem.Text));
                    cmd.Parameters.AddWithValue("@Com_Id", company_id);
                    cmd.Parameters.AddWithValue("@Product_Name", HttpUtility.HtmlDecode(TextBox10.Text));
                    cmd.Parameters.AddWithValue("@Quality_Check", HttpUtility.HtmlDecode(DropDownList5.SelectedItem.Text));

                    CON.Open();
                    cmd.ExecuteNonQuery();
                    CON.Close();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Fabric Entry Saved successfully')", true);
                    TextBox3.Text = "";
                    TextBox2.Text = "";
                    TextBox4.Text = "";
                    TextBox7.Text = "";
                    TextBox12.Text = "";
                    TextBox11.Text = "";
                    TextBox5.Text = "";
                    TextBox8.Text = "";      
                    TextBox10.Text = "";
                    getinvoiceno();
                    getComposition_no();
                    getQualifier_no();
                    BindData();
                    show_Division();
                    show_Descrition();
                    show_Colors();
                    show_CompositionName();
                    show_QualifierName();
                    show_ProductName();
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
        TextBox11.Text = "";
        TextBox5.Text = "";
        TextBox8.Text = "";
        TextBox10.Text = "";
        getinvoiceno();
        getComposition_no();
        getQualifier_no();
        BindData();
        show_Division();
        show_Descrition();
        show_Colors();
        show_CompositionName();
        show_QualifierName();
        show_ProductName();
        Button12.Visible = false;
        Button1.Visible = true;
        DropDownList5.Items.Clear();
        DropDownList5.Items.Insert(0, new ListItem("All", "0"));
        DropDownList5.Items.Insert(1, new ListItem("Yes", "1"));
        DropDownList5.Items.Insert(2, new ListItem("No", "2"));
        DropDownList3.Focus();

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
                cmd.CommandText = "select distinct Department_Name from ADD_Department where Com_Id=@Com_Id and " +
                "Department_Name like @Department_Name + '%'";
                cmd.Parameters.AddWithValue("@Department_Name", prefixText);
                cmd.Parameters.AddWithValue("@Com_Id", company_id);
                cmd.Connection = conn;
                conn.Open();
                List<string> customers = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(sdr["Department_Name"].ToString());
                    }
                }
                conn.Close();
                return customers;
            }
        }
    }

    //[System.Web.Script.Services.ScriptMethod()]
    //[System.Web.Services.WebMethod]

    //public static List<string> SearchMaterial_Name(string prefixText, int count)
    //{
    //    //using (SqlConnection conn = new SqlConnection())
    //    //{
    //    //    conn.ConnectionString = ConfigurationManager.AppSettings["connection"];

    //    //    using (SqlCommand cmd = new SqlCommand())
    //    //    {
    //    //        cmd.CommandText = "select distinct Material_name from Material_Entry where Com_Id=@Com_Id and " +
    //    //        "Material_name like @Material_name + '%'";
    //    //        cmd.Parameters.AddWithValue("@Material_name", prefixText);
    //    //        cmd.Parameters.AddWithValue("@Com_Id", company_id);
    //    //        cmd.Connection = conn;
    //    //        conn.Open();
    //    //        List<string> customers = new List<string>();
    //    //        using (SqlDataReader sdr = cmd.ExecuteReader())
    //    //        {
    //    //            while (sdr.Read())
    //    //            {
    //    //                customers.Add(sdr["Material_name"].ToString());
    //    //            }
    //    //        }
    //    //        conn.Close();
    //    //        return customers;
    //    //    }
    //    //}
    //}

    //protected void TextBox15_TextChanged(object sender, EventArgs e)
    //{

    //    //if (User.Identity.IsAuthenticated)
    //    //{
    //    //    SqlConnection con1000 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
    //    //    SqlCommand cmd1000 = new SqlCommand("select * from user_details where company_name='" + User.Identity.Name + "'", con1000);
    //    //    SqlDataReader dr1000;
    //    //    con1000.Open();
    //    //    dr1000 = cmd1000.ExecuteReader();
    //    //    if (dr1000.Read())
    //    //    {
    //    //        company_id = Convert.ToInt32(dr1000["com_id"].ToString());
    //    //        SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
    //    //        SqlCommand CMD = new SqlCommand("select * from Purchase_Return where Mobile_no='" + TextBox15.Text + "' and Com_Id='" + company_id + "' ", con1);
    //    //        DataTable dt1 = new DataTable();
    //    //        con1.Open();
    //    //        SqlDataAdapter da1 = new SqlDataAdapter(CMD);
    //    //        da1.Fill(dt1);
    //    //        GridView1.DataSource = dt1;
    //    //        GridView1.DataBind();
    //    //    }
    //    //    con1000.Close();
    //    //}
    //}

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
                SqlCommand CMD = new SqlCommand("select * from Fabric_Entry where Com_Id='" + company_id + "' ORDER BY Fab_Pro_id asc", con);
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
                SqlCommand cmd = new SqlCommand("delete from Fabric_Entry where Fab_Pro_id='" + row.Cells[1].Text + "' and Com_Id='" + company_id + "' ", con);
                cmd.ExecuteNonQuery();
                con.Close();

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Fabric Entry Deleted Successfully')", true);

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
                string query = "Select max(Fab_Pro_id) from Fabric_Entry where Com_Id='" + company_id + "'";
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


    private void getComposition_no()
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
                string query = "Select max(Composition_id) from ADD_Composition where Com_Id='" + company_id + "'";
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
    private void getQualifier_no()
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
                string query = "Select max(Quali_Comp_id) from ADD_Qualifier where Com_Id='" + company_id + "'";
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
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please Enter Composition Name')", true);
                }
                else
                {
                    SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd1 = new SqlCommand("select * from ADD_Composition where Composition_Name='" + TextBox9.Text + "' AND Com_Id='" + company_id + "'  ", con1);
                    con1.Open();
                    SqlDataReader dr1;
                    dr1 = cmd1.ExecuteReader();
                    if (dr1.HasRows)
                    {

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Composition Name already exist')", true);
                        TextBox9.Text = "";
                    }
                    else
                    {



                        SqlConnection CON = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                        SqlCommand cmd = new SqlCommand("insert into ADD_Composition values(@Composition_id,@Composition_Name,@Com_Id)", CON);
                        cmd.Parameters.AddWithValue("@Composition_id", Label7.Text);
                        cmd.Parameters.AddWithValue("@Composition_Name", HttpUtility.HtmlDecode(TextBox9.Text));
                        cmd.Parameters.AddWithValue("@Com_Id", company_id);
                        CON.Open();
                        cmd.ExecuteNonQuery();
                        CON.Close();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Composition Entry saved Successfully')", true);
                        getComposition_no();
                        BindData();
                        show_CompositionName();
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
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please Enter Company Name')", true);
                }
                else
                {
                    SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd1 = new SqlCommand("select * from ADD_Qualifier where Quali_Company_Name='" + TextBox14.Text + "' AND Com_Id='" + company_id + "'  ", con1);
                    con1.Open();
                    SqlDataReader dr1;
                    dr1 = cmd1.ExecuteReader();
                    if (dr1.HasRows)
                    {

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Company Name already exist')", true);
                        TextBox14.Text = "";
                    }
                    else
                    {



                        SqlConnection CON = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                        SqlCommand cmd = new SqlCommand("insert into ADD_Qualifier values(@Quali_Comp_id,@Quali_Company_Name,@Com_Id)", CON);
                        cmd.Parameters.AddWithValue("@Quali_Comp_id", Label10.Text);
                        cmd.Parameters.AddWithValue("@Quali_Company_Name", HttpUtility.HtmlDecode(TextBox14.Text));
                        cmd.Parameters.AddWithValue("@Com_Id", company_id);
                        CON.Open();
                        cmd.ExecuteNonQuery();
                        CON.Close();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Qualifier Entry saved Successfully')", true);
                        getQualifier_no();
                        BindData();
                        show_QualifierName();
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
                SqlCommand CMD = new SqlCommand("select * from Fabric_Entry where Product_Name='" + DropDownList2.SelectedItem.Text + "' and  Com_Id='" + company_id + "' ORDER BY Fab_Pro_id asc", con1);
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
                SqlCommand CMD = new SqlCommand("select * from Fabric_Entry where Division='" + TextBox15.Text + "' and  Com_Id='" + company_id + "' ORDER BY Fab_Pro_id asc", con1);
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
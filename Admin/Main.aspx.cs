
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


public partial class Admin_Main : System.Web.UI.Page
{

    public static int company_id = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            BindData();
            getinvoiceno();
            getMaterial_Cat_No();
            getMaterial_Name_No();
            getDescription_No();
            getDivision_No();
            show_Description();
            show_Division();
            show_category();
            show_MaterialName();


            active();
            created();
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
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton IMG = (ImageButton)sender;
        GridViewRow ROW = (GridViewRow)IMG.NamingContainer;
        Label16.Text = ROW.Cells[1].Text;
        DropDownList7.SelectedItem.Text = ROW.Cells[2].Text;
        DropDownList6.SelectedItem.Text = ROW.Cells[3].Text;
        DropDownList8.SelectedItem.Text = ROW.Cells[4].Text;
        TextBox7.Text = ROW.Cells[5].Text;
        TextBox8.Text = ROW.Cells[6].Text;
        DropDownList11.SelectedItem.Text = ROW.Cells[7].Text;
        TextBox10.Text = ROW.Cells[8].Text;
        TextBox13.Text = ROW.Cells[9].Text;
        TextBox15.Text = ROW.Cells[10].Text;
        this.ModalPopupExtender2.Show();

    }
    protected void Button9_Click(object sender, EventArgs e)
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
                SqlCommand cmd = new SqlCommand("update Material_Entry set Material_Category=@Material_Category,Material_name=@Material_name,Description=@Description,Model=@Model,Material_Code=@Material_Code,Material_Division=@Material_Division,Machine_details=@Machine_details,Colors=@Colors,Shades=@Shades where Material_id='" + Label16.Text + "' and Com_Id='" + company_id + "'  ", CON);
                cmd.Parameters.AddWithValue("@Material_Category", DropDownList6.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Material_name", DropDownList7.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Description", DropDownList8.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Model", TextBox7.Text);
                cmd.Parameters.AddWithValue("@Material_Code", TextBox8.Text);
                cmd.Parameters.AddWithValue("@Material_Division", DropDownList11.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Machine_details", TextBox10.Text);
                cmd.Parameters.AddWithValue("@Colors", TextBox13.Text);
                cmd.Parameters.AddWithValue("@Shades", TextBox15.Text);
                CON.Open();
                cmd.ExecuteNonQuery();
                CON.Close();
                this.ModalPopupExtender2.Hide();
                showcustomertype();
                show_category();
                BindData();
                getinvoiceno();
            }
            con1000.Close();
        }
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

                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd = new SqlCommand("delete from category where category_id='" + Label16.Text + "' and Com_Id='" + company_id + "' ", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Label18.Text = "Deleted successfuly";
                BindData();

                getinvoiceno();
            }
            con1000.Close();
        }


    }
    protected void Button1_Click(object sender, EventArgs e)
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
                if (DropDownList4.SelectedItem.Text == "All")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please enter Material Category')", true);
                }
                else
                {
                    SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd1 = new SqlCommand("select * from Material_Entry where Material_Code='" + TextBox14.Text + "' AND Com_Id='" + company_id + "'  ", con1);
                    con1.Open();
                    SqlDataReader dr1;
                    dr1 = cmd1.ExecuteReader();
                    if (dr1.HasRows)
                    {

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Material Code already exist')", true);
                        TextBox14.Text = "";
                    }
                    else
                    {



                        SqlConnection CON = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                        SqlCommand cmd = new SqlCommand("insert into Material_Entry values(@Material_id,@Material_name,@Material_Category,@Description,@Model,@Material_Code,@Material_Division,@Com_Id,@Machine_details,@Colors,@Shades)", CON);
                        cmd.Parameters.AddWithValue("@Material_id", Label1.Text);
                        cmd.Parameters.AddWithValue("@Material_name", HttpUtility.HtmlDecode(DropDownList3.SelectedItem.Text));
                        cmd.Parameters.AddWithValue("@Material_Category", HttpUtility.HtmlDecode(DropDownList4.SelectedItem.Text));
                        cmd.Parameters.AddWithValue("@Description", HttpUtility.HtmlDecode(DropDownList5.SelectedItem.Text));
                        cmd.Parameters.AddWithValue("@Model", HttpUtility.HtmlDecode(TextBox14.Text));
                        cmd.Parameters.AddWithValue("@Material_Code", HttpUtility.HtmlDecode(TextBox5.Text));
                        cmd.Parameters.AddWithValue("@Material_Division", HttpUtility.HtmlDecode(DropDownList10.SelectedItem.Text));
                        cmd.Parameters.AddWithValue("@Com_Id", company_id);
                        cmd.Parameters.AddWithValue("@Machine_details", HttpUtility.HtmlDecode(TextBox9.Text));
                        cmd.Parameters.AddWithValue("@Colors", HttpUtility.HtmlDecode(TextBox11.Text));
                        cmd.Parameters.AddWithValue("@Shades", HttpUtility.HtmlDecode(TextBox12.Text));
                        CON.Open();
                        cmd.ExecuteNonQuery();
                        CON.Close();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Material Entry Saved Successfully')", true);
                        BindData();
                        getinvoiceno();
                        getDescription_No();
                        getDivision_No();
                        show_Description();
                        show_category();
                        show_MaterialName();
                        show_Division();
                        DropDownList3.Items.Clear(); 
                        TextBox14.Text = "";
                        TextBox5.Text = "";
                        TextBox9.Text = "";
                        TextBox11.Text = "";
                        TextBox12.Text = "";
                        con1.Close();

                    }
                }
            }
            con1000.Close();
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        BindData();
        getinvoiceno();
        getMaterial_Cat_No();
        getMaterial_Name_No();
        getDescription_No();
        getDivision_No();
        show_Description();
        show_category();
        show_MaterialName();
        show_Division();
        DropDownList4.Items.Clear(); 
        TextBox14.Text = "";
        TextBox5.Text = "";
        TextBox9.Text = "";
        TextBox11.Text = "";
        TextBox12.Text = "";
    }
    private void active()
    {

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
                SqlCommand CMD = new SqlCommand("select * from Material_Entry where Com_Id='" + company_id + "' ORDER BY Material_id asc", con);
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
                SqlCommand cmd = new SqlCommand("delete from Material_Entry where Material_id='" + row.Cells[1].Text + "' and Com_Id='" + company_id + "' ", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Material Entry deleted successfully')", true);

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

                SqlConnection con1 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["connection"]);
                con1.Open();
                string query = "Select Max(Material_id) from Material_Entry where Com_Id='" + company_id + "' ";
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
    private void getMaterial_Cat_No()
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

                SqlConnection con1 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["connection"]);
                con1.Open();
                string query = "Select Max(Mat_Cat_id) from ADD_Material_Category where Com_Id='" + company_id + "' ";
                SqlCommand cmd1 = new SqlCommand(query, con1);
                SqlDataReader dr = cmd1.ExecuteReader();
                if (dr.Read())
                {
                    string val = dr[0].ToString();
                    if (val == "")
                    {
                        Label29.Text = "1";
                    }
                    else
                    {
                        a = Convert.ToInt32(dr[0].ToString());
                        a = a + 1;
                        Label29.Text = a.ToString();
                    }
                }
            }
            con1000.Close();
        }
    }
    private void getMaterial_Name_No()
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

                SqlConnection con1 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["connection"]);
                con1.Open();
                string query = "Select Max(Mat_Name_id) from ADD_Material_Name where Com_Id='" + company_id + "' ";
                SqlCommand cmd1 = new SqlCommand(query, con1);
                SqlDataReader dr = cmd1.ExecuteReader();
                if (dr.Read())
                {
                    string val = dr[0].ToString();
                    if (val == "")
                    {
                        Label4.Text = "1";
                    }
                    else
                    {
                        a = Convert.ToInt32(dr[0].ToString());
                        a = a + 1;
                        Label4.Text = a.ToString();
                    }
                }
            }
            con1000.Close();
        }
    }

    private void getDescription_No()
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

                SqlConnection con1 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["connection"]);
                con1.Open();
                string query = "Select Max(Desc_id) from ADD_Description where Com_Id='" + company_id + "' ";
                SqlCommand cmd1 = new SqlCommand(query, con1);
                SqlDataReader dr = cmd1.ExecuteReader();
                if (dr.Read())
                {
                    string val = dr[0].ToString();
                    if (val == "")
                    {
                        Label8.Text = "1";
                    }
                    else
                    {
                        a = Convert.ToInt32(dr[0].ToString());
                        a = a + 1;
                        Label8.Text = a.ToString();
                    }
                }
            }
            con1000.Close();
        }
    }
    private void getDivision_No()
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

                SqlConnection con1 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["connection"]);
                con1.Open();
                string query = "Select Max(Dept_id) from ADD_Department where Com_Id='" + company_id + "' ";
                SqlCommand cmd1 = new SqlCommand(query, con1);
                SqlDataReader dr = cmd1.ExecuteReader();
                if (dr.Read())
                {
                    string val = dr[0].ToString();
                    if (val == "")
                    {
                        Label22.Text = "1";
                    }
                    else
                    {
                        a = Convert.ToInt32(dr[0].ToString());
                        a = a + 1;
                        Label22.Text = a.ToString();
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
                SqlCommand cmd = new SqlCommand("Select * from ADD_Material_Name where Com_Id='" + company_id + "' ORDER BY Mat_Name_id  asc", con);
                con.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);


                DropDownList3.DataSource = ds;
                DropDownList3.DataTextField = "Material_Name";
                DropDownList3.DataValueField = "Mat_Name_id";
                DropDownList3.DataBind();
                DropDownList3.Items.Insert(0, new ListItem("All", "0"));

                DropDownList2.DataSource = ds;
                DropDownList2.DataTextField = "Material_Name";
                DropDownList2.DataValueField = "Mat_Name_id";
                DropDownList2.DataBind();
                DropDownList2.Items.Insert(0, new ListItem("All", "0"));

             

                DropDownList7.DataSource = ds;
                DropDownList7.DataTextField = "Material_Name";
                DropDownList7.DataValueField = "Mat_Name_id";
                DropDownList7.DataBind();
                DropDownList7.Items.Insert(0, new ListItem("All", "0"));

                DropDownList9.DataSource = ds;
                DropDownList9.DataTextField = "Material_Name";
                DropDownList9.DataValueField = "Mat_Name_id";
                DropDownList9.DataBind();
                DropDownList9.Items.Insert(0, new ListItem("All", "0"));

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
                SqlCommand cmd = new SqlCommand("Select * from ADD_Material_Category where Com_Id='" + company_id + "' ORDER BY Mat_Cat_id  asc", con);
                con.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                DropDownList1.DataSource = ds;
                DropDownList1.DataTextField = "Material_Category";
                DropDownList1.DataValueField = "Mat_Cat_id";
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0, new ListItem("All", "0"));

                //DropDownList3.DataSource = ds;
                //DropDownList3.DataTextField = "Material_name";
                //DropDownList3.DataValueField = "Mat_Cat_id";
                //DropDownList3.DataBind();
                //DropDownList3.Items.Insert(0, new ListItem("All", "0"));

           

                DropDownList6.DataSource = ds;
                DropDownList6.DataTextField = "Material_Category";
                DropDownList6.DataValueField = "Mat_Cat_id";
                DropDownList6.DataBind();
                DropDownList6.Items.Insert(0, new ListItem("All", "0"));

                con.Close();
            }
            con1000.Close();
        }
    }
    private void show_Description()
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
                SqlCommand cmd = new SqlCommand("Select * from ADD_Description where Com_Id='" + company_id + "' ORDER BY Desc_id  asc", con);
                con.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);


                DropDownList5.DataSource = ds;
                DropDownList5.DataTextField = "Description";
                DropDownList5.DataValueField = "Desc_id";
                DropDownList5.DataBind();
                DropDownList5.Items.Insert(0, new ListItem("All", "0"));

                DropDownList8.DataSource = ds;
                DropDownList8.DataTextField = "Description";
                DropDownList8.DataValueField = "Desc_id";
                DropDownList8.DataBind();
                DropDownList8.Items.Insert(0, new ListItem("All", "0"));

                con.Close();
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
                SqlCommand cmd = new SqlCommand("Select * from ADD_Department where Com_Id='" + company_id + "' ORDER BY Dept_id  asc", con);
                con.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);


                DropDownList10.DataSource = ds;
                DropDownList10.DataTextField = "Department_Name";
                DropDownList10.DataValueField = "Dept_id";
                DropDownList10.DataBind();
                DropDownList10.Items.Insert(0, new ListItem("All", "0"));

                DropDownList11.DataSource = ds;
                DropDownList11.DataTextField = "Department_Name";
                DropDownList11.DataValueField = "Dept_id";
                DropDownList11.DataBind();
                DropDownList11.Items.Insert(0, new ListItem("All", "0"));

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
    protected void Button11_Click(object sender, EventArgs e)
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
                    CheckBox chkdelete = (CheckBox)gvrow.FindControl("CheckBox2");
                    //Condition to check checkbox selected or not
                    if (chkdelete.Checked)
                    {
                        //Getting UserId of particular row using datakey value
                        int usrid = Convert.ToInt32(gvrow.Cells[1].Text);
                        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);

                        con.Open();
                        SqlCommand cmd = new SqlCommand("delete from Material_Entry where Material_id='" + usrid + "' and Com_Id='" + company_id + "'", con);
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
    private void showcustomertype()
    {

    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]

    public static List<string> SearchCustomers2(string prefixText, int count)
    {


        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.AppSettings["connection"];

            using (SqlCommand cmd = new SqlCommand())
            {


                cmd.CommandText = "select Material_Category from Material_Entry where  Com_Id=@Com_Id and  " +
                "Material_Category like @Material_Category + '%' ";
                cmd.Parameters.AddWithValue("@Material_Category", prefixText);
                cmd.Parameters.AddWithValue("@Com_Id", company_id);
                cmd.Connection = conn;
                conn.Open();
                List<string> customers = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(sdr["Material_Category"].ToString());
                    }
                }
                conn.Close();
                return customers;
            }
        }
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
    protected void TextBox3_TextChanged(object sender, EventArgs e)
    {

    }
    protected void TextBox1_TextChanged(object sender, EventArgs e)
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
                SqlCommand CMD = new SqlCommand("select * from Material_Entry where Material_Category='" + TextBox1.Text + "' and Com_Id='" + company_id + "' ", con1);
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
    protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        this.ModalPopupExtender3.Show();
    }
    protected void Button16_Click(object sender, EventArgs e)
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
                if (TextBox2.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please enter Material category')", true);
                }
                else
                {
                    SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd1 = new SqlCommand("select * from ADD_Material_Category where Material_Category='" + TextBox2.Text + "' AND Com_Id='" + company_id + "'  ", con1);
                    con1.Open();
                    SqlDataReader dr1;
                    dr1 = cmd1.ExecuteReader();
                    if (dr1.HasRows)
                    {

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Material Category already exist')", true);
                        TextBox2.Text = "";
                    }
                    else
                    {



                        SqlConnection CON = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                        SqlCommand cmd = new SqlCommand("insert into ADD_Material_Category values(@Mat_Cat_id,@Material_Category,@Com_Id,@Material_Name)", CON);
                        cmd.Parameters.AddWithValue("@Mat_Cat_id", Label29.Text);
                        cmd.Parameters.AddWithValue("@Material_Category", HttpUtility.HtmlDecode(TextBox2.Text));
                        cmd.Parameters.AddWithValue("@Com_Id", company_id);
                        cmd.Parameters.AddWithValue("@Material_Name", HttpUtility.HtmlDecode(DropDownList9.SelectedItem.Text));
                        CON.Open();
                        cmd.ExecuteNonQuery();
                        CON.Close();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Material Category created successfully')", true);
                        BindData();
                        show_category();
                        getMaterial_Cat_No();
                        TextBox2.Text = "";

                        con1.Close();

                    }
                }
            }
            con1000.Close();
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        this.ModalPopupExtender1.Show();
    }
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
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
                SqlCommand cmd = new SqlCommand("Select * from ADD_Material_Category where Material_Name='" + DropDownList3.SelectedItem.Text + "' AND Com_Id='" + company_id + "' ORDER BY Mat_Cat_id  asc", con);
                con.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);


                DropDownList4.DataSource = ds;
                DropDownList4.DataTextField = "Material_Category";
                DropDownList4.DataValueField = "Mat_Cat_id";
                DropDownList4.DataBind();
                DropDownList4.Items.Insert(0, new ListItem("All", "0"));

                con.Close();
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
                if (TextBox3.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please enter Material Nmae')", true);
                }
                else
                {
                    SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd1 = new SqlCommand("select * from ADD_Material_Name where Material_Name='" + TextBox3.Text + "' AND Com_Id='" + company_id + "'  ", con1);
                    con1.Open();
                    SqlDataReader dr1;
                    dr1 = cmd1.ExecuteReader();
                    if (dr1.HasRows)
                    {

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Material Name already exist')", true);
                         TextBox3.Text = "";
                    }
                    else
                    {



                        SqlConnection CON = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                        SqlCommand cmd = new SqlCommand("insert into ADD_Material_Name values(@Mat_Name_id,@Material_Name,@Com_Id)", CON);
                        cmd.Parameters.AddWithValue("@Mat_Name_id", Label4.Text);
                        cmd.Parameters.AddWithValue("@Material_Name", HttpUtility.HtmlDecode(TextBox3.Text));
                        cmd.Parameters.AddWithValue("@Com_Id", company_id);                    
                        CON.Open();
                        cmd.ExecuteNonQuery();
                        CON.Close();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Material Name created successfully')", true);
                        BindData();
                        getMaterial_Name_No();
                        show_MaterialName();
                        show_category();
                        TextBox3.Text = "";
                        con1.Close();

                    }
                }
            }
            con1000.Close();
        }
    }
    protected void DropDownList5_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Button13_Click(object sender, EventArgs e)
    {
        this.ModalPopupExtender4.Show();
    }
    protected void Button18_Click(object sender, EventArgs e)
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
                if (TextBox4.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please enter Description')", true);
                }
                else
                {
                    SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd1 = new SqlCommand("select * from ADD_Description where Description='" + TextBox4.Text + "' AND Com_Id='" + company_id + "'  ", con1);
                    con1.Open();
                    SqlDataReader dr1;
                    dr1 = cmd1.ExecuteReader();
                    if (dr1.HasRows)
                    {

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Description already exist')", true);
                        TextBox4.Text = "";
                    }
                    else
                    {

                        SqlConnection CON = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                        SqlCommand cmd = new SqlCommand("insert into ADD_Description values(@Desc_id,@Description,@Com_Id)", CON);
                        cmd.Parameters.AddWithValue("@Desc_id", Label8.Text);
                        cmd.Parameters.AddWithValue("@Description", HttpUtility.HtmlDecode(TextBox4.Text));
                        cmd.Parameters.AddWithValue("@Com_Id", company_id);
                        CON.Open();
                        cmd.ExecuteNonQuery();
                        CON.Close();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Description created successfully')", true);
                        BindData();
                        getDescription_No();
                        show_Description();
                        TextBox4.Text = "";
                        con1.Close();

                    }
                }
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

                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand CMD = new SqlCommand("select * from Material_Entry where Material_Category='" + DropDownList2.SelectedItem.Text + "'  and Com_Id='" + company_id + "' ORDER BY Material_id asc", con);
                DataTable dt1 = new DataTable();
                SqlDataAdapter da1 = new SqlDataAdapter(CMD);
                da1.Fill(dt1);
                GridView1.DataSource = dt1;
                GridView1.DataBind();
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

                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand CMD = new SqlCommand("select * from Material_Entry where Material_name='" + DropDownList2.SelectedItem.Text + "' and Material_Category='" + DropDownList1.SelectedItem.Text + "' AND Com_Id='" + company_id + "' ORDER BY Material_id asc", con);
                DataTable dt1 = new DataTable();
                SqlDataAdapter da1 = new SqlDataAdapter(CMD);
                da1.Fill(dt1);
                GridView1.DataSource = dt1;
                GridView1.DataBind();
            }
            con1000.Close();
        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        this.ModalPopupExtender5.Show();
    }
    protected void Button21_Click(object sender, EventArgs e)
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
                if (TextBox6.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Material Division enter Description')", true);
                }
                else
                {
                    SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd1 = new SqlCommand("select * from ADD_Department where Department_Name='" + TextBox6.Text + "' AND Com_Id='" + company_id + "'  ", con1);
                    con1.Open();
                    SqlDataReader dr1;
                    dr1 = cmd1.ExecuteReader();
                    if (dr1.HasRows)
                    {

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Material Division already exist')", true);
                        TextBox6.Text = "";
                    }
                    else
                    {

                        SqlConnection CON = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                        SqlCommand cmd = new SqlCommand("insert into ADD_Department values(@Dept_id,@Department_Name,@Com_Id)", CON);
                        cmd.Parameters.AddWithValue("@Dept_id", Label22.Text);
                        cmd.Parameters.AddWithValue("@Department_Name", HttpUtility.HtmlDecode(TextBox6.Text));
                        cmd.Parameters.AddWithValue("@Com_Id", company_id);
                        CON.Open();
                        cmd.ExecuteNonQuery();
                        CON.Close();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Material Division created successfully')", true);
                        BindData();
                        show_Division();
                        getDivision_No();
                        TextBox6.Text = "";
                        con1.Close();

                    }
                }
            }
            con1000.Close();
        }
    }
}
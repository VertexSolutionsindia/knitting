﻿
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
using System.Security.Permissions;
#endregion

public partial class Admin_Sales_entry_wholesales : System.Web.UI.Page
{
    float tot = 0;
    float tot1 = 0;
    DataTable dt = null;
    public static int company_id = 0;
    public static string company_id1 = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {


            SqlConnection con11 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd11 = new SqlCommand("select * from currentfinancialyear where no='1'", con11);
            SqlDataReader dr11;
            con11.Open();
            dr11 = cmd11.ExecuteReader();
            if (dr11.Read())
            {
                Label11.Text = dr11["financial_year"].ToString();

            }
            con11.Close();
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneInfo);
            DateTime date = now;
            TextBox8.Text = Convert.ToDateTime(date).ToString("dd-MM-yyyy");
            TextBox8.Attributes.Add("onkeypress", "return controlEnter('" + TextBox13.ClientID + "', event)");
            TextBox13.Attributes.Add("onkeypress", "return controlEnter('" + TextBox14.ClientID + "', event)");
            TextBox14.Attributes.Add("onkeypress", "return controlEnter('" + DropDownList3.ClientID + "', event)");
            TextBox12.Attributes.Add("onkeypress", "return controlEnter('" + TextBox17.ClientID + "', event)");
            TextBox17.Attributes.Add("onkeypress", "return controlEnter('" + TextBox5.ClientID + "', event)");
            TextBox5.Attributes.Add("onkeypress", "return controlEnter('" + TextBox15.ClientID + "', event)");

            getinvoiceno1();
            getinvoiceno();
            show_category();
            showrating();
            BindData();
       
            show_tax();
            getinvoicenoid();
            show_type();
            active();
            created();
            getinvoiceno1();
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
            SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            con1.Open();
            string query = "Select * from Company_detail  where com_id='" + company_id + "'";
            SqlCommand cmd1 = new SqlCommand(query, con1);
            SqlDataReader dr = cmd1.ExecuteReader();
            if (dr.Read())
            {

                company_id1 = dr["Address"].ToString();

            }
            con1.Close();
            BindData2();

        }
    }
   //A method that returns a string which calls the connection string from the web.config
    private string GetConnectionString()
    {
        //"DBConnection" is the name of the Connection String
        //that was set up from the web.config file
        return System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
    }
    private void getinvoiceno()
    {
        int a;
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


        SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        con1.Open();
        string query = "Select max(convert(int,SubString(invoice_no,PATINDEX('%[0-9]%',invoice_no),Len(invoice_no)))) from sales_credit_entry where Com_Id='" + company_id + "' and year='"+Label11.Text+"'";
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
                TextBox1.Text = a.ToString();
                a = a + 1;
                Label1.Text = a.ToString();
            }
        }
    }
    private void show_type()
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
        SqlCommand cmd = new SqlCommand("Select * from customer_type where Com_Id='" + company_id + "' ORDER BY type_id asc", con);
        con.Open();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);


        DropDownList1.DataSource = ds;
        DropDownList1.DataTextField = "type_name";
        DropDownList1.DataValueField = "type_id";
        DropDownList1.DataBind();
        DropDownList1.Items.Insert(0, new ListItem("All", "0"));

        con.Close();
    }
    #region " [ Button Event ] "
    protected void Button13_Click(object sender, EventArgs e)
    {
        // select appropriate contenttype, while binary transfer it identifies filetype
        string contentType = string.Empty;
        if (DropDownList5.SelectedValue.Equals(".pdf"))
            contentType = "application/pdf";
        if (DropDownList5.SelectedValue.Equals(".doc"))
            contentType = "application/ms-word";
        if (DropDownList5.SelectedValue.Equals(".xls"))
            contentType = "application/xls";

        DataTable dsData = new DataTable();

        DataSet ds = null;
        SqlDataAdapter da = null;



        try
        {
            string constring = ConfigurationManager.AppSettings["connection"];
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("invoice_credit", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@No", Convert.ToInt32(TextBox1.Text));
                    cmd.Parameters.AddWithValue("@Com_Id", Convert.ToInt32(company_id));
                    cmd.Parameters.AddWithValue("@year", Label11.Text);
                    da = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    con.Open();
                    da.Fill(ds);
                    con.Close();

                }
            }
        }
        catch
        {
            throw;
        }



        dsData = ds.Tables[0];

        string FileName = "File_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + DropDownList5.SelectedValue;
        string extension;
        string encoding;
        string mimeType;
        string[] streams;
        Warning[] warnings;

        LocalReport report = new LocalReport();
        report.ReportPath = Server.MapPath("~/Admin/Report2.rdlc");
        ReportDataSource rds = new ReportDataSource();
        rds.Name = "DataSet1";//This refers to the dataset name in the RDLC file
        rds.Value = dsData;
        report.DataSources.Add(rds);

        Byte[] mybytes = report.Render(DropDownList5.SelectedItem.Text, null,
                        out extension, out encoding,
                        out mimeType, out streams, out warnings); //for exporting to PDF
        using (FileStream fs = File.Create(Server.MapPath("~/img/") + FileName))
        {
            fs.Write(mybytes, 0, mybytes.Length);
        }

        Response.ClearHeaders();
        Response.ClearContent();
        Response.Buffer = true;
        Response.Clear();
        Response.ContentType = contentType;
        Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName);
        Response.WriteFile(Server.MapPath("~/img/" + FileName));
        Response.Flush();
        Response.Close();
        Response.End();





    }
    #endregion
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (TextBox13.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please enter customer name')", true);
        }
         else if (DropDownList3.SelectedItem.Text == "All")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please select staff')", true);
        }
         else if (TextBox2.Text == "")
         {
             ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please add products')", true);
         }
        else if (TextBox7.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please give paid amount')", true);
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

                     SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                     SqlCommand cmd1 = new SqlCommand("select * from sales_credit_entry where invoice_no='" + Label1.Text + "' and Com_Id='" + company_id + "' and year='" + Label11.Text + "' ", con1);
                     con1.Open();
                     SqlDataReader dr1;
                     dr1 = cmd1.ExecuteReader();
                     if (dr1.HasRows)
                     {

                         string ststus = "Credit Bill";
                         float value = 0;
                         SqlConnection CON = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                         SqlCommand cmd = new SqlCommand("update sales_credit_entry set date=@date,customer_name=@customer_name,customer_Address=@customer_Address,Mobile_no=@Mobile_no,staff_name=@staff_name,total_qty=@total_qty,total_amount=@total_amount,grand_total=@grand_total,paid_amount=@paid_amount,Pending_amount=@Pending_amount,status=@status,value=@value,Com_Id=@Com_Id,dis_per=@dis_per,discount_amount=@discount_amount where invoice_no=@invoice_no and Com_Id='" + company_id + "' and year='" + Label11.Text + "' ", CON);
                         cmd.Parameters.AddWithValue("@invoice_no", Label1.Text);
                         cmd.Parameters.AddWithValue("@date", Convert.ToDateTime(TextBox8.Text).ToString("MM-dd-yyyy"));
                         cmd.Parameters.AddWithValue("@customer_name", TextBox13.Text);
                         cmd.Parameters.AddWithValue("@customer_Address", TextBox14.Text);
                         cmd.Parameters.AddWithValue("@Mobile_no", TextBox6.Text);
                         cmd.Parameters.AddWithValue("@staff_name", DropDownList3.SelectedItem.Text);
                         cmd.Parameters.AddWithValue("@total_qty", TextBox2.Text);
                         cmd.Parameters.AddWithValue("@total_amount", float.Parse(TextBox10.Text));
                         cmd.Parameters.AddWithValue("@grand_total", float.Parse(TextBox11.Text));
                         cmd.Parameters.AddWithValue("@paid_amount", float.Parse(TextBox7.Text));
                         cmd.Parameters.AddWithValue("@Pending_amount", float.Parse(TextBox9.Text));
                         cmd.Parameters.AddWithValue("@status", ststus);
                         cmd.Parameters.AddWithValue("@value", value);
                         cmd.Parameters.AddWithValue("@Com_Id", company_id);
                         cmd.Parameters.AddWithValue("@dis_per", TextBox23.Text);
                         cmd.Parameters.AddWithValue("@discount_amount", TextBox26.Text);

                         CON.Open();
                         cmd.ExecuteNonQuery();
                         CON.Close();


                         float b11 = 0;
                         float f11 = 0;
                         float c11 = 0;

                         SqlConnection con100 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["connection"]);
                         SqlCommand check_User_Name100 = new SqlCommand("SELECT * FROM receive_amount_status WHERE Buyer = @Buyer and Com_Id='" + company_id + "' and year='" + Label11.Text + "' ", con100);
                         check_User_Name100.Parameters.AddWithValue("@Buyer", TextBox13.Text);
                         con100.Open();
                         SqlDataReader reader100 = check_User_Name100.ExecuteReader();
                         if (reader100.HasRows)
                         {
                             SqlConnection con11 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["connection"]);
                             SqlCommand cmd11 = new SqlCommand("Select * from receive_amount where Buyer='" + TextBox13.Text + "' and invoice_no='" + Label1.Text + "'  and Com_Id='" + company_id + "' and year='" + Label11.Text + "'", con11);
                             con11.Open();
                             SqlDataReader dr11;
                             dr11 = cmd11.ExecuteReader();
                             if (dr11.Read())
                             {

                                 b11 = float.Parse(dr11["pending_amount"].ToString());






                                 SqlConnection con27 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["connection"]);
                                 SqlCommand cd27 = new SqlCommand("update receive_amount_status set pending_amount=pending_amount-@pending_amount where Buyer='" + TextBox13.Text + "' and Com_Id='" + company_id + "' and year='" + Label11.Text + "'", con27);
                                 cd27.Parameters.AddWithValue("@pending_amount", b11);
                                 con27.Open();
                                 cd27.ExecuteNonQuery();
                                 con27.Close();

                                 SqlConnection con272 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["connection"]);
                                 SqlCommand cd272 = new SqlCommand("update receive_amount set pending_amount=pending_amount-@pending_amount,outstanding=outstanding-@outstanding where Buyer='" + TextBox13.Text + "' and  invoice_no='" + Label1.Text + "' and Com_Id='" + company_id + "' and year='" + Label11.Text + "' ", con272);
                                 cd272.Parameters.AddWithValue("@pending_amount", b11);
                                 cd272.Parameters.AddWithValue("@outstanding", b11);
                                 con272.Open();
                                 cd272.ExecuteNonQuery();
                                 con272.Close();

                                 SqlConnection con271 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["connection"]);
                                 SqlCommand cd271 = new SqlCommand("update receive_amount_status set pending_amount=pending_amount+@pending_amount where Buyer='" + TextBox13.Text + "' and Com_Id='" + company_id + "' and year='" + Label11.Text + "'", con271);
                                 cd271.Parameters.AddWithValue("@pending_amount", float.Parse(TextBox9.Text));
                                 con271.Open();
                                 cd271.ExecuteNonQuery();
                                 con271.Close();




                                 string status1 = "Bill";
                                 SqlConnection con26 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["connection"]);
                                 SqlCommand cmd26 = new SqlCommand("update receive_amount set Estimate_value=@Estimate_value,address=@address,total_amount=@total_amount,pay_amount=@pay_amount,pending_amount=@pending_amount,outstanding=outstanding+@outstanding,status=@status where Buyer='" + TextBox13.Text + "' AND invoice_no='" + Label1.Text + "' and year='" + Label11.Text + "'", con26);


                                 cmd26.Parameters.AddWithValue("@Estimate_value", float.Parse(TextBox11.Text));
                                 cmd26.Parameters.AddWithValue("@address", TextBox13.Text);

                                 cmd26.Parameters.AddWithValue("@total_amount", float.Parse(TextBox11.Text));
                                 cmd26.Parameters.AddWithValue("@pay_amount", float.Parse(TextBox7.Text));
                                 cmd26.Parameters.AddWithValue("@pending_amount", float.Parse(TextBox9.Text));
                                 cmd26.Parameters.AddWithValue("@outstanding", float.Parse(TextBox9.Text));
                                 cmd26.Parameters.AddWithValue("@status", status1);


                                 con26.Open();
                                 cmd26.ExecuteNonQuery();
                                 con26.Close();



                             }
                             con11.Close();
                         }

                         con100.Close();

                         ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Cash Sales entry updated successfully')", true);
                         TextBox1.Text = Label1.Text;
                         show_category();
                         getinvoiceno();
                         getinvoiceno1();
                         BindData();
                         BindData2();


                         TextBox2.Text = "";
                         TextBox12.Text = "";
                         TextBox10.Text = "";
                         TextBox11.Text = "";
                         TextBox26.Text = "";
                         TextBox13.Text = "";
                         TextBox14.Text = "";
                         TextBox11.Text = "";
                         TextBox7.Text = "";
                         TextBox9.Text = "";
                         TextBox6.Text = "";
                         var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                         var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneInfo);
                         DateTime date = now;
                         TextBox8.Text = Convert.ToDateTime(date).ToString("dd-MM-yyyy");
                         TextBox23.Text = "";

                     }
                     else
                     {

                         string ststus = "Credit Bill";
                         float value = 0;
                         SqlConnection CON = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                         SqlCommand cmd = new SqlCommand("insert into sales_credit_entry values(@invoice_no,@date,@customer_name,@customer_Address,@Mobile_no,@staff_name,@total_qty,@total_amount,@grand_total,@paid_amount,@Pending_amount,@status,@value,@Com_Id,@dis_per,@discount_amount,@year)", CON);
                         cmd.Parameters.AddWithValue("@invoice_no", Label1.Text);
                         cmd.Parameters.AddWithValue("@date",Convert.ToDateTime(TextBox8.Text).ToString("MM-dd-yyyy"));
                         cmd.Parameters.AddWithValue("@customer_name", TextBox13.Text);
                         cmd.Parameters.AddWithValue("@customer_Address", TextBox14.Text);
                         cmd.Parameters.AddWithValue("@Mobile_no", TextBox6.Text);
                         cmd.Parameters.AddWithValue("@staff_name", DropDownList3.SelectedItem.Text);
                         cmd.Parameters.AddWithValue("@total_qty", TextBox2.Text);
                         cmd.Parameters.AddWithValue("@total_amount", TextBox10.Text);
                         cmd.Parameters.AddWithValue("@grand_total", TextBox11.Text);
                         cmd.Parameters.AddWithValue("@paid_amount", TextBox7.Text);
                         cmd.Parameters.AddWithValue("@Pending_amount", TextBox9.Text);
                         cmd.Parameters.AddWithValue("@status", ststus);
                         cmd.Parameters.AddWithValue("@value", value);
                         cmd.Parameters.AddWithValue("@Com_Id", company_id);

                         cmd.Parameters.AddWithValue("@dis_per", TextBox23.Text);
                         cmd.Parameters.AddWithValue("@discount_amount", TextBox26.Text);
                         cmd.Parameters.AddWithValue("@year", Label11.Text);
                         CON.Open();
                         cmd.ExecuteNonQuery();
                         CON.Close();


                         int a111 = 0;
                         float b11 = 0;
                         float f11 = 0;
                         float c11 = 0;
                         SqlConnection con100 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["connection"]);
                         SqlCommand cmd100 = new SqlCommand("SELECT * FROM receive_amount_status WHERE Buyer = @Buyer and Com_Id='" + company_id + "' and year='" + Label11.Text + "'", con100);
                         cmd100.Parameters.AddWithValue("@Buyer", TextBox13.Text);
                         con100.Open();
                         SqlDataReader reader1 = cmd100.ExecuteReader();
                         if (reader1.HasRows)
                         {
                             SqlConnection con11 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["connection"]);
                             SqlCommand cmd11 = new SqlCommand("Select * from receive_amount_status where Buyer='" + TextBox13.Text + "' and  Com_Id='" + company_id + "' and year='" + Label11.Text + "'", con11);
                             con11.Open();
                             SqlDataReader dr11;
                             dr11 = cmd11.ExecuteReader();
                             if (dr11.Read())
                             {

                                 b11 = float.Parse(dr11["pending_amount"].ToString());


                                 f11 = float.Parse(TextBox9.Text);

                                 c11 = (b11 + f11);





                                 string status1 = "Bill";
                                 SqlConnection con24 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["connection"]);
                                 SqlCommand cmd24 = new SqlCommand("insert into receive_amount values(@Buyer,@Pay_date,@Estimate_value,@address,@total_amount,@pay_amount,@pending_amount,@outstanding,@invoice_no,@Com_Id,@status,@year)", con24);
                                 cmd24.Parameters.AddWithValue("@Buyer", TextBox13.Text);
                                 cmd24.Parameters.AddWithValue("@pay_date",Convert.ToDateTime(TextBox8.Text).ToString("MM-dd-yyyy"));
                                 cmd24.Parameters.AddWithValue("@Estimate_value", float.Parse(TextBox11.Text));
                                 cmd24.Parameters.AddWithValue("@address", TextBox14.Text);

                                 cmd24.Parameters.AddWithValue("@total_amount", float.Parse(string.Format("{0:0.00}", TextBox11.Text)));
                                 cmd24.Parameters.AddWithValue("@pay_amount", float.Parse(TextBox7.Text));
                                 cmd24.Parameters.AddWithValue("@pending_amount", float.Parse(string.Format("{0:0.00}", TextBox9.Text)));
                                 cmd24.Parameters.AddWithValue("@outstanding", float.Parse(string.Format("{0:0.00}", c11)));

                                 cmd24.Parameters.AddWithValue("@invoice_no", Label1.Text);
                                 cmd24.Parameters.AddWithValue("@Com_Id", company_id);
                                 cmd24.Parameters.AddWithValue("@status", status1);
                                 cmd24.Parameters.AddWithValue("@year", Label11.Text);
                                 con24.Open();
                                 cmd24.ExecuteNonQuery();
                                 con24.Close();


                                 SqlConnection con23 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["connection"]);
                                 SqlCommand cmd23 = new SqlCommand("update receive_amount_status set address=@address,total_amount=total_amount+@total_amount,pending_amount=pending_amount+@pending_amount where Buyer='" + TextBox13.Text + "' and Com_Id='" + company_id + "' and year='" + Label11.Text + "' ", con23);

                                 cmd23.Parameters.AddWithValue("@address", TextBox14.Text);

                                 cmd23.Parameters.AddWithValue("@total_amount", float.Parse(string.Format("{0:0.00}", TextBox11.Text)));

                                 cmd23.Parameters.AddWithValue("@pending_amount", float.Parse(string.Format("{0:0.00}", TextBox9.Text)));

                                 con23.Open();
                                 cmd23.ExecuteNonQuery();
                                 con23.Close();


                             }

                             con11.Close();






                         }
                         else
                         {

                             string status1 = "Bill";
                             SqlConnection con23 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["connection"]);
                             SqlCommand cmd23 = new SqlCommand("insert into receive_amount_status values(@Buyer,@address,@total_amount,@pending_amount,@paid_amount,@Com_Id,@year)", con23);
                             cmd23.Parameters.AddWithValue("@Buyer", TextBox13.Text);
                             cmd23.Parameters.AddWithValue("@address", TextBox14.Text);

                             cmd23.Parameters.AddWithValue("@total_amount", float.Parse(string.Format("{0:0.00}", TextBox11.Text)));

                             cmd23.Parameters.AddWithValue("@pending_amount", float.Parse(string.Format("{0:0.00}", TextBox9.Text)));
                             cmd23.Parameters.AddWithValue("@paid_amount", float.Parse(TextBox7.Text));
                             cmd23.Parameters.AddWithValue("@Com_Id", company_id);
                             cmd23.Parameters.AddWithValue("@year", Label11.Text);
                             con23.Open();
                             cmd23.ExecuteNonQuery();
                             con23.Close();
                             string return_by = "";
                             int value1 = 0;
                             SqlConnection con24 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["connection"]);
                             SqlCommand cmd24 = new SqlCommand("insert into receive_amount values(@Buyer,@Pay_date,@Estimate_value,@address,@total_amount,@pay_amount,@pending_amount,@outstanding,@invoice_no,@Com_Id,@status,@year)", con24);
                             cmd24.Parameters.AddWithValue("@Buyer", TextBox13.Text);
                             cmd24.Parameters.AddWithValue("@pay_date",Convert.ToDateTime(TextBox8.Text).ToString("MM-dd-yyyy"));
                             cmd24.Parameters.AddWithValue("@Estimate_value", float.Parse(TextBox11.Text));
                             cmd24.Parameters.AddWithValue("@address", TextBox14.Text);

                             cmd24.Parameters.AddWithValue("@total_amount", float.Parse(string.Format("{0:0.00}", TextBox11.Text)));
                             cmd24.Parameters.AddWithValue("@pay_amount", float.Parse(TextBox7.Text));
                             cmd24.Parameters.AddWithValue("@pending_amount", float.Parse(string.Format("{0:0.00}", TextBox9.Text)));
                             cmd24.Parameters.AddWithValue("@outstanding", float.Parse(string.Format("{0:0.00}", TextBox9.Text)));
                             cmd24.Parameters.AddWithValue("@invoice_no", Label1.Text);
                             cmd24.Parameters.AddWithValue("@Com_Id", company_id);
                             cmd24.Parameters.AddWithValue("@status", status1);
                             cmd24.Parameters.AddWithValue("@year", Label11.Text);
                             con24.Open();
                             cmd24.ExecuteNonQuery();
                             con24.Close();


                         }
                         con100.Close();

                     }

                     /*string name = "Dear Customer, Thanks for shopping with Dream garments. Your invoice value is "+TextBox11.Text+" "+" For any queries contact:9345717284 ";
                     string strUrl = "http://api.mVaayoo.com/mvaayooapi/MessageCompose?user=nazeer.deens@gmail.com:vertex&senderID=TEST SMS&receipientno=" + TextBox6.Text + "&dcs=0&msgtxt=" + name + "&state=4 ";
                     // Create a request object  
                     WebRequest request = HttpWebRequest.Create(strUrl);
                     // Get the response back  
                     HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                     Stream s = (Stream)response.GetResponseStream();
                     StreamReader readStream = new StreamReader(s);
                     string dataString = readStream.ReadToEnd();
                     response.Close();
                     s.Close();
                     readStream.Close();*/




                     ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Sales entry created successfully')", true);

                     show_category();
                     getinvoiceno();
                     getinvoiceno1();
                     BindData();

                     BindData2();

                     TextBox2.Text = "";
                     TextBox12.Text = "";
                     TextBox10.Text = "";
                     TextBox11.Text = "";
                     TextBox26.Text = "";
                     TextBox13.Text = "";
                     TextBox14.Text = "";
                     TextBox11.Text = "";
                     TextBox7.Text = "";
                     TextBox9.Text = "";
                     TextBox6.Text = "";
                  
                     TextBox23.Text = "";

                 }
                 con1000.Close();
             }
         }
     
    }
    protected void ImageButton1_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        ImageButton img = (ImageButton)sender;
        GridViewRow row = (GridViewRow)img.NamingContainer;
        Label38.Text = Label1.Text;
        Label41.Text = row.Cells[0].Text;
        TextBox33.Text = row.Cells[1].Text;
        TextBox27.Text = row.Cells[2].Text;
        TextBox3.Text = row.Cells[3].Text;
        TextBox29.Text = row.Cells[4].Text;
        TextBox4.Text = row.Cells[5].Text;
        TextBox32.Text = row.Cells[6].Text;
        this.ModalPopupExtender5.Show();
    }
    protected void ImageButton2_Click(object sender, System.Web.UI.ImageClickEventArgs e)
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
                GridViewRow ROW = (GridViewRow)img.NamingContainer;
                int s_no = Convert.ToInt32(ROW.Cells[0].Text);
                string productname = ROW.Cells[1].Text;
                float qty = float.Parse(ROW.Cells[3].Text);


                SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);

                con1.Open();

                SqlCommand cmd21 = new SqlCommand("select * from sales_credit_entry_details where invoice_no='" + Label1.Text + "' and s_no='" + s_no + "' and Com_Id='" + company_id + "' and year='" + Label11.Text + "' ", con1);
                SqlDataReader dr11;
                dr11 = cmd21.ExecuteReader();
                if (dr11.Read())
                {
                    string product_name = dr11["product_name"].ToString();
                    float qty1 = float.Parse(dr11["qty"].ToString());
                    SqlConnection CON11 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd11 = new SqlCommand("update product_stock set qty=qty+@qty where Product_name='" + productname + "' and Com_Id='" + company_id + "' and year='" + Label11.Text + "'", CON11);





                    cmd11.Parameters.AddWithValue("@qty", qty1);

                    CON11.Open();
                    cmd11.ExecuteNonQuery();
                    CON11.Close();

                }
                con1.Close();

                SqlConnection con11 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);

                con11.Open();
                SqlCommand cmd111 = new SqlCommand("delete from sales_credit_entry_details where s_no='" + s_no + "' and invoice_no='" + Label1.Text + "' and Com_Id='" + company_id + "' and year='" + Label11.Text + "'", con11);
                cmd111.ExecuteNonQuery();
                con11.Close();





                BindData();
                getinvoiceno1();
            }
            con1000.Close();
        }

    }
    private void SaveDetail(GridViewRow row)
    {

        
        


    }

    protected void Button2_Click(object sender, EventArgs e)
    {

        show_category();
        getinvoiceno();
        getinvoiceno1();
        BindData();
        BindData2();
        TextBox10.Text = "";
        TextBox11.Text = "";
        TextBox13.Text = "";
        TextBox14.Text = "";
        TextBox2.Text = "";

        TextBox6.Text = "";
        TextBox7.Text = "";
        TextBox9.Text = "";
     
        TextBox23.Text = "";
        TextBox26.Text = "";
        var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneInfo);
        DateTime date = now;
        TextBox8.Text = Convert.ToDateTime(date).ToString("dd-MM-yyyy");
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

            }
            con1000.Close();
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand CMD = new SqlCommand("select * from sales_credit_entry_details where invoice_no='" + Label1.Text + "' and Com_Id='" + company_id + "' and year='" + Label11.Text + "' order by s_no asc", con);
        DataTable dt1 = new DataTable();
        SqlDataAdapter da1 = new SqlDataAdapter(CMD);
        da1.Fill(dt1);
        GridView1.DataSource = dt1;
        GridView1.DataBind();
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

            }
            con1000.Close();
        }
        ImageButton img = (ImageButton)sender;
        GridViewRow row = (GridViewRow)img.NamingContainer;
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("delete from product_entry where code='" + row.Cells[1].Text + "' and Com_Id='" + company_id + "' and year='" + Label11.Text + "' ", con);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Product entry deleted successfully')", true);

        BindData();
        show_category();



    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]

    public static List<string> SearchCustomers(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.AppSettings["connection"];

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select distinct barcode from product_stock where Com_Id=@Com_Id and " +
                "barcode like @barcode + '%'";
                cmd.Parameters.AddWithValue("@barcode", prefixText);
                cmd.Parameters.AddWithValue("@Com_Id", company_id);
                cmd.Connection = conn;
                conn.Open();
                List<string> customers = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(sdr["barcode"].ToString());
                    }
                }
                conn.Close();
                return customers;
            }
        }
    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]

    public static List<string> Searchproductname(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.AppSettings["connection"];

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select distinct Product_name from product_stock where Com_Id=@Com_Id and " +
                "Product_name like @Product_name + '%'";
                cmd.Parameters.AddWithValue("@Product_name", prefixText);
                cmd.Parameters.AddWithValue("@Com_Id", company_id);
                cmd.Connection = conn;
                conn.Open();
                List<string> customers = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(sdr["Product_name"].ToString());
                    }
                }
                conn.Close();
                return customers;
            }
        }
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
                cmd.CommandText = "select distinct barcode from product_stock where Com_Id=@Com_Id and  " +
                "barcode like @barcode + '%'";
                cmd.Parameters.AddWithValue("@barcode", prefixText);
                cmd.Parameters.AddWithValue("@Com_Id", company_id);
                cmd.Connection = conn;
                conn.Open();
                List<string> customers = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(sdr["barcode"].ToString());
                    }
                }
                conn.Close();
                return customers;
            }
        }
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
                cmd.CommandText = "select distinct Mobile_no from Customer_Entry where Com_Id=@Com_Id and  " +
                "Mobile_no like @Mobile_no + '%'";
                cmd.Parameters.AddWithValue("@Mobile_no", prefixText);
                cmd.Parameters.AddWithValue("@Com_Id", company_id);
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

    public static List<string> SearchCustomersdetails(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.AppSettings["connection"];

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select distinct Custom_Name from Customer_Entry where Com_Id=@Com_Id and " +
                "Custom_Name like @Custom_Name + '%'";
                cmd.Parameters.AddWithValue("@Custom_Name", prefixText);
                cmd.Parameters.AddWithValue("@Com_Id", company_id);
                cmd.Connection = conn;
                conn.Open();
                List<string> customers = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(sdr["Custom_Name"].ToString());
                    }
                }
                conn.Close();
                return customers;
            }
        }
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        



    }
    private void show_tax()
    {
        
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

            }
            con1000.Close();
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("Select * from Staff_Entry where Com_Id='" + company_id + "' ORDER BY Emp_Code asc", con);
        con.Open();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);


      

        DropDownList3.DataSource = ds;
        DropDownList3.DataTextField = "Emp_Name";
        DropDownList3.DataValueField = "Emp_Code";
        DropDownList3.DataBind();
        DropDownList3.Items.Insert(0, new ListItem("All", "0"));
        con.Close();
        
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
       
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            tot = tot + float.Parse(e.Row.Cells[3].Text);

        }
        TextBox2.Text = tot.ToString();

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            tot1 = tot1 + float.Parse(e.Row.Cells[6].Text);

        }
        TextBox10.Text = tot1.ToString();
      
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {

    }

    

    protected void Button3_Click(object sender, EventArgs e)
    {



       




     





       





    }
    
    protected void TextBox6_TextChanged(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);

                    con.Open();

                    SqlCommand cmd2 = new SqlCommand("select * from Customer_entry where Mobile_no='" + TextBox6.Text + "'", con);
                    SqlDataReader dr1;
                    dr1 = cmd2.ExecuteReader();
                    if (dr1.Read())
                    {


                        TextBox13.Text = dr1["Custom_Name"].ToString();
                        TextBox14.Text = dr1["Custom_Add"].ToString();
                    }
                    con.Close();


       
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            tot = tot + float.Parse(e.Row.Cells[5].Text);
            TextBox10.Text = tot.ToString();
            TextBox11.Text = tot.ToString();
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            tot1 = tot1 + float.Parse(e.Row.Cells[4].Text);
            TextBox2.Text = tot1.ToString();
            
        }
    }

    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {

       
    }
    
    protected void Button5_Click(object sender, EventArgs e)
    {
        Session["Name"] = TextBox13.Text;




        Response.Redirect("SALES_REPORT_VIEW.aspx");
    }
    protected void Gridview2_Load(object sender, System.EventArgs e)
    {

    }


   
   
    
    
    protected void Gridview2_PreRender(object sender, System.EventArgs e)
    {

    }
    protected void TextBox16_TextChanged(object sender, System.EventArgs e)
    {
       
    }
    protected void TextBox2_TextChanged(object sender, System.EventArgs e)
    {
       
    }
    protected void TextBox17_TextChanged(object sender, System.EventArgs e)
    {
       
    }

    protected void TextBox18_TextChanged(object sender, System.EventArgs e)
    {
        
    }
    protected void Button3_Click1(object sender, System.EventArgs e)
    {
       
    }

    protected void TextBox7_TextChanged(object sender, System.EventArgs e)
    {
        try
        {

            float value1 = float.Parse(TextBox11.Text);
            float value2 = float.Parse(TextBox7.Text);
            float total = value1 - value2;
            TextBox9.Text = total.ToString();
        }
        catch (Exception er)
        { }
    }

    protected void Gridview2_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        
    }
    private void getinvoiceno1()
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
        string query = "Select max(s_no) from sales_credit_entry_details where Com_Id='" + company_id + "' and invoice_no='" + Label1.Text + "' and year='" + Label11.Text + "'";
        SqlCommand cmd1 = new SqlCommand(query, con1);
        SqlDataReader dr = cmd1.ExecuteReader();
        if (dr.Read())
        {
            string val = dr[0].ToString();
            if (val == "")
            {
                Label2.Text = "1";
            }
            else
            {
                a = Convert.ToInt32(dr[0].ToString());
                a = a + 1;
                Label2.Text = a.ToString();
            }
        }
                con1.Close();
            }
            con1000.Close();
        }
    }
   
    protected void Button3_Click2(object sender, System.EventArgs e)
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

                con.Open();

                SqlCommand cmd2 = new SqlCommand("select * from product_stock where Product_name='" + TextBox12.Text + "' and Com_Id='" + company_id + "' and year='" + Label11.Text + "' ", con);
                SqlDataReader dr1;
                dr1 = cmd2.ExecuteReader();
                if (dr1.Read())
                {

                    string cat_id = dr1["Category"].ToString();

                    string product_code = dr1["Product_code"].ToString();

                SqlConnection CON1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd1 = new SqlCommand("insert into sales_credit_entry_details values(@invoice_no,@s_no,@Category,@Product_code,@product_name,@mrp,@qty,@dis_per,@dis_amount,@total_amount,@Com_Id,@year)", CON1);
                cmd1.Parameters.AddWithValue("@invoice_no",Label1.Text);
                cmd1.Parameters.AddWithValue("@s_no", Label2.Text);
                cmd1.Parameters.AddWithValue("@Category", cat_id);
             
                cmd1.Parameters.AddWithValue("@Product_code", product_code);
                cmd1.Parameters.AddWithValue("@product_name", TextBox12.Text);

                cmd1.Parameters.AddWithValue("@mrp", TextBox17.Text);
                cmd1.Parameters.AddWithValue("@qty", TextBox5.Text);
                cmd1.Parameters.AddWithValue("@dis_per", TextBox15.Text);
                cmd1.Parameters.AddWithValue("@dis_amount", TextBox16.Text);
                cmd1.Parameters.AddWithValue("@total_amount", TextBox18.Text);
                cmd1.Parameters.AddWithValue("@Com_Id", company_id);
                cmd1.Parameters.AddWithValue("@year", Label11.Text);
                CON1.Open();
                cmd1.ExecuteNonQuery();
                CON1.Close();


                SqlConnection CON11 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd11 = new SqlCommand("update product_stock set qty=qty-@qty  from product_stock where Product_name='" + TextBox12.Text + "' and Com_Id='" + company_id + "' and year='" + Label11.Text + "'", CON11);
                cmd11.Parameters.AddWithValue("@qty", TextBox5.Text);

                CON11.Open();
                cmd11.ExecuteNonQuery();
                CON11.Close();
                BindData();
                getinvoiceno1();
                TextBox12.Text = "";
                TextBox17.Text = "";
                TextBox5.Text = "";
                TextBox15.Text = "";
                TextBox16.Text = "";
                TextBox18.Text = "";
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('product not valid')", true);
            }
            con.Close();
            TextBox12.Focus();
            }
            con1000.Close();
        }
    }

    protected void TextBox12_TextChanged(object sender, System.EventArgs e)
    {


        SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        con1.Open();
        string query = "Select * from subcategory where Com_Id='" + company_id + "' and subcategoryname='" + TextBox12.Text + "'";
        SqlCommand cmd1 = new SqlCommand(query, con1);
        SqlDataReader dr = cmd1.ExecuteReader();
        if (dr.Read())
        {


            TextBox17.Text = dr["mrp"].ToString();
            TextBox5.Text = "1";
            TextBox15.Text = "0";
            TextBox16.Text = "0.0";
        }
        con1.Close();

        TextBox15.Focus();
        try
        {

            float a = float.Parse(TextBox17.Text);
            float b = float.Parse(TextBox5.Text);
            TextBox18.Text = (a * b).ToString();
            TextBox15.Focus();
        }
        catch (Exception we)
        { }
    } 

    protected void TextBox5_TextChanged(object sender, System.EventArgs e)
    {
        try
        {

            float a = float.Parse(TextBox17.Text);
            float b = float.Parse(TextBox5.Text);
            TextBox18.Text = (a * b).ToString();
            TextBox15.Focus();
        }
        catch (Exception we)
        { }
    }
    protected void TextBox15_TextChanged(object sender, System.EventArgs e)
    {
       
            float tax = float.Parse(TextBox15.Text);
            float total = float.Parse(TextBox18.Text);
            TextBox16.Text = string.Format("{0:0.00}", (total * tax / 100)).ToString();
            float A = float.Parse(TextBox16.Text);
            TextBox18.Text = string.Format("{0:0.00}", ( total-A)).ToString();
            Button3.Focus();
       
    }

    /*protected void TextBox4_TextChanged(object sender, System.EventArgs e)
    {
        TextBox5.Focus();
    }*/
    protected void Button6_Click(object sender, System.EventArgs e)
    {
        this.ModalPopupExtender3.Show();
    }
    protected void Button16_Click(object sender, EventArgs e)
    {
        if (TextBox19.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please enter customer name')", true);

        }
        else if (TextBox21.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please enter mobile no')", true);
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
            SqlConnection CON = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd = new SqlCommand("insert into Customer_Entry values(@Custom_Code,@Custom_Name,@Custom_Add,@Mobile_no,@Profession,@Customer_Type,@Com_Id,@friend_name,@Friend_mobile_No)", CON);
            cmd.Parameters.AddWithValue("@Custom_Code", Label29.Text);
            cmd.Parameters.AddWithValue("@Custom_Name", HttpUtility.HtmlDecode(TextBox19.Text));
            cmd.Parameters.AddWithValue("@Custom_Add", HttpUtility.HtmlDecode(TextBox20.Text));
            cmd.Parameters.AddWithValue("@Mobile_no", HttpUtility.HtmlDecode(TextBox21.Text));
            cmd.Parameters.AddWithValue("@Profession", HttpUtility.HtmlDecode(TextBox22.Text));
            cmd.Parameters.AddWithValue("@Customer_Type", HttpUtility.HtmlDecode(DropDownList1.SelectedItem.Text));
            cmd.Parameters.AddWithValue("@Com_Id", company_id);
            cmd.Parameters.AddWithValue("@friend_name", TextBox24.Text);
            cmd.Parameters.AddWithValue("@Friend_mobile_No", TextBox25.Text);
            CON.Open();
            cmd.ExecuteNonQuery();
            CON.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Customer Entry Addded successfully')", true);
            BindData();
            show_type();
            getinvoicenoid();
            TextBox19.Text = "";
            TextBox20.Text = "";
            TextBox21.Text = "";
           
            TextBox22.Text = "";
            TextBox25.Text = "";
            TextBox24.Text = "";

        }



    }
    private void getinvoicenoid()
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
        int a;

        SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        con1.Open();
        string query = "Select Max(Custom_Code) from Customer_Entry where Com_Id='" + company_id + "'";
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
  
    protected void TextBox26_TextChanged(object sender, System.EventArgs e)
    {
        try
        {
            float total = float.Parse(TextBox10.Text);
            float discount_per = float.Parse(TextBox26.Text);
         

            TextBox11.Text = (total - discount_per).ToString();
        }
        catch (Exception er)
        { }
    }
    protected void TextBox13_TextChanged(object sender, System.EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);

        con.Open();

        SqlCommand cmd2 = new SqlCommand("select * from Customer_entry where Custom_Name='" + TextBox13.Text + "'", con);
        SqlDataReader dr1;
        dr1 = cmd2.ExecuteReader();
        if (dr1.Read())
        {


            TextBox6.Text = dr1["Mobile_no"].ToString();
            TextBox14.Text = dr1["Custom_Add"].ToString();
        }
        con.Close();
    }
    protected void TextBox16_TextChanged1(object sender, System.EventArgs e)
    {

    }



    protected void TextBox23_TextChanged(object sender, System.EventArgs e)
    {
        try
        {

            float total = float.Parse(TextBox10.Text);
            float dis = float.Parse(TextBox23.Text);
           
            float total_amount = (total*dis/100);
            TextBox26.Text = total_amount.ToString();
            TextBox11.Text = (total - total_amount).ToString();
        }
        catch (Exception er)
        { }
    }
    protected void Button22_Click(object sender, System.EventArgs e)
    {
        SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);

        con1.Open();

        SqlCommand cmd21 = new SqlCommand("select * from sales_credit_entry_details where invoice_no='" + Label38.Text + "' and s_no='" + Label41.Text + "' and Com_Id='" + company_id + "' and year='" + Label11.Text + "' ", con1);
        SqlDataReader dr11;
        dr11 = cmd21.ExecuteReader();
        if (dr11.Read())
        {
            string product_name = dr11["product_name"].ToString();
            float qty1 = float.Parse(dr11["qty"].ToString());
            SqlConnection CON11 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd11 = new SqlCommand("update product_stock set qty=qty+@qty  from product_stock where Product_name='" + product_name + "' and  Com_Id='" + company_id + "' and year='" + Label11.Text + "'", CON11);





            cmd11.Parameters.AddWithValue("@qty", qty1);

            CON11.Open();
            cmd11.ExecuteNonQuery();
            CON11.Close();

        }
        con1.Close();


        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);

        con.Open();

        SqlCommand cmd2 = new SqlCommand("select * from product_stock where Product_name='" + TextBox33.Text + "' and Com_Id='" + company_id + "' and year='" + Label11.Text + "' ", con);
        SqlDataReader dr1;
        dr1 = cmd2.ExecuteReader();
        if (dr1.Read())
        {

            string cat_id = dr1["Category"].ToString();

            string product_code = dr1["Product_code"].ToString();

            SqlConnection CON1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd1 = new SqlCommand("update sales_credit_entry_details set Category=@Category,Product_code=@Product_code,product_name=@product_name,mrp=@mrp,qty=@qty,dis_per=@dis_per,dis_amount=@dis_amount,total_amount=@total_amount,Com_Id=@Com_Id where invoice_no=@invoice_no and s_no=@s_no and Com_Id='" + company_id + "' and year='" + Label11.Text + "'", CON1);
            cmd1.Parameters.AddWithValue("@invoice_no", Label38.Text);
            cmd1.Parameters.AddWithValue("@s_no", Label41.Text);
            cmd1.Parameters.AddWithValue("@Category", cat_id);
          
            cmd1.Parameters.AddWithValue("@Product_code", product_code);
            cmd1.Parameters.AddWithValue("@product_name", TextBox33.Text);

            cmd1.Parameters.AddWithValue("@mrp", float.Parse(TextBox27.Text));
            cmd1.Parameters.AddWithValue("@qty", TextBox3.Text);
            cmd1.Parameters.AddWithValue("@dis_per", float.Parse(TextBox29.Text));
            cmd1.Parameters.AddWithValue("@dis_amount", float.Parse(TextBox4.Text));
            cmd1.Parameters.AddWithValue("@total_amount", float.Parse(TextBox32.Text));
            cmd1.Parameters.AddWithValue("@Com_Id", company_id);
            CON1.Open();
            cmd1.ExecuteNonQuery();
            CON1.Close();


            SqlConnection CON11 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd11 = new SqlCommand("update product_stock set qty=qty-@qty from product_stock where Product_name='" + TextBox33.Text + "' and Com_Id='" + company_id + "' and year='" + Label11.Text + "'", CON11);





            cmd11.Parameters.AddWithValue("@qty", TextBox3.Text);

            CON11.Open();
            cmd11.ExecuteNonQuery();
            CON11.Close();
        }
        BindData();


    }
    protected void TextBox3_TextChanged(object sender, System.EventArgs e)
    {
        try
        {

            float mrp = float.Parse(TextBox27.Text);
            float qty = float.Parse(TextBox3.Text);
            float dis_amt = float.Parse(TextBox4.Text);
            TextBox32.Text = ((mrp - dis_amt) * qty).ToString();
            this.ModalPopupExtender5.Show();
        }
        catch (Exception we)
        { }
    }
    protected void TextBox29_TextChanged(object sender, System.EventArgs e)
    {
        try
        {

            float dis_per = float.Parse(TextBox29.Text);
            float mrp = float.Parse(TextBox27.Text);
            float qty = float.Parse(TextBox3.Text);
            float dis_amt = (mrp * dis_per) / 100;
            TextBox4.Text = dis_amt.ToString();
            TextBox32.Text = ((mrp - dis_amt) * qty).ToString();
            this.ModalPopupExtender5.Show();
        }
        catch (Exception we)
        { }
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

                if (Convert.ToInt32(Label1.Text) > Convert.ToInt32(1))
                {
                    Label1.Text = (Convert.ToInt32(Label1.Text) - 1).ToString();
                }

                SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd2 = new SqlCommand("select * from sales_credit_entry where invoice_no='" + Label1.Text + "' and Com_Id='" + company_id + "' and year='" + Label11.Text + "'", con2);
                SqlDataReader dr2;
                con2.Open();
                dr2 = cmd2.ExecuteReader();
                if (dr2.Read())
                {

                    TextBox8.Text = Convert.ToDateTime(dr2["date"]).ToString("dd-MM-yyyy");
                    TextBox6.Text = dr2["Mobile_no"].ToString();
                    TextBox13.Text = dr2["customer_name"].ToString();
                    TextBox14.Text = dr2["customer_Address"].ToString();
                    DropDownList3.SelectedItem.Text = dr2["staff_name"].ToString();
                    TextBox2.Text = dr2["total_qty"].ToString();
                    TextBox10.Text = Convert.ToDecimal(dr2["total_amount"]).ToString("#,##0.00");
                    TextBox23.Text = dr2["dis_per"].ToString();
                    TextBox26.Text = dr2["discount_amount"].ToString();
                    TextBox11.Text = Convert.ToDecimal(dr2["grand_total"]).ToString("#,##0.00");
                    TextBox7.Text = Convert.ToDecimal(dr2["paid_amount"]).ToString("#,##0.00");
                    TextBox9.Text = Convert.ToDecimal(dr2["pending_amount"]).ToString("#,##0.00");

                }
                con2.Close();


                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand CMD = new SqlCommand("select * from sales_credit_entry_details where invoice_no='" + Label1.Text + "' and Com_Id='" + company_id + "' and year='" + Label11.Text + "' ORDER BY s_no asc", con);
                DataTable dt1 = new DataTable();
                SqlDataAdapter da1 = new SqlDataAdapter(CMD);
                da1.Fill(dt1);
                GridView1.DataSource = dt1;
                GridView1.DataBind();
                getinvoiceno1();
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

                SqlConnection con21 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd21 = new SqlCommand("select max(invoice_no) from sales_credit_entry where  Com_Id='" + company_id + "' and year='" + Label11.Text + "' ", con21);
                SqlDataReader dr21;
                con21.Open();
                dr21 = cmd21.ExecuteReader();
                if (dr21.Read())
                {
                    int value = Convert.ToInt32(dr21[0].ToString());
                    if (Convert.ToInt32(Label1.Text) < Convert.ToInt32(value + 1))
                    {
                        Label1.Text = (Convert.ToInt32(Label1.Text) + 1).ToString();
                    }
                }
                con21.Close();
                SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd2 = new SqlCommand("select * from sales_credit_entry where invoice_no='" + Label1.Text + "' and Com_Id='" + company_id + "' and year='" + Label11.Text + "'", con2);
                SqlDataReader dr2;
                con2.Open();
                dr2 = cmd2.ExecuteReader();
                if (dr2.Read())
                {



                    TextBox8.Text = Convert.ToDateTime(dr2["date"]).ToString("dd-MM-yyyy");
                    TextBox6.Text = dr2["Mobile_no"].ToString();
                    TextBox13.Text = dr2["customer_name"].ToString();
                    TextBox14.Text = dr2["customer_Address"].ToString();
                    DropDownList3.SelectedItem.Text = dr2["staff_name"].ToString();
                    TextBox2.Text = dr2["total_qty"].ToString();
                    TextBox10.Text = Convert.ToDecimal(dr2["total_amount"]).ToString("#,##0.00");
                    TextBox23.Text = dr2["dis_per"].ToString();
                    TextBox26.Text = dr2["discount_amount"].ToString();
                    TextBox11.Text = Convert.ToDecimal(dr2["grand_total"]).ToString("#,##0.00");
                    TextBox7.Text = Convert.ToDecimal(dr2["paid_amount"]).ToString("#,##0.00");
                    TextBox9.Text = Convert.ToDecimal(dr2["pending_amount"]).ToString("#,##0.00");


                    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand CMD = new SqlCommand("select * from sales_entry_details where invoice_no='" + Label1.Text + "' and Com_Id='" + company_id + "' and year='" + Label11.Text + "' ORDER BY s_no asc", con);
                    DataTable dt1 = new DataTable();
                    SqlDataAdapter da1 = new SqlDataAdapter(CMD);
                    da1.Fill(dt1);
                    GridView1.DataSource = dt1;
                    GridView1.DataBind();
                    getinvoiceno1();
                }
                else
                {

                    show_category();
                    getinvoiceno();
                    getinvoiceno1();
                    BindData();
                    TextBox10.Text = "";
                    TextBox11.Text = "";
                    TextBox13.Text = "";
                    TextBox14.Text = "";
                    TextBox2.Text = "";

                    TextBox6.Text = "";
                    TextBox7.Text = "";
                    TextBox9.Text = "";
                    var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                    var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneInfo);
                    DateTime date = now;
                    TextBox8.Text = Convert.ToDateTime(date).ToString("dd-MM-yyyy");
                    TextBox23.Text = "";
                    TextBox26.Text = "";


                }
                con2.Close();
            }
            con1000.Close();
        }
    }
    protected void Button7_Click(object sender, EventArgs e)
    {
        show_category();
        getinvoiceno();
        getinvoiceno1();
        BindData();
        BindData2();
        TextBox10.Text = "";
        TextBox11.Text = "";
        TextBox13.Text = "";
        TextBox14.Text = "";
        TextBox2.Text = "";

        TextBox6.Text = "";
        TextBox7.Text = "";
        TextBox9.Text = "";

        TextBox23.Text = "";
        TextBox26.Text = "";
        var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneInfo);
        DateTime date = now;
        TextBox8.Text = Convert.ToDateTime(date).ToString("MM-dd-yyyy");
    }
    protected void Button11_Click(object sender, EventArgs e)
    {
        this.ModalPopupExtender2.Show();
    }
    protected void BindData2()
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
                SqlCommand CMD = new SqlCommand("select * from sales_credit_entry where Com_Id='" + company_id + "' ORDER BY  invoice_no asc", con);
                DataTable dt1 = new DataTable();
                SqlDataAdapter da1 = new SqlDataAdapter(CMD);
                da1.Fill(dt1);
                GridView3.DataSource = dt1;
                GridView3.DataBind();
            }
            con1000.Close();
        }

    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
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




                SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd2 = new SqlCommand("select * from sales_credit_entry where invoice_no='" + row.Cells[0].Text + "' and Com_Id='" + company_id + "'", con2);
                SqlDataReader dr2;
                con2.Open();
                dr2 = cmd2.ExecuteReader();
                if (dr2.Read())
                {
                    Label1.Text = dr2["invoice_no"].ToString();
                    TextBox8.Text = Convert.ToDateTime(dr2["date"]).ToString("dd-MM-yyyy");
                    TextBox6.Text = dr2["Mobile_no"].ToString();
                    TextBox13.Text = dr2["customer_name"].ToString();
                    TextBox14.Text = dr2["customer_Address"].ToString();
                    DropDownList3.SelectedItem.Text = dr2["staff_name"].ToString();
                    TextBox2.Text = dr2["total_qty"].ToString();
                    TextBox10.Text = Convert.ToDecimal(dr2["total_amount"]).ToString("#,##0.00");
                    TextBox23.Text = dr2["dis_per"].ToString();
                    TextBox26.Text = dr2["discount_amount"].ToString();
                    TextBox11.Text = Convert.ToDecimal(dr2["grand_total"]).ToString("#,##0.00");
                    TextBox7.Text = Convert.ToDecimal(dr2["paid_amount"]).ToString("#,##0.00");
                    TextBox9.Text = Convert.ToDecimal(dr2["pending_amount"]).ToString("#,##0.00");

                }
                con2.Close();


                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand CMD = new SqlCommand("select * from sales_credit_entry_details where invoice_no='" + Label1.Text + "' and Com_Id='" + company_id + "' and year='" + Label11.Text + "' ORDER BY s_no asc", con);
                DataTable dt1 = new DataTable();
                SqlDataAdapter da1 = new SqlDataAdapter(CMD);
                da1.Fill(dt1);
                GridView1.DataSource = dt1;
                GridView1.DataBind();
                getinvoiceno1();

            }
            con1000.Close();
        }



    }
    protected void Button5_Click1(object sender, EventArgs e)
    {
        getinvoiceno1();
        BindData();
        TextBox12.Text = "";
        TextBox5.Text = "";
        TextBox15.Text = "";
        TextBox16.Text = "";
        TextBox17.Text = "";
        TextBox18.Text = "";

    }
}
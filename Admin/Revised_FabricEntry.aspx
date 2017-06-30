﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Revised_FabricEntry.aspx.cs" Inherits="Admin_Revised_FabricEntry" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>
<html lang="en">
    <head id="Head1" runat="server">
         <meta charset="utf-8">
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->
        <title>TrueCollar</title>
      

              <script type="text/javascript">

                  $(document).ready(function () {

                      $(".selectpicker").selectpicker();

                  });

                 </script>
                  <script type="text/javascript" language="javascript">
                      function controlEnter(obj, event) {
                          var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
                          if (keyCode == 13) {
                              document.getElementById(obj).focus();
                              return false;
                          }
                          else {
                              return true;
                          }
                      }
</script>
<style>
.tablestyles table tr td
{
    padding:5px;
}

</style>

        <!-- Bootstrap -->
          <script src="bootstrap/js/jquery-3.1.1.min.js"></script>

          <script src="bootstrap/js/bootstrap-select.js"></script>
           <link href="bootstrap/css/bootstrap-select.css" rel="stylesheet" />
           <link rel="stylesheet" type="text/css" media="screen" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.7.5/css/bootstrap-select.min.css">
         <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" />
        <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet">
        <link href="css/waves.min.css" type="text/css" rel="stylesheet">
        <!--        <link rel="stylesheet" href="css/nanoscroller.css">-->
        <link href="css/menu.css" type="text/css" rel="stylesheet">
        <link href="css/style.css" type="text/css" rel="stylesheet">
        <link href="css1/Vendorcss.css" type="text/css" rel="stylesheet">
        <link href="font-awesome/css/font-awesome.min.css" rel="stylesheet">
        <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
        <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
        <!--[if lt IE 9]>
          <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
          <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
        <![endif]-->

        
        <style>
            .completionList {
        border:solid 1px Gray;
        margin:0px;
        padding:3px;
        height: 120px;
        overflow:auto;
        background-color:#FAEBD7;     
        } 
        .listItem {
        color: #191919;
        } 
        .itemHighlighted {
        background-color: #ADD6FF;       
        }
            .red
            {
                text-align:center;
            }
            .goo
            {
               color:#13c4a5;
            }
            .goo:hover
            {
                color:#3a5a7a;
            }
            .color
            {
                color:#555555;
                height:30px;
            }
        .dropbox
        {
            width:100%;
            height:30px;
        display: block;
        font-size:16px;
        font-family: 'Open Sans',"HelveticaNeue", "Helvetica Neue", Helvetica, Arial,sans-serif;
   
 }
        .gvwCasesPager
        {
           
          color:black;
          margin-right:20px;
          text-align:right;
          padding:20px;
        }
        .gvwCasesPager a
            {
               
                margin-left:10px;
                margin-right:10px;
                font-size:20px;
                
                 padding:10px;
                
              
              
            }

         .dropbox1
        {
            width:10%;
            height:30px;
           
           
            
        }
        
        .see
        {
           height:400px; 
           margin-top:-60px;
        }
        .see1
        {
            margin-top:-20px;
        }
         .see2
        {
          
            margin-left:-15px;
            margin-bottom:30px;
        }
        
          @media (max-width: 767px)
        {
             .see
        {
           height:400px; 
           margin-top:-10px;
        }
         .see1
        {
            margin-top:-40px;
        }
         .see2
        {
            margin-top:50px;
            
        }
      
        }
        
        </style>
    </head>
    <body>
        <!-- Static navbar -->
 <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        
</asp:ToolkitScriptManager>
    <div>
          <nav class="navbar navbar-inverse yamm navbar-fixed-top">
            <div class="container-fluid">
                <button type="button" class="navbar-minimalize minimalize-styl-2  pull-left "><i class="fa fa-bars"></i></button>
                <span class="search-icon"><i class="fa fa-search"></i></span>
                <div class="search" style="display: none;">
                    <form role="form">
                        <input type="text" class="form-control" autocomplete="off" placeholder="Write something and press enter">
                        <span class="search-close"><i class="fa fa-times"></i></span>
                    </form>
                </div>
                  <div class="navbar-header">
                    <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar" aria-expanded="false" aria-controls="navbar">
                        <span class="sr-only">Toggle navigation</span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                    <a class="navbar-brand" href="#">TrueColler</a>
                </div>
                <div id="navbar" class="navbar-collapse collapse">
<%--                    <ul class="nav navbar-nav">
                        <li class="dropdown">
                           
                          <li class="dropdown">
                            <a href="#" class="dropdown-toggle button-wave" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">
<asp:Button ID="Button4" runat="server"  Text="ADD" class="btn btn-primary"></asp:Button> <span aria-hidden="true" class="glyphicon glyphicon-plus"></span> </a>
                            <ul class="dropdown-menu">
                                <li><a href="Main.aspx"><i class="fa fa-home fa-2x" aria-hidden="true"></i>&nbsp;&nbsp&nbsp;Category</a></li>
                                   <li role="separator" class="divider"></li>
                                <li><a href="Product_entry.aspx"><i class="fa fa-hdd-o" aria-hidden="true"></i>&nbsp;&nbsp&nbsp;Product Entry </a></li>
                                 <li role="separator" class="divider"></li>
                              
                                <li><a href="Purchase_entry.aspx"><i class="fa fa-check-square-o" aria-hidden="true"></i>&nbsp;&nbsp&nbsp;Purchase Entry </a></li>
                                  <li role="separator" class="divider"></li>
                                <li><a href="Stock_Inventory.aspx"><i class="fa fa-edit"></i> &nbsp;&nbsp&nbsp;Stock / Inventory </a></li>
                                 <li role="separator" class="divider"></li>
                                <li><a href="Customer-Entry.aspx"><i class="fa fa-lightbulb-o" aria-hidden="true"></i>  &nbsp;&nbsp&nbsp;New Customer Entry</a></li>

                                <li role="separator" class="divider"></li>
                                <li><a href="Vendor.aspx"><i class="fa fa-thumbs-o-up" aria-hidden="true"></i> &nbsp;&nbsp&nbsp;Supplier Entry </a></li>
                               
                                  <li role="separator" class="divider"></li>
                                <li><a href="Department-Entry.aspx"><i class="fa fa-ticket" aria-hidden="true"></i>&nbsp;&nbsp&nbsp;New Department Entry  </a></li>
                                <li role="separator" class="divider"></li>
                                <li><a href="Sales_entry.aspx"><i class="fa fa-ticket" aria-hidden="true"></i>&nbsp;&nbsp&nbsp;Sales Entry </a></li>
                               
                            </ul>
                        </li>
                    </ul>--%>
                          
                    <ul class="nav navbar-nav navbar-right navbar-top-drops">
                        <li class="dropdown"><a href="#" class="dropdown-toggle button-wave" data-toggle="dropdown">

</a>

                            
                        <li class="dropdown profile-dropdown">
                            <a href="#" class="dropdown-toggle button-wave" data-toggle="dropdown" role="button" ><img src="../default-profile-pic.png" alt="" width="25px"><%=User.Identity.Name%></b></span>  <span class="fa fa-caret-down" aria-hidden="true" style=""></a>
                            <ul class="dropdown-menu">
                             <%--   <li><a href="Profile_main.aspx"><i class="fa fa-user"></i>My Profile</a></li>
                                <li><a href="Seetings.aspx"><i class="fa fa-calendar"></i>Settings</a></li>                         
                                <li><a href="Advanced_Settings.aspx"><i class="fa fa-envelope"></i>Advanced Settings</a></li>
                                <li><a href="#"><i class="fa fa-barcode"></i>Custom Field</a></li>
                                <li class="divider"></li>--%>
                               
                                 <li ><a href="#" ><asp:LinkButton id="LoginLink" Text="Log Out"  class="fa fa-sign-out" aria-hidden="true"
                      OnClick="LoginLink_OnClick" runat="server" /></a></li>
                            </ul>
                        </li>
                    </ul>
                </div><!--/.nav-collapse -->
            </div><!--/.container-fluid -->
        </nav>
        <section class="page">

             <nav class="navbar-aside navbar-static-side" role="navigation">
                <div class="sidebar-collapse nano">
                    <div class="nano-content">
                        <ul class="nav metismenu" id="side-menu">

                            <li class="active">
                                <a href="Dashboard.aspx"><i class="fa fa-home fa-2x" aria-hidden="true"></i> <span class="nav-label">&nbsp;&nbsp;Home </span><span class="fa arrow"></span></a>
                           <ul class="nav nav-second-level collapse">
                                    <li><a href="Dashboard.aspx">Dashboard </a></li>
                           </ul>
                            </li>
                            <li>
                                <a href=""><i class="fa fa-folder-open fa-2x" aria-hidden="true"></i> <span class="nav-label">&nbsp;&nbsp;Master </span><span class="fa arrow"></span></a>
                          
                                   <ul class="nav nav-second-level collapse">
                                   <li><a href="Main.aspx">Material Entry</a></li>
                                    <li><a href="Supplier_Entry.aspx">Supplier Entry</a></li>
                                     <li><a href="Client_Entry.aspx">Client Entry</a></li>
                                    <li><a href="JobWork_Vendor.aspx">Job Work Vendor</a></li>
                                     <li><a href="Staff_Entry.aspx">Staff Entry</a></li>
                          <%--          <li><a href="Customer_type.aspx">Customer Type entry</a></li>
                                    <li><a href="Customer-Entry.aspx">Customer Entry</a></li>
                                    
                                    <li><a href="Department-Entry.aspx">Department Entry</a></li>--%>

                           </ul>
                           

                           </li>
                           

                             <li>
                                <a href="Purchase_Order.aspx"><i class="fa fa-paypal fa-2x" aria-hidden="true"></i> <span class="nav-label">&nbsp;&nbsp; Inventory </span><span class="fa arrow"></span></a>
                             <ul class="nav nav-second-level collapse">
                                    <li><a href="Purchase_Order.aspx">Purchase Order</a></li>
                                    <li><a href="Purchase_Return.aspx">Purchase Return</a></li>
                              <%--       <li><a href="Purchase_report.aspx">Billed Report</a></li>
                                      <li><a href="Unbilled_report.aspx">Unbilled Report</a></li>--%>
                           </ul>
                          
                               
                            </li>
                                  <li>
                                <a href="MachineWiseProduction.aspx"><i class="fa fa-clone fa-2x" aria-hidden="true"></i> <span class="nav-label">&nbsp;&nbsp; Production </span><span class="fa arrow"></span></a>
                             <ul class="nav nav-second-level collapse">
                                    <li><a href="MachineWiseProduction.aspx">Machine Wise Production</a></li>
                                     <li><a href="Fabric_Entry.aspx">Fabric Production</a></li>
                                      <li><a href="Revised_FabricEntry.aspx">Revised Fabric Production</a></li>
                                        <li><a href="Quality_Check.aspx">Quality Check</a></li>
                                   
                           </ul>
                            </li>

                             <li>
                                <a href="Account_ledger.aspx"><i class="fa fa-line-chart fa-2x" aria-hidden="true"></i><span class="nav-label">&nbsp;&nbsp; Accounts </span><span class="fa arrow"></span></a>
                             <ul class="nav nav-second-level collapse">
                                <%--    <li><a href="Account_ledger.aspx">Account ledger</a></li>
                                    <li><a href="Purchase_payment_outstanding.aspx">Billed Payment status</a></li>
                                     <li><a href="Unbilled_payment_outstanding.aspx">UnBilled Payment status</a></li>
                                     <li><a href="Sales_payment_outstanding.aspx">Credit Bill Payment status</a></li>--%>
                           </ul>
                          
                               
                            </li>
                       
                           
                            
                             <li>
                                <a href="Sales_entry.aspx"><i class="fa fa-file-text-o fa-2x" aria-hidden="true"></i> <span class="nav-label">&nbsp;&nbsp; HR & Payroll </span><span class="fa arrow"></span></a>
                             <ul class="nav nav-second-level collapse">
                             <%--   <li><a href="Sales_entry.aspx">Cash Sales</a></li>
                                <li><a href="sales_report_details.aspx">Cash Sales Report</a></li>
                                <li><a href="Sales_credit.aspx">Credit sales</a></li>
                                <li><a href="Sales_credit_report.aspx">Credit sales Report</a></li>--%>
                           </ul>
                          
                               
                            </li>
                            <li>
                                <a href="Sales_entry.aspx"><i class="fa fa-file-text-o fa-2x" aria-hidden="true"></i> <span class="nav-label">&nbsp;&nbsp; Reports </span><span class="fa arrow"></span></a>
                             <ul class="nav nav-second-level collapse">
                              <%--     <li><a href="Day_wise_purchase.aspx">Days wise Purchase</a></li>
                                    <li><a href="Day_and_month_wise_purchase.aspx">Days and month wise purchase</a></li>
                                     <li><a href="Daily_sales.aspx">Days wise sales</a></li>
                                      <li><a href="Day_and_month_wise_report.aspx">Days and month sales</a></li>
                                      <li><a href="Staff_wise_report.aspx">Day wise staff Sales</a></li>
                                    <li><a href="Staff_wise_total _sales.aspx">day and Month wise Staff Sales</a></li>--%>
                                     
                           </ul>
                          
                               
                            </li>
                                            
                        </ul>

                    </div>
                </div>
                
            </nav>
            <div id="wrapper">
                <div class="content-wrapper container">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="page-title see2">
                                <h2><font color="#820000">Revised Fabric Entry</font>
                                 </h2>
                             
                             
  



                                
                            </div>
                            
                        </div>
                    </div><!-- end .page title-->
                     <div class="row">
                    <div class="col-md-12">
                  




                    <div class="row see"  >


                    <div class="container">
 

                            
                            <div class="container">
 
  <div class="panel panel-default">
  <div class="panel-body">
   <div class="col-md-12">
                 <div class="panel-body">
                           <div class="form-horizontal">
                               <br />
                               

                               <div class="form-group"><label class="col-lg-2 control-label">Revised No : </label>

                                    <div class="col-lg-3">
                                     <asp:UpdatePanel ID="UpdatePanel3" runat="server">
   <ContentTemplate>
                                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label> 
                                      </ContentTemplate>
                                <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click"  />
                  <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click"  />
                </Triggers>
                           </asp:UpdatePanel>
                                    </div>
                                </div>

      
                                <div class="form-group"><label class="col-lg-2 control-label">Division : </label>
                              
                                    <div class="col-lg-3">
                                     <asp:UpdatePanel ID="UpdatePanel4" runat="server">
   <ContentTemplate>
   <asp:DropDownList ID="DropDownList3" runat="server" class="form-control input-x2 dropbox"></asp:DropDownList>
                             <%--       <asp:TextBox ID="TextBox3" runat="server" class="form-control input-x2 dropbox" AutoPostBack="true" ontextchanged="TextBox3_TextChanged"></asp:TextBox>
                                      
                                      <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" MinimumPrefixLength="1" ServiceMethod="SearchMaterial_Name" FirstRowSelected = "false" CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="TextBox3"  CompletionListCssClass="completionList"
     CompletionListItemCssClass="listItem"
     CompletionListHighlightedItemCssClass="itemHighlighted">
      </asp:AutoCompleteExtender>--%>
                                    </ContentTemplate>
                                     <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click"  />
                  <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click"  />
                </Triggers>
                           </asp:UpdatePanel>
                                    
                                    </div>
                                            <div class="form-group"><label class="col-lg-3 control-label">Color 3 : </label>
                              
                                    <div class="col-lg-3">
                                     <asp:UpdatePanel ID="UpdatePanel17" runat="server">
   <ContentTemplate>
                                   <asp:DropDownList ID="DropDownList12" runat="server" CssClass="form-control" 
                                        data-width="100%"  Width="265px"></asp:DropDownList>
                                    </ContentTemplate>
                                     <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click"  />
                  <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click"  />
                </Triggers>
                           </asp:UpdatePanel>
                                    
                                    </div>
                                     </div>
                                
                                </div>
                                 
                                <div class="form-group"><label class="col-lg-2 control-label">Revised Fabric No : </label>
                              
                                    <div class="col-lg-1">
                                     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
   <ContentTemplate>
                              <asp:TextBox ID="TextBox13" runat="server" 
                                  class="form-control input-x2 dropbox" Width="265px" AutoPostBack="true"
                                  ontextchanged="TextBox13_TextChanged"></asp:TextBox>
           <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="TextBox13" WatermarkText="Select Fabric ID"></asp:TextBoxWatermarkExtender>
                                  
                           <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" MinimumPrefixLength="1" ServiceMethod="SearchFabricNo" FirstRowSelected = "false" CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="TextBox13"  CompletionListCssClass="completionList"
     CompletionListItemCssClass="listItem"
     CompletionListHighlightedItemCssClass="itemHighlighted">
      </asp:AutoCompleteExtender>
                                    </ContentTemplate>
                                     <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click"  />
                  <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click"  />
                </Triggers>
                           </asp:UpdatePanel>
                                    
                                    </div>
                             
                                    <div class="col-lg-3">
                              
                                     <asp:UpdatePanel ID="UpdatePanel9" runat="server">
   <ContentTemplate> 
                                
                                    </ContentTemplate>
                                     <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click"  />
                  <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click"  />
                </Triggers>
                           </asp:UpdatePanel>
                                    
                                    </div>
                 

                                 <div class="form-group"><label class="col-lg-2 control-label">Color 4 : </label>
                              
                                    <div class="col-lg-3">
                                     <asp:UpdatePanel ID="UpdatePanel11" runat="server">
   <ContentTemplate>
                                   <asp:DropDownList ID="DropDownList9" runat="server" CssClass="form-control" 
                                        data-width="100%"  Width="265px"></asp:DropDownList>
                                    </ContentTemplate>
                                     <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click"  />
                  <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click"  />
                </Triggers>
                           </asp:UpdatePanel>
                                    
                                    </div>
                               
                                </div>
                                </div>
                        
                                      
                                            <div class="form-group"><label class="col-lg-2 control-label">Composition : </label>
                              
                                    <div class="col-lg-1">
                                     <asp:UpdatePanel ID="UpdatePanel30" runat="server">
   <ContentTemplate>
        <asp:DropDownList ID="DropDownList7" runat="server" CssClass="form-control" 
                                        data-width="100%"  Width="95px"></asp:DropDownList>
                                  
                                    </ContentTemplate>
                                     <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click"  />
                  <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click"  />
                </Triggers>
                           </asp:UpdatePanel>
                                    
                                    </div>
                             
                                    <div class="col-lg-2">
                              
                                     <asp:UpdatePanel ID="UpdatePanel31" runat="server">
   <ContentTemplate> 
     <asp:TextBox ID="TextBox7" runat="server" class="form-control input-x2 dropbox" Width="165px"></asp:TextBox>
                              
                                    </ContentTemplate>
                                     <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click"  />
                  <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click"  />
                </Triggers>
                           </asp:UpdatePanel>
                                    
                                    </div>
                                    <div class="col-lg-1">
                                  <asp:Button ID="Button13" runat="server" Text="New" CssClass="btn-primary" 
                                         Width="60px" onclick="Button13_Click"  ></asp:Button>
                                 </div>
                           

                                  <div class="form-group"><label class="col-lg-2 control-label">Color to be Changed : </label>
                              
                                    <div class="col-lg-3">
                                     <asp:UpdatePanel ID="UpdatePanel32" runat="server">
   <ContentTemplate>
                                    <asp:TextBox ID="TextBox8" runat="server" class="form-control input-x2 dropbox"></asp:TextBox>
                                    </ContentTemplate>
                                     <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click"  />
                  <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click"  />
                </Triggers>
                           </asp:UpdatePanel>
                                    
                                    </div>
                               
                                </div>
          
                                </div>
                                 <asp:UpdatePanel ID="UpdatePanel33" runat="server">
   <ContentTemplate>
<asp:Button ID="Button19" runat="server" Text="Button" style="display:none" />
  
  
    <asp:Panel ID="Panel4" runat="server" class="panel1" BorderColor="Black" BorderStyle="Solid" BackColor="White" Direction="LeftToRight" style="display:none;" 
                         HorizontalAlign="Left" ScrollBars="Both" Width="400px" Height="250px" >
    
       
       <div style="padding:12px; border:1px solid #e5e5e5;   border-radius:10px; background-color:#E6E6FA;color:#233445; font-size:15px; font-weight:400px; font-family: 'Open Sans',"HelveticaNeue", "Helvetica Neue", Helvetica, Arial,sans-serif; ">
                     <h3 style="font-size:20px; float:left; " class="control-label"> </h3>Add Composition Details  <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/exit11.png" width="30px" height="30px" style="float:right" />
  
  
         
        </div>
        <div class="tablestyles">
        <table>
       
        <tr>
        <td>
            <asp:Label ID="Label6" runat="server" Text="Composition Id" Width="200px" class="col-lg-3 control-label"></asp:Label></td>
        <td>
            <asp:Label ID="Label7" runat="server" Text="" ></asp:Label></td>
        </tr>
        <tr>
        <td>
            <asp:Label ID="Label8" runat="server" Text="Composition Name" Width="200px" class="col-lg-3 control-label"></asp:Label></td>
        <td>
            <asp:TextBox ID="TextBox9" runat="server"  class="form-control input-x2 dropbox"></asp:TextBox></td>
        </tr>

        </tr>
            <tr>
            <td></td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel34" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Button ID="Button20" runat="server"  CssClass="btn-primary"  onclick="Button20_Click"
                                style="height: 26px" Text="Add Composition" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel35" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="Button21" runat="server"  Visible="false" 
                                Text="Delete" />
                            &nbsp;&nbsp;&nbsp;
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:Label ID="Label23" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
       </div>

        </asp:Panel>
       <asp:ModalPopupExtender ID="ModalPopupExtender4" runat="server" 
           TargetControlID="Button19" PopupControlID="Panel4" 
           CancelControlID="ImageButton4" BackgroundCssClass="modelbackground" >
           
        </asp:ModalPopupExtender>

        </ContentTemplate>
    <Triggers>
                 <asp:AsyncPostBackTrigger ControlID="Button6" EventName="Click"  />
                </Triggers>
    </asp:UpdatePanel>


     

                                               <div class="form-group"><label class="col-lg-2 control-label">Qualifier : </label>
                              
                                    <div class="col-lg-3">
                                     <asp:UpdatePanel ID="UpdatePanel6" runat="server">
   <ContentTemplate>
                                   <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" 
                                        data-width="100%" AutoPostBack="true" width="265px"
                                      ></asp:DropDownList>
                                    </ContentTemplate>
                                     <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click"  />
                  <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click"  />
                </Triggers>
                           </asp:UpdatePanel>
                                    
                                    </div>
                                            <div class="col-lg-1">
                                  <asp:Button ID="Button8" runat="server" Text="New" CssClass="btn-primary" 
                                         Width="60px" onclick="Button8_Click"  ></asp:Button>
                                 </div>
                                        <div class="form-group"><label class="col-lg-2 control-label">Thread Color : </label>
                              
                                    <div class="col-lg-3">
                                     <asp:UpdatePanel ID="UpdatePanel7" runat="server">
   <ContentTemplate>
                                    <asp:TextBox ID="TextBox4" runat="server" class="form-control input-x2 dropbox"></asp:TextBox>
                                    </ContentTemplate>
                                     <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click"  />
                  <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click"  />
                </Triggers>
                           </asp:UpdatePanel>
                                    
                                    </div>
                               
                                </div>
                                </div>
 <asp:UpdatePanel ID="UpdatePanel26" runat="server">
   <ContentTemplate>
<asp:Button ID="Button4" runat="server" Text="Button" style="display:none" />
  
  
    <asp:Panel ID="Panel1" runat="server" class="panel1" BorderColor="Black" BorderStyle="Solid" BackColor="White" Direction="LeftToRight" style="display:none;" 
                         HorizontalAlign="Left" ScrollBars="Both" Width="400px" Height="200px" >
    
       
       <div style="padding:12px; border:1px solid #e5e5e5;   border-radius:10px; background-color:#E6E6FA;color:#233445; font-size:15px; font-weight:400px; font-family: 'Open Sans',"HelveticaNeue", "Helvetica Neue", Helvetica, Arial,sans-serif; ">
                     <h3 style="font-size:20px; float:left; " class="control-label"> </h3>Add Qualifier Details  <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/exit11.png" width="30px" height="30px" style="float:right" />
  
  
         
        </div>
        <div class="tablestyles">
        <table>
       
        <tr>
        <td>
            <asp:Label ID="Label9" runat="server" Text="Qualifier Id" Width="200px" class="col-lg-3 control-label"></asp:Label></td>
        <td>
            <asp:Label ID="Label10" runat="server" Text="" ></asp:Label></td>
        </tr>
        <tr>
        <td>
            <asp:Label ID="Label11" runat="server" Text="Company Name" Width="200px" class="col-lg-3 control-label"></asp:Label></td>
        <td>
            <asp:TextBox ID="TextBox14" runat="server"  class="form-control input-x2 dropbox"></asp:TextBox></td>
        </tr>
            <tr>
            <td></td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel24" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Button ID="Button5" runat="server"  CssClass="btn-primary"  onclick="Button5_Click" 
                                style="height: 26px" Text="Add Qualifier" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel25" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="Button6" runat="server"  Visible="false" 
                                Text="Delete" />
                            &nbsp;&nbsp;&nbsp;
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:Label ID="Label19" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
       </div>

        </asp:Panel>
       <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" 
           TargetControlID="Button4" PopupControlID="Panel1" 
           CancelControlID="ImageButton2" BackgroundCssClass="modelbackground" >
           
        </asp:ModalPopupExtender>

        </ContentTemplate>
    <Triggers>
                 <asp:AsyncPostBackTrigger ControlID="Button6" EventName="Click"  />
                </Triggers>
    </asp:UpdatePanel>
                                         

                     <div class="form-group"><label class="col-lg-2 control-label">Weight : </label>
                              
                                    <div class="col-lg-3">
                                     <asp:UpdatePanel ID="UpdatePanel5" runat="server">
   <ContentTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" class="form-control input-x2 dropbox"></asp:TextBox>
                                    </ContentTemplate>
                                     <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click"  />
                  <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click"  />
                </Triggers>
                           </asp:UpdatePanel>
                                    
                                    </div>
                                                                            <div class="form-group"><label class="col-lg-3 control-label">Fabric Quality : </label>
                              
                                    <div class="col-lg-3">
                                     <asp:UpdatePanel ID="UpdatePanel12" runat="server">
   <ContentTemplate>
                                    <asp:TextBox ID="TextBox6" runat="server" class="form-control input-x2 dropbox"></asp:TextBox>
                                    </ContentTemplate>
                                     <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click"  />
                  <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click"  />
                </Triggers>
                           </asp:UpdatePanel>
                                    
                                    </div>
                               
                                </div>
                                  
                                 <asp:UpdatePanel ID="UpdatePanel22" runat="server">
   <ContentTemplate>
<asp:Button ID="Button9" runat="server" Text="Button" style="display:none" />
  
  
    <asp:Panel ID="Panel3" runat="server" class="panel1" BorderColor="Black" BorderStyle="Solid" BackColor="White" Direction="LeftToRight" style="display:none;" 
                         HorizontalAlign="Left" ScrollBars="Both" Width="400px" Height="250px" >
    
       
       <div style="padding:12px; border:1px solid #e5e5e5;   border-radius:10px; background-color:#E6E6FA;color:#233445; font-size:15px; font-weight:400px; font-family: 'Open Sans',"HelveticaNeue", "Helvetica Neue", Helvetica, Arial,sans-serif; ">
                     <h3 style="font-size:20px; float:left; " class="control-label"> </h3>Add Unit Details  <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/exit11.png" width="30px" height="30px" style="float:right" />
  
  
         
        </div>
        <div class="tablestyles">
        <table>
       
        <tr>
        <td>
            <asp:Label ID="Label20" runat="server" Text="Unit Id" Width="200px" class="col-lg-3 control-label"></asp:Label></td>
        <td>
            <asp:Label ID="Label21" runat="server" Text="" ></asp:Label></td>
        </tr>
        <tr>
        <td>
            <asp:Label ID="Label22" runat="server" Text="Unit Name" Width="200px" class="col-lg-3 control-label"></asp:Label></td>
        <td>
            <asp:TextBox ID="TextBox28" runat="server"  class="form-control input-x2 dropbox"></asp:TextBox></td>
        </tr>

        </tr>
            <tr>
            <td></td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel23" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Button ID="Button10" runat="server"  CssClass="btn-primary" 
                                style="height: 26px" Text="Add Unit" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel27" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="Button11" runat="server"  Visible="false" 
                                Text="Delete" />
                            &nbsp;&nbsp;&nbsp;
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:Label ID="Label34" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
       </div>

        </asp:Panel>
       <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" 
           TargetControlID="Button9" PopupControlID="Panel3" 
           CancelControlID="ImageButton3" BackgroundCssClass="modelbackground" >
           
        </asp:ModalPopupExtender>

        </ContentTemplate>
    <Triggers>
                 <asp:AsyncPostBackTrigger ControlID="Button6" EventName="Click"  />
                </Triggers>
    </asp:UpdatePanel>
                                
                                </div>

                                  <div class="form-group"><label class="col-lg-2 control-label">Yarn Count : </label>
                              
                                    <div class="col-lg-1">
                                     <asp:UpdatePanel ID="UpdatePanel18" runat="server">
   <ContentTemplate> 
    <asp:TextBox ID="TextBox11" runat="server" class="form-control input-x2 dropbox"  Width="80px"></asp:TextBox>
                                    </ContentTemplate>
                                     <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click"  />
                  <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click"  />
                </Triggers>
                           </asp:UpdatePanel>
                                    
                                    </div>
                                                                        <div class="col-lg-1">
                                     <asp:UpdatePanel ID="UpdatePanel36" runat="server">
   <ContentTemplate> 
    <asp:TextBox ID="TextBox5" runat="server" class="form-control input-x2 dropbox"  Width="80px"></asp:TextBox>
                                    </ContentTemplate>
                                     <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click"  />
                  <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click"  />
                </Triggers>
                           </asp:UpdatePanel>
                                    
                                    </div>
                                           <div class="col-lg-1">
                                     <asp:UpdatePanel ID="UpdatePanel37" runat="server">
   <ContentTemplate> 
    <asp:TextBox ID="TextBox3" runat="server" class="form-control input-x2 dropbox"  Width="80px"></asp:TextBox>
 
                                    </ContentTemplate>
                                     <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click"  />
                  <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click"  />
                </Triggers>
                           </asp:UpdatePanel>
                                    
                                    </div>
                                         <div class="form-group"><label class="col-lg-3 control-label">Time Consume : </label>
                              
                                    <div class="col-lg-1">
                                     <asp:UpdatePanel ID="UpdatePanel10" runat="server">
   <ContentTemplate>
                                    <asp:TextBox ID="TextBox12" runat="server" class="form-control input-x2 dropbox" Width="90px"></asp:TextBox>
                                    </ContentTemplate>
                                     <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click"  />
                  <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click"  />
                </Triggers>
                           </asp:UpdatePanel>
                                    
                                    </div>
                             
                                    <div class="col-lg-2">
                              
                                     <asp:UpdatePanel ID="UpdatePanel14" runat="server">
   <ContentTemplate> 
                                   <asp:DropDownList ID="DropDownList4" runat="server" CssClass="form-control" 
                                        data-width="100%" AutoPostBack="true" Width="165px">                             
                                              <asp:ListItem>Hours</asp:ListItem>
                                   <asp:ListItem>Minutes</asp:ListItem>
                                   </asp:DropDownList>

                                 </ContentTemplate>
                                     <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click"  />
                  <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click"  />
                </Triggers>
                           </asp:UpdatePanel>
                                    
                                    </div>
                      

                                </div>
                              

                                     <div class="form-group"><label class="col-lg-2 control-label">Color 1 : </label>
                              
                                    <div class="col-lg-3">
                                     <asp:UpdatePanel ID="UpdatePanel42" runat="server">
   <ContentTemplate>
                                     <asp:DropDownList ID="DropDownList11" runat="server" CssClass="form-control" 
                                        data-width="100%"  Width="265px"></asp:DropDownList>
                                    </ContentTemplate>
                                     <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click"  />
                  <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click"  />
                </Triggers>
                           </asp:UpdatePanel>
                                    
                                    </div>
                             
                                <div class="form-group"><label class="col-lg-3 control-label">Product Name : </label>
                              
                                    <div class="col-lg-3">
                                     <asp:UpdatePanel ID="UpdatePanel13" runat="server">
   <ContentTemplate>
                                    <asp:TextBox ID="TextBox10" runat="server" class="form-control input-x2 dropbox"></asp:TextBox>
                                    </ContentTemplate>
                                     <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click"  />
                  <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click"  />
                </Triggers>
                           </asp:UpdatePanel>
                                    
                                    </div>
                               
                                
                                </div>
                      
                                </div>


                                      <div class="form-group"><label class="col-lg-2 control-label">Color 2 : </label>
                              
                                    <div class="col-lg-3">
                                     <asp:UpdatePanel ID="UpdatePanel20" runat="server">
   <ContentTemplate>
                                    <asp:DropDownList ID="DropDownList8" runat="server" CssClass="form-control" 
                                        data-width="100%"  Width="265px"></asp:DropDownList>
                                    </ContentTemplate>
                                     <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click"  />
                  <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click"  />
                </Triggers>
                           </asp:UpdatePanel>
                                    
                                    </div>
                             
                               <div class="form-group"><label class="col-lg-3 control-label">Quality Check : </label>
                              
                                    <div class="col-lg-3">
                                     <asp:UpdatePanel ID="UpdatePanel19" runat="server">
   <ContentTemplate>
                                   <asp:DropDownList ID="DropDownList5" runat="server" CssClass="form-control" 
                                        data-width="100%" AutoPostBack="true" width="265px">  
                                        <asp:ListItem>Yes</asp:ListItem>
                                   <asp:ListItem>No</asp:ListItem>
                                   </asp:DropDownList>
                                    </ContentTemplate>
                                     <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click"  />
                  <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click"  />
                </Triggers>
                           </asp:UpdatePanel>
                                        </div>
                                    </div>
                      
                                </div>

                            </div>
                      </div>
                      <asp:UpdatePanel ID="UpdatePanel2" runat="server">
   <ContentTemplate>

                      <asp:Button ID="Button1" runat="server" class="btn-primary" Width="70px" Height="30px"  Text="Save" onclick="Button1_Click" onclientclick ="return confirm(' Do you want to Save this Record')"
                          ></asp:Button>&nbsp;
                          <asp:Button ID="Button12" runat="server" class="btn-primary" Width="70px" Height="30px"  Text="Update" onclick="Button12_Click" onclientclick ="return confirm(' Do you want to Update this Record')" 
                          ></asp:Button>&nbsp;
 <asp:Button ID="Button2" runat="server" class="btn-primary" Width="70px" Height="30px"  Text="Clear"  onclick="Button2_Click"
                          ></asp:Button>
                          </ContentTemplate>
                           </asp:UpdatePanel>
                            </asp:Panel>

    </div>
                                        <!-- End .form-group  -->
                                        
                                       
                                       
                                        
                                   
                                </div>
                                 
                            </div><!-- End .panel -->  




                       <asp:TextBox ID="TextBox1" runat="server" 
          AutoPostBack="true"  Width="200"></asp:TextBox>
                           <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="TextBox1" WatermarkText="Search Product Name"></asp:TextBoxWatermarkExtender>
                                  
                           <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" MinimumPrefixLength="1" ServiceMethod="SearchSupplierName" FirstRowSelected = "false" CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="TextBox1"  CompletionListCssClass="completionList"
     CompletionListItemCssClass="listItem"
     CompletionListHighlightedItemCssClass="itemHighlighted">
      </asp:AutoCompleteExtender>
                         <br />
                        



                        </div>
                        <br />
                      
                          <div class="panel panel-default">
  <div class="panel-body">
  <div class="col-md-12">
  <br /> <div class="row">
    <div class="col-md-1" ><h1>Filters</h1>
 
 </div>
 </div>
  <div class="row">


    
   <div class="col-md-4"><h3>Product Name : </h3>
   
    <asp:UpdatePanel ID="UpdatePanel15" runat="server">
   <ContentTemplate>

   <asp:DropDownList ID="DropDownList2" runat="server"  
           class="form-control input-x2 dropbox" data-style="btn-primary1" 
           data-width="100%" AutoPostBack="true" 
           onselectedindexchanged="DropDownList2_SelectedIndexChanged1"   ></asp:DropDownList>
    </ContentTemplate>
                           </asp:UpdatePanel>
   
   </div>

        <div class="col-md-3"><h3>Division :</h3>

      <asp:UpdatePanel ID="UpdatePanel16" runat="server">
   <ContentTemplate>
     <asp:TextBox ID="TextBox15" runat="server" class="form-control input-x2 dropbox" 
           AutoPostBack="true" ontextchanged="TextBox15_TextChanged" ></asp:TextBox> 
       <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" MinimumPrefixLength="1" ServiceMethod="SearchCustomers1" FirstRowSelected = "false" CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="TextBox15"  CompletionListCssClass="completionList"
     CompletionListItemCssClass="listItem"
     CompletionListHighlightedItemCssClass="itemHighlighted">
      </asp:AutoCompleteExtender>
         <div class="col-md-12">
          <asp:Button ID="Button15" runat="server" class="btn-primary" Width="70px" Height="30px"  Text="Clear" onclick="Button15_Click"
                          ></asp:Button>
         </div>
       </ContentTemplate>
                           </asp:UpdatePanel>

     </div>
   </div>
</div>
                                        <!-- End .form-group  -->
                                        
                                       
                                       
                                        
                                   
                                </div>
                                 
                            </div><!-- End .panel -->

                     
                     <br />
                   
                         <div class="container">
 
  <div class="panel panel-default">
  <div class="panel-body">
                        <div class="col-md-12" >
                                <div class="panel-heading">
                                    <h3 class="panel-title"><%--Show &nbsp;--%><%--<asp:DropDownList ID="DropDownList4" runat="server" class="dropbox1" style="margin-top:10px;">
                                    <asp:ListItem>5</asp:ListItem>
                                        <asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem>25</asp:ListItem>
                                        <asp:ListItem>50</asp:ListItem>
                                        <asp:ListItem>100</asp:ListItem>
                                        <asp:ListItem>200</asp:ListItem>
                                        <asp:ListItem>300</asp:ListItem>
                                        <asp:ListItem>400</asp:ListItem>
                                        <asp:ListItem>500</asp:ListItem>
                                        <asp:ListItem>700</asp:ListItem>
                                        <asp:ListItem>1000</asp:ListItem>
                                        <asp:ListItem></asp:ListItem>
                                    
                                    
                                    </asp:DropDownList>--%><%--&nbsp; Entries </h3>--%>
                                    <hr />
                         <%--           <div class="panel-actions">
                                        <a href="#" class="panel-action panel-action-toggle" data-panel-toggle></a>
                                        <a href="#" class="panel-action panel-action-dismiss" data-panel-dismiss></a>
                                    </div>--%>
                                </div>

<div class="panel-body">                          
<div class="col-md-12">
   <asp:UpdatePanel ID="UpdatePanel8" runat="server">
   <ContentTemplate>
   <asp:GridView ID="GridView1" runat="server" Width="100%" CellPadding="3" 
         Font-Size="16px" HeaderStyle-HorizontalAlign="center" 
           AutoGenerateColumns="False" AllowPaging="True" 
        onpageindexchanging="GridView1_PageIndexChanging" 
        onrowdatabound="GridView1_RowDataBound" PageSize="10" BackColor="White" 
           BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
       <Columns>
        <asp:TemplateField HeaderText="Select">
           
           <ItemTemplate>
               <asp:CheckBox ID="CheckBox3" runat="server" />
            </ItemTemplate>
           
            <HeaderStyle CssClass="Grd1" />
            <ItemStyle CssClass="Grd1" />
           
           </asp:TemplateField>
         
           <asp:BoundField HeaderText="Revised No" DataField="Revised_Fab_id"  >
           <HeaderStyle CssClass="Grd1" />
           <ItemStyle CssClass="Grd1" />
           </asp:BoundField>
           <asp:BoundField HeaderText="Product Name" DataField="Product_Name" >
           <HeaderStyle CssClass="Grd1" />
           <ItemStyle CssClass="Grd1" />
           </asp:BoundField>
            <asp:BoundField HeaderText="Fabric ID" DataField="Fabric_No" >
           <HeaderStyle CssClass="Grd1" />
           <ItemStyle CssClass="Grd1" />
           </asp:BoundField>
           <asp:BoundField HeaderText="Division" DataField="Division"  >
             <HeaderStyle CssClass="Grd1" />
           <ItemStyle CssClass="Grd1" />
           </asp:BoundField>
             <asp:BoundField HeaderText="Composition" DataField="Composition"  >
               <HeaderStyle CssClass="Grd1" />
           <ItemStyle CssClass="Grd1" />
           </asp:BoundField>
               <asp:BoundField HeaderText="Comp.Per" DataField="Composition_Per"  >
                 <HeaderStyle CssClass="Grd1" />
           <ItemStyle CssClass="Grd1" />
           </asp:BoundField>
                 <asp:BoundField HeaderText="Qualifier Company" DataField="Qualifier"  >
                   <HeaderStyle CssClass="Grd1" />
           <ItemStyle CssClass="Grd1" />
           </asp:BoundField>
                   <asp:BoundField HeaderText="Color Changed" DataField="Color_Changed"  > 
                   <HeaderStyle CssClass="Grd1" />
           <ItemStyle CssClass="Grd1" />
           </asp:BoundField>
                   <asp:BoundField HeaderText="Quality Check" DataField="Quality_Check"  >
           <HeaderStyle CssClass="Grd1" />
           <ItemStyle CssClass="Grd1" />
           </asp:BoundField>

            <asp:TemplateField HeaderText="Edit">
          <ItemTemplate>
            
          <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/edit4.jpg" Height="20px" Width="20px" onclick="ImageButton1_Click"  ></asp:ImageButton>
          </ItemTemplate>
          
                <HeaderStyle CssClass="Grd1" />
                <ItemStyle CssClass="Grd1" />
          
          </asp:TemplateField>
           <asp:TemplateField HeaderText="Delete">
          <ItemTemplate>
              <asp:ImageButton ID="ImageButton9" runat="server" ImageUrl="~/delete3.png" Height="20px" Width="20px" onclick="ImageButton9_Click" onclientclick ="return confirm(' Do you want to Delete this Record')"  />
          
          </ItemTemplate>
          
               <HeaderStyle CssClass="Grd1" />
               <ItemStyle CssClass="Grd1" />
          
          </asp:TemplateField>
       </Columns>
       <FooterStyle BackColor="White" ForeColor="#000066" />
       <HeaderStyle Height="40px" BackColor="#006699" Font-Bold="True" CssClass="red" 
           ForeColor="White" />
       <PagerSettings FirstPageText="First" LastPageText="Last" />
       <PagerStyle Wrap="true" BorderStyle="Solid" Width="100%" 
           CssClass="gvwCasesPager" BackColor="White" ForeColor="#000066" 
           HorizontalAlign="Left" />
       <RowStyle Height="40px" ForeColor="#000066" />
       <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
       <SortedAscendingCellStyle BackColor="#F1F1F1" />
       <SortedAscendingHeaderStyle BackColor="#007DBB" />
       <SortedDescendingCellStyle BackColor="#CAC9C9" />
       <SortedDescendingHeaderStyle BackColor="#00547E" />
       </asp:GridView>
  </ContentTemplate>
    <Triggers>
               <asp:AsyncPostBackTrigger ControlID="GridView1"  />
                 <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click"  />
                 
                  
               
                   
                 <asp:AsyncPostBackTrigger ControlID="DropDownList2" EventName="SelectedIndexChanged"  /> 
                   <asp:AsyncPostBackTrigger ControlID="TextBox15" EventName="TextChanged" />   
               
                </Triggers>
    </asp:UpdatePanel>


  
</div>                     

 </div>
  <asp:Button ID="Button7" runat="server" onclick="Button7_Click"
           Text="Delete Selected Rows" ></asp:Button>
 <asp:Button ID="Button3" runat="server" onclick="Button3_Click"
           Text="Export to excel" ></asp:Button>
  </div>
 </div>
</div>
</div>
  </div>
 </div>
</div>
</div>
                        

                         
        </section>

        <script type="text/javascript" src="js/jquery.min.js"></script>
        <script type="text/javascript" src="bootstrap/js/bootstrap.min.js"></script>
        <script src="js/metisMenu.min.js"></script>
        <script src="js/jquery-jvectormap-1.2.2.min.js"></script>
        <!-- Flot -->
        <script src="js/flot/jquery.flot.js"></script>
        <script src="js/flot/jquery.flot.tooltip.min.js"></script>
        <script src="js/flot/jquery.flot.resize.js"></script>
        <script src="js/flot/jquery.flot.pie.js"></script>
        <script src="js/chartjs/Chart.min.js"></script>
        <script src="js/pace.min.js"></script>
        <script src="js/waves.min.js"></script>
        <script src="js/jquery-jvectormap-world-mill-en.js"></script>
        <!--        <script src="js/jquery.nanoscroller.min.js"></script>-->
        <script type="text/javascript" src="js/custom.js"></script>
        <script type="text/javascript">
            $(function () {

                var barData = {
                    labels: ["January", "February", "March", "April", "May", "June", "July"],
                    datasets: [
                        {
                            label: "My First dataset",
                            fillColor: "rgba(220,220,220,0.5)",
                            strokeColor: "rgba(220,220,220,0.8)",
                            highlightFill: "rgba(220,220,220,0.75)",
                            highlightStroke: "rgba(220,220,220,1)",
                            data: [65, 59, 80, 81, 56, 55, 40]
                        },
                        {
                            label: "My Second dataset",
                            fillColor: "rgba(14, 150, 236,0.5)",
                            strokeColor: "rgba(14, 150, 236,0.8)",
                            highlightFill: "rgba(14, 150, 236,0.75)",
                            highlightStroke: "rgba(14, 150, 236,1)",
                            data: [28, 48, 40, 19, 86, 27, 90]
                        }
                    ]
                };

                var barOptions = {
                    scaleBeginAtZero: true,
                    scaleShowGridLines: true,
                    scaleGridLineColor: "rgba(0,0,0,.05)",
                    scaleGridLineWidth: 1,
                    barShowStroke: true,
                    barStrokeWidth: 2,
                    barValueSpacing: 5,
                    barDatasetSpacing: 1,
                    responsive: true
                };


                var ctx = document.getElementById("barChart").getContext("2d");
                var myNewChart = new Chart(ctx).Bar(barData, barOptions);

            });
        </script>
        <!-- Google Analytics:  -->
        <script>
            (function (i, s, o, g, r, a, m) {
                i['GoogleAnalyticsObject'] = r;
                i[r] = i[r] || function () {
                    (i[r].q = i[r].q || []).push(arguments);
                }, i[r].l = 1 * new Date();
                a = s.createElement(o),
                        m = s.getElementsByTagName(o)[0];
                a.async = 1;
                a.src = g;
                m.parentNode.insertBefore(a, m)
            })(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');
            ga('create', 'UA-3560057-28', 'auto');
            ga('send', 'pageview');
        </script>
        </form>
    </body>
</html>



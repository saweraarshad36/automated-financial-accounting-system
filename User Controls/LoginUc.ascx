<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LoginUc.ascx.cs" Inherits="LoginUc" %>




<link href="../App_Themes/Forms/StyleSheet.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet" href="../App_Themes/css/bootstrap.min.css" />
<link rel="stylesheet" href="../App_Themes/css/slick.css" />
<link rel="stylesheet" href="../App_Themes/css/slicknav.css" />
<link rel="stylesheet" href="../App_Themes/css/style.css" />

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>


        <table align="center" style="width: 100%; height; background-color: #bfdbff;">
            <tr>
                <td style="width: 20%">&nbsp;</td>
                <td align="left" style="width: 59%; text-align: left;">

                    <div class="form-group">
                        <label>User ID<span  class="text-danger">*</span> </label>
                        <asp:TextBox ID="txtUserName" runat="server" name="user" class="form-control validatenumeric" placeholder="Enter User ID" Width="50%" >import</asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>Password<span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtPassword" runat="server" Width="50%" type="password" name="login-password" class="form-control" placeholder="Enter Password" >123456</asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Button ID="BtnSignIn" runat="server" CssClass="btn btn-primary btn-lg btn-block"
                            Height="35px" OnClick="BtnSignIn_Click" Text="Sign In" Width="50%" />


                    </div>
                    <div class="row login_option">
                        <div class="align-items-center col-12 d-flex flex-column flex-sm-row">
                            <div class="">
                                <%-- <label for="remember" class="mb-0">
                                    <input type="checkbox" name="" id="remember" />
                                    Remember Me</label>
                            </div>--%>
                                <div class="ml-sm-auto"><a onclick="forgotpassword()">Forgot Password?
                                    </a></div>
                            </div>
                        </div>
                </td>

                <td align="left" style="width: 20%; height: 29px">
            </tr>



            <tr>
                <td style="width: 20%">&nbsp;</td>
                <td align="left" style="width: 59%; text-align: left;">
                    <asp:Label ID="lblErrorMessage" runat="server" CssClass="Lbl" Font-Bold="True" Font-Size="20pt" ForeColor="Red" Visible="False"></asp:Label>
                </td>
                <td align="left" style="width: 20%; height: 29px">&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 20%"></td>
                <td align="left" style="width: 59%; text-align: center;">&nbsp;<asp:HyperLink ID="HyperLink1" runat="server" Font-Names="Arial" Font-Size="9pt" ForeColor="Red">Forgot Password ? Click here</asp:HyperLink>
                </td>
                <td style="height: 29px" width="20%">

                    <asp:Label ID="lblCounter" runat="server" CssClass="Lbl" Font-Bold="True" Font-Size="Small"></asp:Label>
                </td>

            </tr>
        </table>

        <table width="100%" border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width: 100%">
            <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/top1.png" Width="100%" /></td>
    </tr>
</table>
<table width="100%" border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td style="height: 50px width: 100%">&nbsp;</td>
    </tr>
</table>
<table width="100%" border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width: 5%">&nbsp;</td>
        <td style="width: 25%">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/top21.png" Width="100%" />
        </td>
        <td style="width: 5%">&nbsp;</td>
        <td style="width: 25%">
            <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/top22.png" Width="100%" />
        </td>
        <td style="width: 5%">&nbsp;</td>
        <td style="width: 25%">
            <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/top23.png" Width="100%" />
        </td>
        <td style="width: 5%">&nbsp;</td>
    </tr>
    <tr>
        <td style="width: 5%">&nbsp;</td>
        <td style="width: 25%"></td>
        <td style="width: 5%">&nbsp;</td>
        <td style="width: 25%"></td>
        <td style="width: 5%">&nbsp;</td>
        <td style="width: 25%"></td>
        <td style="width: 5%">&nbsp;</td>
    </tr>
    <tr>
        <td style="width: 5%">&nbsp;</td>
        <td style="width: 25%">

            <asp:Label ID="Label1" runat="server" Text="Safe &amp; Secure Delivery" CssClass="Lbl"></asp:Label>

        </td>
        <td style="width: 5%">&nbsp;</td>
        <td style="width: 25%">

            <asp:Label ID="Label2" runat="server" Text="Safe &amp; Secure Delivery" CssClass="Lbl"></asp:Label>

        </td>
        <td style="width: 5%">&nbsp;</td>
        <td style="width: 25%">

            <asp:Label ID="Label3" runat="server" Text="Safe &amp; Secure Delivery" CssClass="Lbl"></asp:Label>

        </td>
        <td style="width: 5%">&nbsp;</td>
    </tr>


    <tr>
        <td style="width: 5%">&nbsp;</td>
        <td style="width: 25%">

            <asp:Label ID="Label4" runat="server" Text="Safe &amp; Secure Delivery"></asp:Label>

        </td>
        <td style="width: 5%">&nbsp;</td>
        <td style="width: 25%">

            <asp:Label ID="Label5" runat="server" Text="Safe &amp; Secure Delivery"></asp:Label>

        </td>
        <td style="width: 5%">&nbsp;</td>
        <td style="width: 25%">

            <asp:Label ID="Label6" runat="server" Text="Safe &amp; Secure Delivery"></asp:Label>

        </td>
        <td style="width: 5%">&nbsp;</td>
    </tr>


    <tr>
        <td style="width: 5%">&nbsp;</td>
        <td style="width: 25%">&nbsp;</td>
        <td style="width: 5%">&nbsp;</td>
        <td style="width: 25%">&nbsp;</td>
        <td style="width: 5%">&nbsp;</td>
        <td style="width: 25%">&nbsp;</td>
        <td style="width: 5%">&nbsp;</td>
    </tr>


    <tr>
        <td style="width: 5%">&nbsp;</td>
        <td style="width: 25%">

            <asp:Button ID="Button1" runat="server" Text="Read More    &gt;" CssClass="Btn" />

        </td>
        <td style="width: 5%">&nbsp;</td>
        <td style="width: 25%">

            <asp:Button ID="Button2" runat="server" Text="Read More    &gt;" CssClass="Btn" />

        </td>
        <td style="width: 5%">&nbsp;</td>
        <td style="width: 25%">

            <asp:Button ID="Button3" runat="server" Text="Read More    &gt;" CssClass="Btn" />

        </td>
        <td style="width: 5%">&nbsp;</td>
    </tr>


    <tr>
        <td style="width: 5%">&nbsp;</td>
        <td style="width: 25%">&nbsp;</td>
        <td style="width: 5%">&nbsp;</td>
        <td style="width: 25%">&nbsp;</td>
        <td style="width: 5%">&nbsp;</td>
        <td style="width: 25%">&nbsp;</td>
        <td style="width: 5%">&nbsp;</td>
    </tr>


</table>
<table width="100%" border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td style="height: 60px; background-color: #C0C0C0;"></td>
    </tr>
    <tr>
        <td class="auto-style1" style="background-color: #C0C0C0">
            <asp:Label ID="Label7" runat="server" Text="Our Services" CssClass="LblHdr"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="height: 10px; background-color: #C0C0C0;"></td>
    </tr>
    <tr>
        <td class="auto-style1" style="background-color: #C0C0C0">
            <asp:Label ID="Label9" runat="server" Text="My IFI makes it easier than ever to ship online, get quotes, schedule pickups, find locations, track shipments and more!" CssClass="Lbl1"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="auto-style1" style="background-color: #C0C0C0">&nbsp;</td>
    </tr>
    <tr>
        <td style="background-color: #C0C0C0">
            <table height="50px" width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 10%; background-color: #C0C0C0"></td>

                    <td style="width: 23%; background-color: white; text-align: center;">
                        <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/truck.png" Width="88px" Height="88px" />
                        <br />

                        <asp:Label ID="Label10" runat="server" Text="TRANSPORTATION" CssClass="Lbl"></asp:Label>

                        <br />

                        <asp:Label ID="Label11" runat="server" Text="Truck Transport" CssClass="Lbl1"></asp:Label>

                    </td>
                    <td style="width: 2%; background-color: #C0C0C0"></td>

                    <td style="width: 23%; background-color: white; text-align: center;">
                        <asp:Image ID="Image6" runat="server" ImageUrl="~/Images/airline.png" Width="88px" Height="88px" />
                        <br />

                        <asp:Label ID="Label8" runat="server" Text="AERIAL TRANSPORT" CssClass="Lbl"></asp:Label>

                        <br />

                        <asp:Label ID="Label12" runat="server" Text="By Air Transport" CssClass="Lbl1"></asp:Label>

                    </td>
                    <td style="width: 2%; background-color: #C0C0C0"></td>

                    <td style="width: 23%; background-color: white; text-align: center;">
                        <asp:Image ID="Image7" runat="server" ImageUrl="~/Images/airline.png" Width="88px" Height="88px" />
                        <br />

                        <asp:Label ID="Label13" runat="server" Text="RAIL TRANSPORT" CssClass="Lbl"></asp:Label>

                        <br />

                        <asp:Label ID="Label14" runat="server" Text="By Rail Transport" CssClass="Lbl1"></asp:Label>

                    </td>

                    <td style="width: 10%; background-color: #C0C0C0"></td>
                </tr>
            </table>

        </td>
    </tr>
    <tr>
        <td style="height: 60px; background-color: #C0C0C0;"></td>
    </tr>
    <tr>
        <td style="height: 60px; background-color: #FFFFFF;">&nbsp;</td>
    </tr>
    <tr>
        <td>
            <table height="50px" width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 10%; background-color: white"></td>
                    <td style="width: 38%; background-color: white">
                        <asp:Label ID="Label19" runat="server" Text="Our Testimonials" CssClass="Lbl"></asp:Label>
                    </td>
                    <td style="width: 2%; background-color: white"></td>


                    <td style="width: 40%; background-color: white">
                        <asp:Label ID="Label15" runat="server" Text="Why Choose Us" CssClass="Lbl"></asp:Label>
                    </td>
                    <td style="width: 10%; background-color: white"></td>

                </tr>
                <tr>
                    <td style="width: 10%; background-color: white">&nbsp;</td>
                    <td style="width: 38%; background-color: white">
                        <asp:Label ID="Label20" runat="server" Text="By Rail Transport" CssClass="Lbl1"></asp:Label>
                    </td>
                    <td style="width: 2%; background-color: white">&nbsp;</td>


                    <td style="width: 40%; background-color: white">
                        <asp:Label ID="Label16" runat="server" Text="By Rail Transport" CssClass="Lbl1"></asp:Label>
                    </td>
                    <td style="width: 10%; background-color: white">&nbsp;</td>

                </tr>
                <tr>
                    <td style="width: 10%; background-color: white">&nbsp;</td>
                    <td style="width: 38%; background-color: white">&nbsp;</td>
                    <td style="width: 2%; background-color: white">&nbsp;</td>


                    <td style="width: 40%; background-color: white">&nbsp;</td>
                    <td style="width: 10%; background-color: white">&nbsp;</td>

                </tr>
            </table>
        </td>

    </tr>
</table>
<table style="width: 100%;">
    <tr style="height: 250px; background-image: url('../Images/IFIlogonew.PNG'); background-repeat: no-repeat;">
       
        <td style="width: 50%;">
            <table>
                <tr>
                    <td>
                        <asp:Label ID="Label21" runat="server" Text="Request a Quote" CssClass="LblHdr1"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label17" runat="server" Text="Please  fill the form for quote" CssClass="Lbl"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="TextBox1" runat="server" Height="25px" Width="100%"></asp:TextBox>
                        <asp:TextBox ID="TextBox2" runat="server" Height="25px" Width="100%"></asp:TextBox>
                    </td>
                </tr>

            </table>
        </td>
        
    </tr>
</table>

        


    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="BtnSignIn" EventName="Click" />
    </Triggers>
</asp:UpdatePanel>
<script src="../App_Themes/js/jquery.min.js"></script>

<script src=" https://cdnjs.cloudflare.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"> </script>
<script type="text/javascript" src="../App_Themes/js/Crypto.js"></script>

<!-- JS here -->
    <script src="../App_Themes/js/vendor/modernizr-3.5.0.min.js"></script>
    <!-- <script src="js/vendor/jquery-1.12.4.min.js"></script> -->
    <script src="../App_Themes/js/popper.min.js"></script>
    <script src="../App_Themes/js/bootstrap.min.js"></script>
    <script src="../App_Themes/js/owl.carousel.min.js"></script>
    <script src="../App_Themes/js/isotope.pkgd.min.js"></script>
    <script src="../App_Themes/js/ajax-form.js"></script>
    <script src="../App_Themes/js/waypoints.min.js"></script>
    <script src="../App_Themes/js/jquery.counterup.min.js"></script>
    <script src="../App_Themes/js/imagesloaded.pkgd.min.js"></script>
    <script src="../App_Themes/js/scrollIt.js"></script>
    <script src="../App_Themes/js/jquery.scrollUp.min.js"></script>
    <script src="../App_Themes/js/wow.min.js"></script>
    <script src="../App_Themes/js/nice-select.min.js"></script>
    <script src="../App_Themes/js/jquery.slicknav.min.js"></script>
    <script src="../App_Themes/js/jquery.magnific-popup.min.js"></script>
    <script src="../App_Themes/js/plugins.js"></script>
    <!-- <script src="js/gijgo.min.js"></script> -->
    <script src="../App_Themes/js/slick.min.js"></script>
    <script src="../App_Themes/js/numbertowordconvertconvert.js"></script>

    
    <!--contact js-->
    <script src="../App_Themes/js/contact.js"></script>
    <script src="../App_Themes/js/jquery.ajaxchimp.min.js"></script>
    <script src="../App_Themes/js/jquery.form.js"></script>
  

    <script src="../App_Themes/js/main.js"></script>
  
    <!--toastr-->
   <script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/2.0.1/js/toastr.js"></script>
   <script type="text/javascript" src="//cdn.jsdelivr.net/jquery/1/jquery.min.js"></script>
    <script type="text/javascript" src="//cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/js/toastr.min.js"></script>
    <script src="http://code.jquery.com/jquery-migrate-1.2.1.min.js"></script>
 <script src="//cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>

<%--<table width="100%" border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td id="td1" style="width: 100%;"></td>
        <button type="button" class="collapsible">General Ledger (GL) Modulee       (click to Expand)</button>
        <div class="content">
            <p>The General Ledger (GL) module is the heart of finance package of an ERP system. GL provides a central pool of accounting data required for finance reporting (including statutory reports) and other purpose. One of the important functions of GL is to real time update of sub ledger, thus eliminating the time consuming reconciliation. GL also provides summarized data for use in planning, control and reporting.</p>
            <ul>
                <li><span style="color: #333333;">Flexible chart of account code</span></li>
                <li><span style="color: #333333;">Balance Sheet (Profit &amp; Loss Statement)</span></li>
                <li><span style="color: #333333;">Fixed asset management</span></li>
                <li><span style="color: #333333;">Bank account management</span></li>
                <li><span style="color: #333333;">Budgeting</span></li>
                <li><span style="color: #333333;">Accounts consolidation</span></li>
                <li><span style="color: #333333;">Financial dashboards for upper management</span></li>
            </ul>
            <img src="../Images/MP-img1.jpg" alt="GL" width="300" height="200">
            <img src="../Images/MP-img2.jpg" alt="Client" width="300" height="200">
        </div>
        <div class="content">
        </div>
        <button type="button" class="collapsible">Inventory Module:       (click to Expand)</button>
        <div class="content">
            <p>Inventory module facilitates processes of maintaining the appropriate level of stock in a warehouse. The activities of inventory control involves in identifying inventory requirements, setting targets, providing replenishment techniques and options, monitoring item usages, reconciling the inventory balances, and reporting inventory status. Integration of inventory control module with sales, purchase, finance modules allows ERP systems to generate vigilant executive level reports.</p>
            <ul>
                <li><span style="color: #333333;">Item Master and Catalog</span></li>
                <li><span style="color: #333333;">Stock Requisition</span></li>
                <li><span style="color: #333333;">Stock GRN and GIN Status</span></li>
                <li><span style="color: #333333;">Inventory In/Out</span></li>
                <li><span style="color: #333333;">Item Price List</span></li>
                <li><span style="color: #333333;">Stock Return</span></li>
            </ul>
        </div>
        <button type="button" class="collapsible">Sales Module       (click to Expand)</button>
        <div class="content">
            <p>ERP sales module provides a comprehensive order to cash (O2C) sales mechanics for your business. It provides a complete order to receipt cycle keeping in mind the practical complications in day to day sales like sales commissions set up, discounts process and sales checklist. This module is tightly integrated with inventory and other modules with a vigilant tracking of receivables.</p>
        </div>
    </tr>

</table>--%>
<style>
    .collapsible {
        background-color: #777;
        color: white;
        cursor: pointer;
        padding: 18px;
        width: 100%;
        border: none;
        text-align: left;
        outline: none;
        font-size: 15px;
    }

        .active, .collapsible:hover {
            background-color: #555;
        }

    .content {
        padding: 0 18px;
        display: none;
        overflow: hidden;
        background-color: #f1f1f1;
        width: 100%;
        text-align: left;
    }

    .auto-style1 {
        width: 20%;
        height: 29px;
    }

    .auto-style2 {
        width: 59%;
        height: 29px;
    }

    .auto-style3 {
        height: 29px;
    }
</style>

<script>
    var coll = document.getElementsByClassName("collapsible");
    var i;

    for (i = 0; i < coll.length; i++) {
        coll[i].addEventListener("click", function () {
            this.classList.toggle("active");
            var content = this.nextElementSibling;
            if (content.style.display === "block") {
                content.style.display = "none";
            } else {
                content.style.display = "block";
            }
        });
    }
</script>

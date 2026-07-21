<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Pages_Form" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>

    <style>
       body {
    margin: 0;
    font-family: 'Segoe UI', sans-serif;

    background: url('../Images/images (2).jfif') no-repeat center center fixed;
    background-size: cover;
}

.page-wrapper {
    position: relative;
}

.page-wrapper::before {
    content: "";
    position: absolute;
    width: 180px;
    height: 180px;
    background: #000;
    opacity: 0.04;
    border-radius: 50%;
    top: -60px;
    left: -60px;
}

/* LEFT BOTTOM CIRCLE */
.page-wrapper::after {
    content: "";
    position: absolute;
    width: 120px;
    height: 120px;
    background: #000;
    opacity: 0.06;
    border-radius: 50%;
    bottom: -40px;
    left: 80px;
}

.page-wrapper {
    width: 90%;
    height: 85vh;
    margin: 40px auto;
    background: rgba(255,255,255,0.65);
    backdrop-filter: blur(6px);
    border-radius: 20px;
    display: flex; 
    border: 2px solid #000;
    box-shadow: 0 20px 50px rgba(0,0,0,0.12);

    overflow: hidden; 
}

.login-container {
    width: 38%;
    padding: 90px 60px;
    position: relative;
    z-index: 2;

    background: linear-gradient(135deg, rgba(255,255,255,0.35), rgba(255,255,255,0.15));
    backdrop-filter: blur(22px);
    border-radius: 22px;
    border: 1px solid rgba(255,255,255,0.45);
    box-shadow: 0 30px 60px rgba(0,0,0,0.25), inset 0 1px 1px rgba(255,255,255,0.6);

    max-height: 85vh;
    overflow-y: auto;
    scroll-behavior: smooth;
}

.image-section {
    width: 62%; 
    background: #F1F5F4;
    position: relative;
    overflow: hidden;
    display: flex;
    align-items: center;
    justify-content: center;
}

.login-container {
    width: 38%;
    padding: 90px 60px;
    position: relative;
    z-index: 2;

    /* REAL GLASS */
    background: linear-gradient(
        135deg,
        rgba(255,255,255,0.35),
        rgba(255,255,255,0.15)
    );
    backdrop-filter: blur(22px);
    -webkit-backdrop-filter: blur(22px);

    border-radius: 22px;
    border: 1px solid rgba(255,255,255,0.45);
    box-shadow:
        0 30px 60px rgba(0,0,0,0.25),
        inset 0 1px 1px rgba(255,255,255,0.6);
}
.login-container::before {
    content: "";
    position: absolute;
    width: 260px;
    height: 260px;
    background: rgba(0,0,0,0.04);
    border-radius: 60% 40% 55% 45%;
    top: 40px;
    left: -120px;
    z-index: -1;
}

.login-container::after {
    content: "";
    position: absolute;
    width: 180px;
    height: 180px;
    background: rgba(0,0,0,0.06);
    border-radius: 50% 60% 40% 55%;
    bottom: 60px;
    right: -80px;   
    z-index: -1;
}


.login-container h2 {
    color: #000;
    margin-bottom: 35px;
    font-size: 28px;
}

.login-container input {
    width: 100%;
    padding: 14px 18px;
    margin-bottom: 18px;
    border-radius: 30px;
    border: 1px solid #ccc;
    outline: none;
    font-size: 15px;
}

.login-container input:focus {
    border-color: #000;
}

/* BUTTON */
.btn {
    width: 160px;
    padding: 12px;
    border-radius: 30px;
    border: none;
    background: #000;
    color: #fff;
    cursor: pointer;
    font-size: 15px;
    transition: 0.3s;
}

.btn:hover {
    background: #333;
}

/* CHANGE PASSWORD LINK */
.change-link {
    display: inline-block;
    margin-top: 20px;
    color: #000;
    text-decoration: none;
    font-size: 14px;
}

.change-link:hover {
    text-decoration: underline;
}

.Lbl {
    color: red;
}

.image-section {
    width: 55%;
    background: #F1F5F4;
    position: relative;
    overflow: hidden;
    display: flex;
    align-items: center;
    justify-content: center;
}


/* BIG SOFT SHAPE */
.image-section::before {
    content: "";
    position: absolute;
    width: 520px;
    height: 520px;
    background: radial-gradient(circle at top right, 
        rgba(0,0,0,0.08), 
        rgba(0,0,0,0.02));
    border-radius: 60% 40% 50% 50%;
    top: -120px;
    right: -140px;
    transform: rotate(18deg);
}

/* SECOND LAYER SHAPE */
.image-section::after {
    content: "";
    position: absolute;
    width: 420px;
    height: 420px;
    background: linear-gradient(135deg,
        rgba(0,0,0,0.06),
        rgba(0,0,0,0.01));
    border-radius: 50% 60% 40% 55%;
    bottom: -100px;
    right: 40px;
    transform: rotate(-12deg);
}

/* COMMON SHAPE STYLE */
.shape {
    position: absolute;
    backdrop-filter: blur(10px);
    background: rgba(255,255,255,0.4);
    box-shadow: 0 25px 45px rgba(0,0,0,0.18);
    border-radius: 60% 40% 55% 45%;
    overflow: hidden;
}

.shape img {
    width: 100%;
    height: 100%;
    object-fit: cover;
}

/* SHAPE 1 */
.shape1 {
    width: 360px;
    height: 360px;
    top: -70px;
    right: -120px;
}

/* SHAPE 2 */
.shape2 {
    width: 260px;
    height: 260px;
    top: 160px;
    right: 90px;
}

/* SHAPE 3 */
.shape3 {
    width: 220px;
    height: 220px;
    bottom: -50px;
    right: 20px;
}
.shape1 {
    right: -120px;
}

.shape2 {
    right: 90px;
}

.shape3 {
    right: 20px;
}
 /* Footer black */
 .footer {
     background-color: #000 !important;
     color: #fff !important;
 }

 .footer a {
     color: #fff !important;
 }

 .footer a:hover {
     color: #f5b342 !important;
 }

 .footer_title {
     color: #f5b342 !important;
 }

 .copy_right {
     color: #ccc !important;
 }
.Lbl {
    font-family: Arial;
    font-size: 14px;
    font-weight: bold;
    margin-top: 15px;
    display: block;
}


    </style>
</head>

<body>
<form id="form1" runat="server">

<div class="page-wrapper">

    <!-- LEFT -->
    <div class="login-container">
        <h2>Login</h2>

        <asp:TextBox ID="txtUserName" runat="server" placeholder="Username"></asp:TextBox>
        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Password"></asp:TextBox>

        <asp:Button ID="BtnDone" runat="server" Text="Login" CssClass="btn"
            OnClick="BtnDone_Click" ValidationGroup="grp2" />

       <div style="display:flex; justify-content:space-between; width:100%; margin-top:20px;">
    
    <asp:LinkButton ID="BtnDone1" runat="server" CssClass="change-link"
        OnClick="BtnDone1_Click">
        Change Password?
    </asp:LinkButton>

    <asp:HyperLink ID="lnkRegister" runat="server" CssClass="change-link"
        NavigateUrl="~/Pages/Registration.aspx">
        Register Account?
    </asp:HyperLink>

</div>


       <div id="changePasswordSection">
    <asp:Label ID="Label6" runat="server" Visible="False" Text="New Password"></asp:Label>
    <asp:TextBox ID="txtPassword1" runat="server" Visible="False" TextMode="Password"></asp:TextBox>

    <asp:Label ID="Label5" runat="server" Visible="False" Text="Confirm Password"></asp:Label>
    <asp:TextBox ID="txtPassword2" runat="server" Visible="False" TextMode="Password"></asp:TextBox>

    <asp:Button ID="BtnDone0" runat="server" Text="Password Change"
        CssClass="btn" Style="background:#999999;"
        Visible="False" OnClick="BtnDone0_Click" />
</div>
        <br /><br />
        <asp:Label ID="lblErrorMessage" runat="server"
            CssClass="Lbl" Font-Bold="True" Visible="False"></asp:Label>
    </div>

   <div class="image-section">

    <div class="shape shape1">
        <img src="../Images/stkbake.jpg" />
    </div>

    <div class="shape shape2">
        <img src="../Images/images (2).jfif" />
    </div>

    <div class="shape shape3">
        <img src="../Images/imgc.jfif" />
    </div>

</div>



</div>
           <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css" />

<div class="footer"
     style="width:100%;
            background:#eef1f5;
            border-top:1px solid #eef1f5;
            box-shadow:0 -4px 20px rgba(0,0,0,0.15);
            color:#eef1f5;
            font-family:'Segoe UI', Arial, sans-serif;
            padding:55px 60px 30px 60px;">

  <div class="footer-container"
       style="display:flex;
              justify-content:space-between;
              align-items:flex-start;
              flex-wrap:wrap;
              max-width:1280px;
              margin:0 auto;">

    <!-- GENERAL -->
    <div class="footer_widget" style="flex:1 1 220px; min-width:200px;">
      <h3 style="font-weight:700; font-size:18px; color:#eef1f5;">GENERAL</h3>
      <div style="height:2px; width:45px; background:#eef1f5; margin:8px 0 18px;"></div>

      <p><a href="Login.aspx" style="text-decoration:none; color:#eef1f5;">
        <i class="fas fa-home"></i> Home</a></p>

      <p><a href="about.aspx" style="text-decoration:none; color:#eef1f5;">
        <i class="fas fa-info-circle"></i> About Us</a></p>

      <p><a href="Contact.aspx" style="text-decoration:none; color:#eef1f5;">
        <i class="fas fa-envelope"></i> Contact</a></p>
    </div>

    <!-- CONTACT DETAILS -->
    <div class="footer_widget" style="flex:1 1 340px; min-width:280px;">
      <h3 style="font-weight:700; font-size:18px; color:#eef1f5;">CONTACT DETAILS</h3>
      <div style="height:2px; width:45px; background:#eef1f5; margin:8px 0 18px;"></div>

      <p style="color:#eef1f5;">
        <i class="fas fa-envelope"></i> <b>Email:</b> saweraarshad9921@gmail.com
      </p>
      <p style="color:#eef1f5;">
        <i class="fas fa-phone"></i> <b>Phone:</b> 0335-9990470
      </p>
      <p style="color:#eef1f5;">
        <i class="fas fa-map-marker-alt"></i>
        <b>Head Office:</b> Suite # 608, 6th Floor Business Plaza,<br>
        Mumtaz Hassan Road, Karachi
      </p>
    </div>

    <!-- FOLLOW US -->
    <div class="footer_widget" style="flex:1 1 240px; min-width:220px; text-align:center;">
      <div style="margin-bottom:12px;">
        <img src="../Images/afl3.jfif.png" alt="Logo"
             style="width:90px; border-radius:12px;
                    box-shadow:0 4px 12px rgba(0,0,0,0.25);" />
      </div>

      <h3 style="font-weight:700; font-size:18px; color:#eef1f5;">FOLLOW US</h3>
      <div style="height:2px; width:45px; background:#eef1f5; margin:8px auto 15px;"></div>

      <div style="font-size:20px;">
        <a href="#" style="color:#eef1f5; margin:0 8px;"><i class="fab fa-facebook-f"></i></a>
        <a href="#" style="color:#eef1f5; margin:0 8px;"><i class="fab fa-twitter"></i></a>
        <a href="#" style="color:#eef1f5; margin:0 8px;"><i class="fab fa-linkedin-in"></i></a>
      </div>
    </div>

  </div>

  


  <!-- COPYRIGHT BAR -->
  <div style="border-top:1px solid rgba(255,255,255,0.15); text-align:center; padding-top:12px; margin-top:40px; font-size:13px; color:#bbb;">
    &copy; <script>document.write(new Date().getFullYear());</script> Logix-Soft. All rights reserved.
  </div>
</footer>


</form>
</body>
</html>
<script>
    document.addEventListener('DOMContentLoaded', function () {
        var changeBtn = document.getElementById('<%= BtnDone1.ClientID %>');
        var section = document.getElementById('changePasswordSection');

        changeBtn.addEventListener('click', function () {

            section.querySelectorAll('input, label, button').forEach(function (el) {
                el.style.display = 'block';
            });

            section.scrollIntoView({ behavior: 'smooth', block: 'center' });
        });
    });
</script>
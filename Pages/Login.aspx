<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Pages_Login" %>
<%@ Register Src="~/Controls/UpdateProgressUc.ascx" TagName="UpdateProgressUc" TagPrefix="uc2" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AFMS.com</title>
     <style>
        body {
            margin: 0;
            font-family: 'Segoe UI', sans-serif;
            background: #eef1f5;
            color: #000;
        }
        .topbar {
            background: #111;
            color: #fff;
            padding: 18px 70px;
            display: flex;
            justify-content: space-between;
            align-items: center;
        }

        .logo {
            font-weight: 600;
            font-size: 18px;
        }
        .hero {
            background: #111;
            color: white;
            width: 88%;
            margin: 40px auto;
            border-radius: 30px;
            padding: 80px;
            display: flex;
            justify-content: space-between;
            align-items: center;
        }

        .hero-left {
            width: 45%;
        }

        .hero-left h1 {
            font-size: 46px;
            line-height: 1.2;
            margin-bottom: 20px;
        }

        .hero-left p {
            color: #cfcfcf;
            margin-bottom: 30px;
            font-size: 16px;
        }

        .btn-start {
            background: #fff;
            color: #000;
            padding: 12px 30px;
            border-radius: 25px;
            font-weight: 600;
            text-decoration: none;
        }

        .hero-right {
            position: relative;
        }

        .hero-right img {
            width: 430px;
            border-radius: 20px;
        }

        /* ===== OUR SERVICES ===== */
        .section {
            width: 88%;
            margin: auto;
            margin-top: 70px;
            text-align: center;
        }

        .section h2 {
            font-size: 30px;
            margin-bottom: 10px;
        }

        .section p {
            color: #666;
            margin-bottom: 40px;
        }

        .cards {
            display: flex;
            justify-content: center;
            gap: 25px;
        }

        .card {
            background: #fff;
            width: 260px;
            padding: 28px;
            border-radius: 18px;
            box-shadow: 0 8px 20px rgba(0,0,0,0.12);
            text-align: left;
        }

        .card h4 {
            margin-bottom: 10px;
        }

        .card p {
            font-size: 14px;
            color: #555;
        }

.section.why

{
   margin-top: 30px;         
    padding: 65px 20px; 
    text-align: center;
    background: linear-gradient(rgba(255,255,255,0.92), rgba(255,255,255,0.92)), url("../Images/stock.jpg");
    background-size: cover;
    background-position: center;
    background-repeat: no-repeat;
}

.section.why h2 {
    font-size: 34px;
    margin-bottom: 10px;
}

.section.why > p {
    color: #6b7280;
    margin-bottom: 50px;
    font-size: 16px;
}

.section.why .cards {
    display: flex;
    justify-content: center;
    gap: 28px;
    flex-wrap: nowrap; 
}

.section.why .card {
    width: 250px;
    padding: 30px 22px;
    border-radius: 20px;

    background: rgba(255, 255, 255, 0.12);
    backdrop-filter: blur(14px);
    -webkit-backdrop-filter: blur(14px);

    /* 🔥 NEW FINANCE BORDER */
    border: 1.5px solid #6b7280; 

    box-shadow: 0 15px 40px rgba(0,0,0,0.45);
    text-align: center;
    transition: all 0.35s ease;
}

    /* number */
    .section.why .card span {
        display: block;
        font-size: 32px;
        font-weight: 700;
        color: #6b7280;
        margin-bottom: 14px;
    }

    /* title */
    .section.why .card h4 {
        font-size: 17px;
        margin-bottom: 10px;
        color: #111827;
    }

    /* description */
    .section.why .card p {
        font-size: 14px;
        line-height: 1.5;
        color: #6b7280;
    }

    /* hover */
    .section.why .card:hover {
        transform: translateY(-6px);
        box-shadow: 0 14px 30px rgba(0,0,0,0.15);
    }
        .page-content {
            width: 88%;
            margin: auto;
            margin-top: 80px;
        }

        .footer {
            margin-top: 90px;
            background: #111;
            color: #aaa;
            text-align: center;
            padding: 15px;
        }
.login-container {
    width: 380px;
    background: #ffffff;
    margin: 80px auto;
    padding: 35px;
    border-radius: 20px;
    box-shadow: 0 10px 30px rgba(0,0,0,0.2);
    text-align: center;
}

.login-container h2 {
    margin-bottom: 25px;
}

.login-container input[type=text],
.login-container input[type=password] {
    width: 90%;
    padding: 12px;
    margin-bottom: 15px;
    border-radius: 8px;
    border: 1px solid #ccc;
}

.login-container .btn {
    width: 95%;
    padding: 12px;
    background: #111;
    color: white;
    border: none;
    border-radius: 25px;
    font-weight: 600;
    cursor: pointer;
}

.change-link {
    display: block;
    margin-top: 12px;
    color: #0a7cff;
}
.center-links {
    position: absolute;
    top: 25px;                 /* 🔥 thora aur upar */
    left: 50%;
    transform: translateX(-50%);
    display: flex;
    gap: 25px;
    z-index: 20;
}

.center-links a {
    background: white;
    color: black;
    text-decoration: none;
    padding: 10px 26px;
    border-radius: 50px;
    font-weight: 600;
    font-size: 15px;
    box-shadow: 0 6px 18px rgba(0,0,0,0.4);
    transition: all 0.3s ease;
}

.center-links a:hover {
    background: #e5e7eb;
    transform: translateY(-3px);
}
.hero-video {
    width: 430px;
    height: auto;
    border-radius: 20px;
    object-fit: cover;
}
.hero-right {
    position: relative;
    width: 430px;
}

.video-border {
    padding: 4px;
    border-radius: 24px;
    background: linear-gradient(
        120deg,
        #e5e7eb,
        transparent,
        #e5e7eb
    );
    background-size: 300% 300%;
    animation: sparkle 3s ease-in-out infinite;
    box-shadow: 0 0 18px rgba(229, 231, 235, 0.6);
}
.video-border {
    position: relative;
}
.hero-right {
    position: relative;
}

@keyframes sparkle {
    0% {
        background-position: 0% 50%;
        box-shadow: 0 0 12px rgba(229, 231, 235, 0.4);
    }
    50% {
        background-position: 100% 50%;
        box-shadow: 0 0 28px rgba(229, 231, 235, 0.9);
    }
    100% {
        background-position: 0% 50%;
        box-shadow: 0 0 12px rgba(229, 231, 235, 0.4);
    }
}
.stats {
    position: absolute;
    bottom: 20px;          
    right: -55px;        
    width: 200px;         
    height: 130px;         
    border-radius: 18px;
    padding: 4px;          
    background: linear-gradient(
        120deg,
        #e5e7eb,
        transparent,
        #e5e7eb
    );
    background-size: 300% 300%;
    animation: sparkle 3s ease-in-out infinite;
    box-shadow: 0 12px 35px rgba(0,0,0,0.35);
    z-index: 10;
}

.stats video {
    width: 100%;
    height: 100%;
    border-radius: 14px;
    object-fit: cover;
    background: #fff;
}

.section {
    padding: 70px 20px;
    background: #eef2f1; 
    text-align: center;
}
.section {
    padding-top: 5px;   
    padding-bottom: 60px;
    margin-top: 0;      
}

.section h2 {
    font-size: 34px;
    margin-bottom: 8px;
}

.section p {
    color: #6b7280;
    margin-bottom: 40px;
}

.cards {
    display: flex;
    justify-content: center;
    gap: 28px;           
    flex-wrap: wrap;
}

.card {
    background: #ffffff;
    width: 240px;
    padding: 26px 22px;
    border-radius: 14px;
    box-shadow: 0 8px 20px rgba(0,0,0,0.06);
    transition: all 0.3s ease;
    text-align: left;

    /* 🔥 SIDE BORDER */
    border: 1px solid #4b5563;
}

.card i {
    font-size: 28px;
    color: #9ca3af;
    margin-bottom: 14px;
    display: block;          /* 🔥 important */
    text-align: center;      /* 🔥 icon center */
}


/* TEXT */
.card h4 {
    font-size: 17px;
    margin-bottom: 8px;
    color: #111827;
}

.card p {
    font-size: 14px;
    line-height: 1.5;
    color: #6b7280;
}

.card:hover {
    transform: translateY(-4px);
    box-shadow: 0 14px 28px rgba(0,0,0,0.1);
}
    </style>
</head>
<body>

    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="720000"></asp:ScriptManager>
        <uc2:UpdateProgressUc ID="UpdateProgressUc1" runat="server" />

      
<div class="center-links">
    <a href="Login.aspx">Home</a>
    <a href="contact.aspx">Contact</a>
    <a href="About.aspx">About Us</a>
    <a href="Login.aspx">Login</a>
</div>
<div class="hero">
    <div class="hero-left">
        <h1>Financial Management System</h1>
        <p>
            A system designed to manage income, expenses, financial transactions
            and financial reports efficiently.
        </p>
        <a href="Form.aspx" class="btn-start">Get Started</a>
    </div>

      <div class="hero-right">
    <div class="video-border">
        <video class="hero-video" autoplay muted loop playsinline>
            <source src="<%= ResolveUrl("~/Images/time.mp4") %>" type="video/mp4" />
        </video>
    </div>

   <div class="stats">
    <video autoplay muted loop playsinline>
        <source src="<%= ResolveUrl("~/Images/Finan.mp4") %>" type="video/mp4" />
    </video>
</div>
</div>
    </div>

<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css"/>

<div class="section">
    <h2>Our Services</h2>
    <p>A complete solution for managing and analyzing financial data.</p>

    <div class="cards">
        <div class="card">
            <i class="fas fa-sitemap"></i>
            <h4>Finance Structure</h4>
            <p>
                Design and maintain a structured financial framework
                for accurate classification of accounts and transactions.
            </p>
        </div>

        <div class="card">
            <i class="fas fa-book"></i>
            <h4>Journal & Ledger Management</h4>
            <p>
                Record financial transactions and automatically post
                them to respective ledgers for consistency.
            </p>
        </div>

        <div class="card">
            <i class="fas fa-chart-line"></i>
            <h4>Reports & Dashboard</h4>
            <p>
                Generate trial balance and income reports with
                graphical insights for better decision making.
            </p>
        </div>
    </div>
</div>

<div class="section why">
    <h2>Why Choose Us</h2>
    <p>Reliable financial solutions built for accuracy, clarity, and growth.</p>

    <div class="cards">
        <div class="card">
    <span>01</span>
    <h4>Qualified Financial Experts</h4>
    <p>
        The system is designed following standard accounting principles
        to support accurate financial records, proper account structure,
        and reliable data management.
    </p>
</div>

        <div class="card">
            <span>02</span>
            <h4>Complete Finance Management</h4>
            <p>
                From chart of accounts to reports and dashboards,
                everything is managed in one integrated system.
            </p>
        </div>

        <div class="card">
            <span>03</span>
            <h4>Transparent & Secure Process</h4>
            <p>
                Every transaction is secure, traceable, and clearly
                presented for confident decision-making.
            </p>
        </div>
    </div>
</div>
          <!-- ✅ Login Box -->
       <div class="login-container" style="display:none;">
            <h2>Login</h2>

            <asp:TextBox ID="txtUserName" runat="server" placeholder="Username"></asp:TextBox><br />
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Password"></asp:TextBox><br />

            <asp:Button ID="BtnDone" runat="server" Text="Login" CssClass="btn" OnClick="BtnDone_Click" ValidationGroup="grp2" />

            <asp:LinkButton ID="BtnDone1" runat="server" CssClass="change-link" OnClick="BtnDone1_Click">
                Change Password?
            </asp:LinkButton>

            <asp:Label ID="Label6" runat="server" Visible="False" Text="New Password"></asp:Label><br />
            <asp:TextBox ID="txtPassword1" runat="server" Visible="False" TextMode="Password"></asp:TextBox><br />

            <asp:Label ID="Label5" runat="server" Visible="False" Text="Confirm Password"></asp:Label><br />
            <asp:TextBox ID="txtPassword2" runat="server" Visible="False" TextMode="Password"></asp:TextBox><br />

            <asp:Button ID="BtnDone0" runat="server" Text="Password Change" CssClass="btn" Style="background:#999999;" Visible="False" OnClick="BtnDone0_Click" />

            <br /><br />
            <asp:Label ID="lblErrorMessage" runat="server" CssClass="Lbl" Font-Bold="True" Visible="False"></asp:Label>
        </div>


      </form>
</body>
</html>

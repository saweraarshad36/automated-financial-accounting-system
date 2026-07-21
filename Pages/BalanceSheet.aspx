<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BalanceSheet.aspx.cs" Inherits="Pages_BalanceSheet" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>Balance Sheet – AFMS</title>
<style>

*, *::before, *::after { box-sizing: border-box; margin: 0; padding: 0; }

body {
    font-family: 'Segoe UI', Arial, sans-serif;
    background: #f0f2f5;
    color: #1a1a1a;
}

/* ── Sidebar ── */
.sidebar {
    width: 80px;
    height: 100vh;
    background: black;
    position: fixed;
    top: 0;
    left: 0;
    display: flex;
    flex-direction: column;
    align-items: center;
    padding: 12px 0;
    z-index: 100;
}

.sidebar-logo {
    width: 44px;
    height: 44px;
    border-radius: 10px;
    overflow: hidden;
    display: flex;
    align-items: center;
    justify-content: center;
    margin-bottom: 4px;
    flex-shrink: 0;
    background: #000;
}
.sidebar-logo img {
    width: 100%;
    height: 100%;
    object-fit: cover;
}

.sidebar-brand {
    font-size: 9px;
    font-weight: 700;
    color: rgba(255,255,255,0.85);
    letter-spacing: 0.12em;
    text-transform: uppercase;
    margin-bottom: 14px;
}

.sidebar-nav {
    display: flex;
    flex-direction: column;
    align-items: center;
    gap: 4px;
    flex: 1;
    width: 100%;
}

.sidebar a {
    width: 80px;
    height: 40px;
    border-radius: 10px;
    display: flex;
    align-items: center;
    justify-content: center;
    color: rgba(255,255,255,0.7);
    text-decoration: none;
    position: relative;
    transition: background 0.15s, color 0.15s;
}
.sidebar a:hover {
    background: rgba(255,255,255,0.28);
    color: #fff;
}
.sidebar a.active {
    background: rgba(255,255,255,0.35);
    color: #fff;
}
.sidebar a::after {
    content: attr(data-tip);
    position: absolute;
    left: 86px;
    top: 50%;
    transform: translateY(-50%);
    background: #3a3a4a;
    color: #fff;
    font-size: 12px;
    padding: 5px 10px;
    border-radius: 6px;
    white-space: nowrap;
    opacity: 0;
    pointer-events: none;
    transition: opacity 0.15s;
    z-index: 200;
}
.sidebar a:hover::after { opacity: 1; }

.sidebar-sep {
    width: 28px;
    height: 1px;
    background: rgba(255,255,255,0.3);
    margin: 6px 0;
}

/* ── Top Nav ── */
.topnav {
    position: fixed;
    top: 0;
    left: 80px;
    right: 0;
    height: 52px;
    background: #fff;
    border-bottom: 1px solid rgba(0,0,0,0.07);
    display: flex;
    align-items: center;
    padding: 0 28px;
    gap: 8px;
    z-index: 90;
}
.topnav-crumb { font-size: 12px; color: #9a9a9a; }
.topnav-arrow { font-size: 12px; color: #c0c0c0; }
.topnav-title { font-size: 14px; font-weight: 600; color: #1a1a1a; }

/* ── Main content ── */
.main {
    margin-left: 64px;
    padding-top: 48px;
    min-height: 100vh;
}
.content { padding: 24px; }

/* ── Card ── */
.card {
    background: #fff;
    border-radius: 10px;
    border: 1px solid rgba(0,0,0,.07);
    padding: 22px 24px;
    max-width: 960px;
    margin: 0 auto;
}

.card-title {
    font-size: 16px; font-weight: 600;
    color: #111;
    margin-bottom: 20px;
    padding-bottom: 14px;
    border-bottom: 1px solid #f0f0f0;
}

/* ── Form ── */
.form-row {
    display: grid;
    grid-template-columns: 1fr 1fr;
    gap: 16px;
    margin-bottom: 18px;
}

.field label {
    display: block;
    font-size: 12px; font-weight: 600;
    color: #666;
    margin-bottom: 6px;
    text-transform: uppercase;
    letter-spacing: .05em;
}

.field .textbox {
    width: 100%; height: 36px;
    border: 1px solid #ddd;
    border-radius: 7px;
    background: #fafafa;
    color: #1a1a1a;
    font-size: 13px;
    padding: 0 11px;
    outline: none;
    transition: border-color .14s, background .14s;
}
.field .textbox:focus {
    border-color: #888;
    background: #fff;
}

.btn-row { text-align: center; margin-bottom: 20px; }

.btn {
    height: 36px; padding: 0 28px;
    background: #111; color: #fff;
    border: none; border-radius: 7px;
    font-size: 13px; font-weight: 500;
    cursor: pointer;
    transition: background .14s, transform .1s;
}
.btn:hover { background: #333; }
.btn:active { transform: scale(.98); }

.lbl-msg {
    display: block;
    font-size: 12px;
    color: #888;
    text-align: center;
    margin-bottom: 16px;
    min-height: 18px;
}

/* ── Panels ── */
.panels {
    display: grid;
    grid-template-columns: 1fr 1fr;
    gap: 16px;
}

.panel {
    border: 1px solid rgba(0,0,0,.08);
    border-radius: 8px;
    overflow: hidden;
}

.panel-hdr {
    padding: 11px 14px;
    display: flex; align-items: center;
    justify-content: space-between;
}
.panel-hdr.assets {
    background: #f0faf4;
    border-bottom: 1px solid #d4edda;
}
.panel-hdr.liab {
    background: #fff5f5;
    border-bottom: 1px solid #f5c6c6;
}

.ph-label {
    font-size: 12px; font-weight: 600;
    text-transform: uppercase; letter-spacing: .05em;
}
.assets .ph-label { color: #1a7a40; }
.liab .ph-label { color: #c0392b; }

.ph-total {
    font-size: 12px; font-weight: 600;
    padding: 3px 9px; border-radius: 5px;
}
.assets .ph-total { background: #d4edda; color: #145a2e; }
.liab .ph-total { background: #fce8e8; color: #922b21; }

/* ── GridView tables ── */
.panel table {
    width: 100%;
    border-collapse: collapse;
}

.panel table th {
    font-size: 11px; font-weight: 600;
    color: #999;
    text-align: left;
    padding: 8px 14px;
    background: #fafafa;
    border-bottom: 1px solid #f0f0f0;
    text-transform: uppercase;
    letter-spacing: .04em;
}

.panel table td {
    font-size: 12px;
    color: #333;
    padding: 9px 14px;
    border-bottom: 1px solid #f7f7f7;
}

.panel table tr:last-child td { border-bottom: none; }
.panel table tr:hover td { background: #fafafa; }

</style>
</head>
       <invent-assistant assistant-id="ast_6m7o6yUd12iDwuRooEsDtW"></invent-assistant>
<script type="text/javascript" src="https://www.useinvent.com/embed.js" async defer></script>
<body>

<form id="form1" runat="server">

<div class="sidebar">
    <div class="sidebar-logo">
        <img src="../Images/afl3.jfif.png" alt="AFMS Logo" />
    </div>
    <div class="sidebar-brand">AFMS</div>

    <div class="sidebar-nav">
        <a href="Dashboard.aspx" data-tip="Dashboard">
            <svg width="16" height="16" viewBox="0 0 16 16" fill="none">
                <rect x="2" y="2" width="5" height="5" rx="1" fill="currentColor" opacity="0.7"/>
                <rect x="9" y="2" width="5" height="5" rx="1" fill="currentColor" opacity="0.4"/>
                <rect x="2" y="9" width="5" height="5" rx="1" fill="currentColor" opacity="0.4"/>
                <rect x="9" y="9" width="5" height="5" rx="1" fill="currentColor" opacity="0.7"/>
            </svg>
        </a>
        <a href="FinanceStructure.aspx" data-tip="Finance Structure">
            <svg width="16" height="16" viewBox="0 0 16 16" fill="none">
                <rect x="2" y="3" width="12" height="2" rx="1" fill="currentColor"/>
                <rect x="2" y="7" width="8" height="2" rx="1" fill="currentColor" opacity="0.6"/>
                <rect x="2" y="11" width="5" height="2" rx="1" fill="currentColor" opacity="0.35"/>
            </svg>
        </a>
        <a href="JournalEntry.aspx" data-tip="Journal Entry">
            <svg width="16" height="16" viewBox="0 0 16 16" fill="none">
                <path d="M3 5h10M3 8h7M3 11h5" stroke="currentColor" stroke-width="1.4" stroke-linecap="round"/>
            </svg>
        </a>
        <a href="Ledger.aspx" data-tip="Ledger">
            <svg width="16" height="16" viewBox="0 0 16 16" fill="none">
                <rect x="3" y="2" width="10" height="12" rx="1.5" stroke="currentColor" stroke-width="1.3"/>
                <path d="M5 6h6M5 9h4" stroke="currentColor" stroke-width="1.2" stroke-linecap="round"/>
            </svg>
        </a>
        <a href="TrialBalance.aspx" data-tip="Trial Balance">
            <svg width="16" height="16" viewBox="0 0 16 16" fill="none">
                <path d="M2 12l4-4 2 2 6-6" stroke="currentColor" stroke-width="1.4" stroke-linecap="round" stroke-linejoin="round"/>
            </svg>
        </a>
        <a href="IncomeStatement.aspx" data-tip="Income Statement">
            <svg width="16" height="16" viewBox="0 0 16 16" fill="none">
                <rect x="2" y="2" width="12" height="12" rx="1.5" stroke="currentColor" stroke-width="1.3"/>
                <path d="M5 5h6M5 8h4M9 11l1.5-2 1.5 2" stroke="currentColor" stroke-width="1.2" stroke-linecap="round" stroke-linejoin="round"/>
            </svg>
        </a>
        <a href="BalanceSheet.aspx" data-tip="Balance Sheet">
            <svg width="16" height="16" viewBox="0 0 16 16" fill="none">
                <rect x="2" y="2" width="12" height="12" rx="2" stroke="currentColor" stroke-width="1.3"/>
                <path d="M8 2v12M2 8h12" stroke="currentColor" stroke-width="1.2"/>
            </svg>
        </a>
    </div>

    <div class="sidebar-sep"></div>
    <a href="Login.aspx" data-tip="Logout">
        <svg width="15" height="15" viewBox="0 0 15 15" fill="none">
            <path d="M5 13H3a1 1 0 01-1-1V3a1 1 0 011-1h2M10 10.5L13 7.5l-3-3M13 7.5H5" stroke="currentColor" stroke-width="1.3" stroke-linecap="round" stroke-linejoin="round"/>
        </svg>
    </a>
</div>

<div class="topnav">
    <span class="topnav-crumb">AFMS</span>
    <span class="topnav-arrow">›</span>
    <span class="topnav-title">Balance Sheet</span>
<div style="margin-left:auto; font-size:13px; font-weight:600; color:#4a4a5a;">
    Welcome,
    <asp:Label ID="lblUsername" runat="server"></asp:Label>
</div>
    </div>
   
<!-- Main -->
<div class="main">
    <div class="content">
        <div class="card">

            <div class="card-title">Balance Sheet</div>

            <!-- Date fields -->
            <div class="form-row">
                <div class="field">
                    <label>From Date</label>
                    <asp:TextBox ID="txtFromDate" runat="server" TextMode="Date" CssClass="textbox"></asp:TextBox>
                </div>
                <div class="field">
                    <label>To Date</label>
                    <asp:TextBox ID="txtToDate" runat="server" TextMode="Date" CssClass="textbox"></asp:TextBox>
                </div>
            </div>

            <!-- Search button -->
            <div class="btn-row">
                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn" OnClick="btnSearch_Click" />
                <asp:Button ID="btnExportPdf" runat="server" Text="Export as PDF" 
    OnClick="btnExportPdf_Click" CssClass="btn" />
            </div>

            <!-- Message label -->
            <asp:Label ID="lblMsg" runat="server" CssClass="lbl-msg" Font-Bold="true"></asp:Label>

            <!-- Two panels -->
            <div class="panels">

                <!-- Assets -->
                <div class="panel">
                    <div class="panel-hdr assets">
                        <span class="ph-label">Assets Accounts</span>
                        <span class="ph-total">
                            <asp:Label ID="lblIncomeTotal" runat="server"></asp:Label>
                        </span>
                    </div>
                    <asp:GridView ID="gvIncome" runat="server" AutoGenerateColumns="false"
                        Width="100%" BorderStyle="None" GridLines="None"
                        HeaderStyle-CssClass="gv-head" RowStyle-CssClass="gv-row">
                        <Columns>
                            <asp:BoundField DataField="AccountCode" HeaderText="Code" />
                            <asp:BoundField DataField="AccountDescription" HeaderText="Account Name" />
                            <asp:BoundField DataField="Amount" HeaderText="Amount" />
                        </Columns>
                    </asp:GridView>
                </div>

                <!-- Liability & Equity -->
                <div class="panel">
                    <div class="panel-hdr liab">
                        <span class="ph-label">Liability &amp; Equity</span>
                        <span class="ph-total">
                            <asp:Label ID="lblExpenseTotal" runat="server"></asp:Label>
                        </span>
                    </div>
                    <asp:GridView ID="gvExpense" runat="server" AutoGenerateColumns="false"
                        Width="100%" BorderStyle="None" GridLines="None"
                        HeaderStyle-CssClass="gv-head" RowStyle-CssClass="gv-row">
                        <Columns>
                            <asp:BoundField DataField="AccountCode" HeaderText="Code" />
                            <asp:BoundField DataField="AccountDescription" HeaderText="Account Name" />
                            <asp:BoundField DataField="Amount" HeaderText="Amount" />
                        </Columns>
                    </asp:GridView>
                </div>

            </div>
        </div>
    </div>
</div>

</form>

<script>
    var currentPage = window.location.pathname.split("/").pop();
    document.querySelectorAll(".sidebar a").forEach(function (a) {
        if (a.getAttribute("href") === currentPage) a.classList.add("active");
    });
</script>

</body>
</html>

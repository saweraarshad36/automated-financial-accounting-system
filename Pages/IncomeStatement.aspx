<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IncomeStatement.aspx.cs" Inherits="Pages_IncomeStatement" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>Income Statement</title>
<style>

*, *::before, *::after { box-sizing: border-box; margin: 0; padding: 0; }

body {
    font-family: Arial, sans-serif;
    background: #eef0f2;
    color: #1a1a1a;
}

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
.topnav-right {
    margin-left: auto;
    display: flex;
    align-items: center;
    gap: 10px;
}
.topnav-user { font-size: 12px; color: #9a9a9a; }
.topnav-avatar {
    width: 28px;
    height: 28px;
    border-radius: 50%;
    background: #6b6b7e;
    color: #fff;
    font-size: 11px;
    font-weight: 500;
    display: flex;
    align-items: center;
    justify-content: center;
}

/* ── Main ── */
.main {
    margin-left: 80px;
    padding-top: 52px;
    min-height: 100vh;
    background: #eef0f2;
}

.page-body {
    padding: 28px 32px;
    display: flex;
    flex-direction: column;
    gap: 20px;
}

/* ── Cards ── */
.card {
    background: #fff;
    border: 1px solid rgba(0,0,0,0.08);
    border-radius: 12px;
    overflow: hidden;
}

.card-header {
    padding: 14px 20px;
    border-bottom: 1px solid rgba(0,0,0,0.07);
    display: flex;
    align-items: center;
    gap: 10px;
}
.card-header-icon {
    width: 28px;
    height: 28px;
    border-radius: 7px;
    background: #f0f0f0;
    display: flex;
    align-items: center;
    justify-content: center;
    flex-shrink: 0;
}
.card-header h2 {
    font-size: 13px;
    font-weight: 600;
    color: #1a1a1a;
    margin: 0;
}

/* ── Filter ── */
.filter-body {
    padding: 20px;
    display: grid;
    grid-template-columns: 1fr 1fr auto;
    gap: 16px;
    align-items: end;
}

.field { display: flex; flex-direction: column; gap: 5px; }

label {
    font-size: 10px;
    font-weight: 600;
    color: #9a9a9a;
    text-transform: uppercase;
    letter-spacing: 0.06em;
}

.textbox {
    width: 100%;
    padding: 8px 10px;
    border: 1px solid rgba(0,0,0,0.12);
    border-radius: 8px;
    background: #fff;
    color: #1a1a1a;
    font-size: 13px;
    font-family: Arial, sans-serif;
    outline: none;
    transition: border-color 0.15s, box-shadow 0.15s;
}
.textbox:focus {
    border-color: #6b6b7e;
    box-shadow: 0 0 0 3px rgba(107,107,126,0.1);
}

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

.msg-wrap {
    padding: 0 20px 16px;
}

.statement-body {
    padding: 20px;
    display: grid;
    grid-template-columns: 1fr 1fr;
    gap: 16px;
}

.stmt-panel {
    border: 1px solid rgba(0,0,0,0.08);
    border-radius: 10px;
    overflow: hidden;
}

.stmt-panel-header {
    padding: 11px 16px;
    display: flex;
    align-items: center;
    justify-content: space-between;
}

.stmt-panel-header.income  { background: #edf7f2; }
.stmt-panel-header.expense { background: #fdf2f2; }

.stmt-panel-header .panel-title {
    font-size: 12px;
    font-weight: 600;
    letter-spacing: 0.04em;
    text-transform: uppercase;
}
.stmt-panel-header.income  .panel-title { color: #2a6a4a; }
.stmt-panel-header.expense .panel-title { color: #8a3a3a; }

.stmt-panel-header .panel-total {
    font-size: 13px;
    font-weight: 600;
}
.stmt-panel-header.income  .panel-total { color: #2a6a4a; }
.stmt-panel-header.expense .panel-total { color: #8a3a3a; }

.table {
    width: 100%;
    border-collapse: collapse;
    font-size: 13px;
}
.table th {
    font-size: 10px;
    font-weight: 600;
    color: #9a9a9a;
    text-align: left;
    padding: 9px 14px;
    background: #f7f7f7;
    border-bottom: 1px solid rgba(0,0,0,0.07);
    text-transform: uppercase;
    letter-spacing: 0.05em;
    white-space: nowrap;
}
.table td {
    padding: 10px 14px;
    border-bottom: 1px solid rgba(0,0,0,0.05);
    color: #1a1a1a;
    vertical-align: middle;
}
.table tr:last-child td { border-bottom: none; }
.table tr:hover td { background: #f7f7f7; }

@media (max-width: 860px) {
    .statement-body { grid-template-columns: 1fr; }
    .filter-body    { grid-template-columns: 1fr; }
}
</style>
</head>

<script>
    window.onload = function () {
        var currentPage = window.location.pathname.split("/").pop();
        var links = document.querySelectorAll(".sidebar a");
        links.forEach(function (link) {
            if (link.getAttribute("href") === currentPage) {
                link.classList.add("active");
            }
        });
    };
</script>
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
        <a href="IncomeStatement.aspx" data-tip="Income Statement" class="active">
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
    <span class="topnav-title">Income Statement</span>
        <div style="margin-left:auto; font-size:13px; font-weight:600; color:#4a4a5a;">
    Welcome,
    <asp:Label ID="lblUsername" runat="server"></asp:Label>
</div></div>
   

<div class="main">
<div class="page-body">

    <!-- Filter Card -->
    <div class="card">
        <div class="card-header">
            <div class="card-header-icon">
                <svg width="14" height="14" viewBox="0 0 14 14" fill="none">
                    <circle cx="6" cy="6" r="4" stroke="#6b6b7e" stroke-width="1.3"/>
                    <path d="M10 10l2.5 2.5" stroke="#6b6b7e" stroke-width="1.3" stroke-linecap="round"/>
                </svg>
            </div>
            <h2>Search Income Statement</h2>
        </div>
        <div class="filter-body">
            <div class="field">
                <label>From Date</label>
                <asp:TextBox ID="txtFromDate" runat="server" TextMode="Date" CssClass="textbox"></asp:TextBox>
            </div>
            <div class="field">
                <label>To Date</label>
                <asp:TextBox ID="txtToDate" runat="server" TextMode="Date" CssClass="textbox"></asp:TextBox>
            </div>
            
                <div class="d-flex justify-content-center gap-2" style="margin: 20px 0;">
    <asp:Button ID="btnSearch" runat="server" Text="Search" 
        OnClick="btnSearch_Click" CssClass="btn btn-dark" 
        style="padding: 10px 30px; border-radius: 6px;" />

    <asp:Button ID="btnExportPdf" runat="server" Text="Export as PDF" 
        OnClick="btnExportPdf_Click" CssClass="btn btn-dark" 
        style="padding: 10px 30px; border-radius: 6px;" />
</div>

           
        </div>
        <div class="msg-wrap">
            <asp:Label ID="lblMsg" runat="server" Font-Bold="true" style="font-size:13px; color:#8a3a3a;"></asp:Label>
        </div>
    </div>

    <div class="card">
        <div class="card-header">
            <div class="card-header-icon">
                <svg width="14" height="14" viewBox="0 0 14 14" fill="none">
                    <rect x="1" y="2" width="12" height="10" rx="1.5" stroke="#6b6b7e" stroke-width="1.2"/>
                    <path d="M1 5.5h12M4.5 2v10" stroke="#6b6b7e" stroke-width="1.2"/>
                </svg>
            </div>
            <h2>Statement Details</h2>
        </div>

        <div class="statement-body">

            <div class="stmt-panel">
                <div class="stmt-panel-header income">
                    <span class="panel-title">Income Accounts</span>
                    <span class="panel-total">
                        <asp:Label ID="lblIncomeTotal" runat="server"></asp:Label>
                    </span>
                </div>
                <asp:GridView ID="gvIncome" runat="server" AutoGenerateColumns="false"
                    CssClass="table" Width="100%" GridLines="None">
                    <Columns>
                        <asp:BoundField DataField="AccountCode"        HeaderText="Code" />
                        <asp:BoundField DataField="AccountDescription" HeaderText="Account Name" />
                        <asp:BoundField DataField="Amount"             HeaderText="Amount" />
                    </Columns>
                </asp:GridView>
            </div>

            <div class="stmt-panel">
                <div class="stmt-panel-header expense">
                    <span class="panel-title">Expense Accounts</span>
                    <span class="panel-total">
                        <asp:Label ID="lblExpenseTotal" runat="server"></asp:Label>
                    </span>
                </div>
                <asp:GridView ID="gvExpense" runat="server" AutoGenerateColumns="false"
                    CssClass="table" Width="100%" GridLines="None">
                    <Columns>
                        <asp:BoundField DataField="AccountCode"        HeaderText="Code" />
                        <asp:BoundField DataField="AccountDescription" HeaderText="Account Name" />
                        <asp:BoundField DataField="Amount"             HeaderText="Amount" />
                    </Columns>
                </asp:GridView>
            </div>

        </div>
    </div>

</div>
</div>

</form>
</body>
</html>

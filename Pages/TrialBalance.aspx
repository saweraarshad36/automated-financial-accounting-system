<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TrialBalance.aspx.cs" Inherits="Pages_TrialBalance" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>Trial Balance</title>
<style>
*, *::before, *::after { box-sizing: border-box; margin: 0; padding: 0; }

body {
    font-family: Arial, sans-serif;
    background: #f4f6f8;
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

.sidebar-brand {
    font-size: 9px;
    font-weight: 700;
    color: rgba(255,255,255,0.85);
    letter-spacing: 0.12em;
    text-transform: uppercase;
    margin-bottom: 14px;
}
.sidebar-logo img {
    width: 100%;
    height: 100%;
    object-fit: cover;
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
    left: 52px;
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

.main {
    margin-left: 64px;
    padding-top: 52px;
    min-height: 100vh;
    background: #f4f6f8;
}
.page-body {
    padding: 28px 32px;
    display: flex;
    flex-direction: column;
    gap: 20px;
}
.filter-card {
    background: #fff;
    border: 1px solid rgba(0,0,0,0.08);
    border-radius: 12px;
    overflow: hidden;
}
.card-header {
    padding: 15px 22px;
    border-bottom: 1px solid rgba(0,0,0,0.07);
    display: flex; align-items: center; gap: 10px;
}
.card-header-icon {
    width: 28px; height: 28px; border-radius: 7px;
    background: #EEEDFE;
    display: flex; align-items: center; justify-content: center;
}
.card-header h2 {
    font-size: 14px; font-weight: 500; color: #1a1a1a; margin: 0;
}
.card-header p {
    font-size: 11px; color: #888780; margin: 1px 0 0;
}
.filter-body {
    padding: 20px 22px;
    display: flex;
    flex-direction: column;
    gap: 16px;
}

/* Date row */
.date-row {
    display: grid;
    grid-template-columns: 1fr 1fr 1fr;
    gap: 14px;
}
.field { display: flex; flex-direction: column; gap: 5px; }
label {
    font-size: 11px; font-weight: 500;
    color: #5f5e5a;
    text-transform: uppercase;
    letter-spacing: 0.05em;
}
.textbox {
    width: 100%; padding: 8px 10px;
    border: 1px solid rgba(0,0,0,0.14);
    border-radius: 8px;
    background: #fff; color: #1a1a1a;
    font-size: 13px; font-family: Arial, sans-serif;
    outline: none;
    transition: border-color 0.15s, box-shadow 0.15s;
}
.textbox:focus {
    border-color: #4F46E5;
    box-shadow: 0 0 0 3px rgba(79,70,229,0.1);
}
select.textbox {
    appearance: none;
    -webkit-appearance: none;
    background-image: url("data:image/svg+xml;utf8,<svg xmlns='http://www.w3.org/2000/svg' width='10' height='6'><path d='M0 0l5 6 5-6z' fill='%235f5e5a'/></svg>");
    background-repeat: no-repeat;
    background-position: right 10px center;
    padding-right: 28px;
    cursor: pointer;
}

/* Button row */
.btn-row {
    display: flex; flex-wrap: wrap; gap: 8px;
    align-items: center;
}
.btn {
    padding: 8px 16px;
    border-radius: 8px;
    font-size: 13px; font-weight: 500;
    cursor: pointer; border: none;
    font-family: Arial, sans-serif;
    transition: opacity 0.15s;
}
.btn:hover { opacity: 0.85; }

#btnSearch { background: #4F46E5; color: #fff; }
#btnReset  { background: #f1efe8; color: #5f5e5a; border: 1px solid rgba(0,0,0,0.12); }
#btnToday  { background: #E1F5EE; color: #0F6E56; border: 1px solid #9FE1CB; }
#btnMonth  { background: #E6F1FB; color: #185FA5; border: 1px solid #B5D4F4; }
#btnFY     { background: #FAEEDA; color: #854F0B; border: 1px solid #FAC775; }

.msg-wrap {
    font-size: 13px;
    color: #0F6E56;
    min-height: 18px;
}

/* ══════════════════════════════════
   RESULTS CARD
══════════════════════════════════ */
.results-card {
    background: #fff;
    border: 1px solid rgba(0,0,0,0.08);
    border-radius: 12px;
    overflow: hidden;
}
.results-header {
    padding: 14px 22px;
    border-bottom: 1px solid rgba(0,0,0,0.07);
    display: flex; align-items: center; justify-content: space-between;
}
.results-header-left {
    display: flex; align-items: center; gap: 10px;
}
.results-header-icon {
    width: 28px; height: 28px; border-radius: 7px;
    background: #E1F5EE;
    display: flex; align-items: center; justify-content: center;
}
.results-header h3 {
    font-size: 14px; font-weight: 500; color: #1a1a1a; margin: 0;
}

/* Totals summary strip */
.totals-strip {
    display: flex; gap: 12px;
}
.total-chip {
    display: flex; align-items: center; gap: 6px;
    padding: 4px 12px;
    border-radius: 20px;
    font-size: 12px; font-weight: 500;
}
.chip-debit  { background: #E6F1FB; color: #185FA5; }
.chip-credit { background: #EAF3DE; color: #3B6D11; }

/* GridView */
.table {
    width: 100%;
    border-collapse: collapse;
    font-size: 13px;
}
.table th {
    font-size: 11px; font-weight: 500;
    color: #888780;
    text-align: left;
    padding: 10px 20px;
    background: #f8f8f6;
    border-bottom: 1px solid rgba(0,0,0,0.07);
    text-transform: uppercase;
    letter-spacing: 0.04em;
    white-space: nowrap;
}
.table td {
    padding: 11px 20px;
    border-bottom: 1px solid rgba(0,0,0,0.06);
    color: #1a1a1a;
    vertical-align: middle;
}
.table tr:last-child td { border-bottom: none; }
.table tr:hover td { background: #f8f8f6; }

/* Footer row */
.table tfoot td {
    padding: 12px 20px;
    background: #f1efe8;
    font-weight: 500;
    font-size: 13px;
    border-top: 1px solid rgba(0,0,0,0.09);
    border-bottom: none;
}

/* Debit / Credit number columns right-aligned */
.table th:nth-child(3),
.table th:nth-child(4),
.table td:nth-child(3),
.table td:nth-child(4),
.table tfoot td:nth-child(3),
.table tfoot td:nth-child(4) {
    text-align: right;
    font-variant-numeric: tabular-nums;
}

/* Totals footer below grid */
.totals-bar {
    display: flex; gap: 32px;
    padding: 14px 22px;
    border-top: 1px solid rgba(0,0,0,0.07);
    background: #f8f8f6;
}
.total-item { display: flex; align-items: baseline; gap: 8px; }
.total-label { font-size: 12px; color: #5f5e5a; }
.total-value { font-size: 16px; font-weight: 500; color: #1a1a1a; font-variant-numeric: tabular-nums; }
.total-value.debit-val  { color: #185FA5; }
.total-value.credit-val { color: #3B6D11; }
/* PAGING */
.table td table {
    margin: 10px auto;
}

.table td table td {
    border: none;
    padding: 4px;
}

.table td table td a,
.table td table td span {
    display: inline-block;
    padding: 6px 12px;
    border-radius: 6px;
    background: #fff;
    color: #1a1a1a;
    text-decoration: none;
    border: 1px solid rgba(0,0,0,0.1);
    font-size: 12px;
    min-width: 32px;
    text-align: center;
}

.table td table td span {
    background: #4F46E5;
    color: #fff;
    border-color: #4F46E5;
}
</style>
</head>
    <script>
        var currentPage = window.location.pathname.split("/").pop();

        var links = document.querySelectorAll(".sidebar a");

        links.forEach(function (link) {
            var href = link.getAttribute("href");

            if (href === currentPage) {
                link.classList.add("active");
            }
        });
    </script>
        <invent-assistant assistant-id="ast_6m7o6yUd12iDwuRooEsDtW"></invent-assistant>
<script type="text/javascript" src="https://www.useinvent.com/embed.js" async defer></script>
<body>
<form id="form1" runat="server">

<div class="sidebar">

    <div class="sidebar-logo">
   <img src="../Images/afl3.jfif.png" alt="AFMS Logo" />
</div>
<div class="sidebar-brand">AFMS</div>   <!-- Yeh add karo -->

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
    <span class="topnav-title">Trial Balance</span>
    <div style="margin-left:auto; font-size:13px; font-weight:600; color:#4a4a5a;">
    Welcome,
    <asp:Label ID="lblUsername" runat="server"></asp:Label>
</div></div>

<!-- ══ MAIN ══ -->
<div class="main">
<div class="page-body">

    <!-- Filter Card -->
    <div class="filter-card">
        <div class="card-header">
            <div class="card-header-icon">
                <svg width="14" height="14" viewBox="0 0 14 14" fill="none">
                    <rect x="1" y="1" width="12" height="12" rx="2" stroke="#534AB7" stroke-width="1.2"/>
                    <path d="M4 7h6M7 4v6" stroke="#534AB7" stroke-width="1.2" stroke-linecap="round"/>
                </svg>
            </div>
            <div>
                <h2>Trial Balance</h2>
                <p>Select a date range to generate the report</p>
            </div>
        </div>
        <div class="filter-body">
            <div class="date-row">
                <div class="field">
                    <label>From date</label>
                    <asp:TextBox ID="txtFromDate" runat="server" TextMode="Date" CssClass="textbox"></asp:TextBox>
                </div>
                <div class="field">
                    <label>To date</label>
                    <asp:TextBox ID="txtToDate" runat="server" TextMode="Date" CssClass="textbox"></asp:TextBox>
                </div>
                <div class="field">
                    <label>Show Entries</label>
                    <asp:DropDownList ID="ddlShowEntries" runat="server" CssClass="textbox"
                        AutoPostBack="True"
                        OnSelectedIndexChanged="ddlShowEntries_SelectedIndexChanged">
                        <asp:ListItem Text="Posted Only" Value="Posted" Selected="True" />
                        <asp:ListItem Text="Draft Only" Value="Draft" />
                        <asp:ListItem Text="All Entries" Value="All" />
                    </asp:DropDownList>
                </div>
            </div>
            <div class="btn-row">
                <asp:Button ID="btnSearch" runat="server" Text="Search"          CssClass="btn" OnClick="btnSearch_Click"/>
                <asp:Button ID="btnReset"  runat="server" Text="Reset"           CssClass="btn" OnClick="btnReset_Click"/>
                <asp:Button ID="btnToday"  runat="server" Text="Today"           CssClass="btn" OnClick="btnToday_Click"/>
                <asp:Button ID="btnMonth"  runat="server" Text="Current Month"   CssClass="btn" OnClick="btnMonth_Click"/>
                <asp:Button ID="btnFY"     runat="server" Text="Financial Year"  CssClass="btn" OnClick="btnFY_Click"/>
                <asp:Button ID="btnExportPdf" runat="server" Text="Export PDF" CssClass="btn" OnClick="btnExportPdf_Click"/>
            </div>
            <div class="msg-wrap">
                <asp:Label ID="lblMessage" runat="server" ForeColor="Green"></asp:Label>
            </div>
        </div>
    </div>

    <!-- Results Card -->
    <div class="results-card">
        <div class="results-header">
            <div class="results-header-left">
                <div class="results-header-icon">
                    <svg width="14" height="14" viewBox="0 0 14 14" fill="none">
                        <rect x="1" y="2" width="12" height="10" rx="1.5" stroke="#0F6E56" stroke-width="1.2"/>
                        <path d="M1 5.5h12M4.5 2v10" stroke="#0F6E56" stroke-width="1.2"/>
                    </svg>
                </div>
                <h3>Account ledger</h3>
            </div>
            <div class="totals-strip">
                <div class="total-chip chip-debit">
                    Debit &nbsp;
                    <asp:Label ID="lblTotalDebit" runat="server">—</asp:Label>
                </div>
                <div class="total-chip chip-credit">
                    Credit &nbsp;
                    <asp:Label ID="lblTotalCredit" runat="server">—</asp:Label>
                </div>
            </div>
        </div>

        <asp:GridView ID="gvTrialBalance" runat="server"
    AutoGenerateColumns="false"
    CssClass="table"
    ShowFooter="true"

    AllowPaging="true"
    PageSize="10"
    OnPageIndexChanging="gvTrialBalance_PageIndexChanging">
            <Columns>
                <asp:BoundField DataField="AccountCode" HeaderText="Account Code" />
                <asp:BoundField DataField="AccountName" HeaderText="Account Name" />
                <asp:BoundField DataField="Debit"       HeaderText="Debit"  DataFormatString="{0:N2}" />
                <asp:BoundField DataField="Credit"      HeaderText="Credit" DataFormatString="{0:N2}" />
            </Columns>
        </asp:GridView>
    </div>

</div>
</div>

</form>
</body>
</html>

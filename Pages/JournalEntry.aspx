<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JournalEntry.aspx.cs" Inherits="Pages_JournalEntry" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>Journal Entry</title>

<style>

*, *::before, *::after { box-sizing: border-box; margin: 0; padding: 0; }

body {
    font-family: 'Segoe UI', Tahoma, sans-serif;
    background: #eef2f7;
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
    margin-left: 100px;
    margin-top: 52px;
    padding: 28px 28px 40px;
}

/* ── CARD ── */
.card {
    background: #fff;
    border-radius: 12px;
    border: 0.5px solid #e2e8f0;
    overflow: hidden;
}

.card-header {
    padding: 16px 22px;
    border-bottom: 1px solid rgba(0,0,0,0.07);
    display: flex;
    align-items: center;
    justify-content: space-between;
    gap: 12px;
}

.card-title { font-size: 14px; font-weight: 600; color: #1a1a1a; }

.card-body { padding: 20px 22px; }

/* Search + button row */
.toolbar {
    display: flex;
    align-items: center;
    gap: 10px;
    margin-bottom: 18px;
}

.search-wrap {
    display: flex;
    align-items: center;
    gap: 8px;
    padding: 8px 12px;
    border: 1px solid rgba(0,0,0,0.12);
    border-radius: 8px;
    background: #f7f7f7;
    flex: 1;
    max-width: 300px;
}
.search-wrap svg { flex-shrink: 0; }

.textbox {
    border: none;
    background: transparent;
    outline: none;
    font-size: 13px;
    color: #1a1a1a;
    width: 100%;
    font-family: 'Segoe UI', Tahoma, sans-serif;
}

.btn {
    padding: 9px 16px;
    border-radius: 8px;
    font-size: 13px;
    font-weight: 500;
    cursor: pointer;
    border: none;
    font-family: 'Segoe UI', Tahoma, sans-serif;
    transition: opacity 0.15s;
    white-space: nowrap;
}
.btn:hover { opacity: 0.85; }
.btn-search { background: #3b82f6; color: #fff; }
.btn-new    { background: #22c55e; color: #fff; }

/* record count badge */
.record-count {
    font-size: 11px;
    color: #9a9a9a;
    background: #f0f0f0;
    padding: 3px 10px;
    border-radius: 20px;
}

/* ── TABLE ── */
.grid {
    width: 100%;
    border-collapse: collapse;
    font-size: 13px;
}

.grid th {
    font-size: 11px;
    font-weight: 600;
    color: #9a9a9a;
    text-align: left;
    padding: 10px 14px;
    background: #f7f7f7;
    border-bottom: 1px solid rgba(0,0,0,0.07);
    text-transform: uppercase;
    letter-spacing: 0.04em;
    white-space: nowrap;
}

.grid td {
    padding: 11px 14px;
    border-bottom: 1px solid rgba(0,0,0,0.05);
    color: #1a1a1a;
    vertical-align: middle;
}

.grid tr:last-child td { border-bottom: none; }
.grid tr:hover td     { background: #f7f7f7; }

/* ── PAGINATION ── */
.pagination-bar {
    display: flex;
    align-items: center;
    justify-content: space-between;
    padding: 12px 16px;
    border-top: 1px solid rgba(0,0,0,0.06);
    background: #fafafa;
}

.pagination-info {
    font-size: 12px;
    color: #9a9a9a;
}

/* pager row inside GridView */
.grid tr.pager td {
    padding: 0;
    border: none;
    background: transparent;
}

.grid tr.pager td table {
    border-collapse: separate;
    border-spacing: 3px;
}

.grid tr.pager td a,
.grid tr.pager td span {
    display: inline-flex;
    align-items: center;
    justify-content: center;
    min-width: 30px;
    height: 30px;
    padding: 0 8px;
    border-radius: 7px;
    font-size: 12px;
    font-weight: 500;
    border: 1px solid rgba(0,0,0,0.10);
    background: #fff;
    color: #4a4a5a;
    cursor: pointer;
    text-decoration: none;
    transition: background 0.12s, border-color 0.12s;
}

.grid tr.pager td a:hover {
    background: #f0f0f0;
    border-color: rgba(0,0,0,0.18);
}

/* Current active page — filled dark */
.grid tr.pager td span {
    background: #4a4a5a;
    color: #fff;
    border-color: #4a4a5a;
    cursor: default;
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
<asp:ScriptManager runat="server" />

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

    <div style="display:flex; align-items:center; gap:8px;">
        <span class="topnav-crumb">AFMS</span>
        <span class="topnav-arrow">›</span>
        <span class="topnav-title">Journal Entry</span>
    </div>

    <div style="margin-left:auto; font-size:13px; color:#1e293b; font-weight:600;">
        Welcome,
        <asp:Label ID="lblUsername" runat="server"></asp:Label>
    </div>

</div>

<div class="main">
<div class="card">

    <!-- CARD HEADER -->
    <div class="card-header">
        <span class="card-title">Journal Entry</span>
        <span class="record-count">
            <asp:Label ID="lblRecordCount" runat="server" Text="0 records" />
        </span>
    </div>

    <div class="card-body">

        <!-- TOOLBAR -->
        <div class="toolbar">
            <div class="search-wrap">
                <svg width="14" height="14" viewBox="0 0 14 14" fill="none">
                    <circle cx="6" cy="6" r="4" stroke="#9a9a9a" stroke-width="1.3"/>
                    <path d="M10 10l2.5 2.5" stroke="#9a9a9a" stroke-width="1.3" stroke-linecap="round"/>
                </svg>
                <asp:TextBox ID="txtSearchVoucher" runat="server" CssClass="textbox"
                    placeholder="Search voucher no..."></asp:TextBox>
            </div>
            <asp:Button ID="btnSearch" runat="server" Text="Search"
                CssClass="btn btn-search" OnClick="btnSearch_Click" />
            <asp:Button ID="btnNew" runat="server" Text="+ New Entry"
                CssClass="btn btn-new" PostBackUrl="New.aspx" />
        </div>

        <!-- GRID -->
        <asp:GridView ID="gvJournal" runat="server" AutoGenerateColumns="false"
            CssClass="grid" Width="100%"
            AllowPaging="true"
            PageSize="10"
            OnPageIndexChanging="gvJournal_PageIndexChanging"
            PagerStyle-CssClass="pager">

            <PagerSettings
                Mode="NumericFirstLast"
                FirstPageText="«"
                LastPageText="»"
                PageButtonCount="5" />

            <Columns>
                <asp:BoundField DataField="VoucherNo"       HeaderText="Voucher No" />
                <asp:BoundField DataField="VoucherType"     HeaderText="Type" />
                <asp:BoundField DataField="TransactionDate" HeaderText="Date" />
                <asp:BoundField DataField="DC"              HeaderText="D/C" />
                <asp:BoundField DataField="Amount"          HeaderText="Amount" />
                <asp:BoundField DataField="Narration"       HeaderText="Narration" />
            </Columns>
        </asp:GridView>

        <!-- PAGE INFO BAR -->
        <div class="pagination-bar">
            <span class="pagination-info">
                <asp:Label ID="lblPageInfo" runat="server" Text="" />
            </span>
        </div>

    </div>
</div>
</div>

</form>
</body>
</html>

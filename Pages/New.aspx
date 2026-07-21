<%@ Page Language="C#" AutoEventWireup="true" CodeFile="New.aspx.cs" Inherits="Pages_New" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Journal Entry</title>
    <style>
        body { font-family: Calibri, sans-serif; background: #f4f6f8; margin: 0; padding: 20px; padding-left: 100px; }


.page-layout {
    display: flex;
    align-items: flex-start;
    gap: 20px;
    max-width: 1300px;
    margin: 50px auto 20px auto;
}

.form-container {
    background: #fff;
    border-radius: 8px;
    padding: 24px;
    flex: 1;
    min-width: 0;
    box-shadow: 0 2px 8px rgba(0,0,0,0.1);
}
h2 { color: #2c3e50; border-bottom: 2px solid #3498db; padding-bottom: 8px; }
        label { font-weight: bold; display: inline-block; width: 140px; margin-top: 8px; }
        input[type="text"], select { padding: 6px 10px; border: 1px solid #ccc; border-radius: 4px; min-width: 200px; }
        .btn-row { margin-top: 16px; display: flex; flex-wrap: wrap; gap: 8px; align-items: center; }
        input[type="submit"] { padding: 8px 18px; border: none; border-radius: 4px; cursor: pointer; font-size: 14px; }
        .btn-add        { background: #3498db; color: #fff; }
        .btn-newvoucher { background: #8e44ad; color: #fff; }
        .btn-save       { background: #27ae60; color: #fff; }
        .btn-back       { background: #95a5a6; color: #fff; }

        /* Voucher badge — same voucher no. wali rows ek hi rang ki dikhein */
        .voucher-badge {
            display: inline-block;
            background: #3498db;
            color: #fff;
            border-radius: 12px;
            padding: 2px 10px;
            font-size: 13px;
            font-weight: bold;
            white-space: nowrap;
        }

        /* ===== FIXED: keep Voucher badge + Status badge on a single line, never wrap ===== */
        .voucher-cell {
            display: flex;
            align-items: center;
            gap: 6px;
            flex-wrap: nowrap;
            white-space: nowrap;
        }

        /* ===== FIXED: wraps the grid so it can never overflow into the Drafts panel ===== */
        .grid-wrapper {
            width: 100%;
            max-width: 100%;
            overflow-x: auto;
        }
        .grid-wrapper table {
            min-width: 700px;
        }

        /* ===== NEW: Status badges ===== */
        .status-badge {
            display: inline-block;
            border-radius: 12px;
            padding: 2px 10px;
            font-size: 12px;
            font-weight: bold;
            margin-left: 6px;
        }
        .status-posted { background: #e8f9ee; color: #27ae60; border: 1px solid #27ae60; }
        .status-draft  { background: #fff4e5; color: #e67e22; border: 1px solid #e67e22; }

        .info-box {
            background: #eaf4ff;
            border-left: 4px solid #3498db;
            padding: 8px 14px;
            border-radius: 4px;
            margin-top: 12px;
            font-size: 13px;
            color: #2c3e50;
        }

        table { width: 100%; border-collapse: collapse; margin-top: 20px; font-size: 14px; }
        table th { background: #2c3e50; color: #fff; padding: 10px; text-align: left; }
        table td { padding: 8px 10px; border-bottom: 1px solid #e0e0e0; }
        table tr:hover { background: #f0f7ff; }
        table input[type="text"], table select { min-width: 100px; padding: 4px 6px; }
        .dc-debit  { color: #27ae60; font-weight: bold; }
        .dc-credit { color: #e74c3c; font-weight: bold; }

        /* Same voucher group highlight */
        .grp-even { background: #fafffe; }
        .grp-odd  { background: #f5f0ff; }
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
.grid .pager {
    text-align: left;
    padding: 12px 0;
}

.grid .pager table {
    width: auto !important;
    margin: 0;
    border-collapse: separate;
    border-spacing: 6px;
}

.grid .pager td {
    border: none !important;
    padding: 0 !important;
    background: transparent !important;
}

.grid .pager a,
.grid .pager span {
    display: inline-block;
    min-width: 42px;
    height: 42px;
    line-height: 42px;
    text-align: center;
    border-radius: 10px;
    border: 1px solid #d6d6d6;
    background: #fff;
    color: #4a4a5a;
    font-size: 15px;
    font-weight: 500;
    text-decoration: none;
}

.grid .pager a:hover {
    background: #f3f4f6;
}

.grid .pager span {
    background: #4a4a5a;
    color: #fff;
    border-color: #4a4a5a;
}

/* ===== NEW: Drafts side panel ===== */
.drafts-panel {
    width: 260px;
    flex-shrink: 0;
    background: #fff;
    border-radius: 8px;
    padding: 14px;
    box-shadow: 0 2px 8px rgba(0,0,0,0.1);
    border-top: 4px solid #e67e22;
}
.drafts-panel-header {
    display: flex;
    align-items: center;
    justify-content: space-between;
    margin-bottom: 10px;
}
.drafts-panel-header h3 {
    margin: 0;
    font-size: 15px;
    color: #2c3e50;
}
.drafts-count {
    background: #e67e22;
    color: #fff;
    font-size: 12px;
    font-weight: bold;
    border-radius: 10px;
    padding: 2px 9px;
}
.drafts-empty {
    font-size: 13px;
    color: #95a5a6;
    text-align: center;
    padding: 20px 0;
}
.drafts-grid {
    max-height: 600px;
    overflow-y: auto;
}
.drafts-grid table {
    margin-top: 0;
    font-size: 12.5px;
}
.drafts-grid th {
    background: #fdf1e6;
    color: #e67e22;
    padding: 7px 8px;
}
.drafts-grid td {
    padding: 7px 8px;
    vertical-align: middle;
}
.drafts-grid tr:hover {
    background: #fff8f0;
}
.draft-diff {
    color: #e74c3c;
    font-weight: bold;
}
.draft-select-btn {
    background: #e67e22;
    color: #fff;
    border: none;
    border-radius: 4px;
    padding: 4px 9px;
    font-size: 11.5px;
    cursor: pointer;
}
.draft-select-btn:hover { background: #d35400; }

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
    <span class="topnav-title">Journal Entry</span>
    <div style="margin-left:auto; font-size:13px; font-weight:600; color:#4a4a5a;">
    Welcome,
    <asp:Label ID="lblUsername" runat="server"></asp:Label>
</div></div>

<!-- ===== NEW: wrapper holding main form + drafts side panel ===== -->
<div class="page-layout">

<div class="form-container">

    <label>Voucher No</label>
    <asp:TextBox ID="txtVoucher" runat="server" ReadOnly="true"
        style="background:#eaf4ff; font-weight:bold; color:#2c3e50;"></asp:TextBox>
    <br />

    <label>Voucher Type</label>
    <asp:DropDownList ID="ddlType" runat="server">
        <asp:ListItem>Journal</asp:ListItem>
        <asp:ListItem>Sale</asp:ListItem>
        <asp:ListItem>Purchase</asp:ListItem>
    </asp:DropDownList>
    <br />

    <label>Transaction Date</label>
    <asp:TextBox ID="txtDate" runat="server" TextMode="Date"></asp:TextBox>
    <br /><br />
    <label>Filter</label>
<asp:DropDownList ID="ddlFilterDC" runat="server" AutoPostBack="true"
    OnSelectedIndexChanged="ddlFilterDC_SelectedIndexChanged">
    <asp:ListItem Value="All">All</asp:ListItem>
    <asp:ListItem Value="Debit">Debit</asp:ListItem>
    <asp:ListItem Value="Credit">Credit</asp:ListItem>
</asp:DropDownList>
<br /><br />

    <%-- Info box --%>
    <div class="info-box">
        <b>Current Voucher:</b>
        <asp:Label ID="lblCurrentVoucher" runat="server" style="color:#3498db; font-weight:bold;"></asp:Label>
        &nbsp;|&nbsp;
        <b>Add Row</b> = Add a row to the same voucher. &nbsp;|&nbsp;
        <b>New Voucher</b> = Add a row to the new voucher. Begin a new voucher.
    </div>

    <div class="btn-row">
        <asp:Button ID="btnAddRow" runat="server" Text="Add Row (Same Voucher)"
            CssClass="btn-add" OnClick="btnAddRow_Click" />

        <asp:Button ID="btnNewVoucher" runat="server" Text="New Voucher"
            CssClass="btn-newvoucher" OnClick="btnNewVoucher_Click" />

        <asp:Button ID="btnShowAll" runat="server" Text="Show All Vouchers"
            CssClass="btn-back" OnClick="btnShowAll_Click" />

        <asp:Label ID="lblStatus"
runat="server"
Font-Bold="true"
Font-Size="14px">
</asp:Label>
       
    </div>

   <div class="grid-wrapper">
   <asp:GridView ID="gv" runat="server"
    CssClass="grid"
    AutoGenerateColumns="false"
    DataKeyNames="Id,VoucherNo,AccountCode,DC,Amount,Narration,Status"
    AllowPaging="true"
    PageSize="10"
    PagerStyle-CssClass="pager"
    OnPageIndexChanging="gv_PageIndexChanging"
    OnRowDeleting="gv_RowDeleting"
    OnRowEditing="gv_RowEditing"
    OnRowCancelingEdit="gv_RowCancelingEdit"
    OnRowUpdating="gv_RowUpdating">

    <PagerSettings
        Mode="NumericFirstLast"
        FirstPageText="«"
        LastPageText="»"
        PageButtonCount="5" />

        <Columns>

            <%-- Voucher No — badge style + Status badge --%>
            <asp:TemplateField HeaderText="Voucher No">
                <ItemTemplate>
                    <div class="voucher-cell">
                        <span class="voucher-badge"><%# Eval("VoucherNo") %></span>
                        <span class='<%# Eval("Status").ToString() == "Posted" ? "status-badge status-posted" : "status-badge status-draft" %>'>
                            <%# Eval("Status") %>
                        </span>
                    </div>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtVoucherNo" runat="server"
                        Text='<%# Bind("VoucherNo") %>'
                        ReadOnly="true" Width="70px"></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>

            <%-- Account --%>
            <asp:TemplateField HeaderText="Account">
                <ItemTemplate><%# Eval("AccountDescription") %></ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="ddlAccount" runat="server"></asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>

            <%-- Debit / Credit --%>
            <asp:TemplateField HeaderText="Debit / Credit">
                <ItemTemplate>
                    <span class='<%# Eval("DC").ToString() == "Debit" ? "dc-debit" : "dc-credit" %>'>
                        <%# Eval("DC") %>
                    </span>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="ddlDC" runat="server">
                        <asp:ListItem Value="Debit">Debit</asp:ListItem>
                        <asp:ListItem Value="Credit">Credit</asp:ListItem>
                    </asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>

            <%-- Amount --%>
            <asp:TemplateField HeaderText="Amount">
                <ItemTemplate><%# Eval("Amount") %></ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtAmount" runat="server"
                        Text='<%# Bind("Amount") %>' Width="90px"></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Narration">
                <ItemTemplate><%# Eval("Narration") %></ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtNarration" runat="server"
                        Text='<%# Bind("Narration") %>' Width="150px"></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:CommandField ShowEditButton="true" ShowDeleteButton="true"
                ButtonType="Button" />

        </Columns>
    </asp:GridView>
    </div>
    <div style="margin-top:15px;">
    

    <asp:Label ID="lblPage" runat="server" visible="false"></asp:Label>
</div>
    <br />
    <asp:Button ID="btnBack" runat="server" Text="Back"
        CssClass="btn-back" PostBackUrl="JournalEntry.aspx" />

</div>

<!-- ===== NEW: Draft Vouchers side panel ===== -->
<asp:Panel ID="pnlDrafts" runat="server" CssClass="drafts-panel">
    <div class="drafts-panel-header">
        <h3>⚠ Draft Vouchers</h3>
        <span class="drafts-count"><asp:Label ID="lblDraftCount" runat="server">0</asp:Label></span>
    </div>
    <div class="drafts-grid">
        <asp:GridView ID="gvDrafts" runat="server"
            AutoGenerateColumns="false"
            DataKeyNames="VoucherNo"
            OnSelectedIndexChanged="gvDrafts_SelectedIndexChanged"
            GridLines="None"
            EmptyDataText="No draft vouchers.">
            <Columns>
                <asp:BoundField DataField="VoucherNo" HeaderText="Voucher" />
                <asp:TemplateField HeaderText="Diff">
                    <ItemTemplate>
                        <span class="draft-diff"><%# Eval("Difference") %></span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnSelectDraft" runat="server"
                            CssClass="draft-select-btn"
                            CommandName="Select"
                            Text="Open" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Panel>

</div> 

</form>
</body>
</html>

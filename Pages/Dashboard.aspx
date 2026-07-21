<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Pages_Dashboard" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Finance Dashboard</title>

   <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <style>
        * { margin: 0; padding: 0; box-sizing: border-box; }

        body {
            font-family: 'Segoe UI', Arial, sans-serif;
            background: #F1F5F4;
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

        .container {
    margin-left: 80px;
    margin-top: 70px;
    padding: 28px 28px 40px;
}

        .page-header { margin-bottom: 22px; }
        .page-header h1 { font-size: 18px; font-weight: 500; color: #1e293b; }
        .page-header p  { font-size: 13px; color: #64748b; margin-top: 3px; }

        /* ── CARDS ── */
        .cards {
            display: grid;
            grid-template-columns: repeat(5, 1fr);
            gap: 16px;
            margin-bottom: 22px;
        }

        .card {
            background: #ffffff;
            padding: 18px 20px;
            border-radius: 12px;
            border: 0.5px solid #e2e8f0;
        }

        .card-icon {
            width: 36px; height: 36px;
            border-radius: 8px;
            display: flex; align-items: center; justify-content: center;
            margin-bottom: 14px;
        }
        .card-icon svg { width: 16px; height: 16px; }

        .icon-teal   { background: #e0f4f1; }
        .icon-red    { background: #fce8e8; }
        .icon-blue   { background: #e8eef8; }
        .icon-amber  { background: #fef3e2; }
        .icon-purple { background: #ede8f8; }

        .card-label {
            font-size: 11px; font-weight: 500;
            color: #94a3b8;
            text-transform: uppercase; letter-spacing: 0.6px;
            margin-bottom: 8px;
        }
        .card-value {
            font-size: 22px; font-weight: 500;
            color: #1e293b;
            margin-bottom: 7px;
        }
        .card-value-sm {
            font-size: 16px; font-weight: 500;
            color: #1e293b;
            margin-bottom: 4px;
            margin-top: 10px;
        }
        .card-sub {
            font-size: 12px;
        }
        .card-sub.up     { color: #0ea5a0; }
        .card-sub.down   { color: #e05252; }
        .card-sub.muted  { color: #94a3b8; }

        .card-divider {
            height: 1px;
            background: #f1f5f9;
            margin: 12px 0;
        }

       
        .charts-grid {
            display: grid;
            grid-template-columns: 2fr 1fr;
            gap: 16px;
            margin-bottom: 22px;
        }

        
        .chart-card {
            background: #ffffff;
            border-radius: 12px;
            border: 0.5px solid #e2e8f0;
            padding: 20px 22px;
        }

        .chart-header {
            display: flex; align-items: center; justify-content: space-between;
            margin-bottom: 16px;
        }
        .chart-title { font-size: 13px; font-weight: 500; color: #1e293b; }
        .chart-sub   { font-size: 11px; color: #94a3b8; margin-top: 2px; }

        .legend { display: flex; gap: 14px; flex-wrap: wrap; }
        .legend-item { display: flex; align-items: center; gap: 5px; font-size: 11px; color: #64748b; }
        .legend-dot  { width: 7px; height: 7px; border-radius: 50%; flex-shrink: 0; }

        #lineChart { width: 100% !important; }
        #pieChart  { width: 100% !important; }
    </style>
</head>
        <invent-assistant assistant-id="ast_6m7o6yUd12iDwuRooEsDtW"></invent-assistant>
<script type="text/javascript" src="https://www.useinvent.com/embed.js" async defer></script>
<body>
    <script>
        function loadChart(labels, revenue, expense) {
            var ctx = document.getElementById('lineChart').getContext('2d');
            new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: labels,
                    datasets: [
                        {
                            label: 'Revenue',
                            data: revenue,
                            borderColor: '#16a34a',
                            backgroundColor: 'rgba(22,163,74,0.1)',
                            fill: true
                        },
                        {
                            label: 'Expenses',
                            data: expense,
                            borderColor: '#dc2626',
                            backgroundColor: 'rgba(220,38,38,0.1)',
                            fill: true
                        }
                    ]
                },
                options: {
                    responsive: true,
                    maintainAspectRatio: true,
                    scales: {
                        y: { beginAtZero: true }
                    }
                }
            });
        }

        function loadPieChart(curRev, curExp, prevRev, prevExp) {
            var ctx = document.getElementById('pieChart').getContext('2d');
            new Chart(ctx, {
                type: 'pie',
                data: {
                    labels: ['Current Revenue', 'Current Expense', 'Prev Revenue', 'Prev Expense'],
                    datasets: [{
                        data: [curRev, curExp, prevRev, prevExp],
                        backgroundColor: [
                            'rgba(22,163,74,0.85)',
                            'rgba(220,38,38,0.85)',
                            'rgba(59,130,246,0.85)',
                            'rgba(249,115,22,0.85)'
                        ],
                        borderWidth: 2,
                        borderColor: '#fff'
                    }]
                },
                options: {
                    responsive: true,
                    maintainAspectRatio: true,
                    plugins: {
                        legend: {
                            position: 'bottom',
                            labels: {
                                font: { size: 11 },
                                padding: 12,
                                boxWidth: 12
                            }
                        },
                        tooltip: {
                            callbacks: {
                                label: function (ctx) {
                                    return ' PKR ' + ctx.parsed.toLocaleString();
                                }
                            }
                        }
                    }
                }
            });
        }

        var currentPage = window.location.pathname.split("/").pop();
        var links = document.querySelectorAll(".sidebar a");
        links.forEach(function (link) {
            var href = link.getAttribute("href");
            if (href === currentPage) {
                link.classList.add("active");
            }
        });
    </script>

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
    <span class="topnav-crumb">AFMS</span>
    <span class="topnav-arrow">›</span>
    <span class="topnav-title">Dashboard</span>

    <div style="margin-left:auto; font-size:13px; font-weight:600; color:#4a4a5a;">
        Welcome,
        <asp:Label ID="lblUsername" runat="server"></asp:Label>
    </div>
</div>
<div class="container">

    <div class="cards">

        <div class="card">
            <div class="card-icon icon-teal">
                <svg viewBox="0 0 16 16" fill="none">
                    <path d="M8 2a6 6 0 100 12A6 6 0 008 2zm1 6.5H7.5V5h1v3h1.5l-1 1.5z" fill="#0ea5a0"/>
                </svg>
            </div>
            <div class="card-label">Revenue (Income)</div>
            <div class="card-value">PKR <asp:Label ID="lblRevenue" runat="server" /></div>
            <div class="card-sub up">Income Accounts</div>
        </div>

        <div class="card">
            <div class="card-icon icon-red">
                <svg viewBox="0 0 16 16" fill="none">
                    <path d="M8 2a6 6 0 100 12A6 6 0 008 2zm-.75 3.5h1.5v4h-1.5v-4zm0 5h1.5v1.5h-1.5V10.5z" fill="#e05252"/>
                </svg>
            </div>
            <div class="card-label">Expenses</div>
            <div class="card-value">PKR<asp:Label ID="lblExpense" runat="server" /></div>
            <div class="card-sub down">Expense Accounts</div>
        </div>

        <div class="card">
            <div class="card-icon icon-teal">
                <svg viewBox="0 0 16 16" fill="none">
                    <path d="M3 11l3-3 2 2 5-5" stroke="#0ea5a0" stroke-width="1.4" stroke-linecap="round" stroke-linejoin="round"/>
                </svg>
            </div>
            <div class="card-label">Net Profit</div>
            <div class="card-value">PKR<asp:Label ID="lblProfit" runat="server" /></div>
            <div class="card-sub up">Revenue - Expense</div>
        </div>

        <div class="card">
            <div class="card-icon icon-amber">
                <svg viewBox="0 0 16 16" fill="none">
                    <rect x="3" y="5" width="10" height="7" rx="1" stroke="#d97706" stroke-width="1.3"/>
                    <path d="M5 5V4a3 3 0 016 0v1" stroke="#d97706" stroke-width="1.3"/>
                </svg>
            </div>
            <div class="card-label">Cash Flow</div>
            <div class="card-value">PKR<asp:Label ID="lblCash" runat="server" /></div>
            <div class="card-sub up">Net Flow</div>
        </div>

        <div class="card">
            <div class="card-icon icon-purple">
                <svg viewBox="0 0 16 16" fill="none">
                    <circle cx="8" cy="8" r="5.5" stroke="#7c3aed" stroke-width="1.3"/>
                    <path d="M8 5v3.5l2 1.5" stroke="#7c3aed" stroke-width="1.3" stroke-linecap="round" stroke-linejoin="round"/>
                </svg>
            </div>
            <div class="card-label">Previous Year</div>
            <div class="card-value">PKR<asp:Label ID="lblPrevRevenue" runat="server" /></div>
            <div class="card-sub muted">Revenue · Prev Year</div>
            <div class="card-divider"></div>
            <div class="card-value-sm">PKR<asp:Label ID="lblPrevExpense" runat="server" /></div>
            <div class="card-sub muted">Expense · Prev Year</div>
        </div>

    </div>

    <div class="charts-grid">

        <div class="chart-card">
            <div class="chart-header">
                <div>
                    <div class="chart-title">Monthly Revenue &amp; Expenses</div>
                    <div class="chart-sub">Current year breakdown by month</div>
                </div>
                <div class="legend">
                    <span class="legend-item"><span class="legend-dot" style="background:#16a34a"></span>Revenue</span>
                    <span class="legend-item"><span class="legend-dot" style="background:#dc2626"></span>Expenses</span>
                </div>
            </div>
            <canvas id="lineChart" width="800" height="300"></canvas>
        </div>

        <div class="chart-card">
            <div class="chart-header">
                <div>
                    <div class="chart-title">Year-over-Year</div>
                    <div class="chart-sub">Current vs Previous Year</div>
                </div>
            </div>
            <div class="legend" style="margin-bottom:12px; flex-wrap:wrap; gap:8px;">
                <span class="legend-item"><span class="legend-dot" style="background:rgba(22,163,74,0.85)"></span>Cur. Revenue</span>
                <span class="legend-item"><span class="legend-dot" style="background:rgba(220,38,38,0.85)"></span>Cur. Expense</span>
                <span class="legend-item"><span class="legend-dot" style="background:rgba(59,130,246,0.85)"></span>Prev. Revenue</span>
                <span class="legend-item"><span class="legend-dot" style="background:rgba(249,115,22,0.85)"></span>Prev. Expense</span>
            </div>
            <canvas id="pieChart" width="400" height="300"></canvas>
        </div>

    </div>

</div>

</form>

</body>
</html>
